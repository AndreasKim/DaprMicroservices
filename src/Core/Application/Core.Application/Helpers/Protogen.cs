﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Core.Application.Interfaces;
using Core.Application.Models;
using MediatR;
using ProtoBuf;

namespace Core.Application.Helpers
{
    public static class Protogen
    {
        [Conditional("DEBUG")]
        public static void Generate(Assembly assembly)
        {
            var requestTypes = assembly.GetTypes().Where(p => p.GetInterfaces()
                .Any(i => (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>))));
            var responseTypes = assembly.GetTypes().Where(p => p.GetInterfaces().Any(i => i == typeof(IResponse)));

            var method = typeof(Serializer).GetMethods().FirstOrDefault(p => p.IsGenericMethod && p.IsPublic && p.IsStatic && p.Name == "GetProto")
                ?? throw new ArgumentNullException("GetProto");

            var service = assembly.GetTypes().Single(p => p.IsSubclassOf(typeof(DaprBaseService)));
            var mappingDict = new Dictionary<string, (Type? Type, bool IsRequest)>();

            var builder = new StringBuilder();
            builder.AppendLine($"// This file has been generated by {typeof(Protogen).Namespace}/Protogen.cs");
            builder.AppendLine();

            builder.AppendLine("syntax = \"proto3\";");
            builder.AppendLine($"option csharp_namespace = \"Services.{service.Name}.Generated\";");
            builder.AppendLine();

            builder.AppendLine("// Requests");
            foreach (var type in requestTypes)
            {
                var name = GenerateProtoString(method, builder, type,
                    @"(message \w*)(Command|Query)( {.*)", @"$1Request$3");
                mappingDict.Add(name, (type, true));
            }
            builder.AppendLine();

            builder.AppendLine("// Responses");
            foreach (var type in responseTypes)
            {
                var name = GenerateProtoString(method, builder, type,
                     @"(message \w*)(Dto)( {.*)", @"$1Response$3");
                mappingDict.Add(name, (type, false));
            }

            var result = builder.ToString();

            WriteToFile(builder, "Protos", $"{service.Name}.proto");

            CreateMappingProfile(mappingDict, service);
        }

        private static void CreateMappingProfile(Dictionary<string, (Type? Type, bool IsRequest)> mappingDict, Type service)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"// This file has been generated by {typeof(Protogen).Namespace}/Protogen.cs");

            builder.AppendLine("using AutoMapper;");
            builder.AppendLine($"using Services.{service.Name}.Generated;");

            var nameSpaces = mappingDict.Select(p => p.Value.Type!.Namespace).Distinct().ToList();
            nameSpaces.ForEach(p => builder.AppendLine($"using {p};"));
            builder.AppendLine();

            builder.AppendLine("namespace Services.ProductsService.Protos;");
            builder.AppendLine();

            builder.Append("public class GrpcProfile : Profile\r\n{\r\n\tpublic GrpcProfile()\r\n\t{");
            builder.AppendLine();
            foreach (var item in mappingDict)
            {
                if (item.Value.IsRequest)
                {
                    builder.AppendLine($"\t\tCreateMap<{item.Key},{item.Value.Type!.Name}>();");
                }
                else
                {
                    builder.AppendLine($"\t\tCreateMap<{item.Value.Type!.Name},{item.Key}>();");
                }
            }
            builder.Append("\t}\r\n}");

            WriteToFile(builder, "Common", $"GrpcProfile.cs");
        }

        private static void WriteToFile(StringBuilder builder, string basePath, string name)
        {
            string path = Path.Combine(basePath, name);
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            File.WriteAllText(path, builder.ToString());
        }

        private static string GenerateProtoString(MethodInfo method, StringBuilder builder,
            Type type, string pattern, string replacement)
        {
            var genericMethod = method.MakeGenericMethod(type);
            var protoOutput = genericMethod.Invoke(null, Array.Empty<object>())
                ?? throw new InvalidOperationException("Cannot invoke GetProto method.");
            var protoStr = protoOutput.ToString();
            protoStr = protoStr?.Substring(protoStr.IndexOf("message")) ?? "";

            protoStr = Regex.Replace(protoStr, pattern, replacement);
            builder.Append(protoStr);

            var name = Regex.Match(protoStr, @"message (\w*) {.*").Groups[1].Value;
            return name;
        }
    }
}
