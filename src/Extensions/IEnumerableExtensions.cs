using System.Linq;

namespace System.Collections.Generic
{
  public static class IEnumerableExtensions
  {
    public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
    {
      foreach (T item in enumeration)
      {
        action(item);
      }
    }

    public static int IndexOf<T>(this IEnumerable<T> obj, T value)
    {
      return obj.IndexOf(value, null);
    }

    public static int IndexOf<T>(this IEnumerable<T> obj, T value, IEqualityComparer<T> comparer)
    {
      if (comparer == null)
      {
        throw new ArgumentNullException(nameof(comparer));
      }

      comparer ??= EqualityComparer<T>.Default;
      var found = obj.Select((a, i) => new { a, i }).FirstOrDefault(x => comparer.Equals(x.a, value));
      return found == null ? -1 : found.i;
    }

    /// <summary>
    /// Very useful helper extension for batching rows of data.  This will provide an enumerable, enumerable list of a certain size to loop with - helpful when building rows of data with a specific size.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="batchSize"></param>
    /// <returns></returns>
    public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> obj, int batchSize)
    {
      return obj.Select((x, i) => new { Val = x, Idx = i }).GroupBy(x => x.Idx / batchSize, (k, g) => g.Select(x => x.Val));
    }

    /// <summary>
    /// Adds an item to the collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static IEnumerable<T> Add<T>(this IEnumerable<T> obj, T value)
    {
      foreach (var cur in obj)
      {
        yield return cur;
      }

      yield return value;
    }

    /// <summary>
    /// Returns true if any of the string values in collection start with value.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static bool StartsWithAny(this string value, IEnumerable<string> collection)
    {
      if (collection == null)
      {
        throw new ArgumentNullException(nameof(collection));
      }

      return collection.Any(x => x.StartsWith(value));
    }

    /// <summary>
    /// Merges the contents of the target dictionary with the source dictionary.  Any keys found in the source will be overwritten by values in the target.  If an item is not found in the source, it's added to the source dictionary.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> target)
    {
      if (target == null)
      {
        throw new ArgumentNullException(nameof(target));
      }

      foreach (KeyValuePair<TKey, TValue> item in target)
      {
        if (source.ContainsKey(item.Key))
        {
          source[item.Key] = item.Value;
        }
        else
        {
          source.Add(item.Key, item.Value);
        }
      }

      return source;
    }

    /// <summary>
    /// Unions one item with another collecition of the same type
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, TSource second)
    {
      if (second == null)
      {
        throw new ArgumentNullException(nameof(second));
      }

      return first.Union(new[] { second });
    }

    /// <summary>
    /// Adds another item to the first array returning the array with the added item
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <returns></returns>
    public static TSource[] Push<TSource>(this TSource[] first, TSource second)
    {
      if (second == null)
      {
        throw new ArgumentNullException(nameof(second));
      }

      return first.Union(new[] { second }).ToArray();
    }

    /// <summary>
    /// Wraps this object instance into an IEnumerable&lt;T&gt;
    /// consisting of a single item.
    /// </summary>
    /// <typeparam name="T">Type of the wrapped object.</typeparam>
    /// <param name="item">The object to wrap.</param>
    /// <returns>
    /// An IEnumerable&lt;T&gt; consisting of a single item.
    /// </returns>
    public static IEnumerable<T> Yield<T>(this T item)
    {
      yield return item;
    }

    /// <summary>
    /// Returns a random item from the collection, or null if the collection is empty.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static T Random<T>(this IEnumerable<T> collection)
    {
      if (collection == null)
      {
        return default(T);
      }

      IList<T> list = collection.ToList();

      switch (list.Count)
      {
        case 1:
          {
            return list.First();
          }
        case 0:
          {
            return default(T);
          }
        default:
          {
            int randomIndex = new Random().Next(list.Count);
            return list[randomIndex];
          }
      }
    }

    public static IEnumerable<int> Range(int from, int to, int step = 1)
    {
      for (int i = from; i <= to; i += step)
      {
        yield return i;
      }
    }
  }
}