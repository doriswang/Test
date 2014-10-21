using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Framework.DataAccess;
using Test.Framework.Extensions;
using Test.Identity.Entity;
using Test.Identity.Model;

namespace Test.Data.Repositories
{
    public class UserLoginsRepository : BaseRepository, IUserLoginsRepository
    {
        #region Private Members

        #endregion

        #region Constructors
        public UserLoginsRepository(IDatabase database)
            : base(database)
        {
        }

        public UserLoginsRepository(string connectionName)
            : base(connectionName)
        {
        }
        #endregion

        public bool Delete(UserLogin userLogin)
        {
            return this.Database.Delete<UserLogin>(userLogin);
        }

        public async Task<bool> DeleteAsync(UserLogin userLogin)
        {
            var result = await this.Database.DeleteAsync<UserLogin>(userLogin);
            return result;
        }

        public bool Delete(string userId)
        {
            return Delete(new UserLogin { UserId = userId });
        }

        public async Task<bool> DeleteAsync(string userId)
        {
            var result = await DeleteAsync(new UserLogin { UserId = userId });
            return result;
        }

        public bool Insert(UserLogin userLogin)
        {
            return this.Database.Insert<UserLogin>(userLogin);
        }

        public async Task<bool> InsertAsync(UserLogin userLogin)
        {
            var result = await this.Database.InsertAsync<UserLogin>(userLogin);
            return result;
        }

        public string FindUserIdByLogin(string loginProvider, string providerKey)
        {
            if (loginProvider.IsNullOrEmpty() ||
                providerKey.IsNullOrEmpty())
                return null;

            string commandText = "Select UserId from userlogins where LoginProvider = @loginProvider and ProviderKey = @providerKey";
            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("loginProvider", loginProvider);
            parameters.AddParameter("providerKey", providerKey);

            var result = this.Database.Select<UserLogin>(commandText, parameters).FirstOrDefault();

            if (result == null)
                return null;

            return result.UserId;
        }

        public async Task<string> FindUserIdByLoginAsync(string loginProvider, string providerKey)
        {
            if (loginProvider.IsNullOrEmpty() ||
                providerKey.IsNullOrEmpty())
                return null;

            string commandText = "Select UserId from userlogins where LoginProvider = @loginProvider and ProviderKey = @providerKey";
            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("loginProvider", loginProvider);
            parameters.AddParameter("providerKey", providerKey);

            var tempResult = await this.Database.SelectAsync<UserLogin>(commandText, parameters);
            var result = tempResult.FirstOrDefault();

            if (result == null)
                return null;

            return result.UserId;
        }

        public IEnumerable<UserLogin> FindByUserId(string userId)
        {
            string commandText = "Select * from userlogins where UserId = @userId";
            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("userId", userId);

            return this.Database.Select<UserLogin>(commandText, parameters);
        }

        public async Task<IEnumerable<UserLogin>> FindByUserIdAsync(string userId)
        {
            string commandText = "Select * from userlogins where UserId = @userId";
            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("userId", userId);

            var result = await this.Database.SelectAsync<UserLogin>(commandText, parameters);
            return result;
        }
    }
}
