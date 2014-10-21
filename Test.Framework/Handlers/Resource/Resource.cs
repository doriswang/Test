using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Framework.Validation;

namespace Test.Framework.Handlers.Resource
{
    public abstract class Resource
    {
        private readonly List<Link> links;

        public IEnumerable<Link> Links { get { return links; } }

        public Resource()
        {
            links = new List<Link>();
        }

        public void AddLink(Link link)
        {
            Ensure.Argument.NotNull(link, "link");
            links.Add(link);
        }
    }
}
