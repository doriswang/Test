using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Identity.Entity;
using Test.Identity.Model;

namespace Test.Data.Repositories
{
    public interface IUserLoginsRepository
    {
        bool Delete(UserLogin userLogin);
        Task<bool> DeleteAsync(UserLogin userLogin);
        bool Delete(string userId);
        Task<bool> DeleteAsync(string userId);
        bool Insert(UserLogin userLogin);
        Task<bool> InsertAsync(UserLogin userLogin);
        string FindUserIdByLogin(string loginProvider, string providerKey);
        Task<string> FindUserIdByLoginAsync(string loginProvider, string providerKey);
        IEnumerable<UserLogin> FindByUserId(string userId);
        Task<IEnumerable<UserLogin>> FindByUserIdAsync(string userId);
    }
}
