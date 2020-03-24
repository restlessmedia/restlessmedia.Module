namespace restlessmedia.Module
{
  /// <summary>
  /// Logging abstraction
  /// </summary>
  public interface ILog
  {
    void Info(string message, string detail = null);

    void Warning(string message, string detail = null);

    void Error(string message, string detail = null);
  }
}