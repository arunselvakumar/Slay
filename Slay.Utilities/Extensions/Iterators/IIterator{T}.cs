namespace Slay.Utilities.Extensions.Iterators
{
    using System.Collections.Generic;

    public interface IIterator<T> : IEnumerable<T>, IEnumerator<T>
    {
        IEnumerable<T> Source { get; set; }
    }
}