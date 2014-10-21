using System.Diagnostics;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;

namespace Test.Framework.Configuration
{
    public class WebConfiguration : IWebConfiguration
    {
        #region IWebConfiguration Members

        public NameValueCollection AppSettings
        {
            [DebuggerStepThrough]
            get
            {
                return ConfigurationManager.AppSettings;
            }
        }

        [DebuggerStepThrough]
        public string ConnectionStrings(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public IEnumerable<string> GetConnectionStringNames()
        {
            var connectionStrings = ConfigurationManager.ConnectionStrings;
            foreach (ConnectionStringSettings item in connectionStrings)
            {
                if (item.Name.ToLower().Contains("connectionstring")) yield return item.Name;
            }
        }

        [DebuggerStepThrough]
        public object GetSection(string sectionName)
        {
            return ConfigurationManager.GetSection(sectionName);
        }

        [DebuggerStepThrough]
        public T GetSection<T>(string sectionName)
        {
            return (T)ConfigurationManager.GetSection(sectionName);
        }

        public IEnumerable<string> GetCacheStringNames()
        {
            var cacheStrings = ConfigurationManager.GetSection("cacheStrings");
            if (cacheStrings == null) return null;
            return new List<string>();
        }

        #endregion
    }
}
