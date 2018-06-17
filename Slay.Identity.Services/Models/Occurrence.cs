namespace Slay.Identity.Services.Models
{
    using System;

    public class Occurrence
    {
        public Occurrence()
            : this(DateTime.UtcNow)
        {
        }

        public Occurrence(DateTime occuranceInstantUtc)
        {
            this.Instant = occuranceInstantUtc;
        }

        public DateTime Instant { get; }
    }
}