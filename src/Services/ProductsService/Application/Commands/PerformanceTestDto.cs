using Core.Application.Interfaces;
using ProtoBuf;

namespace Core.Domain.Entities;

[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
public class PerformanceTestDto : IResponse
{
    public List<string> StringList { get; set; } = new List<string>();
    public List<int> IntList { get; set; } = new List<int>();

}
