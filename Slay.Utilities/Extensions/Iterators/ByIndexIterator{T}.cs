namespace Slay.Utilities.Extensions.Iterators
{
    using System.Collections.Generic;

    public abstract class ByIndexIterator<T> : Iterator<T>
    {
        private int _currentIndex;

        protected ByIndexIterator(IEnumerable<T> source, int index)
        {
            this.Source = source;
            this.Index = index;
            this.Enumerator = this.Source.GetEnumerator();
        }

        public int Index { get; }

        public IEnumerator<T> Enumerator { get; set; }

        public override bool MoveNext()
        {
            var moved = this.Enumerator.MoveNext();
            if (moved)
            {
                if (this._currentIndex == this.Index)
                {
                    this.OnWantedIndexHit();
                }
                else
                {
                    this.OnWrongIndexHit();
                }

                this._currentIndex++;
                return true;
            }

            return false;
        }

        public abstract void OnWantedIndexHit();

        public abstract void OnWrongIndexHit();
    }
}