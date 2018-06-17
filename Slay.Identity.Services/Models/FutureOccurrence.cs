namespace Slay.Identity.Services.Models
{
    using System;

    public sealed class FutureOccurrence : Occurrence
    {
        public FutureOccurrence()
        {
        }

        public FutureOccurrence(DateTime willOccurOn)
            : base(willOccurOn)
        {
        }
    }
}