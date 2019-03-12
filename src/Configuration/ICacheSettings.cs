using System.Collections.Generic;

namespace restlessmedia.Module.Configuration
{
  public interface ICacheSettings
  {
    bool Ssl { get; }

    string AccessKey { get; }

    IEnumerable<EndPoint> EndPointCollection { get; }

    int Retry { get; }

    int Timeout { get; }
  }
}