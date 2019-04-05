using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace restlessmedia.Module.UnitTest.Extensions
{
  [TestClass]
  public class EnumerationExtensionsTests
  {
    [TestMethod]
    public void SetFlag()
    {
      Test test = Test.None;
      test.SetFlag(Test.One);

      test.SetFlag(Test.Two);
    }

    [Flags]
    private enum Test
    {
      None = 0,
      One = 1,
      Two = 2,
      Four = 4,
      All = 7,
    }
  }
}
