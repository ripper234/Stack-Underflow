using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
