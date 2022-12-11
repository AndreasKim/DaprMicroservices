﻿using AutoMapper.Internal;
using Core.Application.Common.Attributes;
using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client.Autogen.Grpc.v1;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text.Json;

namespace Core.Application.Common.Models
{
    public class DaprBaseService : AppCallback.AppCallbackBase
    {
        private const string PUBSUBNAME = "pubsub";
        /// <summary>
        /// State store name.
        /// </summary>
        public const string StoreName = "statestore";

        private readonly ILogger<DaprBaseService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="daprClient"></param>
        /// <param name="logger"></param>
        public DaprBaseService(ILogger<DaprBaseService> logger)
        {
            _logger = logger;
            Initialize();
        }

        private void Initialize()
        {
            var endpoints = this.GetType().GetMethods().Where(p => p.Has<GrpcEndpoint>());
            foreach (var endpoint in endpoints)
            {
                var endpointInfo = endpoint.GetCustomAttribute<GrpcEndpoint>();
                CreateHandlerMapping<GrpcEndpoint>(endpoint, nameof(AddInvokeHandler), endpointInfo.Name);
            }

            var pubSubEndpoints = this.GetType().GetMethods().Where(p => p.Has<PubSubEndpoint>());
            foreach (var endpoint in pubSubEndpoints)
            {
                var endpointInfo = endpoint.GetCustomAttribute<PubSubEndpoint>();
                CreateHandlerMapping<GrpcEndpoint>(endpoint, nameof(AddTopicEvent), endpointInfo.Name);
            }
            Console.WriteLine();
        }

        private void CreateHandlerMapping<T>(MethodInfo endpoint, string eventType, string handlerName) where T : Attribute 
        {
            var returntype = endpoint.ReturnParameter.ParameterType;
            var arguments = endpoint.GetParameters()
                .Select(p => p.ParameterType)
                .Append(returntype)
                .ToArray(); // Todo: Check arguments for request, context form

            var funcType = typeof(Func<,,>).MakeGenericType(arguments);
            var funcDelegate = endpoint.CreateDelegate(funcType, this);

            var method = typeof(DaprBaseService).GetMethod(eventType, BindingFlags.Instance | BindingFlags.NonPublic);
            var genericMethod = method.MakeGenericMethod(arguments[0], returntype.GetGenericArguments()[0]);
            genericMethod.Invoke(this, new object[] { handlerName, funcDelegate });
        }

        protected static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        protected ListTopicSubscriptionsResponse TopicSubscriptions { get; set; }

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

        protected void AddTopicEvent<TRequest, TModel>(string handlerName, Func<TRequest, ServerCallContext, Task<TModel>> handler)
            where TRequest : IMessage, new()
            where TModel : IMessage, new()
        {
            TopicEventHandlers.Add(handlerName,
                async (request, context) => await HandleTopicEvent<TRequest, TModel>(request, async p => await handler(p, context)));
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
            _logger.LogInformation($"Incoming invoke request for: {request.Method}");
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
                    await handler.Invoke(request, context);
                }
                else { throw new ArgumentException("Event topic is not defined"); }
            }

            return new TopicEventResponse();
        }

        protected void AddSubscription(string topic)
        {
            TopicSubscriptions.Subscriptions.Add(new TopicSubscription
            {
                PubsubName = PUBSUBNAME,
                Topic = topic
            });
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

        private static async Task HandleIOStream<TRequest, TModel>(InvokeRequest request, InvokeResponse response,
            Func<TRequest, Task<TModel>> handler)
            where TRequest : IMessage, new()
            where TModel : IMessage, new()
        {
            var input = request.Data.Unpack<TRequest>();
            var output = await handler(input);
            response.Data = Any.Pack(output);
        }

        private static async Task HandleTopicEvent<TRequest, TModel>(TopicEventRequest request, Func<TRequest, Task<TModel>> handler)
            where TRequest : IMessage, new()
            where TModel : IMessage, new()
        {
            var input = JsonSerializer.Deserialize<TRequest>(request.Data.ToStringUtf8(), jsonOptions);
            await handler(input);
        }


    }
}
