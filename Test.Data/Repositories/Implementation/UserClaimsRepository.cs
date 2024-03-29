﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Test.Framework.DataAccess;
using Test.Framework.Extensions;
using Test.Identity.Entity;
using Test.Identity.Model;

namespace Test.Data.Repositories
{
    public class UserClaimsRepository : BaseRepository, IUserClaimsRepository
    {
        #region Private Members

        #endregion

        #region Constructors
        public UserClaimsRepository(IDatabase database)
            : base(database)
        {
        }

        public UserClaimsRepository(string connectionName)
            : base(connectionName)
        {
        }
        #endregion

        public IEnumerable<UserClaim> Get(string userId)
        {
            string commandText = "Select * from userclaims where UserId = @userId";
            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("userId", userId);

            return this.Database.Select<UserClaim>(commandText, parameters);
        }

        public async Task<IEnumerable<UserClaim>> GetAsync(string userId)
        {
            string commandText = "Select * from userclaims where UserId = @userId";
            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("userId", userId);

            var result = await this.Database.SelectAsync<UserClaim>(commandText, parameters);
            return result;
        }

        public bool Delete(string userId)
        {
            string commandText = "Delete from userclaims where UserId = @userId";
            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("userId", userId);

            return this.Database.Execute(new List<SqlCommand> { new SqlCommand(commandText, parameters) });
        }

        public async Task<bool> DeleteAsync(string userId)
        {
            string commandText = "Delete from userclaims where UserId = @userId";
            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("userId", userId);

            var result = await this.Database.ExecuteAsync(new List<SqlCommand> { new SqlCommand(commandText, parameters) });
            return result;
        }

        public bool Delete(UserClaim userClaim)
        {
            return this.Database.Delete<UserClaim>(userClaim);
        }

        public async Task<bool> DeleteAsync(UserClaim userClaim)
        {
            var result = await this.Database.DeleteAsync<UserClaim>(userClaim);
            return result;
        }

        public bool Insert(UserClaim userClaim)
        {
            return this.Database.Insert<UserClaim>(userClaim);
        }

        public async Task<bool> InsertAsync(UserClaim userClaim)
        {
            var result = await this.Database.InsertAsync<UserClaim>(userClaim);
            return result;
        }
    }
}
