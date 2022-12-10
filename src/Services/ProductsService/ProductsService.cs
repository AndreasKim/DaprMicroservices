using Core.Application.Common.Models;
using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client;
using Dapr.Client.Autogen.Grpc.v1;
using Google.Protobuf.WellKnownTypes;
using Google.Protobuf;
using Grpc.Core;
using GrpcServiceSample.Generated;
using System.Text.Json;

namespace Services.ProductsService
{
    public class ProductsService : DaprBaseService
    {
        private const string PUBSUBNAME = "pubsub";
        /// <summary>
        /// State store name.
        /// </summary>
        public const string StoreName = "statestore";

        private readonly ILogger<ProductsService> _logger;
        private readonly DaprClient _daprClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="daprClient"></param>
        /// <param name="logger"></param>
        public ProductsService(DaprClient daprClient, ILogger<ProductsService> logger) : base(daprClient, logger)
        {
            _daprClient = daprClient;
            _logger = logger;
            AddInvokeHandlers();
            AddTopicSubscriptions();
            AddTopicEvents();
        }

        private void AddInvokeHandlers()
        {
            AddInvokeHandler<GetAccountRequest, Account>("getaccount", GetAccount);
            AddInvokeHandler<Transaction, Account>("deposit", Deposit);
        }

        private void AddTopicSubscriptions()
        {
            AddSubscription("deposit");
            AddSubscription("withdraw");
        }

        private void AddTopicEvents()
        {
            AddTopicEvent<GetAccountRequest, Account>("getaccount", GetAccount);
            AddTopicEvent<Transaction, Account>("deposit", Deposit);
            AddTopicEvent<Transaction, Account>("withdram", Withdraw);
        }

        /// <summary>
        /// GetAccount
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<GrpcServiceSample.Generated.Account> GetAccount(GetAccountRequest input, ServerCallContext context)
        {
            var state = await _daprClient.GetStateEntryAsync<Models.Account>(StoreName, input.Id);
            return new GrpcServiceSample.Generated.Account() { Id = state.Value.Id, Balance = (int)state.Value.Balance, };
        }

        /// <summary>
        /// Deposit
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<GrpcServiceSample.Generated.Account> Deposit(GrpcServiceSample.Generated.Transaction transaction, ServerCallContext context)
        {
            _logger.LogDebug("Enter deposit");
            var state = await _daprClient.GetStateEntryAsync<Models.Account>(StoreName, transaction.Id);
            state.Value ??= new Models.Account() { Id = transaction.Id, };
            state.Value.Balance += transaction.Amount;
            await state.SaveAsync();
            return new GrpcServiceSample.Generated.Account() { Id = state.Value.Id, Balance = (int)state.Value.Balance, };
        }

        /// <summary>
        /// Withdraw
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<GrpcServiceSample.Generated.Account> Withdraw(GrpcServiceSample.Generated.Transaction transaction, ServerCallContext context)
        {
            _logger.LogDebug("Enter withdraw");
            var state = await _daprClient.GetStateEntryAsync<Models.Account>(StoreName, transaction.Id);

            if (state.Value == null)
            {
                throw new Exception($"NotFound: {transaction.Id}");
            }

            state.Value.Balance -= transaction.Amount;
            await state.SaveAsync();
            return new GrpcServiceSample.Generated.Account() { Id = state.Value.Id, Balance = (int)state.Value.Balance, };
        }

    }
}
