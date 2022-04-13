using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoilerPlate.Test
{
    [TestFixtureSource(nameof(BrowsersByDomain))]
    public abstract class BrowserTestBase : TestBase, IDisposable
    {
        private static readonly string[] Browsers = { "Chrome", "Edge", "Firefox" };
        protected static readonly object[] BrowsersByDomain = Sites
            .SelectMany(site => Browsers.Select(browser => new TestConfig(site, browser))).ToArray();

        protected IWebDriver Driver => Config.Driver;
        protected WebDriverWait Waiter(int seconds = 5) => new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));

        protected BrowserTestBase(TestConfig config) : base(config)
        {
        }

        [SetUp]
        public void Setup()
        {
            Dispose();
            Config.Driver = WebDriverFactory.Create(Config.BrowserName);
            Driver.Manage().Window.Maximize();
        }

        [TearDown]
        public virtual void TearDown()
        {
            Dispose();
        }


        #region Helpers
        protected void SetTextByName(string name, string value)
        {
            IWebElement element = Waiter().Until(x => x.FindElement(By.CssSelector($"[name = '{name}']")));
            element.Clear();
            element.SendKeys(value);
        }

        protected void SetTextById(string id, string value)
        {
            IWebElement element = Waiter().Until(x => x.FindElement(By.Id(id)));
            element.Clear();
            element.SendKeys(value);
        }

        protected string GetTextByName(string name)
        {
            IWebElement element = Waiter().Until(x => x.FindElement(By.CssSelector($"[name = '{name}']")));
            return element.Text;
        }

        protected IReadOnlyList<IWebElement> GetByCssSelector(string selector)
        {
            IReadOnlyList<IWebElement> elements = Waiter().Until(x => x.FindElements(By.CssSelector(selector)));
            return elements;
        }

        protected string GetTextById(string id)
        {
            IWebElement element = Waiter().Until(x => x.FindElement(By.Id(id)));
            return element.Text;
        }

        protected void ClickSubmitButton()
        {
            IWebElement submitButton = Waiter().Until(x => x.FindElement(By.CssSelector("[type = 'submit']")));
            submitButton.Click();
        }

        protected bool IsElementRendered(By by)
        {
            try
            {
                Waiter().Until(x => x.FindElement(by));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        public void Dispose()
        {
            if (Driver is not null)
            {
                Driver.Quit();
                Driver.Dispose();
                Config.Driver = null;
            }
        }
    }
}