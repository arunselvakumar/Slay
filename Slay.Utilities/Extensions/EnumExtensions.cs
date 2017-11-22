using System;
using System.Collections.Generic;
using System.Linq;

namespace Slay.Utilities.Extensions
{
    public static class EnumExtensions
    {
        public static TEnum ToEnum<TEnum>(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }

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
