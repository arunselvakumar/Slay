using System;
using System.Collections.Generic;

namespace Slay.Utilities.Extensions.Iterators
{
	public class ForEachFunctionIterator<T> : ForEachIterator<T>
	{
		public Func<T, T> Function { get; }

		public ForEachFunctionIterator(IEnumerable<T> source, Func<T, T> function) :
			base(source)
		{
			this.Function = function;
		}

		public override void ForEachIndex()
		{
			this.Current = this.Function(this.Enumerator.Current);
		}
	}
}