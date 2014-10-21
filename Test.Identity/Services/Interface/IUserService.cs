using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Identity.Model;

namespace Test.Identity.Services
{
    public interface IUserService<TUser>
        where TUser: IdentityUser
    {
        string GetUserName(string userId);
        Task<string> GetUserNameAsync(string userId);
        string GetUserId(string userName);
        Task<string> GetUserIdAsync(string userName);
        TUser GetUserById(string userId);
        Task<TUser> GetUserByIdAsync(string userId);
        List<TUser> GetUserByName(string userName);
        Task<List<TUser>> GetUserByNameAsync(string userName);
        List<TUser> GetUserByEmail(string email);
        Task<List<TUser>> GetUserByEmailAsync(string email);
        string GetPasswordHash(string userId);
        Task<string> GetPasswordHashAsync(string userId);
        bool SetPasswordHash(string userId, string passwordHash);
        Task<bool> SetPasswordHashAsync(string userId, string passwordHash);
        string GetSecurityStamp(string userId);
        Task<string> GetSecurityStampAsync(string userId);
        bool Insert(TUser user);
        Task<bool> InsertAsync(TUser user);
        bool Delete(string userId);
        Task<bool> DeleteAsync(string userId);
        bool Delete(TUser user);
        Task<bool> DeleteAsync(TUser user);
        bool Update(TUser user);
        Task<bool> UpdateAsync(TUser user);
    }
}
