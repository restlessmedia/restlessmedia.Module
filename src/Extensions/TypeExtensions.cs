using MiscUtil.Linq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace restlessmedia.Module.Extensions
{
  public static class TypeExtensions
  {
    /// <summary>
    /// Creates a new instance of the specified type
    /// </summary>
    /// <typeparam name="TFirst"></typeparam>
    /// <typeparam name="TSecond"></typeparam>
    /// <typeparam name="TReturn"></typeparam>
    /// <param name="type"></param>
    /// <param name="first"></param>
    /// <returns></returns>
    public static TReturn New<TFirst, TSecond, TReturn>(this Type type, TFirst first, TSecond second)
    {
      return TypeExt.Ctor<TFirst, TSecond, TReturn>(type)(first, second);
    }

    /// <summary>
    /// Creates a new instance of the specified type
    /// </summary>
    /// <typeparam name="TFirst"></typeparam>
    /// <typeparam name="TReturn"></typeparam>
    /// <param name="type"></param>
    /// <param name="first"></param>
    /// <returns></returns>
    public static TReturn New<TFirst, TReturn>(this Type type, TFirst first)
    {
      return TypeExt.Ctor<TFirst, TReturn>(type)(first);
    }

    public static bool IsAssignableToGenericType(this Type type, Type genericType)
    {
      if (type == null || genericType == null)
      {
        return false;
      }

      return type == genericType || type.MapsToGenericTypeDefinition(genericType) || type.HasInterfaceThatMapsToGenericTypeDefinition(genericType) || type.BaseType.IsAssignableToGenericType(genericType);
    }

    /// <summary>
    /// Returns the public instance properties and fields for the given type.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static IEnumerable<MemberInfo> GetPublicMembers(this Type type)
    {
      const BindingFlags _flags = BindingFlags.Instance | BindingFlags.Public;
      return type.GetFields(_flags)
        .Cast<MemberInfo>()
        .Concat(type.GetProperties(_flags));
    }

    /// <summary>
    /// Returns the value of the member if it's a field or property otherwise, null.
    /// </summary>
    /// <param name="member"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static object GetMemberValue(this MemberInfo member, object target)
    {
      switch (member.MemberType)
      {
        case MemberTypes.Field:
          return ((FieldInfo)member).GetValue(target);
        case MemberTypes.Property:
          return ((PropertyInfo)member).GetValue(target);
        default:
          throw new ArgumentException("MemberInfo must be of type FieldInfo or PropertyInfo", "member");
      }
    }

    /// <summary>
    /// Sets the member's value on the target object.
    /// </summary>
    /// <param name="member"></param>
    /// <param name="target"></param>
    /// <param name="value"></param>
    public static void SetMemberValue(this MemberInfo member, object target, object value)
    {
      switch (member.MemberType)
      {
        case MemberTypes.Field:
          ((FieldInfo)member).SetValue(target, value);
          break;
        case MemberTypes.Property:
          ((PropertyInfo)member).SetValue(target, value, null);
          break;
        default:
          throw new ArgumentException("MemberInfo must be of type FieldInfo or PropertyInfo", "member");
      }
    }

    /// <summary>
    /// If true, the given type inherits <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool Inherits<T>(this Type type)
    {
      return Inherits(type, typeof(T));
    }

    /// <summary>
    /// If true, the given type inherits <see cref="baseType"/>.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="baseType"></param>
    /// <returns></returns>
    public static bool Inherits(this Type type, Type baseType)
    {
      if (type.BaseType == null)
      {
        return false;
      }

      if (type.BaseType == baseType)
      {
        return true;
      }

      if (baseType.IsAssignableFrom(type.BaseType) || baseType.IsAssignableFrom(type))
      {
        return true;
      }

      if (type.BaseType.IsGenericType)
      {
        return type.BaseType.GetGenericTypeDefinition() == baseType;
      }

      return type.BaseType == baseType;
    }

    /// <summary>
    /// Returns the return type of the field or property.
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    public static Type ReturnType(this MemberInfo member)
    {
      switch (member.MemberType)
      {
        case MemberTypes.Field:
          {
            return ((FieldInfo)member).FieldType;
          }
        case MemberTypes.Property:
          {
            return ((PropertyInfo)member).PropertyType;
          }
        default:
          throw new ArgumentException("MemberInfo must be of type FieldInfo or PropertyInfo", "member");
      }
    }

    /// <summary>
    /// If true, the type is nullable.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsNullable(this Type type)
    {
      return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    /// <summary>
    /// If true, the type is nullable.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="underlyingType"></param>
    /// <returns></returns>
    public static bool IsNullable(this Type type, out Type underlyingType)
    {
      underlyingType = Nullable.GetUnderlyingType(type);
      return underlyingType != null;
    }

    private static bool HasInterfaceThatMapsToGenericTypeDefinition(this Type givenType, Type genericType)
    {
      return givenType.GetInterfaces().Where(it => it.IsGenericType).Any(it => it.GetGenericTypeDefinition() == genericType);
    }

    private static bool MapsToGenericTypeDefinition(this Type givenType, Type genericType)
    {
      return genericType.IsGenericTypeDefinition && givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType;
    }
  }
}