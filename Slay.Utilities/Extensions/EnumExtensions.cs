using System;

namespace Slay.Utilities.Extensions
{
    public static class EnumExtensions
    {
        public static TEnum ToEnum<TEnum>(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }
    }
}