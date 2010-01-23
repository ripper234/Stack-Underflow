#region

using System.Collections.Generic;

#endregion

namespace StackUnderflow.Common
{
    public static class DictionaryExtensions
    {
        public static TValue TryGetValueWithDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary == null)
                return default(TValue);

            TValue value;
            return dictionary.TryGetValue(key, out value) ? value :default(TValue);
        }
    }
}