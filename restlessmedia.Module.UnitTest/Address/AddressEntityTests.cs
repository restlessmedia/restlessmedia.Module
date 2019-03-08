using Microsoft.VisualStudio.TestTools.UnitTesting;
using restlessmedia.Module.Address;
using Should;

namespace restlessmedia.UnitTest.Abstractions.Models
{
  [TestClass]
  public class AddressEntityTests
  {
    [TestMethod]
    public void ToVague_returns_string()
    {
      new AddressEntity
      {
        NameNumber = "9 Pendley House",
        Address01 = "Whiston Road",
        Address02 = "Haggerston",
        PostCode = "E2 8SF"
      }.ToVague().ShouldEqual("Whiston Road, Haggerston, E2 8SF");
    }
  }
}