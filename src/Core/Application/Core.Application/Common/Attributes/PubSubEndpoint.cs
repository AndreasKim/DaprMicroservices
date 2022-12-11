namespace Core.Application.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PubSubEndpoint : Attribute
    {
        public PubSubEndpoint(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
