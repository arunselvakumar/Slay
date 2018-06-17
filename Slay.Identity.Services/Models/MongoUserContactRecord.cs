namespace Slay.Identity.Services.Models
{
    using System;

    public abstract class MongoUserContactRecord : IEquatable<MongoUserEmail>
    {
        protected MongoUserContactRecord(string value)
        {
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Value { get; }

        public ConfirmationOccurrence ConfirmationRecord { get; private set; }

        public bool Equals(MongoUserEmail other)
        {
            return other.Value.Equals(this.Value);
        }

        public bool IsConfirmed()
        {
            return this.ConfirmationRecord != null;
        }

        public void SetConfirmed()
        {
            this.SetConfirmed(new ConfirmationOccurrence());
        }

        public void SetConfirmed(ConfirmationOccurrence confirmationRecord)
        {
            if (this.ConfirmationRecord == null)
            {
                this.ConfirmationRecord = confirmationRecord;
            }
        }

        public void SetUnconfirmed()
        {
            this.ConfirmationRecord = null;
        }
    }
}