using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Test.Identity.Model;

namespace Test.Identity.Services
{
    public interface IUserClaimsService
    {
        ClaimsIdentity FindByUserId(string userId);
        Task<ClaimsIdentity> FindByUserIdAsync(string userId);
        bool Delete(string userId);
        Task<bool> DeleteAsync(string userId);
        bool Insert(Claim userClaim, string userId);
        Task<bool> InsertAsync(Claim userClaim, string userId);
        bool Delete(IdentityUser user, Claim claim);
        Task<bool> DeleteAsync(IdentityUser user, Claim claim);
    }
}
