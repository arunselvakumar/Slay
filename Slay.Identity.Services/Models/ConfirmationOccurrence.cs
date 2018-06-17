namespace Slay.Identity.Services.Models
{
    using System;

    public sealed class ConfirmationOccurrence : Occurrence
    {
        public ConfirmationOccurrence()
        {
        }

        public ConfirmationOccurrence(DateTime confirmedOn)
            : base(confirmedOn)
        {
        }
    }
}