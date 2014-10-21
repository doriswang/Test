using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Identity.Model;

namespace Test.Identity.Services
{
    public interface IUserRolesService
    {
        List<string> FindByUserId(string userId);
        Task<List<string>> FindByUserIdAsync(string userId);
        bool Delete(string userId);
        Task<bool> DeleteAsync(string userId);
        bool Insert(IdentityUser user, string roleId);
        Task<bool> InsertAsync(IdentityUser user, string roleId);
    }
}
