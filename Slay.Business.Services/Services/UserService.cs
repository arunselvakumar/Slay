namespace Slay.Business.Services.Services
{
    using System.Threading.Tasks;

    using Slay.Business.ServicesContracts.Services;
    using Slay.Models.BusinessObjects.User;
    using Slay.Utilities.ServiceResult;

    public sealed class UserService : IUserService
    {
        public Task<ServiceResult<UserBo>> GetFollowersList(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResult<UserBo>> GetFollowingList(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}