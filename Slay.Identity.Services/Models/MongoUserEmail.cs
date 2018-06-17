namespace Slay.Identity.Services.Models
{
    using System;

    public class MongoUserEmail : MongoUserContactRecord
    {
        public MongoUserEmail(string email)
            : base(email)
        {
        }

        public string NormalizedValue { get; private set; }

        public virtual void SetNormalizedEmail(string normalizedEmail)
        {
            this.NormalizedValue = normalizedEmail ?? throw new ArgumentNullException(nameof(normalizedEmail));
        }
    }
}