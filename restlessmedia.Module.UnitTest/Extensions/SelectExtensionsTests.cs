using FakeItEasy;
using restlessmedia.Module.Configuration;
using SqlBuilder;
using System.Data;
using Xunit;

namespace restlessmedia.Module.UnitTest.Extensions
{
  public class SelectExtensionsTests
  {
    [Fact]
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
