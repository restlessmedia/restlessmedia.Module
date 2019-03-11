using restlessmedia.Module;
using restlessmedia.Module.Data;
using System;

namespace restlessmedia.Module.Data
{
  public interface IEntityDataProvider : IDataProvider
  {
    byte GetNextRank(EntityType sourceType, EntityType targetType, int sourceEntityId);

    void Move(EntityType sourceType, EntityType targetType, int sourceEntityId, int targetEntityId, MoveDirection direction);

    void AutoRank(EntityType sourceType, EntityType targetType, int sourceEntityId);

    DateTime? UpdatedDate(EntityType entityType, int entityId);
  }
}