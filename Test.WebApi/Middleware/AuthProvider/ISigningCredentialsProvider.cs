using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.WebApi.Middleware
{
    public interface ISigningCredentialsProvider
    {
        SigningCredentials GetSigningCredentials(string issuer, string audience);
    }
}
