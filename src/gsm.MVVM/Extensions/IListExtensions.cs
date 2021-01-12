using System;
using System.Linq;
using System.Collections.Generic;

namespace gsm.MVVM.Extensions
{
    public static class IListExtensions
    {
        public static void RemoveWhere<T>(this IList<T> source, Func<T, bool> predicate)
        {
            var candidates = source.Where(predicate).ToList();

            foreach (var candidate in candidates)
            {
                source.Remove(candidate);
            }
        }
    }
}
