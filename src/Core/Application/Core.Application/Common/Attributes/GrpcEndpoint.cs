namespace Core.Application.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GrpcEndpoint : Attribute
    {
        public GrpcEndpoint(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
