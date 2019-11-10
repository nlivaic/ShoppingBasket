using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.SharedKernel
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Returns `second` if all elements of `second` can be found in `first` list.
        /// Returns empty list if at least one element of `second` is not found in `first` list.
        /// </summary>
        public static IEnumerable<T> StrictIntersect<T>(this IEnumerable<T> first, IEnumerable<T> second) where T : BaseEntity<Guid, T>
        {
            var groupedFirst = first.GroupBy(i => i.Id);
            var groupedSecond = second.GroupBy(d => d.Id);
            var isStrictIntersect = groupedSecond.All(gd => groupedFirst.SingleOrDefault(gi => gi.Key == gd.Key)?.Count() >= gd.Count());
            return isStrictIntersect ? second : new List<T>();
        }
    }
}