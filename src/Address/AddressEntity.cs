namespace restlessmedia.Module.Address
{
  public class AddressEntity : Entity, IAddress
  {
    public AddressEntity() { }

    public AddressEntity(AddressType addressType = AddressType.Address)
      : this()
    {
      AddressType = addressType;
    }

    public override EntityType EntityType
    {
      get
      {
        return EntityType.Address;
      }
    }

    public override int? EntityId
    {
      get
      {
        return AddressId;
      }
    }

    [Ignore]
    public int? AddressId { get; set; }

    /// <summary>
    /// Currently this piggy backs on the title property
    /// </summary>
    public string KnownAs { get; set; }

    public string NameNumber { get; set; }

    public AddressType AddressType { get; set; }

    public virtual string Address01 { get; set; }

    public string Address02 { get; set; }

    public string Town { get; set; }

    public string City { get; set; }

    public string County { get; set; }

    public virtual string PostCode { get; set; }

    public Marker Marker
    {
      get
      {
        return _marker = _marker ?? new Marker();
      }
      set
      {
        _marker = value;
      }
    }

    public string CountryCode { get; set; }

    /// <summary>
    /// Returns a csv string of the address
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      const string separator = ", ";
      return this.ToString(separator);
    }

    private Marker _marker = null;
  }
}