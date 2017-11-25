using System.Collections.Generic;
using System.Linq;

namespace Slay.Utilities.Extensions
{
	public static class EnumerableExtensions
	{
		public static bool IsAny<T>(this IEnumerable<T> source)
		{
			return source != null && source.Any();
		}
	}
}