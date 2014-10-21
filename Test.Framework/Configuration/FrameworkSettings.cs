using System;
using Test.Framework.Extensions;
using Test.Framework.Extensibility;

namespace Test.Framework.Configuration
{
    public static class FrameworkSettings
    {
        private static IWebConfiguration configuration = Container.Resolve<IWebConfiguration>();

        public static string NoReplyEmail { get { return configuration.AppSettings["NoReplyEmail"]; } }
        public static string CryptoSaltValue { get { return configuration.AppSettings["CryptoSaltValue"]; } }
        public static string CryptoPassPhrase { get { return configuration.AppSettings["CryptoPassPhrase"]; } }
        public static string CryptoInitVector { get { return configuration.AppSettings["CryptoInitVector"]; } }
        public static string TemplateDirectory { get { return configuration.AppSettings["TemplateDirectory"]; } }
        public static int CryptoKeySize { get { return configuration.AppSettings["CryptoKeySize"].ToInteger(); } }
        public static bool EnableSSLMail { get { return configuration.AppSettings["EnableSSLMail"].ToBoolean(); } }
        public static string NoReplyDisplayName { get { return configuration.AppSettings["NoReplyDisplayName"]; } }
        public static string CryptoHashAlgorithm { get { return configuration.AppSettings["CryptoHashAlgorithm"]; } }
        public static int CryptoPasswordIterations { get { return configuration.AppSettings["CryptoPasswordIterations"].ToInteger(); } }
        public static double CachingIntervalInMinutes { get { return configuration.AppSettings["CachingIntervalInMinutes"].ToDouble(); } }
        public static string MembaseSessionTimeoutInMinutes { get { return configuration.AppSettings["MembaseSessionTimeoutInMinutes"]; } }
    }
}
