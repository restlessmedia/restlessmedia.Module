namespace restlessmedia.Module
{
  public interface IEntity
  {
    int? EntityGuid { get; }

    int? EntityId { get; }

    EntityType EntityType { get; }
  }
}