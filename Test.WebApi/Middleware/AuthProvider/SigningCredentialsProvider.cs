using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using Test.Data;
using Thinktecture.IdentityModel;

namespace Test.WebApi.Middleware
{
    public class SigningCredentialsProvider : ISigningCredentialsProvider
    {
        private IDataProvider dataProvider;
        public SigningCredentialsProvider(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public SigningCredentials GetSigningCredentials(string issuer, string audience)
        {
            if (issuer != AppSettings.JwtIssuerName ||
                audience != AppSettings.JwtAllowedAudience)
                return null;

            var symmetricKey = Convert.FromBase64String(AppSettings.JwtSigningKey);
            //var symmetricKey = new RandomBufferGenerator(256 / 8).GenerateBufferFromSeed(256 / 8);
            return new SigningCredentials(
                        new InMemorySymmetricSecurityKey(symmetricKey),
                        "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256",
                        "http://www.w3.org/2001/04/xmlenc#sha256");
        }

        //public SigningCredentials GetSigningCredentials(string issuer, string audience)
        //{
        //    RSACryptoServiceProvider publicAndPrivate = new RSACryptoServiceProvider();
        //    RsaKeyGenerationResult keyGenerationResult = GenerateRsaKeys();
        //    publicAndPrivate.FromXmlString(keyGenerationResult.PublicAndPrivateKey);

        //    return new SigningCredentials(new RsaSecurityKey(publicAndPrivate), SecurityAlgorithms.RsaSha256Signature, SecurityAlgorithms.Sha256Digest);
        //}

        //public SigningCredentials GetSigningCredentials(string issuer, string audience)
        //{
        //    var cert = X509.LocalMachine.My.SubjectDistinguishedName.Find("CN=as.local", false).First();
        //    return new X509SigningCredentials(cert);
        //}

        private static RsaKeyGenerationResult GenerateRsaKeys()
        {
            RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider(2048);
            RSAParameters publicKey = myRSA.ExportParameters(true);
            RsaKeyGenerationResult result = new RsaKeyGenerationResult();
            result.PublicAndPrivateKey = myRSA.ToXmlString(true);
            result.PublicKeyOnly = myRSA.ToXmlString(false);
            return result;
        }
    }
    public class RsaKeyGenerationResult
    {
        public string PublicKeyOnly { get; set; }
        public string PublicAndPrivateKey { get; set; }
    }
    public class RandomBufferGenerator
    {
        private readonly Random _random = new Random();
        private readonly byte[] _seedBuffer;

        public RandomBufferGenerator(int maxBufferSize)
        {
            _seedBuffer = new byte[maxBufferSize];

            _random.NextBytes(_seedBuffer);
        }

        public byte[] GenerateBufferFromSeed(int size)
        {
            int randomWindow = _random.Next(0, size);

            byte[] buffer = new byte[size];

            Buffer.BlockCopy(_seedBuffer, randomWindow, buffer, 0, size - randomWindow);
            Buffer.BlockCopy(_seedBuffer, 0, buffer, size - randomWindow, randomWindow);

            return buffer;
        }
    }
}
