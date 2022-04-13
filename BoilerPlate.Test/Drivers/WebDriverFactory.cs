using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Edge.SeleniumTools;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using WebDriverManager.DriverConfigs.Impl;

namespace BoilerPlate.Test
{
    public class WebDriverFactory
    {
        public static IWebDriver Create(string browserName)
        {
            bool isDebuggerAttached = System.Diagnostics.Debugger.IsAttached;
            return browserName.ToLowerInvariant() switch
            {
                "chrome" => CreateChromeDriver(isDebuggerAttached),
                "edge" => CreateEdgeDriver(isDebuggerAttached),
                "firefox" => CreateFirefoxDriver(isDebuggerAttached),
                "internetexplorer" => CreateExplorerDriver(isDebuggerAttached),
                _ => throw new NotSupportedException($"The browser '{browserName}' is not supported."),
            };
        }

        private static IWebDriver CreateChromeDriver(bool isDebuggerAttached)
        {
            var driver = new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            var driverDirectory = System.IO.Path.GetDirectoryName(driver) ?? ".";
            var options = new ChromeOptions();
            var tmp = Path.GetTempFileName() + "dir";
            Directory.CreateDirectory(tmp);

            options.AddArguments("user-data-dir=" + tmp.Replace("\\", "/"));
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--headless");

            if (!isDebuggerAttached)
            {
                options.AddArgument("--headless");
            }

            // HACK Workaround for "(unknown error: DevToolsActivePort file doesn't exist)"
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                options.AddArgument("--no-sandbox");

            return new ChromeDriver(driverDirectory, options);
        }

        private static IWebDriver CreateEdgeDriver(bool isDebuggerAttached)
        {
            var driver = new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            var driverDirectory = System.IO.Path.GetDirectoryName(driver) ?? ".";

            var options = new EdgeOptions()
            {
                UseChromium = true,
            };

            if (!isDebuggerAttached)
                options.AddArgument("--headless");


            return new EdgeDriver(driverDirectory, options);
        }

        private static IWebDriver CreateFirefoxDriver(bool isDebuggerAttached)
        {
            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.AcceptInsecureCertificates = true;

            if (!isDebuggerAttached)
                firefoxOptions.AddArgument("--headless");

            var driver = new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            var driverDirectory = System.IO.Path.GetDirectoryName(driver) ?? ".";

            return new FirefoxDriver(driverDirectory, firefoxOptions, TimeSpan.FromSeconds(20));
        }

        private static IWebDriver CreateExplorerDriver(bool isDebuggerAttached)
        {
            var driver = new WebDriverManager.DriverManager().SetUpDriver(new InternetExplorerConfig());
            var driverDirectory = Path.GetDirectoryName(driver) ?? ".";

            return new InternetExplorerDriver(driverDirectory, new InternetExplorerOptions {IgnoreZoomLevel = true});
        }
    }
}