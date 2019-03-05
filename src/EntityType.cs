namespace restlessmedia.Module
{
  public enum EntityType : int
  {
    News = 100,
    Product = 101,
    Color = 102,
    Size = 103,
    Category = 104,
    ProductDetail = 105,
    ProductOption = 106,
    /// <summary>
    /// Deprecated, use enquiry
    /// </summary>
    Correspondence = 108,
    Sale = 109,
    /// <summary>
    /// Blog post
    /// </summary>
    Post = 110,
    Comment = 111,
    SaleItem = 112,
    Shipping = 113,
    Place = 114,
    Address = 115,
    Contact = 116,
    /// <summary>
    /// Replaces correspondence
    /// </summary>
    Enqiury = 117,
    Property = 118,
    Testimonial = 119,
    Development = 120,

    File = 200,
    Email = 203,

    Audit = 300,
    Meta = 301
  }
}