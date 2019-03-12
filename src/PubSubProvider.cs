using System;
using System.Collections.Concurrent;

namespace restlessmedia.Module
{
  public class PubSubProvider : IPubSubProvider
  {
    static PubSubProvider()
    {
      _store = new ConcurrentDictionary<string, Action<object>>();
    }

    public PubSubProvider()
      : base() { }

    public void Publish(string channel, string argument = null)
    {
      Action<object> handler;

      if (_store.TryGetValue(channel, out handler))
      {
        handler(argument);
      }
    }

    public void Unsubscribe(string channel)
    {
      Action<object> handler;
      _store.TryRemove(channel, out handler);
    }

    public void Subscribe(string channel, Action<object> handler)
    {
      _store.AddOrUpdate(channel, handler, (c, h) => handler);
    }

    private static ConcurrentDictionary<string, Action<object>> _store;
  }
}