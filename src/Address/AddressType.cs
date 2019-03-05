namespace restlessmedia.Module.Address
{
  public enum AddressType : byte
  {
    BillingAddress = 0,
    DeliveryAddress = 1,
    HomeAddress = 2,
    BusinessAddress = 3,
    /// <summary>
    /// No specific address type, usually used for places where the address type is irrelevant i.e. someones home
    /// </summary>
    Address = 4
  }
}