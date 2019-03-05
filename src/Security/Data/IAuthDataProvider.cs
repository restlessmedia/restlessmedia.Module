using restlessmedia.Module.Data;

namespace restlessmedia.Module.Security.Data
{
  public interface IAuthDataProvider : IDataProvider
  {
    Auth Read(AuthServiceType type, string username);

    int Save(Auth auth);
  }
}