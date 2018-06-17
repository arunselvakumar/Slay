namespace Slay.Identity.Services.Models
{
    using System;
    using System.Security.Claims;

    public class MongoUserClaim : IEquatable<MongoUserClaim>, IEquatable<Claim>
    {
        public MongoUserClaim(Claim claim)
        {
            this.ClaimType = claim.Type;
            this.ClaimValue = claim.Value;
        }

        public MongoUserClaim(string claimType, string claimValue)
        {
            this.ClaimType = claimType ?? throw new ArgumentNullException(nameof(claimType));
            this.ClaimValue = claimValue;
        }

        public string ClaimType { get; }

        public string ClaimValue { get; }

        public bool Equals(MongoUserClaim other)
        {
            return other.ClaimType.Equals(this.ClaimType) && other.ClaimValue.Equals(this.ClaimValue);
        }

        public bool Equals(Claim other)
        {
            return other.Type.Equals(this.ClaimType) && other.Value.Equals(this.ClaimValue);
        }
    }
}