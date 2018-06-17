namespace Slay.Identity.Services.Models
{
    using System;

    using Microsoft.AspNetCore.Identity;

    public class MongoUserLogin : IEquatable<MongoUserLogin>, IEquatable<UserLoginInfo>
    {
        public MongoUserLogin(UserLoginInfo loginInfo)
        {
            this.LoginProvider = loginInfo.LoginProvider;
            this.ProviderKey = loginInfo.ProviderKey;
        }

        public string LoginProvider { get; }

        public string ProviderKey { get; }

        public bool Equals(MongoUserLogin other)
        {
            return other.LoginProvider.Equals(this.LoginProvider) && other.ProviderKey.Equals(this.ProviderKey);
        }

        public bool Equals(UserLoginInfo other)
        {
            return other.LoginProvider.Equals(this.LoginProvider) && other.ProviderKey.Equals(this.ProviderKey);
        }
    }
}