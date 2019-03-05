using restlessmedia.Module.Security.Data.Sql;
using SqlBuilder.DataServices;

namespace restlessmedia.Module.Security.Data
{
  /// <summary>
  /// Provider for working with the Membership User Profile.
  /// </summary>
  /// <remarks>As we can't inject into the .net MembershipProfileProvider, this provider enables us to re-use the profile provider functionality normally utilised by the security data provider.</remarks>
  public class ProfileDataProvider : ProfileSqlDataProvider, IProfileDataProvider
  {
    public ProfileDataProvider(IConnectionFactory connectionFactory, IRetry retry)
      : base(connectionFactory, retry) { }
  }
}