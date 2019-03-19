using System;
using System.Collections;

namespace restlessmedia.Module.Extensions
{
  public static class ExceptionExtensions
  {
    /// <summary>
    /// Returns a message comprising of all the top level and inner exception messages (if they exist).
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static string Messages(this Exception exception, string separator = " / ")
    {
      ArrayList messages = new ArrayList();
      messages.Add(exception.Message);
      Exception inner = exception.InnerException;

      while (inner != null)
      {
        messages.Add(inner.Message);
        inner = inner.InnerException;
      }

      return string.Join(separator, messages.ToArray());
    }
  }
}