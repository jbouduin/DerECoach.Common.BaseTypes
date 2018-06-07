using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DerECoach.Common.BaseTypes
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Perform the provided Item for each item in the IEnumerable
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        public static void ForEach<TItem>(this IEnumerable<TItem> enumerable, Action<TItem> action)
        {
            if (enumerable == null) return;
            foreach (var item in enumerable) action(item);
        }

        /// <summary>
        /// Call the provided function for each item in the IEnumerable and return a IList of function return values
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        public static IList<TResult> ForEach<TItem, TResult>(this IEnumerable<TItem> enumerable, Func<TItem, TResult> func)
        {
            var results = new List<TResult>();
            if (enumerable == null) return results;

            results.AddRange(enumerable.Select(func));
            return results;
        }

        /// <summary>
        /// Check if the provided IEnumerable is null or has no items
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns>false if the IEnumerable is null or has no entries</returns>
        public static bool IsNullOrEmpty<TItem>(this IEnumerable<TItem> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        /// <summary>
        /// Check if the provided IEnumerable of strings is null or has no or only null or whitespace items
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns>false if the IEnumerable is null, has no entries or all entries are null or whitespace</returns>
        public static bool IsNullOrWhiteSpace(this IEnumerable<string> enumerable)
        {
            if (enumerable == null)
                return true;
            var asList = enumerable.ToList();
            return !asList.Any() || asList.All(string.IsNullOrWhiteSpace);
        }

        /// <summary>
        /// Perform the provided action for each item in the IEnumerable
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        public static void ForEachObject(this IEnumerable enumerable, Action<object> action)
        {
            foreach (var item in enumerable) action(item);
        }
        
        /// <summary>
        /// Convert the IEnumerable of strings into a commaseparated string
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static string ToCommaSeparatedString(this IEnumerable<string> enumerable)
        {
            var asList = enumerable.ToList();
            return string.Join(", ", asList.Where(string.IsNullOrWhiteSpace));
        }

        /// <summary>
        /// Convert the IEnumerable of strings into a commaseparated string with all strings sorted alphabetically
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static string ToCommaSeparatedStringInAlphabeticalOrder(this IEnumerable<string> enumerable)
        {
            return ToCommaSeparatedString(enumerable.OrderBy(s => s));
        }

        /// <summary>
        /// Convert the IEnumerable to an observable collection
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static ObservableCollection<TItem> ToObservableCollection<TItem>(this IEnumerable<TItem> list)
        {
            return new ObservableCollection<TItem>(list);
        }

        /// <summary>
        /// Convert the IEnumerable to a hash set
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static HashSet<TItem> ToHashSet<TItem>(this IEnumerable<TItem> list)
        {
            return new HashSet<TItem>(list);
        }

        /// <summary>
        /// Convert the IEnumerable of items to a list of items converted by the conversion function
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="list"></param>
        /// <param name="conversionFunc"></param>
        /// <returns></returns>
        public static List<TNew> ConvertToList<TItem, TNew>(this IEnumerable<TItem> list, Func<TItem, TNew> conversionFunc)
        {
            return list != null ? list.Select(conversionFunc).ToList() : new List<TNew>();
        }

        /// <summary>
        /// Convert the the observable conversion using the selector function
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        public static void SortObservableCollection<TSource, TKey>(this ObservableCollection<TSource> source, Func<TSource, TKey> selector)
        {
            foreach (var item in source.ToArray())
            {
                var oldIndex = source.IndexOf(item);
                var list = source.OrderBy(selector).ToList();
                var newIndex = list.IndexOf(item);
                source.Move(oldIndex, newIndex);
            }
        }
        
    }
}