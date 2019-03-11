using restlessmedia.Module.Data;
using System;

namespace restlessmedia.Module
{
  internal class EntityService : IEntityService
  {
    public EntityService(IEntityDataProvider entityDataProvider)
    {
      _entityDataProvider = entityDataProvider ?? throw new ArgumentNullException(nameof(entityDataProvider));
    }

    public byte GetNextRank(EntityType sourceType, EntityType targetType, int sourceEntityId)
    {
      return _entityDataProvider.GetNextRank(sourceType, targetType, sourceEntityId);
    }

    public void Move(EntityType sourceType, EntityType targetType, int sourceEntityId, int targetEntityId, MoveDirection direction)
    {
      // don't support left or right moving so exit if trying to do so
      if (direction == MoveDirection.Left || direction == MoveDirection.Right)
      {
        return;
      }

      _entityDataProvider.Move(sourceType, targetType, sourceEntityId, targetEntityId, direction);
    }

    public void AutoRank(EntityType sourceType, EntityType targetType, int sourceEntityId)
    {
      _entityDataProvider.AutoRank(sourceType, targetType, sourceEntityId);
    }

    public DateTime? UpdatedDate(EntityType entityType, int entityId)
    {
      return _entityDataProvider.UpdatedDate(entityType, entityId);
    }

    private readonly IEntityDataProvider _entityDataProvider;
  }
}