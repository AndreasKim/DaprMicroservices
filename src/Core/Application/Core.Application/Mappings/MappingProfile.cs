using AutoMapper;
using Core.Application.Mappings.Converters;
using System.Reflection;

namespace Core.Application.Mappings;

public class MappingProfile : Profile
{
    private static readonly string[] excluded = new string[] { "System.", "Microsoft.", "Dapr.", "MediatR", "Swashbuckle.", "protobuf-net.", "Ardalis.", "Grpc." };
    public MappingProfile()
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()
            .Where(p => !p.IsDynamic && !excluded.Any(x => p.FullName?.Contains(x) ?? false)))
        {
            ApplyMappingsFromAssembly(assembly);
        }

        CreateMap<string, Uri?>().ConvertUsing<StringToUriConverter>();
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        ApplyMappingForType(assembly, typeof(IMapFrom<>), nameof(IMapFrom<object>.Mapping));
        ApplyMappingForType(assembly, typeof(IMapTo<>), nameof(IMapTo<object>.Mapping));
    }

    private void ApplyMappingForType(Assembly assembly, Type mapFromType, string mappingMethodName)
    {
        bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

        var argumentTypes = new Type[] { typeof(Profile) };

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(mappingMethodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Count > 0)
                {
                    foreach (var @interface in interfaces)
                    {
                        var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                        interfaceMethodInfo?.Invoke(instance, new object[] { this });
                    }
                }
            }
        }
    }
}
