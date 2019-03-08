namespace restlessmedia.Module
{
  public interface ICategory
  {
    int? CategoryId { get; set; }

    string Title { get; set; }

    string Description { get; set; }

    int? CategoryParentId { get; set; }
  }
}
