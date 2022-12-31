using AutoMapper.Internal;
using Core.Application.Attributes;
using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client.Autogen.Grpc.v1;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using Serilog.Context;

namespace Core.Application.Models
{
    public class DaprBaseService : AppCallback.AppCallbackBase
    {
        private const string PUBSUBNAME = "pubsub";

        private readonly ILogger<DaprBaseService> _logger;
        private readonly ActivitySource _daprActivity;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="daprClient"></param>
        /// <param name="logger"></param>
        public DaprBaseService(ILogger<DaprBaseService> logger, ActivitySource daprActivity)
        {
            _logger = logger;
            _daprActivity = daprActivity;

            InitializeEndpoints();
        }

        private void InitializeEndpoints()
        {
            var endpoints = GetType().GetMethods().Where(p => p.Has<GrpcEndpoint>());
            foreach (var endpoint in endpoints)
            {
                var endpointInfo = endpoint.GetCustomAttribute<GrpcEndpoint>();
                CreateHandlerMapping<GrpcEndpoint>(endpoint, nameof(AddInvokeHandler), endpointInfo.Name);
            }

            var pubSubEndpoints = GetType().GetMethods().Where(p => p.Has<PubSubEndpoint>());
            foreach (var endpoint in pubSubEndpoints)
            {
                var endpointInfo = endpoint.GetCustomAttribute<PubSubEndpoint>();
                CreateHandlerMapping<GrpcEndpoint>(endpoint, nameof(AddTopicEvent), endpointInfo.Name);
            }
        }

        private void CreateHandlerMapping<T>(MethodInfo endpoint, string eventType, string handlerName) where T : Attribute
        {
            var returnType = endpoint.ReturnParameter.ParameterType;
            var returnTypeArguments = returnType.IsGenericType ? returnType.GetGenericArguments()[0] : returnType;

            var arguments = endpoint.GetParameters()
                .Select(p => p.ParameterType)
                .Append(returnType)
                .ToArray(); // Todo: Check arguments for request, context form

            var funcType = typeof(Func<,,>).MakeGenericType(arguments);
            var funcDelegate = endpoint.CreateDelegate(funcType, this);

            var method = typeof(DaprBaseService).GetMethod(eventType, BindingFlags.Instance | BindingFlags.NonPublic);
            var genericMethod = method.MakeGenericMethod(arguments[0], returnTypeArguments);
            genericMethod.Invoke(this, new object[] { handlerName, funcDelegate });
        }

        protected static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        protected ListTopicSubscriptionsResponse TopicSubscriptions { get; set; } = new ListTopicSubscriptionsResponse();

        protected Dictionary<string, Func<InvokeRequest, ServerCallContext, InvokeResponse, Task>> InvokeHandlers { get; set; }
            = new Dictionary<string, Func<InvokeRequest, ServerCallContext, InvokeResponse, Task>>();
        protected Dictionary<string, Func<TopicEventRequest, ServerCallContext, Task>> TopicEventHandlers { get; set; }
            = new Dictionary<string, Func<TopicEventRequest, ServerCallContext, Task>>();
        protected void AddInvokeHandler<TRequest, TModel>(string handlerName, Func<TRequest, ServerCallContext, Task<TModel>> handler)
            where TRequest : IMessage, new()
            where TModel : IMessage, new()
        {
            InvokeHandlers.Add(handlerName,
                async (request, context, response) => await HandleIOStream<TRequest, TModel>(request, response, async p => await handler(p, context)));
        }

        protected void AddTopicEvent<TRequest, TModel>(string handlerName, Func<TRequest, ServerCallContext, Task> handler)
            where TRequest : IMessage, new()
        {
            TopicEventHandlers.Add(handlerName,
                async (request, context) => await HandleTopicEvent<TRequest>(request, async p => await handler(p, context)));

            AddSubscription(handlerName);
        }

        /// <summary>
        /// implement OnIvoke to support getaccount, deposit and withdraw
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<InvokeResponse> OnInvoke(InvokeRequest request, ServerCallContext context)
        {
            var response = new InvokeResponse();

            var jobId = Log("Incoming invoke request for: {Method}", request.Method);

            using var activity = _daprActivity.StartActivity($"{nameof(DaprBaseService)}/ProcessInvoke");
            activity!.AddTag("JobId", jobId);

            if (InvokeHandlers.TryGetValue(request.Method, out var handler))
            {
                await handler.Invoke(request, context, response);
            }
            else
            {
                throw new ArgumentException("Request method is not defined");
            }
            return response;
        }

        /// <summary>
        /// implement OnTopicEvent to handle deposit and withdraw event
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<TopicEventResponse> OnTopicEvent(TopicEventRequest request, ServerCallContext context)
        {
            if (request.PubsubName == "pubsub")
            {
                if (TopicEventHandlers.TryGetValue(request.Topic, out var handler))
                {
                    var jobId = Log("Incoming pubsub request for: {Topic}", request.Topic);

                    using var activity = _daprActivity.StartActivity($"{nameof(DaprBaseService)}/ProcessPubSub");
                    activity!.AddTag("JobId", jobId);

                    await handler.Invoke(request, context);
                }
                else { throw new ArgumentException("Event topic is not defined"); }
            }

            return new TopicEventResponse();
        }

        /// <summary>
        /// implement ListTopicSubscriptions to register deposit and withdraw subscriber
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<ListTopicSubscriptionsResponse> ListTopicSubscriptions(Empty request, ServerCallContext context)
        {
            return Task.FromResult(TopicSubscriptions);
        }

        protected void AddSubscription(string topic)
        {
            TopicSubscriptions.Subscriptions.Add(new TopicSubscription
            {
                PubsubName = PUBSUBNAME,
                Topic = topic
            });
        }

        private string Log(string message, params string[] args)
        {
            var jobId = Guid.NewGuid().ToString();
            using (LogContext.PushProperty("JobId", jobId))
            {
                _logger.LogInformation(message, args);
            }
            return jobId;
        }

        private static async Task HandleIOStream<TRequest, TModel>(InvokeRequest request, InvokeResponse response,
            Func<TRequest, Task<TModel>> handler)
            where TRequest : IMessage, new()
            where TModel : IMessage, new()
        {
            var input = request.Data.Unpack<TRequest>();
            var output = await handler(input);

            response.Data = Any.Pack(output ?? new TModel());
        }

        private static async Task HandleTopicEvent<TRequest>(TopicEventRequest request, Func<TRequest, Task> handler)
            where TRequest : IMessage, new()
        {
            var input = JsonSerializer.Deserialize<TRequest>(request.Data.ToStringUtf8(), jsonOptions);
            await handler(input);
        }
    }
}
