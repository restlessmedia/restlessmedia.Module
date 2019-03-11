namespace restlessmedia.Module.Data
{
  public class EntityDataProvider : EntitySqlDataProvider, IEntityDataProvider
  {
    public EntityDataProvider(IDataContext context)
      : base(context) { }
  }
}