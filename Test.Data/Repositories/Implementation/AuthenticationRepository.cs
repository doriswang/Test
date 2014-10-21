using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Framework.DataAccess;

namespace Test.Data.Repositories
{
    public class AuthenticationRepository : BaseRepository, IAuthenticationRepository
    {
        #region Private Members
        
        #endregion

        #region Constructors
        public AuthenticationRepository(IDatabase database)
            : base(database)
        {
        }

        public AuthenticationRepository(string connectionName)
            : base(connectionName)
        {
        } 
        #endregion

    }
}
