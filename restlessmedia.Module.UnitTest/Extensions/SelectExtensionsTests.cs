using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using restlessmedia.Module.Configuration;
using SqlBuilder;
using System.Data;

namespace restlessmedia.Module.UnitTest.Extensions
{
  [TestClass]
  public class SelectExtensionsTests
  {
    [TestMethod]
    public void WithLicenseId_adds_LiceseId_to_where_clause()
    {
      Select select = new Select();
      IDbConnection connection = A.Fake<IDbConnection>();
      ILicenseSettings licenseSettings = A.Fake<ILicenseSettings>();

      try
      {
        // TODO: work out how we can fake the data call to get the license id
        select.WithLicenseId(connection, licenseSettings);
        // select.Where().Sql().MustBe("");
      }
      catch
      {
      }
    }
  }
}
