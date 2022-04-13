using OpenQA.Selenium;
using System;

namespace BoilerPlate.Test
{
    public class TestConfig : ICloneable
    {
        public IWebDriver Driver { get; set; }
        public string SiteName { get; set; }
        public string Domain { get; set; }
        public string RootUrl => $"https://{Domain}/";
        public string RootUrlInsecure => $"http://{Domain}/";
        public string BrowserName { get; set; }

        public TestConfig(string siteName, string domain)
        {
            SiteName = siteName;
            Domain = domain;
        }
        public TestConfig(TestConfig domainConfig, string browserName)
        {
            Driver = domainConfig.Driver;
            SiteName = domainConfig.SiteName;
            Domain = domainConfig.Domain;
            BrowserName = browserName;
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(BrowserName))
            {
                return SiteName;
            }
            return $"{SiteName} - {BrowserName}";
        }

        public object Clone()
        {
            return new TestConfig(this, BrowserName);
        }
    }
}