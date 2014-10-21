using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Test.Framework.DataAccess;

namespace Test.Framework.Extensions
{
    public static class SqlCommandExtensions
    {
        [DebuggerStepThrough]
        public static void Add(this IList<SqlCommand> queries, SqlCommand query, int timeout = 15, string errorMessage = null)
        {
            if (queries == null) return;
            queries.Add(new SqlCommand(query.Statement, query.Parameters, timeout, errorMessage));
        }
    }
}
