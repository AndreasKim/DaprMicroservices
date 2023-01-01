using Core.Application.Mappings;
using Core.Domain.Entities;
using MediatR;
using ProtoBuf;

namespace Services.ProductsService.Application.Commands;

[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
public record PerformanceTestCommand : IRequest<PerformanceTestDto>, IMapTo<PerformanceTestDto>
{
    public List<string> StringList { get; set; } = new List<string>();
    public List<int> IntList { get; set; } = new List<int>();
}

public class PerformanceTestCommandHandler : IRequestHandler<PerformanceTestCommand, PerformanceTestDto>
{
    public Task<PerformanceTestDto> Handle(PerformanceTestCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new PerformanceTestDto
        {
            IntList = request.IntList,
            StringList =  request.StringList
        });
    }

}
