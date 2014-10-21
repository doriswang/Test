using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Identity.Model;

namespace Test.Identity.Services
{
    public interface IRoleService
    {
        bool Delete(string roleId);
        Task<bool> DeleteAsync(string roleId);
        bool Insert(IdentityRole role);
        Task<bool> InsertAsync(IdentityRole role);
        string GetRoleName(string roleId);
        Task<string> GetRoleNameAsync(string roleId);
        string GetRoleId(string roleName);
        Task<string> GetRoleIdAsync(string roleName);
        IdentityRole GetRoleById(string roleId);
        Task<IdentityRole> GetRoleByIdAsync(string roleId);
        IdentityRole GetRoleByName(string roleName);
        Task<IdentityRole> GetRoleByNameAsync(string roleName);
        bool Update(IdentityRole role);
        Task<bool> UpdateAsync(IdentityRole role);
    }
}
