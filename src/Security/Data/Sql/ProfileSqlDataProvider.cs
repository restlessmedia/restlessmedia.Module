using Dapper;
using restlessmedia.Module.Address;
using SqlBuilder.DataServices;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace restlessmedia.Module.Security.Data.Sql
{
  public class ProfileSqlDataProvider : SqlAccess
  {
    public ProfileSqlDataProvider(IConnectionFactory connectionFactory, IRetry retry)
      : base(connectionFactory, retry) { }

    public void UpdateProfile(string applicationName, string userName, System.Configuration.SettingsPropertyValueCollection valueCollection)
    {
      IEnumerable<SettingsPropertyValue> values = valueCollection.Cast<SettingsPropertyValue>();

      SettingsPropertyValue firstName = values.FirstOrDefault(x => x.Name == "FirstName");
      SettingsPropertyValue surname = values.FirstOrDefault(x => x.Name == "Surname");
      SettingsPropertyValue contactNumber = values.FirstOrDefault(x => x.Name == "ContactNumber");

      Execute("dbo.SPUpdateProfile", new { applicationName = applicationName, userName = userName, firstName = firstName.PropertyValue.ToString(), surname = surname.PropertyValue.ToString(), contactNumber = contactNumber.PropertyValue.ToString() });
    }

    public dynamic ReadProfile(string applicationName, string userName)
    {
      return Query("dbo.SPReadProfile", new { applicationName = applicationName, userName = userName }).FirstOrDefault();
    }

    public UserProfileAddress ReadProfileAddress(string applicationName, string userName, AddressType type)
    {
      return Query<UserProfileAddress>("dbo.SPReadProfileAddress", new { applicationName = applicationName, userName = userName, addressType = (byte)type }).FirstOrDefault();
    }

    public void UpdateProfileAddress(string applicationName, string userName, AddressEntity address)
    {
      DynamicParameters parameters = new DynamicParameters();

      parameters.Add("@applicationName", applicationName);
      parameters.Add("@userName", userName);
      parameters.Add("@addressType", (byte)address.AddressType);
      parameters.Add(address);

      Execute("dbo.SPUpdateProfileAddress", parameters);
    }

    public int DeleteProfiles(string[] usernames)
    {
      ExecuteWithTransaction((transaction) =>
      {
        foreach (string username in usernames)
        {
          transaction.Connection.Execute("dbo.SPDeleteProfile", new { username = username }, transaction: transaction, commandType: CommandType.StoredProcedure);
        }
      });

      // we probably need to return the count of the ids that were deleted - for the moment, fuck that.
      return -1;
    }
  }
}