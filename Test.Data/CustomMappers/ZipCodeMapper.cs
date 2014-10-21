using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Entities.Models;
using Test.Framework.DataAccess;

namespace Test.Data.Mappers
{
    public class ZipCodeMapper : ISelectable<ZipCode>
    {
        #region ISelectable<ZipCode> Members

        public ZipCode ApplySelect(DataReader reader)
        {
            return new ZipCode
            {
                Code = reader.GetStringNullable("Code"),
                CBSA = reader.GetStringNullable("CBSA"),
                StateCode = reader.GetStringNullable("StateCode"),
                City = reader.GetStringNullable("City"),
                StateName = reader.GetStringNullable("StateName")
            };
        }

        public ZipCode ApplySelect(DataReader reader, ISet<string> columns)
        {
            var result = new ZipCode();

            if (columns.Contains("Code")) result.Code = reader.GetStringNullable("Code");
            if (columns.Contains("CBSA")) result.CBSA = reader.GetStringNullable("CBSA");
            if (columns.Contains("StateCode")) result.StateCode = reader.GetStringNullable("StateCode");
            if (columns.Contains("StateName")) result.StateName = reader.GetStringNullable("StateName");
            if (columns.Contains("City")) result.City = reader.GetStringNullable("City");

            return result;
        }

        #endregion
    }
}
