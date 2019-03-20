using SqlBuilder.DataServices;

namespace restlessmedia.Module.Data.Sql
{
  public abstract class SqlDataProviderBase : SqlAccess
  {
    public SqlDataProviderBase(IDataContext dataContext)
      : base(dataContext.ConnectionFactory, dataContext.Retry)
    {
      DataContext = dataContext;
    }

    public IDataContext DataContext { get; private set; }

    protected ModelCollection<T> ModelQuery<T>(string commandName, dynamic param = null)
    {
      return new ModelCollection<T>(Query<T>(commandName, param));
    }
  }
}