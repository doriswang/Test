using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Entities.Models
{
    public class ZipCode : IDataModel
    {
        public string Code { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string City { get; set; }
        public string CBSA { get; set; }

        public override string ToString()
        {
            return new StringBuilder()
            .AppendFormat("Zip: {0}{1}", this.Code, Environment.NewLine)
            .AppendFormat("City: {0}{1}", this.City, Environment.NewLine)
            .AppendFormat("State: {0}{1}", this.StateCode, Environment.NewLine)
            .AppendFormat("CBSA: {0}{1}", this.CBSA, Environment.NewLine)
            .AppendFormat("{0}", Environment.NewLine).ToString();
        }
    }
}
