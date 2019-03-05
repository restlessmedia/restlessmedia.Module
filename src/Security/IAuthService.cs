namespace restlessmedia.Module.Security
{
  public interface IAuthService
  {
    Auth Read(AuthServiceType type, string username);

    int Save(Auth auth);
  }
}