using System;

namespace restlessmedia.Module
{
  public class LicenseException : Exception
  {
    public LicenseException(string message)
      : base(message)
    { }
  }
}