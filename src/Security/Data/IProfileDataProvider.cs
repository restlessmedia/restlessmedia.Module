using restlessmedia.Module.Address;
using System.Configuration;

namespace restlessmedia.Module.Security.Data
{
  public interface IProfileDataProvider
  {
    void UpdateProfile(string applicationName, string userName, SettingsPropertyValueCollection valueCollection);

    dynamic ReadProfile(string applicationName, string userName);

    UserProfileAddress ReadProfileAddress(string applicationName, string userName, AddressType type);

    void UpdateProfileAddress(string applicationName, string userName, AddressEntity address);

    int DeleteProfiles(string[] usernames);
  }
}