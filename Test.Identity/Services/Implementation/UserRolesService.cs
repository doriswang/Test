using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data;
using Test.Identity.Model;
using Test.Framework.Extensions;
using Test.Identity.Entity;

namespace Test.Identity.Services
{
    /// <summary>
    /// Class that represents the UserRoles table in the MySQL Database
    /// </summary>
    public class UserRolesService : IUserRolesService
    {
        private IDataProvider dataProvider;

        /// <summary>
        /// Constructor that takes a MySQLDatabase instance 
        /// </summary>
        /// <param name="database"></param>
        public UserRolesService(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        /// <summary>
        /// Returns a list of user's roles
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public List<string> FindByUserId(string userId)
        {
            //List<string> roles = new List<string>();
            //string commandText = "Select Roles.Name from UserRoles, Roles where UserRoles.UserId = @userId and UserRoles.RoleId = Roles.Id";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@userId", userId);

            //var rows = _database.Query(commandText, parameters);
            //foreach (var row in rows)
            //{
            //    roles.Add(row["Name"]);
            //}

            //return roles;
            var result = dataProvider.UserRolesRepository.FindByUserId(userId);

            if (result.IsNullOrEmpty())
                return null;

            return result.ToList();
        }

        public async Task<List<string>> FindByUserIdAsync(string userId)
        {
            //List<string> roles = new List<string>();
            //string commandText = "Select Roles.Name from UserRoles, Roles where UserRoles.UserId = @userId and UserRoles.RoleId = Roles.Id";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@userId", userId);

            //var rows = _database.Query(commandText, parameters);
            //foreach (var row in rows)
            //{
            //    roles.Add(row["Name"]);
            //}

            //return roles;
            var result = await dataProvider.UserRolesRepository.FindByUserIdAsync(userId);

            if (result.IsNullOrEmpty())
                return null;

            return result.ToList();
        }

        /// <summary>
        /// Deletes all roles from a user in the UserRoles table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public bool Delete(string userId)
        {
            //string commandText = "Delete from UserRoles where UserId = @userId";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("UserId", userId);

            //return _database.Execute(commandText, parameters);
            return dataProvider.UserRolesRepository.Delete(userId);
        }

        public async Task<bool> DeleteAsync(string userId)
        {
            //string commandText = "Delete from UserRoles where UserId = @userId";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("UserId", userId);

            //return _database.Execute(commandText, parameters);
            var result = await dataProvider.UserRolesRepository.DeleteAsync(userId);
            return result;
        }

        /// <summary>
        /// Inserts a new role for a user in the UserRoles table
        /// </summary>
        /// <param name="user">The User</param>
        /// <param name="roleId">The Role's id</param>
        /// <returns></returns>
        public bool Insert(IdentityUser user, string roleId)
        {
            //string commandText = "Insert into UserRoles (UserId, RoleId) values (@userId, @roleId)";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("userId", user.Id);
            //parameters.Add("roleId", roleId);

            //return _database.Execute(commandText, parameters);
            if (user == null ||
                user.Id.IsNullOrEmpty() ||
                roleId.IsNullOrEmpty())
                return false;

            return dataProvider.UserRolesRepository.Insert(new UserRole { UserId = user.Id, RoleId = roleId });
        }

        public async Task<bool> InsertAsync(IdentityUser user, string roleId)
        {
            //string commandText = "Insert into UserRoles (UserId, RoleId) values (@userId, @roleId)";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("userId", user.Id);
            //parameters.Add("roleId", roleId);

            //return _database.Execute(commandText, parameters);
            if (user == null ||
                user.Id.IsNullOrEmpty() ||
                roleId.IsNullOrEmpty())
                return false;

            var result = await dataProvider.UserRolesRepository.InsertAsync(new UserRole { UserId = user.Id, RoleId = roleId });
            return result;
        }
    }
}
