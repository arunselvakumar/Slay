﻿namespace Slay.Utilities.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object @this)
        {
            return @this == null;
        }

        public static bool IsNotNull(this object @this)
        {
            return @this != null;
        }
    }
}