using System.Collections.Generic;

namespace Slay.Utilities.Extensions.Iterators
{
	public interface IIterator<T> : IEnumerable<T>, IEnumerator<T>
	{
		IEnumerable<T> Source { get; set; }
	}
}