﻿using System.ComponentModel;
using System.Reflection;

namespace System
{
  /// <summary>
  /// Extensions for strongly typed enum
  /// </summary>
  public static class EnumerationExtensions
  {
    /// <summary>
    /// If applicable, returns the value of description attribute for an enum value
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetDescription(this Enum value)
    {
      FieldInfo field = value.GetType().GetField(value.ToString());
      if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
      {
        return attribute.Description;
      }

      return value.ToString();
    }
  }
}