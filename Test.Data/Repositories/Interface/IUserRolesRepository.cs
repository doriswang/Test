using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Identity.Entity;
using Test.Identity.Model;

namespace Test.Data.Repositories
{
    public interface IUserRolesRepository
    {
        IEnumerable<string> FindByUserId(string userId);
        Task<IEnumerable<string>> FindByUserIdAsync(string userId);
        bool Delete(string userId);
        Task<bool> DeleteAsync(string userId);
        bool Insert(UserRole userRole);
        Task<bool> InsertAsync(UserRole userRole);
        bool Update(UserRole userRole);
        Task<bool> UpdateAsync(UserRole userRole);
        bool Delete(UserRole userRole);
        Task<bool> DeleteAsync(UserRole userRole);
    }
}
