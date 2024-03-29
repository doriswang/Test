﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Framework.DataAccess;
using Test.Framework.Extensions;
using Test.Identity.Entity;
using Test.Identity.Model;
using Dapper;

namespace Test.Data.Repositories
{
    public class UserRolesRepository : BaseRepository, IUserRolesRepository
    {
        #region Private Members

        #endregion

        #region Constructors
        public UserRolesRepository(IDatabase database)
            : base(database)
        {
        }

        public UserRolesRepository(string connectionName)
            : base(connectionName)
        {
        }
        #endregion

        public IEnumerable<string> FindByUserId(string userId)
        {
            IEnumerable<string> result = null;
            string commandText = "Select roles.Name from userroles, roles where userroles.UserId = @userId and userroles.RoleId = roles.Id";
            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("userId", userId);

            using (var connection = this.Database.Connection)
            {
                result = connection.Query<string>(connection.CreateDapperCommand(new SqlCommand(commandText, parameters)));
            }

            return result;
        }

        public async Task<IEnumerable<string>> FindByUserIdAsync(string userId)
        {
            IEnumerable<string> result = null;
            string commandText = "Select roles.Name from userroles, roles where userroles.UserId = @userId and userroles.RoleId = roles.Id";
            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("userId", userId);

            using (var connection = this.Database.Connection)
            {
                result = await connection.QueryAsync<string>(connection.CreateDapperCommand(new SqlCommand(commandText, parameters)));
            }

            return result;
        }

        public bool Delete(UserRole userRole)
        {
            return this.Database.Delete<UserRole>(userRole);
        }

        public async Task<bool> DeleteAsync(UserRole userRole)
        {
            var result = await this.Database.DeleteAsync<UserRole>(userRole);
            return result;
        }

        public bool Delete(string userId)
        {
            return Delete(new UserRole { UserId = userId });
        }

        public async Task<bool> DeleteAsync(string userId)
        {
            var result = await DeleteAsync(new UserRole { UserId = userId });
            return result;
        }

        public bool Insert(UserRole userRole)
        {
            return this.Database.Insert<UserRole>(userRole);
        }

        public async Task<bool> InsertAsync(UserRole userRole)
        {
            var result = await this.Database.InsertAsync<UserRole>(userRole);
            return result;
        }

        public bool Update(UserRole userRole)
        {
            return this.Database.Update<UserRole>(userRole);
        }

        public async Task<bool> UpdateAsync(UserRole userRole)
        {
            var result = await this.Database.UpdateAsync<UserRole>(userRole);
            return result;
        }

    }
}
