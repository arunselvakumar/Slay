namespace Slay.Utilities.Extensions.Iterators
{
    using System;
    using System.Collections.Generic;

    public class ForEachFunctionIterator<T> : ForEachIterator<T>
    {
        public ForEachFunctionIterator(IEnumerable<T> source, Func<T, T> function)
            : base(source)
        {
            this.Function = function;
        }

        public Func<T, T> Function { get; }

        public override void ForEachIndex()
        {
            this.Current = this.Function(this.Enumerator.Current);
        }
    }
}