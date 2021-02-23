using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace restlessmedia.Module.Data.Sql
{
  public static class UDTHelper
  {
    public static SqlMetaData[] GetSqlMetaData(object obj)
    {
      return GetSqlMetaData(obj.GetType());
    }

    public static SqlMetaData[] GetSqlMetaData<T>()
    {
      return GetSqlMetaData(typeof(T));
    }

    public static SqlMetaData[] GetSqlMetaData(Type type)
    {
      return GetMetaData(type).Select(x => x.ToSqlMetaData()).ToArray();
    }

    public static SqlParameter CreateParameter<T>(string name, string typeName, IEnumerable<T> values)
      where T : UDTBase
    {
      return new SqlParameter(name, SqlDbType.Structured)
      {
        TypeName = typeName,
        Value = values.Select(x => x.DataRecord)
      };
    }

    public static SqlParameter CreateParameter<T>(string name, string typeName, params T[] values)
      where T : UDTBase
    {
      return CreateParameter(name, typeName, values);
    }

    public static SqlParameter CreateParameter(string name, IEnumerable<int> values)
    {
      return CreateParameter(name, UDTInt.TypeName, values.Select(x => new UDTInt(x)));
    }

    public static void AddParameter<T>(SqlCommand command, string name, string typeName, IEnumerable<T> values)
      where T : UDTBase
    {
      command.Parameters.Add(CreateParameter(name, typeName, values));
    }

    public static void AddParameter<T>(SqlCommand command, string name, string typeName, params T[] values)
      where T : UDTBase
    {
      AddParameter(command, name, typeName, (IEnumerable<T>)values);
    }

    public static void AddParameter(SqlCommand command, string name, IEnumerable<int> values)
    {
      AddParameter(command, name, UDTInt.TypeName, values.Select(x => new UDTInt(x)));
    }

    public static SqlDataRecord GetData<T>(T data)
      where  T : UDTBase
    {
      return data.DataRecord;
    }

    public static IEnumerable<SqlDataRecord> GetData<T>(IEnumerable<T> data)
      where  T : UDTBase
    {
      return data.Select(GetData);
    }

    private static MetaData[] GetMetaData(Type type)
    {
      return type.GetProperties(_flags).Select(x => GetMetaData(x)).ToArray();
    }

    private static T GetAttribute<T>(PropertyInfo property, bool inherit)
          where T : Attribute
    {
      return GetAttributes<T>(property, inherit).FirstOrDefault();
    }

    private static IEnumerable<T> GetAttributes<T>(PropertyInfo property, bool inherit)
        where T : Attribute
    {
      return property.GetCustomAttributes(typeof(T), inherit).Cast<T>();
    }

    private static MetaData GetMetaData(PropertyInfo property)
    {
      return GetAttribute<MetaData>(property, true);
    }

    private class Cache
    {
      public Cache()
      {
        _items = new Dictionary<Type, MetaData[]>();
      }

      public MetaData[] Get(Type type, Func<Type, MetaData[]> valueFactory)
      {
        MetaData[] metaData = _items[type];

        if (metaData == null)
        {
          _items.Add(type, metaData = valueFactory(type));
        }

        return metaData;
      }

      private readonly IDictionary<Type, MetaData[]> _items;
    }

    private readonly static BindingFlags _flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.GetProperty;
  }
}