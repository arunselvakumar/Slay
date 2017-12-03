using System.Collections.Generic;

namespace Slay.Utilities.Extensions.Iterators
{
	public abstract class ByIndexIterator<T> : Iterator<T>
	{
		public int Index { get; }
		
		public IEnumerator<T> Enumerator { get; set; }

		private int currentIndex = 0;

		protected ByIndexIterator(IEnumerable<T> source, int index)
		{
			this.Source = source;
			this.Index = index;
			this.Enumerator = this.Source.GetEnumerator();
		}

		public override bool MoveNext()
		{
			var moved = this.Enumerator.MoveNext();
			if (moved)
			{
				if (this.currentIndex == this.Index)
				{
					OnWantedIndexHit();
				}
				else
				{
					OnWrongIndexHit();
				}
				this.currentIndex++;
				return true;
			}
			return false;
		}

		public abstract void OnWantedIndexHit();

		public abstract void OnWrongIndexHit();
	}
}