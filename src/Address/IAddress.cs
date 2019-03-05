namespace restlessmedia.Module.Address
{
  public interface IAddress
  {
    int? AddressId { get; set; }

    string KnownAs { get; set; }

    string NameNumber { get; set; }

    //AddressType AddressType { get; }

    string Address01 { get; set; }

    string Address02 { get; set; }

    string Town { get; set; }

    string City { get; set; }

    //string County { get; set; }

    string PostCode { get; set; }

    /// <summary>
    /// Returns the postal area of a post code i.e if the post code is BN26 the postal area code is BN
    /// </summary>
    Marker Marker { get; set; }

    string CountryCode { get; set; }
  }
}