namespace Slay.Business.ServicesContracts.Services
{
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Slay.Models.BusinessObjects.User;
    using Slay.Utilities.Extensions;
    using Slay.Utilities.ServiceResult;

    [ContractClass(typeof(IUserServiceContract))]
    public interface IUserService
    {
        Task<ServiceResult<UserIdentityBo>> GetFollowersList([NotNull] string userId, CancellationToken token);

        Task<ServiceResult<UserIdentityBo>> GetFollowingList([NotNull] string userId, CancellationToken token);

        Task<ServiceResult<object>> FollowUser([NotNull] string userId, CancellationToken token);

        Task<ServiceResult<object>> UnfollowUser([NotNull] string userId, CancellationToken token);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/code-contracts")]
    [ContractClassFor(typeof(IUserService))]
    internal abstract class IUserServiceContract : IUserService
    {
        public Task<ServiceResult<UserIdentityBo>> GetFollowersList(string userId, CancellationToken token)
        {
            Contract.Requires(userId.IsNotNullOrEmpty());
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<Task<ServiceResult<UserIdentityBo>>>().IsNotNull());

            return default(Task<ServiceResult<UserIdentityBo>>);
        }

        public Task<ServiceResult<UserIdentityBo>> GetFollowingList(string userId, CancellationToken token)
        {
            Contract.Requires(userId.IsNotNullOrEmpty());
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<Task<ServiceResult<UserIdentityBo>>>().IsNotNull());

            return default(Task<ServiceResult<UserIdentityBo>>);
        }

        public Task<ServiceResult<object>> FollowUser(string userId, CancellationToken token)
        {
            Contract.Requires(userId.IsNotNullOrEmpty());
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<Task<ServiceResult<UserIdentityBo>>>().IsNotNull());

            return default(Task<ServiceResult<object>>);
        }

        public Task<ServiceResult<object>> UnfollowUser(string userId, CancellationToken token)
        {
            Contract.Requires(userId.IsNotNullOrEmpty());
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<Task<ServiceResult<UserIdentityBo>>>().IsNotNull());

            return default(Task<ServiceResult<object>>);
        }
    }
}