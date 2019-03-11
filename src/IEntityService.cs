using System;

namespace restlessmedia.Module
{
  public interface IEntityService : IService
  {
    /// <summary>
    /// Returns the next rank available
    /// </summary>
    /// <param name="sourceType"></param>
    /// <param name="targetType"></param>
    /// <param name="sourceEntityId"></param>
    /// <returns></returns>
    byte GetNextRank(EntityType sourceType, EntityType targetType, int sourceEntityId);

    /// <summary>
    /// Moves a entity up or down rank
    /// </summary>
    /// <param name="sourceType"></param>
    /// <param name="targetType"></param>
    /// <param name="sourceEntityId"></param>
    /// <param name="targetEntityId"></param>
    /// <param name="direction"></param>
    void Move(EntityType sourceType, EntityType targetType, int sourceEntityId, int targetEntityId, MoveDirection direction);

    /// <summary>
    /// Automatically ranks entities
    /// </summary>
    /// <param name="sourceType"></param>
    /// <param name="targetType"></param>
    /// <param name="sourceEntityId"></param>
    void AutoRank(EntityType sourceType, EntityType targetType, int sourceEntityId);

    DateTime? UpdatedDate(EntityType entityType, int entityId);
  }
}
