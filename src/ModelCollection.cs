using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace restlessmedia.Module
{
  [Serializable]
  [CollectionDataContract]
  public class ModelCollection<T> : List<T> /* Remove this - just support IEnumerable<T> (in interface) */, IModelCollection
  {
    public ModelCollection()
      : this(0) { }

    public ModelCollection(int capacity)
      : base(capacity) { }

    public ModelCollection(IEnumerable<T> collection, Paging paging)
      : base(collection)
    {
      Paging = paging;
    }

    public ModelCollection(IEnumerable<T> collection)
      : base(collection)
    {
      Paging = new Paging(Count);
    }

    public ModelCollection(IEnumerable<T> collection, int totalCount)
     : this(collection, new Paging(totalCount)) { }

    public ModelCollection(IEnumerable<T> collection, int totalCount, int page)
     : this(collection, new Paging(totalCount, page)) { }

    public static ModelCollection<T> One(T item)
    {
      return new ModelCollection<T>(1) { item };
    }

    public static ModelCollection<T> Empty()
    {
      return new ModelCollection<T>(0);
    }

    /// <summary>
    /// Returns a range from the list using a pagesize and page number
    /// </summary>
    /// <param name="maxPerPage"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    public IEnumerable<T> GetPage(int page)
    {
      return Count == 0 ? new ModelCollection<T>(0) : this.Skip(page > 1 ? Paging.MaxPerPage * (page - 1) : 0).Take(Paging.MaxPerPage);
    }

    public T Random()
    {
      if (Count == 0)
      {
        return default(T);
      }

      Random rnd = new Random();
      return this[rnd.Next(0, Count - 1)];
    }

    public Paging Paging { get; private set; }
  }
}