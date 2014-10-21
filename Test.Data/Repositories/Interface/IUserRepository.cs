using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Identity.Entity;
using Test.Identity.Model;

namespace Test.Data.Repositories
{
    public interface IUserRepository
    {
        string GetUserName(string userId);
        Task<string> GetUserNameAsync(string userId);
        string GetUserId(string userName);
        Task<string> GetUserIdAsync(string userName);
        User GetUserById(string userId);
        Task<User> GetUserByIdAsync(string userId);
        IEnumerable<User> GetUserByName(string userName);
        Task<IEnumerable<User>> GetUserByNameAsync(string userName);
        IEnumerable<User> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetUserByEmailAsync(string email);
        string GetPasswordHash(string userId);
        Task<string> GetPasswordHashAsync(string userId);
        bool SetPasswordHash(string userId, string passwordHash);
        Task<bool> SetPasswordHashAsync(string userId, string passwordHash);
        string GetSecurityStamp(string userId);
        Task<string> GetSecurityStampAsync(string userId);
        bool Insert(User user);
        Task<bool> InsertAsync(User user);
        bool Delete(string userId);
        Task<bool> DeleteAsync(string userId);
        bool Delete(User user);
        Task<bool> DeleteAsync(User user);
        bool Update(User user);
        Task<bool> UpdateAsync(User user);
    }
}
