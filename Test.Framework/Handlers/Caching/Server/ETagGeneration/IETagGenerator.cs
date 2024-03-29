﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Test.Framework.Handlers.Caching.Server.ETagGeneration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IETagGenerator
    {
        EntityTagHeaderValue Generate(string url, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders);
    }
}
