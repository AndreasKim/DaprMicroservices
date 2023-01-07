// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;

namespace Core.Application.Models
{
    public static class Guard
    {
        public static T ThrowIfNull<T>(this T? caller,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (caller == null)
            {
                throw new ArgumentNullException($"{typeof(T)} in {filePath} for member {memberName} at line {lineNumber}");
            }
            else
            {
                return caller;
            }
        }
    }
}
