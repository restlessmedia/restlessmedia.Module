using restlessmedia.Module.Configuration;
using restlessmedia.Module.Data;
using restlessmedia.Module.Security.Data.Sql;

namespace restlessmedia.Module.Security.Data
{
  /// <summary>
  /// This is public due to the membership provider and how we deal with DI for that framework feature
  /// </summary>
  public class SecurityDataProvider : SecuritySqlDataProvider, ISecurityDataProvider
  {
    public SecurityDataProvider(IDataContext context, IRoleSettings roleSettings)
      : base(context, roleSettings) { }
  }
}