namespace MultiTenant.Core.Extensions;

public static class CoreExtensions
{
    public static bool IsAssignableToGenericType(this Type typeToCheck, Type typeToCompare)
    {
        var interfaceTypes = typeToCheck.GetInterfaces();

        foreach (var it in interfaceTypes)
        {
            if (it.IsGenericType && it.GetGenericTypeDefinition() == typeToCompare)
                return true;
        }

        if (typeToCheck.IsGenericType && typeToCheck.GetGenericTypeDefinition() == typeToCompare)
            return true;

        Type baseType = typeToCheck.BaseType;
        if (baseType == null)
            return false;

        return baseType.IsAssignableToGenericType(typeToCompare);
    }
}
