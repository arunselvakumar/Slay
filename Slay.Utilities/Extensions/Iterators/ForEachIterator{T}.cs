namespace Slay.Utilities.Extensions.Iterators
{
    using System;
    using System.Collections.Generic;

    public abstract class ForEachIterator<T> : ByIndexIterator<T>
    {
        protected ForEachIterator(IEnumerable<T> source)
            : base(source, -1)
        {
        }

        public override void OnWantedIndexHit()
        {
            throw new NotImplementedException();
        }

        public override void OnWrongIndexHit()
        {
            this.ForEachIndex();
        }

        public abstract void ForEachIndex();
    }
}