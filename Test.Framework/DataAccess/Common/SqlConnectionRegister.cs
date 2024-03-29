﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Test.Framework.Configuration;
using Test.Framework.Extensibility;
using MySql.Data.MySqlClient;
using LightInject;
using System.Data.SqlClient;

namespace Test.Framework.DataAccess
{
    public static class ConnectionRegister
    {
        public static void Register(IList<string> connectionNames, SqlDbmsType dbmsType)
        {
            switch (dbmsType)
            {
                case SqlDbmsType.MySql:
                    MySqlRegisterLightInject(connectionNames);
                    break;
                case SqlDbmsType.SqlServer:
                    SqlServerRegisterLightInject(connectionNames);
                    break;
                case SqlDbmsType.Oracle:
                    throw new NotImplementedException("Oracle Database Implementation Not Present");
                case SqlDbmsType.SqlLite:
                    throw new NotImplementedException("SqlLite Database Implementation Not Present");
                case SqlDbmsType.PostGreSql:
                    throw new NotImplementedException("PostGreSql Database Implementation Not Present");
                default:
                    MySqlRegisterLightInject(connectionNames);
                    break;
            }
        }

        private static void SqlServerRegisterLightInject(IList<string> connectionNames)
        {
            var configuration = Container.Resolve<IWebConfiguration>();
            connectionNames.ToList().ForEach(connectionName => {
                var container = (ServiceContainer)Container.resolver.GetUnderlyingContainer();
                container.Register<IDbConnection>(factory => 
                    new SqlConnection(configuration.ConnectionStrings(connectionName)), 
                    connectionName);
            });
        }

        private static void MySqlRegister(IList<string> connectionNames)
        {
            var configuration = Container.Resolve<IWebConfiguration>();
            connectionNames.ToList().ForEach(connectionName =>
            {
                Container.RegisterInstance<IDbConnection, MySqlConnection>(
                    connectionName, 
                    new MySqlConnection(configuration.ConnectionStrings(connectionName)), 
                    ObjectLifeSpans.Transient);
            });
        }
        private static void MySqlRegisterLightInject(IList<string> connectionNames)
        {
            var configuration = Container.Resolve<IWebConfiguration>();
            connectionNames.ToList().ForEach(connectionName =>
            {
                var container = (ServiceContainer)Container.resolver.GetUnderlyingContainer();
                container.Register<IDbConnection>(factory => 
                    new MySqlConnection(configuration.ConnectionStrings(connectionName)), 
                    connectionName);
            });
        }
    }
}
