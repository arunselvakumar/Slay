using System;
using System.Collections.Generic;

namespace Slay.Utilities.Extensions.Iterators
{
	public class ForEachActionIterator<T> : ForEachIterator<T>
	{
		public Action<T> Action { get; }

		public ForEachActionIterator(IEnumerable<T> source, Action<T> action) :
			base(source)
		{
			this.Action = action;
		}

		public override void ForEachIndex()
		{
			this.Current = this.Enumerator.Current;
			this.Action(this.Current);
		}
	}
}