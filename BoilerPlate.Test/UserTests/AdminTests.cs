using BoilerPlate.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;

namespace BoilerPlate.Test
{
    [Category("Admin")]
    public class AdminTests : BrowserTestBase
    {
        private const string AdminUsername = "admin";
        private const string AdminPassword = "qwert123";
        
        private AdminSignInPageObject _signinPageObject;

        public AdminTests(TestConfig config) : base(config)
        {
        }

        [SetUp]
        public new void Setup()
        {
            _signinPageObject = new AdminSignInPageObject(Driver, Config);
            _signinPageObject.GoToAdminPage();
        }

        [TestCase]
        public void Admin_SignIn_HappyPath()
        {
            _signinPageObject.SetSingInPageFields(AdminUsername, AdminPassword);
            _signinPageObject.SubmitButton_Click();
          
            Assert.AreEqual($"{Config.RootUrl}settings/{(short)Countries.UK}", Driver.Url);
        }

        [TestCase]
        public void Admin_SignIn_UserNameIsEmpty_ShouldNotBeSignedIn()
        {
            _signinPageObject.SetSingInPageFields(string.Empty, AdminPassword);
            _signinPageObject.SubmitButton_Click();

            var validationMessage = GetTextById("UserName-error");
            Assert.AreEqual(validationMessage, "The UserName field is required.");
        }

        [TestCase]
        public void Admin_SignIn_PasswordIsEmpty_ShouldNotBeSignedIn()
        {
            _signinPageObject.SetSingInPageFields(AdminUsername, string.Empty);
            _signinPageObject.SubmitButton_Click();

            var validationMessage = GetTextById("Password-error");
            Assert.AreEqual(validationMessage, "The Password field is required.");
        }

        [TestCase]
        public void Admin_SignIn_PasswordAndUserNameAreEmpty_ShouldNotBeSignedIn()
        {
            _signinPageObject.SetSingInPageFields(string.Empty, string.Empty);
            _signinPageObject.SubmitButton_Click();

            var passwordValidationMessage = GetTextById("Password-error");
            var userNameValidationMessage = GetTextById("UserName-error");

            Assert.AreEqual(passwordValidationMessage, "The Password field is required.");
            Assert.AreEqual(userNameValidationMessage, "The UserName field is required.");
        }

        [TestCase]
        public void AnonymousUser_AccessHangfire_DashboardShouldNotBeDIsplayed()
        {
            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Navigate().GoToUrl($"{Config.RootUrl}hangfire");

            Assert.IsFalse(IsElementRendered(By.Id("hangfireConfig")));
        }

        [TestCase]
        public void Admin_UserNameOrPasswordIsWrong_ShouldNotBeSignedIn()
        {
            var signInModel = new SignInModelFaker().Generate();

            _signinPageObject.SetSingInPageFields(signInModel.UserName, signInModel.Password);
            _signinPageObject.SubmitButton_Click();

            var validationMessages = Driver.FindElements(By.XPath("/html/body/div/main/div/div/form/div[1]/ul/li"));
            Assert.IsTrue(validationMessages.Any(x=>x.Text == "Invalid Login Attempt"));
        }
    }
}