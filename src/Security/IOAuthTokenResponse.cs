namespace restlessmedia.Module.Security
{
  public interface IOAuthTokenResponse
  {
    string Token { get; }
    
    string TokenSecret { get; }
    
    decimal UserId { get; }
    
    string VerificationString { get; }
  }
}
