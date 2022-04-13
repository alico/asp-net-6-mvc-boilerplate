using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace BoilerPlate.Test
{
    public class AdminSignInPageObject
    {
        private IWebDriver _driver;
        private TestConfig _config;

        public AdminSignInPageObject(IWebDriver webDriver, TestConfig config)
        {
            _driver = webDriver;
            _config = config;
        }

        private IWebElement _userNameTextField => _driver.FindElement(By.Name("UserName"));
        private IWebElement _passwordTextField => _driver.FindElement(By.Name("Password"));
        private IWebElement _submitButton => _driver.FindElement(By.ClassName("btn-primary"));

        public void SetSingInPageFields(string userName, string password)
        {
            _userNameTextField.Clear();
            _userNameTextField.SendKeys(userName);

            _passwordTextField.Clear();
            _passwordTextField.SendKeys(password);
        }

        public void SubmitButton_Click()
        {
            _submitButton.Click();
        }

        public void GoToAdminPage()
        {
            _driver.Navigate().GoToUrl($"{_config.RootUrl}admin");
        }
    }
}
