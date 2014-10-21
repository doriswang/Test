using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Test.Framework.Configuration
{
    public interface IWebConfiguration
    {
        NameValueCollection AppSettings
        {
            get;
        }

        string ConnectionStrings(string name);
        object GetSection(string sectionName);
        T GetSection<T>(string sectionName);

        IEnumerable<string> GetConnectionStringNames();
        IEnumerable<string> GetCacheStringNames();
    }
}
