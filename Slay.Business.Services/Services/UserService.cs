namespace Slay.Business.Services.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    using Slay.Business.ServicesContracts.Services;
    using Slay.Models.BusinessObjects.User;
    using Slay.Utilities.ServiceResult;

    public sealed class UserService : IUserService
    {
        public async Task<ServiceResult<UserIdentityBo>> GetFollowersList(string userId, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResult<UserIdentityBo>> GetFollowingList(string userId, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResult<object>> FollowUser(string userId, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResult<object>> UnfollowUser(string userId, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}