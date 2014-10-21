using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data;
using Test.Identity.Model;
using Test.Framework.Extensions;
using AutoMapper;
using Test.Identity.Entity;

namespace Test.Identity.Services
{
    /// <summary>
    /// Class that represents the Users table in the MySQL Database
    /// </summary>
    public class UserService<TUser> : IUserService<TUser>
        where TUser : IdentityUser
    {
        private IDataProvider dataProvider;
        /// <summary>
        /// Constructor that takes a MySQLDatabase instance 
        /// </summary>
        /// <param name="database"></param>
        public UserService(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        #region Private Methods

        private User CreateUserInstance(TUser user)
        {
            var result = new User();
            Mapper.CreateMap<TUser, User>();
            result = Mapper.Map<User>(user);
            return result;
        }

        private static List<TUser> CreateTUserInstance(IEnumerable<User> results)
        {
            List<TUser> users = new List<TUser>();

            if (results.IsNullOrEmpty())
                return null;

            Mapper.CreateMap<User, TUser>();

            foreach (var result in results)
            {
                if (result == null)
                    continue;

                TUser user = CreateTUserInstance(result);

                users.Add(user);
            }

            return users;
        }

        private static TUser CreateTUserInstance(User result)
        {
            if (result == null)
                return null;

            Mapper.CreateMap<User, TUser>();

            TUser user = Mapper.Map<TUser>(result);

            return user;
        }

        #endregion

        /// <summary>
        /// Returns the user's name given a user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserName(string userId)
        {
            //string commandText = "Select Name from Users where Id = @id";
            //Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@id", userId } };

            //return _database.GetStrValue(commandText, parameters);
            return dataProvider.UserRepository.GetUserName(userId);
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            //string commandText = "Select Name from Users where Id = @id";
            //Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@id", userId } };

            //return _database.GetStrValue(commandText, parameters);
            var result = await dataProvider.UserRepository.GetUserNameAsync(userId);
            return result;
        }

        /// <summary>
        /// Returns a User ID given a user name
        /// </summary>
        /// <param name="userName">The user's name</param>
        /// <returns></returns>
        public string GetUserId(string userName)
        {
            //string commandText = "Select Id from Users where UserName = @name";
            //Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@name", userName } };

            //return _database.GetStrValue(commandText, parameters);
            return dataProvider.UserRepository.GetUserId(userName);
        }

        public async Task<string> GetUserIdAsync(string userName)
        {
            //string commandText = "Select Id from Users where UserName = @name";
            //Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@name", userName } };

            //return _database.GetStrValue(commandText, parameters);
            var result = await dataProvider.UserRepository.GetUserIdAsync(userName);
            return result;
        }

        /// <summary>
        /// Returns an TUser given the user's id
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public TUser GetUserById(string userId)
        {
            //string commandText = "Select * from Users where Id = @id";
            //Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@id", userId } };

            //var rows = _database.Query(commandText, parameters);
            //if (rows != null && rows.Count == 1)
            //{
            //    var row = rows[0];
            //    user = (TUser)Activator.CreateInstance(typeof(TUser));
            //    user.Id = row["Id"];
            //    user.UserName = row["UserName"];
            //    user.PasswordHash = string.IsNullOrEmpty(row["PasswordHash"]) ? null : row["PasswordHash"];
            //    user.SecurityStamp = string.IsNullOrEmpty(row["SecurityStamp"]) ? null : row["SecurityStamp"];
            //    user.Email = string.IsNullOrEmpty(row["Email"]) ? null : row["Email"];
            //    user.EmailConfirmed = row["EmailConfirmed"] == "1" ? true : false;
            //    user.PhoneNumber = string.IsNullOrEmpty(row["PhoneNumber"]) ? null : row["PhoneNumber"];
            //    user.PhoneNumberConfirmed = row["PhoneNumberConfirmed"] == "1" ? true : false;
            //    user.LockoutEnabled = row["LockoutEnabled"] == "1" ? true : false;
            //    user.LockoutEndDateUtc = string.IsNullOrEmpty(row["LockoutEndDateUtc"]) ? DateTime.Now : DateTime.Parse(row["LockoutEndDateUtc"]);
            //    user.AccessFailedCount = string.IsNullOrEmpty(row["AccessFailedCount"]) ? 0 : int.Parse(row["AccessFailedCount"]);
            //}

            //return user;

            var result = dataProvider.UserRepository.GetUserById(userId);

            if (result == null)
                return null;

            return CreateTUserInstance(result);
        }

        public async Task<TUser> GetUserByIdAsync(string userId)
        {
            //string commandText = "Select * from Users where Id = @id";
            //Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@id", userId } };

            //var rows = _database.Query(commandText, parameters);
            //if (rows != null && rows.Count == 1)
            //{
            //    var row = rows[0];
            //    user = (TUser)Activator.CreateInstance(typeof(TUser));
            //    user.Id = row["Id"];
            //    user.UserName = row["UserName"];
            //    user.PasswordHash = string.IsNullOrEmpty(row["PasswordHash"]) ? null : row["PasswordHash"];
            //    user.SecurityStamp = string.IsNullOrEmpty(row["SecurityStamp"]) ? null : row["SecurityStamp"];
            //    user.Email = string.IsNullOrEmpty(row["Email"]) ? null : row["Email"];
            //    user.EmailConfirmed = row["EmailConfirmed"] == "1" ? true : false;
            //    user.PhoneNumber = string.IsNullOrEmpty(row["PhoneNumber"]) ? null : row["PhoneNumber"];
            //    user.PhoneNumberConfirmed = row["PhoneNumberConfirmed"] == "1" ? true : false;
            //    user.LockoutEnabled = row["LockoutEnabled"] == "1" ? true : false;
            //    user.LockoutEndDateUtc = string.IsNullOrEmpty(row["LockoutEndDateUtc"]) ? DateTime.Now : DateTime.Parse(row["LockoutEndDateUtc"]);
            //    user.AccessFailedCount = string.IsNullOrEmpty(row["AccessFailedCount"]) ? 0 : int.Parse(row["AccessFailedCount"]);
            //}

            //return user;

            var result = await dataProvider.UserRepository.GetUserByIdAsync(userId);

            if (result == null)
                return null;

            return CreateTUserInstance(result);
        }

        /// <summary>
        /// Returns a list of TUser instances given a user name
        /// </summary>
        /// <param name="userName">User's name</param>
        /// <returns></returns>
        public List<TUser> GetUserByName(string userName)
        {
            //List<TUser> users = new List<TUser>();
            //string commandText = "Select * from Users where UserName = @name";
            //Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@name", userName } };

            //var rows = _database.Query(commandText, parameters);
            //foreach (var row in rows)
            //{
            //    TUser user = (TUser)Activator.CreateInstance(typeof(TUser));
            //    user.Id = row["Id"];
            //    user.UserName = row["UserName"];
            //    user.PasswordHash = string.IsNullOrEmpty(row["PasswordHash"]) ? null : row["PasswordHash"];
            //    user.SecurityStamp = string.IsNullOrEmpty(row["SecurityStamp"]) ? null : row["SecurityStamp"];
            //    user.Email = string.IsNullOrEmpty(row["Email"]) ? null : row["Email"];
            //    user.EmailConfirmed = row["EmailConfirmed"] == "1" ? true : false;
            //    user.PhoneNumber = string.IsNullOrEmpty(row["PhoneNumber"]) ? null : row["PhoneNumber"];
            //    user.PhoneNumberConfirmed = row["PhoneNumberConfirmed"] == "1" ? true : false;
            //    user.LockoutEnabled = row["LockoutEnabled"] == "1" ? true : false;
            //    user.TwoFactorEnabled = row["TwoFactorEnabled"] == "1" ? true : false;
            //    user.LockoutEndDateUtc = string.IsNullOrEmpty(row["LockoutEndDateUtc"]) ? DateTime.Now : DateTime.Parse(row["LockoutEndDateUtc"]);
            //    user.AccessFailedCount = string.IsNullOrEmpty(row["AccessFailedCount"]) ? 0 : int.Parse(row["AccessFailedCount"]);
            //    users.Add(user);
            //}

            //return users;

            var result = dataProvider.UserRepository.GetUserByName(userName);

            if (result == null)
                return null;

            return CreateTUserInstance(result);
        }

        public async Task<List<TUser>> GetUserByNameAsync(string userName)
        {
            //List<TUser> users = new List<TUser>();
            //string commandText = "Select * from Users where UserName = @name";
            //Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@name", userName } };

            //var rows = _database.Query(commandText, parameters);
            //foreach (var row in rows)
            //{
            //    TUser user = (TUser)Activator.CreateInstance(typeof(TUser));
            //    user.Id = row["Id"];
            //    user.UserName = row["UserName"];
            //    user.PasswordHash = string.IsNullOrEmpty(row["PasswordHash"]) ? null : row["PasswordHash"];
            //    user.SecurityStamp = string.IsNullOrEmpty(row["SecurityStamp"]) ? null : row["SecurityStamp"];
            //    user.Email = string.IsNullOrEmpty(row["Email"]) ? null : row["Email"];
            //    user.EmailConfirmed = row["EmailConfirmed"] == "1" ? true : false;
            //    user.PhoneNumber = string.IsNullOrEmpty(row["PhoneNumber"]) ? null : row["PhoneNumber"];
            //    user.PhoneNumberConfirmed = row["PhoneNumberConfirmed"] == "1" ? true : false;
            //    user.LockoutEnabled = row["LockoutEnabled"] == "1" ? true : false;
            //    user.TwoFactorEnabled = row["TwoFactorEnabled"] == "1" ? true : false;
            //    user.LockoutEndDateUtc = string.IsNullOrEmpty(row["LockoutEndDateUtc"]) ? DateTime.Now : DateTime.Parse(row["LockoutEndDateUtc"]);
            //    user.AccessFailedCount = string.IsNullOrEmpty(row["AccessFailedCount"]) ? 0 : int.Parse(row["AccessFailedCount"]);
            //    users.Add(user);
            //}

            //return users;

            var result = await dataProvider.UserRepository.GetUserByNameAsync(userName);

            if (result == null)
                return null;

            return CreateTUserInstance(result);
        }

        public List<TUser> GetUserByEmail(string email)
        {
            return null;
        }

        public async Task<List<TUser>> GetUserByEmailAsync(string email)
        {
            var result = await Task.FromResult<List<TUser>>(null);
            return result;
        }

        /// <summary>
        /// Return the user's password hash
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public string GetPasswordHash(string userId)
        {
            //string commandText = "Select PasswordHash from Users where Id = @id";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@id", userId);

            //var passHash = _database.GetStrValue(commandText, parameters);
            //if (string.IsNullOrEmpty(passHash))
            //{
            //    return null;
            //}

            //return passHash;
            return dataProvider.UserRepository.GetPasswordHash(userId);
        }

        public async Task<string> GetPasswordHashAsync(string userId)
        {
            //string commandText = "Select PasswordHash from Users where Id = @id";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@id", userId);

            //var passHash = _database.GetStrValue(commandText, parameters);
            //if (string.IsNullOrEmpty(passHash))
            //{
            //    return null;
            //}

            //return passHash;
            var result = await dataProvider.UserRepository.GetPasswordHashAsync(userId);
            return result;
        }

        /// <summary>
        /// Sets the user's password hash
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public bool SetPasswordHash(string userId, string passwordHash)
        {
            //string commandText = "Update Users set PasswordHash = @pwdHash where Id = @id";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@pwdHash", passwordHash);
            //parameters.Add("@id", userId);

            //return _database.Execute(commandText, parameters);

            return dataProvider.UserRepository.SetPasswordHash(userId, passwordHash);
        }

        public async Task<bool> SetPasswordHashAsync(string userId, string passwordHash)
        {
            //string commandText = "Update Users set PasswordHash = @pwdHash where Id = @id";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@pwdHash", passwordHash);
            //parameters.Add("@id", userId);

            //return _database.Execute(commandText, parameters);

            var result = await dataProvider.UserRepository.SetPasswordHashAsync(userId, passwordHash);
            return result;
        }

        /// <summary>
        /// Returns the user's security stamp
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetSecurityStamp(string userId)
        {
            //string commandText = "Select SecurityStamp from Users where Id = @id";
            //Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@id", userId } };
            //var result = _database.GetStrValue(commandText, parameters);

            //return result;

            return dataProvider.UserRepository.GetSecurityStamp(userId);
        }

        public async Task<string> GetSecurityStampAsync(string userId)
        {
            //string commandText = "Select SecurityStamp from Users where Id = @id";
            //Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@id", userId } };
            //var result = _database.GetStrValue(commandText, parameters);

            //return result;

            var result = await dataProvider.UserRepository.GetSecurityStampAsync(userId);
            return result;
        }

        /// <summary>
        /// Inserts a new user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Insert(TUser user)
        {
            //            string commandText = @"Insert into Users (UserName, Id, PasswordHash, SecurityStamp,Email,EmailConfirmed,PhoneNumber,PhoneNumberConfirmed, AccessFailedCount,LockoutEnabled,LockoutEndDateUtc,TwoFactorEnabled)
            //                values (@name, @id, @pwdHash, @SecStamp,@email,@emailconfirmed,@phonenumber,@phonenumberconfirmed,@accesscount,@lockoutenabled,@lockoutenddate,@twofactorenabled)";
            //            Dictionary<string, object> parameters = new Dictionary<string, object>();
            //            parameters.Add("@name", user.UserName);
            //            parameters.Add("@id", user.Id);
            //            parameters.Add("@pwdHash", user.PasswordHash);
            //            parameters.Add("@SecStamp", user.SecurityStamp);
            //            parameters.Add("@email", user.Email);
            //            parameters.Add("@emailconfirmed", user.EmailConfirmed);
            //            parameters.Add("@phonenumber", user.PhoneNumber);
            //            parameters.Add("@phonenumberconfirmed", user.PhoneNumberConfirmed);
            //            parameters.Add("@accesscount", user.AccessFailedCount);
            //            parameters.Add("@lockoutenabled", user.LockoutEnabled);
            //            parameters.Add("@lockoutenddate", user.LockoutEndDateUtc);
            //            parameters.Add("@twofactorenabled", user.TwoFactorEnabled);

            //            return _database.Execute(commandText, parameters);

            return dataProvider.UserRepository.Insert(CreateUserInstance(user));
        }

        public async Task<bool> InsertAsync(TUser user)
        {
            //            string commandText = @"Insert into Users (UserName, Id, PasswordHash, SecurityStamp,Email,EmailConfirmed,PhoneNumber,PhoneNumberConfirmed, AccessFailedCount,LockoutEnabled,LockoutEndDateUtc,TwoFactorEnabled)
            //                values (@name, @id, @pwdHash, @SecStamp,@email,@emailconfirmed,@phonenumber,@phonenumberconfirmed,@accesscount,@lockoutenabled,@lockoutenddate,@twofactorenabled)";
            //            Dictionary<string, object> parameters = new Dictionary<string, object>();
            //            parameters.Add("@name", user.UserName);
            //            parameters.Add("@id", user.Id);
            //            parameters.Add("@pwdHash", user.PasswordHash);
            //            parameters.Add("@SecStamp", user.SecurityStamp);
            //            parameters.Add("@email", user.Email);
            //            parameters.Add("@emailconfirmed", user.EmailConfirmed);
            //            parameters.Add("@phonenumber", user.PhoneNumber);
            //            parameters.Add("@phonenumberconfirmed", user.PhoneNumberConfirmed);
            //            parameters.Add("@accesscount", user.AccessFailedCount);
            //            parameters.Add("@lockoutenabled", user.LockoutEnabled);
            //            parameters.Add("@lockoutenddate", user.LockoutEndDateUtc);
            //            parameters.Add("@twofactorenabled", user.TwoFactorEnabled);

            //            return _database.Execute(commandText, parameters);

            var result = await dataProvider.UserRepository.InsertAsync(CreateUserInstance(user));
            return result;
        }

        /// <summary>
        /// Deletes a user from the Users table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public bool Delete(string userId)
        {
            //string commandText = "Delete from Users where Id = @userId";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@userId", userId);

            //return _database.Execute(commandText, parameters);

            return dataProvider.UserRepository.Delete(userId);
        }

        public async Task<bool> DeleteAsync(string userId)
        {
            //string commandText = "Delete from Users where Id = @userId";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@userId", userId);

            //return _database.Execute(commandText, parameters);

            var result = await dataProvider.UserRepository.DeleteAsync(userId);
            return result;
        }

        /// <summary>
        /// Deletes a user from the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Delete(TUser user)
        {
            return Delete(user.Id);
        }

        public async Task<bool> DeleteAsync(TUser user)
        {
            var result = await DeleteAsync(user.Id);
            return result;
        }

        /// <summary>
        /// Updates a user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Update(TUser user)
        {
            //            string commandText = @"Update Users set UserName = @userName, PasswordHash = @pswHash, SecurityStamp = @secStamp, 
            //                Email=@email, EmailConfirmed=@emailconfirmed, PhoneNumber=@phonenumber, PhoneNumberConfirmed=@phonenumberconfirmed,
            //                AccessFailedCount=@accesscount, LockoutEnabled=@lockoutenabled, LockoutEndDateUtc=@lockoutenddate, TwoFactorEnabled=@twofactorenabled  
            //                WHERE Id = @userId";
            //            Dictionary<string, object> parameters = new Dictionary<string, object>();
            //            parameters.Add("@userName", user.UserName);
            //            parameters.Add("@pswHash", user.PasswordHash);
            //            parameters.Add("@secStamp", user.SecurityStamp);
            //            parameters.Add("@userId", user.Id);
            //            parameters.Add("@email", user.Email);
            //            parameters.Add("@emailconfirmed", user.EmailConfirmed);
            //            parameters.Add("@phonenumber", user.PhoneNumber);
            //            parameters.Add("@phonenumberconfirmed", user.PhoneNumberConfirmed);
            //            parameters.Add("@accesscount", user.AccessFailedCount);
            //            parameters.Add("@lockoutenabled", user.LockoutEnabled);
            //            parameters.Add("@lockoutenddate", user.LockoutEndDateUtc);
            //            parameters.Add("@twofactorenabled", user.TwoFactorEnabled);

            //            return _database.Execute(commandText, parameters);
            return dataProvider.UserRepository.Update(CreateUserInstance(user));
        }

        public async Task<bool> UpdateAsync(TUser user)
        {
            //            string commandText = @"Update Users set UserName = @userName, PasswordHash = @pswHash, SecurityStamp = @secStamp, 
            //                Email=@email, EmailConfirmed=@emailconfirmed, PhoneNumber=@phonenumber, PhoneNumberConfirmed=@phonenumberconfirmed,
            //                AccessFailedCount=@accesscount, LockoutEnabled=@lockoutenabled, LockoutEndDateUtc=@lockoutenddate, TwoFactorEnabled=@twofactorenabled  
            //                WHERE Id = @userId";
            //            Dictionary<string, object> parameters = new Dictionary<string, object>();
            //            parameters.Add("@userName", user.UserName);
            //            parameters.Add("@pswHash", user.PasswordHash);
            //            parameters.Add("@secStamp", user.SecurityStamp);
            //            parameters.Add("@userId", user.Id);
            //            parameters.Add("@email", user.Email);
            //            parameters.Add("@emailconfirmed", user.EmailConfirmed);
            //            parameters.Add("@phonenumber", user.PhoneNumber);
            //            parameters.Add("@phonenumberconfirmed", user.PhoneNumberConfirmed);
            //            parameters.Add("@accesscount", user.AccessFailedCount);
            //            parameters.Add("@lockoutenabled", user.LockoutEnabled);
            //            parameters.Add("@lockoutenddate", user.LockoutEndDateUtc);
            //            parameters.Add("@twofactorenabled", user.TwoFactorEnabled);

            //            return _database.Execute(commandText, parameters);
            var result = await dataProvider.UserRepository.UpdateAsync(CreateUserInstance(user));
            return result;
        }
    }
}
