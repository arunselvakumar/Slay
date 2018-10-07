namespace Slay.Dal.Repositories
{
    using Slay.DalContracts.Repositories;
    using Slay.Models.Entities;

    public sealed class UserRepository : RepositoryBase<UserIdentityEntity>, IUserRepository
    {
        public UserRepository() : base("Slay", "Users")
        {
            
        }
    }
}