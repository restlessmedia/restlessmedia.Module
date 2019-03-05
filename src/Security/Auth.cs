namespace restlessmedia.Module.Security
{
  public class Auth : IOAuthTokens
  {
    #region Constructors

    public Auth() { }

    public Auth(AuthServiceType type, IOAuthTokenResponse tokens, string username, string consumerKey, string consumerSecret)
    {
      Type = type;
      Username = username;
      ConsumerKey = consumerKey;
      ConsumerSecret = consumerSecret;
      AccessToken = tokens.Token;
      AccessTokenSecret = tokens.TokenSecret;
    }

    public int? AuthId { get; set; }

    public string ConsumerKey { get; private set; }

    public string ConsumerSecret { get; private set; }

    public string AccessToken { get; private set; }

    public string AccessTokenSecret { get; private set; }

    /// <summary>
    /// In the context of twitter, the username of the account.  This is used to get the auth account without knowing the id and must be unique to the system.
    /// </summary>
    public string Username { get; set; }

    public AuthServiceType Type { get; set; }

    #endregion
  }
}