using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Identity.Model;

namespace Test.Identity.Services
{
    public interface IUserLoginsService
    {
        bool Delete(IdentityUser user, UserLoginInfo login);
        Task<bool> DeleteAsync(IdentityUser user, UserLoginInfo login);
        bool Delete(string userId);
        Task<bool> DeleteAsync(string userId);
        bool Insert(IdentityUser user, UserLoginInfo login);
        Task<bool> InsertAsync(IdentityUser user, UserLoginInfo login);
        string FindUserIdByLogin(UserLoginInfo userLogin);
        Task<string> FindUserIdByLoginAsync(UserLoginInfo userLogin);
        List<UserLoginInfo> FindByUserId(string userId);
        Task<List<UserLoginInfo>> FindByUserIdAsync(string userId);
    }
}
