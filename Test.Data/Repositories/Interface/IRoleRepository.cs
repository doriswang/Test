using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Identity.Entity;
using Test.Identity.Model;

namespace Test.Data.Repositories
{
    public interface IRoleRepository
    {
        bool Delete(string roleId);
        Task<bool> DeleteAsync(string roleId);
        bool Delete(Role role);
        Task<bool> DeleteAsync(Role role);
        bool Insert(Role role);
        Task<bool> InsertAsync(Role role);
        string GetRoleName(string roleId);
        Task<string> GetRoleNameAsync(string roleId);
        string GetRoleId(string roleName);
        Task<string> GetRoleIdAsync(string roleName);
        Role GetRoleById(string roleId);
        Task<Role> GetRoleByIdAsync(string roleId);
        Role GetRoleByName(string roleName);
        Task<Role> GetRoleByNameAsync(string roleName);
        bool Update(Role role);
        Task<bool> UpdateAsync(Role role);
    }
}
