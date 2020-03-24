using restlessmedia.Module.Configuration;
using System;
using System.Diagnostics;

namespace restlessmedia.Module
{
  /// <summary>
  /// Implementation of <see cref="ILog"/> based on <see cref="System.Diagnostics.Trace"/>.
  /// </summary>
  internal class TraceLog : ILog
  {
    public void Error(string message, string detail = null)
    {
      Trace.TraceError(string.Concat(message, Environment.NewLine, detail));
    }

    public void Info(string message, string detail = null)
    {
      Trace.TraceInformation(string.Concat(message, Environment.NewLine, detail));
    }

    public void Warning(string message, string detail = null)
    {
      Trace.TraceWarning(string.Concat(message, Environment.NewLine, detail));
    }
  }
}