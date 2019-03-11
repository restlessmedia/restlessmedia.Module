using restlessmedia.Module.Security.Data.Sql;
using System;
using System.Linq;

namespace restlessmedia.Module.Data
{
  public class EntitySqlDataProvider : SqlDataProviderBase
  {
    public EntitySqlDataProvider(IDataContext context)
      : base(context)
    { }

    public byte GetNextRank(EntityType sourceType, EntityType targetType, int sourceEntityId)
    {
      const string commandName = "dbo.SPEntityNextRank";
      return Query<byte>(commandName, new { sourceEntityType = (int)sourceType, targetEntityType = (int)targetType, sourceEntityId = sourceEntityId }).FirstOrDefault();
    }

    public void Move(EntityType sourceType, EntityType targetType, int sourceEntityId, int targetEntityId, MoveDirection direction)
    {
      const string commandName = "dbo.SPEntityMoveRank";
      Execute(commandName, new { sourceEntityType = (int)sourceType, targetEntityType = (int)targetType, sourceEntityId = sourceEntityId, targetEntityId = targetEntityId, directionFlag = (byte)direction });
    }

    public void AutoRank(EntityType sourceType, EntityType targetType, int sourceEntityId)
    {
      const string commandName = "dbo.SPEntityAutoRank";
      Execute(commandName, new { sourceEntityType = (int)sourceType, targetEntityType = (int)targetType, sourceEntityId = sourceEntityId });
    }

    public DateTime? UpdatedDate(EntityType entityType, int entityId)
    {
      return Query<DateTime>("SELECT ISNULL(UpdatedDate, CreatedDate) FROM TEntity WHERE EntityType = @entityType and EntityId = @entityId", new { entityType, entityId }, System.Data.CommandType.Text).FirstOrDefault();
    }
  }
}