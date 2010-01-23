using System;
using System.Collections.Generic;
using System.Linq;

namespace StackUnderflow.Common
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Returns a random element from the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static T Random<T>(this IList<T> list, Random random)
        {
            return list.Skip(random.Next(list.Count)).First();
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            TValue value;
            return dict.TryGetValue(key, out value) ? value : default(TValue);
        }

        public static TValue? GetOrNull<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
                        where TValue : struct
        {
            if (dict == null)
                return null;

            TValue value;
            if (dict.TryGetValue(key, out value))
                return value;

            return null;
        }
    }
}
