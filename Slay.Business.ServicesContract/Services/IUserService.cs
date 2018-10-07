namespace Slay.Business.ServicesContracts.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Slay.Models.BusinessObjects.User;
    using Slay.Utilities.ServiceResult;

    public interface IUserService
    {
        Task<ServiceResult<UserIdentityBo>> GetFollowersList([NotNull] string userId, CancellationToken token);

        Task<ServiceResult<UserIdentityBo>> GetFollowingList([NotNull] string userId, CancellationToken token);
    }
}