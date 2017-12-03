using System;
using System.Collections.Generic;
using System.Linq;
using Slay.Utilities.Extensions.Iterators;

namespace Slay.Utilities.Extensions
{
	public static class EnumerableExtensions
	{
		public static bool IsAny<T>(this IEnumerable<T> source)
		{
			return source != null && source.Any();
		}

		public static IEnumerable<TValue> ForEach<TValue>(this IEnumerable<TValue> source, Action<TValue> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			return new ForEachActionIterator<TValue>(source, action);
		}

		public static IEnumerable<TValue> ForEach<TValue>(this IEnumerable<TValue> source, Func<TValue, TValue> function)
		{
			if (function == null)
			{
				throw new ArgumentNullException(nameof(function));
			}

			return new ForEachFunctionIterator<TValue>(source, function);
		}
	}
}