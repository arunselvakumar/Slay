using System.Collections;
using System.Collections.Generic;

namespace Slay.Utilities.Extensions.Iterators
{
	public abstract class Iterator<T> : IIterator<T>
	{
		public IEnumerable<T> Source { get; set; }

		public T Current { get; set; }

		object IEnumerator.Current { get; }

		public virtual IEnumerator<T> GetEnumerator()
		{
			return this;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		public virtual void Reset()
		{
		}

		public virtual void Dispose()
		{
			this.Current = default(T);
		}

		public abstract bool MoveNext();
	}
}