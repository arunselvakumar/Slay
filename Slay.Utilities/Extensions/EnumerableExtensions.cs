namespace Slay.Utilities.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Slay.Utilities.Extensions.Iterators;

    public static class EnumerableExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            return !@this.Any();
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            return @this.Any();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> @this)
        {
            return @this == null || !@this.Any();
        }

        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> @this)
        {
            return @this != null && @this.Any();
        }

        public static string StringJoin<T>(this IEnumerable<T> @this, string separator)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            return string.Join(separator, @this);
        }

        public static string StringJoin<T>(this IEnumerable<T> @this, char separator)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            return string.Join(separator.ToString(), @this);
        }

        public static IEnumerable<TValue> ForEach<TValue>(this IEnumerable<TValue> @this, Action<TValue> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return new ForEachActionIterator<TValue>(@this, action);
        }

        public static IEnumerable<TValue> ForEach<TValue>(this IEnumerable<TValue> @this, Func<TValue, TValue> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return new ForEachFunctionIterator<TValue>(@this, function);
        }
    }
}