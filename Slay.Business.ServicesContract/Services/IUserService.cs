namespace Slay.Business.ServicesContracts.Services
{
    using System.Threading.Tasks;

    using Slay.Models.BusinessObjects.User;
    using Slay.Utilities.ServiceResult;

    public interface IUserService
    {
        Task<ServiceResult<UserBo>> GetFollowersList(string userId);

        Task<ServiceResult<UserBo>> GetFollowingList(string userId);
    }
}