#pragma warning disable 1998
namespace Slay.Business.ServicesContracts.Services
{
    using System.Collections.Generic;
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
        Task<ServiceResult<IEnumerable<UserIdentityBo>>> GetFollowersList(string userId, CancellationToken token);

        Task<ServiceResult<IEnumerable<UserIdentityBo>>> GetFollowingList(string userId, CancellationToken token);

        Task<ServiceResult<object>> FollowUser([NotNull] string userId, CancellationToken token);

        Task<ServiceResult<object>> UnfollowUser([NotNull] string userId, CancellationToken token);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/code-contracts")]
    [ContractClassFor(typeof(IUserService))]
    internal abstract class IUserServiceContract : IUserService
    {
        public async Task<ServiceResult<IEnumerable<UserIdentityBo>>> GetFollowersList(string userId, CancellationToken token)
        {
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<ServiceResult<IEnumerable<UserIdentityBo>>>().IsNotNull());

            return default(ServiceResult<IEnumerable<UserIdentityBo>>);
        }

        public async Task<ServiceResult<IEnumerable<UserIdentityBo>>> GetFollowingList(string userId, CancellationToken token)
        {
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<ServiceResult<IEnumerable<UserIdentityBo>>>().IsNotNull());

            return default(ServiceResult<IEnumerable<UserIdentityBo>>);
        }

        public async Task<ServiceResult<object>> FollowUser(string userId, CancellationToken token)
        {
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<ServiceResult<UserIdentityBo>>().IsNotNull());

            return default(ServiceResult<object>);
        }

        public async Task<ServiceResult<object>> UnfollowUser(string userId, CancellationToken token)
        {
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<ServiceResult<UserIdentityBo>>().IsNotNull());

            return default(ServiceResult<object>);
        }
    }
}