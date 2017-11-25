using System;
using System.Collections.Generic;
using System.Linq;

namespace Slay.Utilities.Extensions
{
	public static class EnumExtensions
    {
		/// <summary>
		/// Converts String to Enum.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">value</exception>
		public static TEnum ToEnum<TEnum>(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (TEnum)Enum.Parse(typeof(TEnum), value);
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

            return source.Select(x => (T) Enum.Parse(typeof(T), x));
        }
    }
}