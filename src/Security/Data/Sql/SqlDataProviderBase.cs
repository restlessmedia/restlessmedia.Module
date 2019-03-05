using restlessmedia.Module.Data;
using SqlBuilder.DataServices;

namespace restlessmedia.Module.Security.Data.Sql
{
  public abstract class SqlDataProviderBase : SqlAccess
  {
    public SqlDataProviderBase(IDataContext dataContext)
      : base(dataContext.ConnectionFactory, dataContext.Retry)
    {
      DataContext = dataContext;
    }

    public IDataContext DataContext { get; private set; }
  }
}