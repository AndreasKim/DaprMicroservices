using Core.Application.Attributes;
using Core.Application.Models;
using Dapr.Client;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Services.ProductsService.Application.Commands;

namespace Services.ProductsService
{
    public class ProductsService : DaprBaseService, IHostedService
    {
        private const string PUBSUBNAME = "pubsub";
        /// <summary>
        /// State store name.
        /// </summary>
        public const string StoreName = "statestore";

        private readonly ILogger<ProductsService> _logger;
        private readonly ISender _sender;
        private readonly DaprClient _daprClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="daprClient"></param>
        /// <param name="logger"></param>
        public ProductsService(DaprClient daprClient, ILogger<ProductsService> logger, ISender sender) : base(logger)
        {
            _daprClient = daprClient;
            _logger = logger;
            _sender = sender;
        }

        [GrpcEndpoint("createproduct")]
        public async Task<Int32Value> CreateProduct(CreateProductCommandDto input, ServerCallContext context)
        {         
            var prod = await _sender.Send(new CreateProductCommand() { Description = input.Description }); 
            return new Int32Value() { Value = prod.Id };
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        //[PubSubEndpoint("deposit")]
        //public async Task<GrpcServiceSample.Generated.Account> Deposit(GrpcServiceSample.Generated.Transaction transaction, ServerCallContext context)
        //{
        //    _logger.LogDebug("Enter deposit");
        //    var state = await _daprClient.GetStateEntryAsync<GrpcServiceSample.Generated.Account>(StoreName, transaction.Id);
        //    state.Value ??= new Account() { Id = transaction.Id, };
        //    state.Value.Balance += transaction.Amount;
        //    await state.SaveAsync();
        //    return new GrpcServiceSample.Generated.Account() { Id = state.Value.Id, Balance = (int)state.Value.Balance, };
        //}

        //[PubSubEndpoint("withdraw")]
        //public async Task<GrpcServiceSample.Generated.Account> Withdraw(GrpcServiceSample.Generated.Transaction transaction, ServerCallContext context)
        //{
        //    _logger.LogDebug("Enter withdraw");
        //    var state = await _daprClient.GetStateEntryAsync<Account>(StoreName, transaction.Id);

        //    if (state.Value == null)
        //    {
        //        throw new Exception($"NotFound: {transaction.Id}");
        //    }

        //    state.Value.Balance -= transaction.Amount;
        //    await state.SaveAsync();
        //    return new GrpcServiceSample.Generated.Account() { Id = state.Value.Id, Balance = (int)state.Value.Balance, };
        //}
    }
}
