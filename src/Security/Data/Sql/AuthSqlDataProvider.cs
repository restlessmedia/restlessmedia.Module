using restlessmedia.Module.Data;
using System.Linq;

namespace restlessmedia.Module.Security.Data.Sql
{
  public class AuthSqlDataProvider : SqlDataProviderBase
  {
    public AuthSqlDataProvider(IDataContext context)
      : base(context) { }

    public Auth Read(AuthServiceType type, string username)
    {
      Auth auth = Query<Auth>("dbo.SPReadAuth", new { username = username }).FirstOrDefault();
      auth.Type = type;
      return auth;
    }

    public int Save(Auth auth)
    {
      return Query<int>("dbo.SPCreateAuth", new { username = auth.Username, consumerKey = auth.ConsumerKey, consumerSecret = auth.ConsumerSecret, accessToken = auth.AccessToken, accessTokenSecret = auth.AccessTokenSecret }).First();
    }
  }
}