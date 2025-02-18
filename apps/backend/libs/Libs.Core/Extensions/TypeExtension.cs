using System;
using System.Collections.Generic;
using System.Linq;

namespace FwksLabs.Libs.Core.Extensions;

public static class TypeExtension
{
    public static List<object?> FindConfigurationFromAssembly<T>(this Type type) =>
        type.Assembly
            .GetTypes()
            .Where(x => x.IsAssignableTo(typeof(T)))
            .Select(Activator.CreateInstance)
            .ToList();
}
