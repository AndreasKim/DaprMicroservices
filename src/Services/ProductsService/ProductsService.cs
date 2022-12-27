using AutoMapper;
using Core.Application.Attributes;
using Core.Application.Models;
using Dapr.Client;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Services.ProductsService.Application.Commands;
using Services.ProductsService.Application.Queries;
using Services.ProductsService.Generated;

namespace Services.ProductsService
{
    public class ProductsService : DaprBaseService
    {
        /// <summary>
        /// State store name.
        /// </summary>
        public const string StoreName = "statestore";

        private readonly ILogger<ProductsService> _logger;
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly DaprClient _daprClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="daprClient"></param>
        /// <param name="logger"></param>
        public ProductsService(DaprClient daprClient, ILogger<ProductsService> logger, ISender sender, IMapper mapper) : base(logger)
        {
            _daprClient = daprClient;
            _logger = logger;
            _sender = sender;
            _mapper = mapper;
        }

        [GrpcEndpoint("createproduct")]
        public async Task<Int32Value> CreateProduct(CreateProductRequest request, ServerCallContext context)
        {
            var command = _mapper.Map<CreateProductCommand>(request);

            var prod = await _sender.Send(command); 
            return new Int32Value() { Value = prod.Id }; 
        }

        [GrpcEndpoint("getproduct")]
        public async Task<GetProductByIdResponse> GetProduct(GetProductByIdRequest request, ServerCallContext context)
        {
            var query = _mapper.Map<GetProductByIdQuery>(request);

            var prod = await _sender.Send(query);

            var response = _mapper.Map<GetProductByIdResponse>(prod);
            return response;
        }

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
