namespace Slay.Business.ServicesContracts.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Slay.Models.BusinessObjects.User;
    using Slay.Utilities.ServiceResult;

    public interface IUserService
    {
        Task<ServiceResult<UserBo>> GetFollowersList([NotNull] string userId, CancellationToken token);

        Task<ServiceResult<UserBo>> GetFollowingList([NotNull] string userId, CancellationToken token);
    }
}