using System.Collections.Generic;

namespace StackUnderflow.Common
{
    public static class DictionaryExtensions
    {
        public static void Test()
        {
            var x = new Dictionary<int, string>().TryGetValueWithDefault(123);
        }

        public static TValue TryGetValueWithDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? 
                                                              value : 
                                                                        default(TValue);
        }
    }
}


