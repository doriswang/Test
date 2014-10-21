using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Test.Data;
using Test.Identity.Model;
using Test.Framework.Extensions;
using Test.Identity.Entity;

namespace Test.Identity.Services
{
    /// <summary>
    /// Class that represents the UserClaims table in the MySQL Database
    /// </summary>
    public class UserClaimsService : IUserClaimsService
    {
        private IDataProvider dataProvider;

        /// <summary>
        /// Constructor that takes a MySQLDatabase instance 
        /// </summary>
        /// <param name="database"></param>
        public UserClaimsService(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        /// <summary>
        /// Returns a ClaimsIdentity instance given a userId
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public ClaimsIdentity FindByUserId(string userId)
        {
            ClaimsIdentity claims = new ClaimsIdentity();
            //string commandText = "Select * from UserClaims where UserId = @userId";
            //Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@UserId", userId } };

            //var rows = _database.Query(commandText, parameters);

            if (userId.IsNullOrEmpty())
                return claims;

            var rows = dataProvider.UserClaimsRepository.Get(userId);

            if (rows.IsNullOrEmpty())
                return claims;

            foreach (var row in rows)
            {
                if (row == null ||
                    row.ClaimType.IsNullOrEmpty() ||
                    row.ClaimValue.IsNullOrEmpty())
                    continue;

                //Claim claim = new Claim(row["ClaimType"], row["ClaimValue"]);
                //claims.AddClaim(claim);

                claims.AddClaim(new Claim(row.ClaimType, row.ClaimValue));
            }

            return claims;
        }

        public async Task<ClaimsIdentity> FindByUserIdAsync(string userId)
        {
            ClaimsIdentity claims = new ClaimsIdentity();
            //string commandText = "Select * from UserClaims where UserId = @userId";
            //Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@UserId", userId } };

            //var rows = _database.Query(commandText, parameters);

            if (userId.IsNullOrEmpty())
                return claims;

            var rows = await dataProvider.UserClaimsRepository.GetAsync(userId);

            if (rows.IsNullOrEmpty())
                return claims;

            foreach (var row in rows)
            {
                if (row == null ||
                    row.ClaimType.IsNullOrEmpty() ||
                    row.ClaimValue.IsNullOrEmpty())
                    continue;

                //Claim claim = new Claim(row["ClaimType"], row["ClaimValue"]);
                //claims.AddClaim(claim);

                claims.AddClaim(new Claim(row.ClaimType, row.ClaimValue));
            }

            return claims;
        }

        /// <summary>
        /// Deletes all claims from a user given a userId
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public bool Delete(string userId)
        {
            //string commandText = "Delete from UserClaims where UserId = @userId";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("userId", userId);

            //return _database.Execute(commandText, parameters);
            return dataProvider.UserClaimsRepository.Delete(userId);
        }

        public async Task<bool> DeleteAsync(string userId)
        {
            //string commandText = "Delete from UserClaims where UserId = @userId";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("userId", userId);

            //return _database.Execute(commandText, parameters);
            var result = await dataProvider.UserClaimsRepository.DeleteAsync(userId);
            return result;
        }

        /// <summary>
        /// Inserts a new claim in UserClaims table
        /// </summary>
        /// <param name="userClaim">User's claim to be added</param>
        /// <param name="userId">User's id</param>
        /// <returns></returns>
        public bool Insert(Claim userClaim, string userId)
        {
            //string commandText = "Insert into UserClaims (ClaimValue, ClaimType, UserId) values (@value, @type, @userId)";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("value", userClaim.Value);
            //parameters.Add("type", userClaim.Type);
            //parameters.Add("userId", userId);

            //return _database.Execute(commandText, parameters);
            if (userClaim == null ||
                userClaim.Type.IsNullOrEmpty() ||
                userClaim.Value.IsNullOrEmpty() ||
                userId.IsNullOrEmpty())
                return false;

            return dataProvider.UserClaimsRepository.Insert(new UserClaim { UserId = userId, ClaimType = userClaim.Type, ClaimValue = userClaim.Value });
        }

        public async Task<bool> InsertAsync(Claim userClaim, string userId)
        {
            //string commandText = "Insert into UserClaims (ClaimValue, ClaimType, UserId) values (@value, @type, @userId)";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("value", userClaim.Value);
            //parameters.Add("type", userClaim.Type);
            //parameters.Add("userId", userId);

            //return _database.Execute(commandText, parameters);
            if (userClaim == null ||
                userClaim.Type.IsNullOrEmpty() ||
                userClaim.Value.IsNullOrEmpty() ||
                userId.IsNullOrEmpty())
                return false;

            var result = await dataProvider.UserClaimsRepository.InsertAsync(new UserClaim { UserId = userId, ClaimType = userClaim.Type, ClaimValue = userClaim.Value });
            return result;
        }

        /// <summary>
        /// Deletes a claim from a user 
        /// </summary>
        /// <param name="user">The user to have a claim deleted</param>
        /// <param name="claim">A claim to be deleted from user</param>
        /// <returns></returns>
        public bool Delete(IdentityUser user, Claim claim)
        {
            //string commandText = "Delete from UserClaims where UserId = @userId and @ClaimValue = @value and ClaimType = @type";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("userId", user.Id);
            //parameters.Add("value", claim.Value);
            //parameters.Add("type", claim.Type);

            //return _database.Execute(commandText, parameters);

            if (user == null ||
                claim == null ||
                claim.Value.IsNullOrEmpty() ||
                claim.Type.IsNullOrEmpty())
                return false;

            return dataProvider.UserClaimsRepository.Delete(new UserClaim { UserId = user.Id, ClaimType = claim.Type, ClaimValue = claim.Value });
        }

        public async Task<bool> DeleteAsync(IdentityUser user, Claim claim)
        {
            //string commandText = "Delete from UserClaims where UserId = @userId and @ClaimValue = @value and ClaimType = @type";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("userId", user.Id);
            //parameters.Add("value", claim.Value);
            //parameters.Add("type", claim.Type);

            //return _database.Execute(commandText, parameters);

            if (user == null ||
                claim == null ||
                claim.Value.IsNullOrEmpty() ||
                claim.Type.IsNullOrEmpty())
                return false;

            var result = await dataProvider.UserClaimsRepository.DeleteAsync(new UserClaim { UserId = user.Id, ClaimType = claim.Type, ClaimValue = claim.Value });
            return result;
        }
    }
}
