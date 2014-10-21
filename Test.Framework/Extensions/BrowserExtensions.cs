using System;
using System.Web;
using System.Diagnostics;

namespace Test.Framework.Extensions
{
    public static class BrowserExtensions
    {
        [DebuggerStepThrough]
        public static bool IsBrowserAllowed(this HttpBrowserCapabilities browser)
        {
            if (browser.Browser.IsEqual("IE") && browser.MajorVersion < 8)
            {
                return false;
            }

            if (browser.Browser.IsEqual("Firefox") && browser.MajorVersion < 3)
            {
                return false;
            }

            return true;
        }

        [DebuggerStepThrough]
        public static bool IsBrowserAllowed(this HttpBrowserCapabilitiesBase browser)
        {
            if (browser.Browser.IsEqual("IE") && browser.MajorVersion < 8)
            {
                return false;
            }

            if (browser.Browser.IsEqual("Firefox") && browser.MajorVersion < 3)
            {
                return false;
            }

            return true;
        }
    }
}
