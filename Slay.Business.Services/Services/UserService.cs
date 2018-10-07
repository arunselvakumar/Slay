namespace Slay.Business.Services.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    using Slay.Business.ServicesContracts.Services;
    using Slay.Models.BusinessObjects.User;
    using Slay.Utilities.ServiceResult;

    public sealed class UserService : IUserService
    {
        public Task<ServiceResult<UserIdentityBo>> GetFollowersList(string userId, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResult<UserIdentityBo>> GetFollowingList(string userId, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}