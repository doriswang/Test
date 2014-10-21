using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.Mappers;
using Test.Entities.Models;
using Test.Framework.DataAccess;

namespace Test.Data
{
    public static class EntityMap
    {
        public static void Initialize()
        {
            CustomMapper.Register<ZipCode>(new ZipCodeMapper());
        }
    }
}
