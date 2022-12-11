using System.Reflection;
using System.Windows.Input;
using AutoMapper;
using Core.Application.Common.Interfaces;
using Google.Api;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Core.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            ApplyMappingsFromAssembly(assembly);
        }

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
