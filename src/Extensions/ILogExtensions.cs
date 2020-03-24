using System;
using System.Runtime.CompilerServices;

namespace restlessmedia.Module
{
  public static class ILogExtensions
  {
    public static void Exception(this ILog log, Exception exception, [CallerMemberName] string caller = null)
    {
      log.Error(string.Concat(caller, Environment.NewLine, exception.ToString()));
    }
  }
}