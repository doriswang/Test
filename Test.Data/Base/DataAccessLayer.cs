using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.Mappers;
using Test.Framework.Configuration;
using Test.Framework.DataAccess;
using Test.Framework.Extensibility;

namespace Test.Data
{
    public static class DataAccessLayer
    {
        public static void Initialize()
        {
            SqlDbmsType dbType = SqlDbmsType.SqlServer;
            var configuration = Container.Resolve<IWebConfiguration>();
            List<string> connectionStringNames = configuration.GetConnectionStringNames().ToList();
            ConnectionRegister.Register(connectionStringNames, dbType);
            OrmRegister.Register(connectionStringNames, OrmType.Dapper, dbType);
            DbRegister.Register(connectionStringNames, dbType);
            Container.Register<IDataProvider, DataProvider>(ObjectLifeSpans.Singleton);
            DataRegister.Initialize();
            EntityMap.Initialize();
        }
    }
}
