using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Test.Identity.Entity;
using Test.Identity.Model;

namespace Test.Data.Repositories
{
    public interface IUserClaimsRepository
    {
        IEnumerable<UserClaim> Get(string userId);
        Task<IEnumerable<UserClaim>> GetAsync(string userId);
        bool Delete(string userId);
        Task<bool> DeleteAsync(string userId);
        bool Delete(UserClaim userClaim);
        Task<bool> DeleteAsync(UserClaim userClaim);
        bool Insert(UserClaim userClaim);
        Task<bool> InsertAsync(UserClaim userClaim);
    }
}
