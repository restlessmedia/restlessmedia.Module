using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace restlessmedia.Module.Configuration
{
  public abstract class TypedCollection<T> : ConfigurationElementCollection
    where T : ConfigurationElement, new()
  {
    public override ConfigurationElementCollectionType CollectionType
    {
      get
      {
        return ConfigurationElementCollectionType.AddRemoveClearMap;
      }
    }

    public T this[int index]
    {
      get { return (T)BaseGet(index); }
    }

    public new T this[string name]
    {
      get { return (T)BaseGet(name); }
    }

    protected override ConfigurationElement CreateNewElement()
    {
      return new T();
    }

    public int IndexOf(T item)
    {
      return BaseIndexOf(item);
    }

    public void Add(T item)
    {
      BaseAdd(item);
    }

    public abstract void Remove(T item);

    public void RemoveAt(int index)
    {
      BaseRemoveAt(index);
    }

    public void Remove(string name)
    {
      BaseRemove(name);
    }

    public void Clear()
    {
      BaseClear();
    }

    public IEnumerable<T> GetAll()
    {
      return this.Cast<T>();
    }

    public bool Exists(string name)
    {
      return BaseGet(name) != null;
    }
  }
}