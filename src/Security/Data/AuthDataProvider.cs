using restlessmedia.Module.Data;
using restlessmedia.Module.Security.Data.Sql;

namespace restlessmedia.Module.Security.Data
{
  public class AuthDataProvider : AuthSqlDataProvider, IAuthDataProvider
  {
    public AuthDataProvider(IDataContext context)
      : base(context) { }
  }
}