using Newtonsoft.Json;
using System;

namespace restlessmedia.Module
{
  [Serializable]
  public abstract class Entity : IEntity
  {
    public virtual int Id
    {
      get
      {
        return EntityId.GetValueOrDefault(0);
      }
    }

    [Ignore]
    [JsonIgnore]
    public abstract EntityType EntityType { get; }

    [Ignore]
    [JsonIgnore]
    public abstract int? EntityId { get; }

    [Ignore]
    [JsonIgnore]
    public int? EntityGuid { get; set; }

    public virtual string Title { get; set; }
  }
}