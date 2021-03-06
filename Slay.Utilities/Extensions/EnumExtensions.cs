﻿namespace Slay.Utilities.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumExtensions
    {
        /// <summary>
        /// Converts String to Enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="this">The @this.</param>
        /// <returns>TEnum</returns>
        /// <exception cref="ArgumentNullException">@this</exception>
        public static TEnum ToEnum<TEnum>(this string @this)where TEnum : struct
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            return Enum.TryParse<TEnum>(@this, true, out var result) ? result : default(TEnum);
        }

        /// <summary>
        /// Converts Enumerable of Strings to Enumerable of Enums.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public static IEnumerable<T> ToEnums<T>(this IEnumerable<string> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(x => (T)Enum.Parse(typeof(T), x));
        }
    }
}