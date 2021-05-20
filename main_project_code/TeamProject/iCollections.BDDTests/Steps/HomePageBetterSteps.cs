using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace iCollections.BDDTests.Steps
{
    [Binding]
    public class HomePageBetterSteps
    {
        private readonly ScenarioContext _ctx;
        //private Table _userTable;
        private string _hostBaseName = @"https://localhost:5001/";//5001/";
        private readonly IWebDriver _driver;

        public HomePageBetterSteps(ScenarioContext scenarioContext, IWebDriver driver)
        {
            _driver = driver;
            _ctx = scenarioContext;
        }


        [Given(@"I am on the Home Page")]
        public void GivenIAmOnTheHomePage()
        {
            _driver.Navigate().GoToUrl(_hostBaseName);
        }

        [Given(@"I am a User")]
        public void GivenIAmAUser()
        {
            // Doesn't matter which user, so pick one
            IEnumerable<TestUser> users = (IEnumerable<TestUser>)_ctx["Users"];
            _ctx["FirstName"] = users.First().FirstName;
        }


        [When(@"I Click the '(.*)' button")]
        public void WhenIClickTheButton(string buttonId)
        {
            _driver.FindElement(By.Id(buttonId)).Click();
        }

        [When(@"I Click the '(.*)' Dropdown button")]
        public void WhenIClickTheDropdownButton(string buttonId)
        {
            _driver.FindElement(By.Id("loginDropdown")).Click();
            _driver.FindElement(By.Id(buttonId)).Click();
        }

        [Then(@"I am redirected to the '(.*)' page")]
        public void ThenIAmRedirectedToThePage(string pageName)
        {
            //IWebDriver driver = (IWebDriver)_ctx["WebDriver"];
            if (pageName.Equals("Dashboard"))
            {
                Assert.That(_driver.Url, Is.EqualTo(_hostBaseName + @"Dashboard"));
            }
            else if (pageName.Equals("ocean_environment"))
            {
                Assert.That(_driver.Url, Is.EqualTo(_hostBaseName + @"ocean_environment"));
            }
            else if (pageName.Equals("gallery_environment"))
            {
                Assert.That(_driver.Url, Is.EqualTo(_hostBaseName + @"gallery_environment"));
            }
            else if (pageName.Equals("EnvironmentSelection"))
            {
                Assert.That(_driver.Url, Is.EqualTo(_hostBaseName + @"CreateCollection/EnvironmentSelection"));
            }
            else if (pageName.Equals("PhotoSelection"))
            {
                Assert.That(_driver.Url, Is.EqualTo(_hostBaseName + @"CreateCollection/PhotoSelection"));
            }
            else
            {
                Assert.Fail();
            }
        }

        [When(@"I am a logged in user on the HomePage")]
        [Given(@"I am a logged in user on the HomePage")]
        public void IamaloggedinuserontheHomePage()
        {
            _driver.Navigate().GoToUrl(_hostBaseName + @"Identity/Account/Login");
            string firstName = (string)_ctx["FirstName"];
            IEnumerable<TestUser> users = (IEnumerable<TestUser>)_ctx["Users"];
            TestUser u = users.Where(u => u.FirstName == firstName).FirstOrDefault();
            _driver.Navigate().GoToUrl(_hostBaseName + @"Identity/Account/Login");
            _driver.FindElement(By.Id("Input_Email")).SendKeys(u.Email);
            _driver.FindElement(By.Id("Input_Password")).SendKeys(u.Password);
            _driver.FindElement(By.Id("account")).FindElement(By.CssSelector("button[type=submit]")).Click();
            _driver.Navigate().GoToUrl(_hostBaseName);
        }


      

    }
}
