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

        [PubSubEndpoint("updateproduct")]
        public async Task UpdateProduct(UpdateProductRequest request, ServerCallContext context)
        {
            var query = _mapper.Map<UpdateProductCommand>(request);

            await _sender.Send(query);
        }

        [PubSubEndpoint("deleteproduct")]
        public async Task DeleteProduct(DeleteProductRequest request, ServerCallContext context)
        {
            var query = _mapper.Map<DeleteProductCommand>(request);

            await _sender.Send(query);
        }
    }
}
