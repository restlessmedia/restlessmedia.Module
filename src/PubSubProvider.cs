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
      if (_store.TryGetValue(channel, out Action<object> handler))
      {
        handler(argument);
      }
    }

    public void Unsubscribe(string channel)
    {
      _store.TryRemove(channel, out Action<object> handler);
    }

    public void Subscribe(string channel, Action<object> handler)
    {
      _store.AddOrUpdate(channel, handler, (c, h) => handler);
    }

    private static readonly ConcurrentDictionary<string, Action<object>> _store;
  }
}