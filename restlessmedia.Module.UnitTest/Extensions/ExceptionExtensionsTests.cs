using restlessmedia.Test;
using System;
using System.Linq;
using Xunit;

namespace restlessmedia.Module.UnitTest.Extensions
{
  public class ExceptionExtensionsTests
  {
    [Fact]
    public void AllMessages_returns_messages()
    {
      Exception innerException = new Exception("inner");
      Exception exception = new Exception("outer", innerException);

      exception.AllMessages().Count().MustBe(2);
      exception.AllMessages().First().MustBe("outer");
      exception.AllMessages().Skip(1).First().MustBe("inner");
    }

    [Fact]
    public void Messages_returns_messages_with_separator()
    {
      Exception innerException = new Exception("inner");
      Exception exception = new Exception("outer", innerException);

      exception.Messages(",").MustBe("outer,inner");
    }
  }
}