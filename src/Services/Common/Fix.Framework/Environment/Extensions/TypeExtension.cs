using System;

namespace Fix.Environment.Extensions
{

    //public static class AssignableExtensions
    //{
    /// <summary>
    /// Determines whether the <paramref name="genericType"/> is assignable from
    /// <paramref name="givenType"/> taking into account generic definitions
    /// </summary>
    //    public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
    //    {
    //        if (givenType == null || genericType == null)
    //        {
    //            return false;
    //        }

    //        return givenType == genericType
    //          || givenType.MapsToGenericTypeDefinition(genericType)
    //          || givenType.HasInterfaceThatMapsToGenericTypeDefinition(genericType)
    //          || givenType.BaseType.IsAssignableToGenericType(genericType);
    //    }

    //    private static bool HasInterfaceThatMapsToGenericTypeDefinition(this Type givenType, Type genericType)
    //    {
    //        return givenType
    //          .GetInterfaces()
    //          .Where(it => it.IsGenericType)
    //          .Any(it => it.GetGenericTypeDefinition() == genericType);
    //    }

    //    private static bool MapsToGenericTypeDefinition(this Type givenType, Type genericType)
    //    {
    //        return genericType.IsGenericTypeDefinition
    //          && givenType.IsGenericType
    //          && givenType.GetGenericTypeDefinition() == genericType;
    //    }
    //}
    public static class TypeExtension
    {
        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            Type baseType = givenType.BaseType;
            if (baseType == null) return false;

            return IsAssignableToGenericType(baseType, genericType);
        }

        public static bool IsDerivedInterface(this Type givenType)
        {
            return (givenType.GetInterfaces().Length > 0);
        }
        public static bool IsDerivedAbstract(this Type givenType)
        {
            return (givenType.BaseType != null && givenType.BaseType.IsAbstract);
        }
    }
}
