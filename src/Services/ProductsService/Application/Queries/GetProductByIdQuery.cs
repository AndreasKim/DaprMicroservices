using AutoMapper;
using Core.Application.Interfaces;
using Core.Application.Mappings;
using Core.Domain.Entities;
using MediatR;
using ProtoBuf;
using Services.ProductsService.Application.Specifications;

namespace Services.ProductsService.Application.Queries;

[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
public record GetProductByIdQuery : IRequest<GetProductByIdDto>, IMapTo<ProductFilter>
{
    public int Id { get; set; }
    public bool IncludeSalesInfo { get; set; }
    public bool IncludeRatings { get; set; }
}


public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdDto>
{
    private readonly IRepository<Product> _repository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IRepository<Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetProductByIdDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<ProductFilter>(request);

        var product = await _repository.SingleOrDefaultAsync(new ProductSpecification(filter), cancellationToken);

        return _mapper.Map<GetProductByIdDto>(product);
    }
}
