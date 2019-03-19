using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace restlessmedia.Module.UnitTest.Extensions
{
  [TestClass]
  public class ExceptionExtensionsTests
  {
    [TestMethod]
    public void AllMessages_returns_messages()
    {
      Exception innerException = new Exception("inner");
      Exception exception = new Exception("outer", innerException);

      exception.AllMessages().Count().MustBe(2);
      exception.AllMessages().First().MustBe("outer");
      exception.AllMessages().Skip(1).First().MustBe("inner");
    }

    [TestMethod]
    public void Messages_returns_messages_with_separator()
    {
      Exception innerException = new Exception("inner");
      Exception exception = new Exception("outer", innerException);

      exception.Messages(",").MustBe("outer,inner");
    }
  }
}