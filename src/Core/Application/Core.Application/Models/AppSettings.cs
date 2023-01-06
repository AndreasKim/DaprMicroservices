// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Core.Application.Models;

public class AppSettings
{
    public Connectionstrings? ConnectionStrings { get; set; }
    public Serilog? Serilog { get; set; }
    public string? AllowedHosts { get; set; }
    public string? SeqServerUrl { get; set; }
    public string? ZipkinServerUrl { get; set; }
}

public class Connectionstrings
{
    public string? DefaultConnection { get; set; }
}

public class Serilog
{
    public Minimumlevel? MinimumLevel { get; set; }
}

public class Minimumlevel
{
    public string? Default { get; set; }
    public Override? Override { get; set; }
}

public class Override
{
    public string? Microsoft { get; set; }
    public string? System { get; set; }
}
