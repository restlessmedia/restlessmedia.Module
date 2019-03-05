using restlessmedia.Module.Security.Data;
using System;

namespace restlessmedia.Module.Security
{
  public class AuthService : IAuthService
  {
    public AuthService(IAuthDataProvider authDataProvider)
    {
      _authDataProvider = authDataProvider ?? throw new ArgumentNullException(nameof(authDataProvider));
    }

    public Auth Read(AuthServiceType type, string username)
    {
      return _authDataProvider.Read(type, username);
    }

    public int Save(Auth auth)
    {
      if (auth == null)
      {
        throw new ArgumentNullException(nameof(auth));
      }

      return _authDataProvider.Save(auth);
    }

    private readonly IAuthDataProvider _authDataProvider;
  }
}