using System.Collections.Generic;

namespace System
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
      return string.Join(separator, AllMessages(exception));
    }

    public static IEnumerable<string> AllMessages(this Exception exception)
    {
      Exception inner = exception;
      while (inner != null)
      {
        yield return inner.Message;
        inner = inner.InnerException;
      }
    }
  }
}