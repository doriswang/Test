using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Test.Data;
using Test.Framework.Extensibility;
using Test.Identity.Model;
using Test.Identity.Services;

namespace Test.Identity.Stores
{
    public class UserStore<TUser> : IUserLoginStore<TUser>,
        IUserClaimStore<TUser>,
        IUserRoleStore<TUser>,
        IUserPasswordStore<TUser>,
        IUserSecurityStampStore<TUser>,
        IQueryableUserStore<TUser>,
        IUserEmailStore<TUser>,
        IUserPhoneNumberStore<TUser>,
        IUserTwoFactorStore<TUser, string>,
        IUserLockoutStore<TUser, string>,
        IUserStore<TUser>
        where TUser : IdentityUser
    {
        private IDataProvider dataProvider;
        private IUserService<TUser> userService;
        private IRoleService roleService;
        private IUserRolesService userRolesService;
        private IUserClaimsService userClaimsService;
        private IUserLoginsService userLoginsService;

        public IQueryable<TUser> Users
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        /// <summary>
        /// Default constructor that initializes a new MySQLDatabase
        /// instance using the Default Connection string
        /// </summary>
        public UserStore()
        {
            new UserStore<TUser>(Container.Resolve<IDataProvider>());
        }

        /// <summary>
        /// Constructor that takes a MySQLDatabase as argument 
        /// </summary>
        /// <param name="database"></param>
        public UserStore(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
            userService = Container.Resolve<IUserService<TUser>>();
            roleService = Container.Resolve<IRoleService>();
            userRolesService = Container.Resolve<IUserRolesService>();
            userClaimsService = Container.Resolve<IUserClaimsService>();
            userLoginsService = Container.Resolve<IUserLoginsService>();
        }

        /// <summary>
        /// Insert a new TUser in the UserTable
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task CreateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var result = await userService.InsertAsync(user);
        }

        /// <summary>
        /// Returns an TUser instance based on a userId query 
        /// </summary>
        /// <param name="userId">The user's Id</param>
        /// <returns></returns>
        public async Task<TUser> FindByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Null or empty argument: userId");
            }

            var tempResult = await userService.GetUserByIdAsync(userId);
            TUser result = tempResult as TUser;
            if (result != null)
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Returns an TUser instance based on a userName query 
        /// </summary>
        /// <param name="userName">The user's name</param>
        /// <returns></returns>
        public async Task<TUser> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Null or empty argument: userName");
            }

            var tempResult = await userService.GetUserByNameAsync(userName);
            List<TUser> result = tempResult as List<TUser>;

            // Should I throw if > 1 user?
            if (result != null && result.Count == 1)
            {
                return result.FirstOrDefault();
            }

            return null;
        }

        /// <summary>
        /// Updates the UsersTable with the TUser instance values
        /// </summary>
        /// <param name="user">TUser to be updated</param>
        /// <returns></returns>
        public async Task UpdateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var result = await userService.UpdateAsync(user);
        }

        public void Dispose()
        {
        }

        /// <summary>
        /// Inserts a claim to the UserClaimsTable for the given user
        /// </summary>
        /// <param name="user">User to have claim added</param>
        /// <param name="claim">Claim to be added</param>
        /// <returns></returns>
        public async Task AddClaimAsync(TUser user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("user");
            }

            var result = await userClaimsService.InsertAsync(claim, user.Id);
        }

        /// <summary>
        /// Returns all claims for a given user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            ClaimsIdentity identity = await userClaimsService.FindByUserIdAsync(user.Id);

            if (identity == null ||
                identity.Claims == null ||
                !identity.Claims.Any())
                return null;

            return identity.Claims.ToList();
        }

        /// <summary>
        /// Removes a claim froma user
        /// </summary>
        /// <param name="user">User to have claim removed</param>
        /// <param name="claim">Claim to be removed</param>
        /// <returns></returns>
        public async Task RemoveClaimAsync(TUser user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            var result = await userClaimsService.DeleteAsync(user, claim);
        }

        /// <summary>
        /// Inserts a Login in the UserLoginsTable for a given User
        /// </summary>
        /// <param name="user">User to have login added</param>
        /// <param name="login">Login to be added</param>
        /// <returns></returns>
        public async Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            var result = await userLoginsService.InsertAsync(user, login);
        }

        /// <summary>
        /// Returns an TUser based on the Login info
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<TUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            var userId = await userLoginsService.FindUserIdByLoginAsync(login);
            if (userId != null)
            {
                var tempResult = await userService.GetUserByIdAsync(userId);
                TUser user = tempResult as TUser;
                if (user != null)
                {
                    return user;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns list of UserLoginInfo for a given TUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            List<UserLoginInfo> userLogins = new List<UserLoginInfo>();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            List<UserLoginInfo> logins = await userLoginsService.FindByUserIdAsync(user.Id);
            if (logins != null)
            {
                return logins;
            }

            return null;
        }

        /// <summary>
        /// Deletes a login from UserLoginsTable for a given TUser
        /// </summary>
        /// <param name="user">User to have login removed</param>
        /// <param name="login">Login to be removed</param>
        /// <returns></returns>
        public async Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            var result = await userLoginsService.DeleteAsync(user, login);
        }

        /// <summary>
        /// Inserts a entry in the UserRoles table
        /// </summary>
        /// <param name="user">User to have role added</param>
        /// <param name="roleName">Name of the role to be added to user</param>
        /// <returns></returns>
        public async Task AddToRoleAsync(TUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            }

            string roleId = await roleService.GetRoleIdAsync(roleName);
            if (!string.IsNullOrEmpty(roleId))
            {
                var result = await userRolesService.InsertAsync(user, roleId);
            }
        }

        /// <summary>
        /// Returns the roles for a given TUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetRolesAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            List<string> roles = await userRolesService.FindByUserIdAsync(user.Id);
            {
                if (roles != null)
                {
                    return roles;
                }
            }

            return null;
        }

        /// <summary>
        /// Verifies if a user is in a role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> IsInRoleAsync(TUser user, string role)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentNullException("role");
            }

            List<string> roles = await userRolesService.FindByUserIdAsync(user.Id);
            {
                if (roles != null && roles.Contains(role))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes a user from a role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task RemoveFromRoleAsync(TUser user, string role)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task DeleteAsync(TUser user)
        {
            if (user != null)
            {
                var result = await userService.DeleteAsync(user);
            }
        }

        /// <summary>
        /// Returns the PasswordHash for a given TUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> GetPasswordHashAsync(TUser user)
        {
            string passwordHash = await userService.GetPasswordHashAsync(user.Id);
            return passwordHash;
        }

        /// <summary>
        /// Verifies if user has password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> HasPasswordAsync(TUser user)
        {
            var result = await userService.GetPasswordHashAsync(user.Id);
            var hasPassword = !string.IsNullOrEmpty(result);

            return hasPassword;
        }

        /// <summary>
        /// Sets the password hash for a given TUser
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;

            return Task.FromResult<Object>(null);
        }

        /// <summary>
        ///  Set security stamp
        /// </summary>
        /// <param name="user"></param>
        /// <param name="stamp"></param>
        /// <returns></returns>
        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            user.SecurityStamp = stamp;

            return Task.FromResult(0);

        }

        /// <summary>
        /// Get security stamp
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetSecurityStampAsync(TUser user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        /// <summary>
        /// Set email on user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task SetEmailAsync(TUser user, string email)
        {
            user.Email = email;
            var result = await userService.UpdateAsync(user);
        }

        /// <summary>
        /// Get email from user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetEmailAsync(TUser user)
        {
            return Task.FromResult(user.Email);
        }

        /// <summary>
        /// Get if user email is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        /// <summary>
        /// Set when user email is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public async Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            user.EmailConfirmed = confirmed;
            var result = await userService.UpdateAsync(user);
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<TUser> FindByEmailAsync(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("email");
            }

            var tempResult = await userService.GetUserByEmailAsync(email);
            TUser result = tempResult as TUser;
            if (result != null)
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Set user phone number
        /// </summary>
        /// <param name="user"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public async Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            user.PhoneNumber = phoneNumber;
            var result = await userService.UpdateAsync(user);
        }

        /// <summary>
        /// Get user phone number
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetPhoneNumberAsync(TUser user)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        /// <summary>
        /// Get if user phone number is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        /// <summary>
        /// Set phone number if confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public async Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            user.PhoneNumberConfirmed = confirmed;
            var result = await userService.UpdateAsync(user);
        }

        /// <summary>
        /// Set two factor authentication is enabled on the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public async Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            user.TwoFactorEnabled = enabled;
            var result = await userService.UpdateAsync(user);
        }

        /// <summary>
        /// Get if two factor authentication is enabled on the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        /// <summary>
        /// Get user lock out end date
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            return
                Task.FromResult(user.LockoutEndDateUtc.HasValue
                    ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
                    : new DateTimeOffset());
        }


        /// <summary>
        /// Set user lockout end date
        /// </summary>
        /// <param name="user"></param>
        /// <param name="lockoutEnd"></param>
        /// <returns></returns>
        public async Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            user.LockoutEndDateUtc = lockoutEnd.UtcDateTime;
            var result = await userService.UpdateAsync(user);
        }

        /// <summary>
        /// Increment failed access count
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            user.AccessFailedCount++;
            var result = await userService.UpdateAsync(user);
            return user.AccessFailedCount;
        }

        /// <summary>
        /// Reset failed access count
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task ResetAccessFailedCountAsync(TUser user)
        {
            user.AccessFailedCount = 0;
            var result = await userService.UpdateAsync(user);
        }

        /// <summary>
        /// Get failed access count
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>
        /// Get if lockout is enabled for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            return Task.FromResult(user.LockoutEnabled);
        }

        /// <summary>
        /// Set lockout enabled for user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public async Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            user.LockoutEnabled = enabled;
            var result = await userService.UpdateAsync(user);
        }
    }
}
