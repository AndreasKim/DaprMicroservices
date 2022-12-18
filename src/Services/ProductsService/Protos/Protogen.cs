using Core.Application.Common.Mappings;
using Core.Application.Common.Models;
using MediatR.Wrappers;
using ProtoBuf;
using ProtoBuf.Meta;
using System.Reflection;
using System.Text;

namespace Services.ProductsService.Protos
{
    public static class Protogen
    {
        public static void Generate() 
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes().Where(p => p.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>)));         
            var method = typeof(Serializer).GetMethods().FirstOrDefault(p => p.IsGenericMethod && p.IsPublic && p.IsStatic && p.Name == "GetProto")
                ?? throw new ArgumentNullException("GetProto");

            var service = assembly.GetTypes().First(p => p.IsSubclassOf(typeof(DaprBaseService)));

            var builder = new StringBuilder();
            builder.AppendLine("syntax = \"proto3\";");
            builder.AppendLine($"option csharp_namespace = \"Services.{service.Name}\";");
            foreach (var type in types)
            {
                var genericMethod = method.MakeGenericMethod(type);
                var protoOutput = genericMethod.Invoke(null, Array.Empty<object>()) 
                    ?? throw new InvalidOperationException("Cannot invoke GetProto method."); 
                var protoStr = protoOutput.ToString();
                protoStr = protoStr?.Substring(protoStr.IndexOf("message")) ?? "";

                var indexOfBracket = protoStr.IndexOf("{");
                protoStr = protoStr.Insert(indexOfBracket - 1, "Dto");

                builder.Append(protoStr);
            }

            var result = builder.ToString();

            string path = Path.Combine("Protos", "data.proto");
            File.WriteAllText(path, result);
        }
    }
}
