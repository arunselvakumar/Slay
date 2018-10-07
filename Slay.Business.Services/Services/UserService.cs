namespace Slay.Business.Services.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using Slay.Business.ServicesContracts.Services;
    using Slay.DalContracts.Repositories;
    using Slay.Models.BusinessObjects.User;
    using Slay.Models.Entities;
    using Slay.Utilities.Extensions;
    using Slay.Utilities.ServiceResult;

    public sealed class UserService : IUserService
    {
        private readonly IMapper _autoMapperService;

        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="autoMapperService">The automatic mapper service.</param>
        /// <param name="userRepository">The user repository.</param>
        public UserService(IMapper autoMapperService, IUserRepository userRepository)
        {
            this._autoMapperService = autoMapperService;
            this._userRepository = userRepository;
        }

        public async Task<ServiceResult<IEnumerable<UserIdentityBo>>> GetFollowersList(string userId, CancellationToken token)
        {
            if (userId.IsNullOrEmpty())
            {
                return new ServiceResult<IEnumerable<UserIdentityBo>> { Errors = new[] { new Error { Code = "USER_USERID_MANDATORY_ERROR" } } };
            }

            var repositoryResult = await this._userRepository.GetByIdAsync(userId, token);

            var followersUserIds = repositoryResult.Followers;

            var usersList = new List<UserIdentityEntity>();

            foreach (var followerUserId in followersUserIds)
            {
                usersList.Add(await this._userRepository.GetByIdAsync(followerUserId, token));
            }

            var mapperResult = usersList.Select(x => this._autoMapperService.Map<UserIdentityBo>(x));

            return new ServiceResult<IEnumerable<UserIdentityBo>> { Value = mapperResult };
        }

        public async Task<ServiceResult<IEnumerable<UserIdentityBo>>> GetFollowingList(string userId, CancellationToken token)
        {
            if (userId.IsNullOrEmpty())
            {
                return new ServiceResult<IEnumerable<UserIdentityBo>> { Errors = new[] { new Error { Code = "USER_USERID_MANDATORY_ERROR" } } };
            }

            var repositoryResult = await this._userRepository.GetByIdAsync(userId, token);

            var followingUserIds = repositoryResult.Following;

            var usersList = new List<UserIdentityEntity>();

            foreach (var followerUserId in followingUserIds)
            {
                usersList.Add(await this._userRepository.GetByIdAsync(followerUserId, token));
            }

            var mapperResult = usersList.Select(x => this._autoMapperService.Map<UserIdentityBo>(x));

            return new ServiceResult<IEnumerable<UserIdentityBo>> { Value = mapperResult };
        }

        public async Task<ServiceResult<object>> FollowUser(string userId, CancellationToken token)
        {
            if (userId.IsNullOrEmpty())
            {
                return new ServiceResult<object> { Errors = new[] { new Error { Code = "USER_USERID_MANDATORY_ERROR" } } };
            }

            var repositoryResult = await this._userRepository.GetByIdAsync(userId, token);

            repositoryResult.Following = repositoryResult.Following.Append(userId);

            await this._userRepository.UpdateAsync("user_from_userContext", repositoryResult, token);

            // ToDo: Update 'user_from_userContext' List As Well

            return new ServiceResult<object>();
        }

        public async Task<ServiceResult<object>> UnfollowUser(string userId, CancellationToken token)
        {
            if (userId.IsNullOrEmpty())
            {
                return new ServiceResult<object> { Errors = new[] { new Error { Code = "USER_USERID_MANDATORY_ERROR" } } };
            }

            var repositoryResult = await this._userRepository.GetByIdAsync(userId, token);

            repositoryResult.Following = repositoryResult.Following.Where(x => x != userId);

            await this._userRepository.UpdateAsync("user_from_userContext", repositoryResult, token);

            // ToDo: Update 'user_from_userContext' List As Well

            return new ServiceResult<object>();
        }
    }
}