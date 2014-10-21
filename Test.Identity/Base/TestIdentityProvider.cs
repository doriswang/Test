using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Framework.Extensibility;
using Microsoft.AspNet.Identity;
using Test.Identity.Model;
using Test.Identity.Stores;
using Test.Identity.Services;

namespace Test.Identity
{
    public static class TestIdentityProvider
    {
        public static void Initialize()
        {
            Container.Register<IUserService<IdentityUser>, UserService<IdentityUser>>();
            Container.Register<IRoleService, RoleService>();
            Container.Register<IUserClaimsService, UserClaimsService>();
            Container.Register<IUserLoginsService, UserLoginsService>();
            Container.Register<IUserRolesService, UserRolesService>();
            Container.Register<IUserStore<IdentityUser>, UserStore<IdentityUser>>();
        }
    }
}
