namespace restlessmedia.Module.Security
{
  public interface IOAuthTokens
  {
    /// <summary>
    /// Gets the access token
    /// </summary>
    string AccessToken { get; }

    /// <summary>
    /// Gets the access token secret
    /// </summary>
    string AccessTokenSecret { get; }

    /// <summary>
    /// Gets the consumer key
    /// </summary>
    string ConsumerKey { get; }

    /// <summary>
    /// Gets the consumer secret
    /// </summary>
    string ConsumerSecret { get; }
  }
}