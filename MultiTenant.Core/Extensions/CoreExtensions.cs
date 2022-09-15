namespace MultiTenant.Core.Extensions;

public static class CoreExtensions
{
    /// <summary>
    /// Check if a type is assignable from a generic type
    /// </summary>
    /// <param name="typeToCheck">Type to check</param>
    /// <param name="baseType">Base type</param>
    /// <returns>true if types are assignables. Otherwise, false.</returns>
    public static bool IsAssignableToGenericType(this Type typeToCheck, Type baseType)
    {
        var interfaceTypes = typeToCheck.GetInterfaces();

        foreach (var it in interfaceTypes)
        {
            if (it.IsGenericType && it.GetGenericTypeDefinition() == baseType)
                return true;
        }

        if (typeToCheck.IsGenericType && typeToCheck.GetGenericTypeDefinition() == baseType)
            return true;

        Type checkingType = typeToCheck.BaseType;
        if (checkingType == null)
            return false;

        return checkingType.IsAssignableToGenericType(baseType);
    }
}
