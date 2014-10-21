using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Entities;
using Test.Framework.DataAccess;

namespace Test.Data
{
    public static class DataRegister
    {
        public static void Initialize()
        {
            var type = typeof(IDataModel);

            var currentAssemblyTypes = AppDomain.CurrentDomain.GetAssemblies()
                .ToList()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract).ToList();

            currentAssemblyTypes.ForEach(t =>
            {
                PropertyCache.Register(t);
            });
        }
    }
}
