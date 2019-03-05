using System.Collections;

namespace restlessmedia.Module
{
  public interface IModelCollection : IList // TODO: change to IEnumerable
  {
    Paging Paging { get; }
  }
}