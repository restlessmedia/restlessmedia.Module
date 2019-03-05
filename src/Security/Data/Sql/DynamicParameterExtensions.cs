using Dapper;
using restlessmedia.Module.Address;
using System.Data;

namespace restlessmedia.Module.Security.Data
{
  public static class DynamicParameterExtensions
  {
    public static void Add(this DynamicParameters parameters, IAddress address)
    {
      AddId(parameters, "addressId", address.AddressId);
      parameters.Add("knownAs", address.KnownAs);
      parameters.Add("nameNumber", address.NameNumber);
      parameters.Add("address01", address.Address01);
      parameters.Add("address02", address.Address02);
      parameters.Add("postCode", address.PostCode);
      parameters.Add("countryCode", address.CountryCode);
      parameters.Add("town", address.Town);
      parameters.Add("city", address.City);
      // don't store zero markers - the db will allow null
      parameters.Add("latitude", address.Marker != null && address.Marker.Latitude != 0 ? address.Marker.Latitude : (double?)null);
      parameters.Add("longitute", address.Marker != null && address.Marker.Longitude != 0 ? address.Marker.Longitude : (double?)null);
    }

    public static void AddId(this DynamicParameters parameters, string name, int? value)
    {
      parameters.Add(name, value, DbType.Int32, ParameterDirection.InputOutput, 4);
    }
  }
}