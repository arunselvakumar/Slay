namespace Slay.Utilities.Extensions.Iterators
{
    using System;
    using System.Collections.Generic;

    public class ForEachActionIterator<T> : ForEachIterator<T>
    {
        public ForEachActionIterator(IEnumerable<T> source, Action<T> action)
            : base(source)
        {
            this.Action = action;
        }

        public Action<T> Action { get; }

        public override void ForEachIndex()
        {
            this.Current = this.Enumerator.Current;
            this.Action(this.Current);
        }
    }
}