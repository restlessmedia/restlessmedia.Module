using System;

namespace restlessmedia.Module
{
  public interface IPubSubProvider : IProvider
  {
    void Subscribe(string channel, Action<object> handler);

    void Unsubscribe(string channel);

    void Publish(string channel, string argument = null);
  }
}