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

namespace Fuji.BDDTests.Steps
{
    public class TestiCollectionPost
    {
        public string Owner { get; set; }
        public string CollectionName { get; set; }
    }

    [Binding]
    public sealed class ProfilePicturesSteps
    {

        private readonly ScenarioContext _ctx;
        private string _hostBaseName = @"https://localhost:5001/";
        private readonly IWebDriver _driver;

        public ProfilePicturesSteps(ScenarioContext scenarioContext, IWebDriver driver)
        {
            _driver = driver;
            _ctx = scenarioContext;
        }

        [Given(@"I am logged in")]
        public void WhenILogin()
        {
            _driver.Navigate().GoToUrl(_hostBaseName + @"Identity/Account/Login");
            // Enter email and password into the input fields
            _driver.FindElement(By.Id("Input_Email")).SendKeys("clark@example.com");
            _driver.FindElement(By.Id("Input_Password")).SendKeys("Abcd987?6");  // could submit form by "hitting enter" with (u.Password + Keys.Enter)
            // can "submit" the form by calling submit on any element in the form or actually click the submit button
            _driver.FindElement(By.Id("account")).FindElement(By.CssSelector("button[type=submit]")).Click();
        }

        [Given(@"others I follow/friends with have posted iCollections")]
        public void OthersPostedICollections(Table table)
        {
            IEnumerable<TestiCollectionPost> postediCollections = table.CreateSet<TestiCollectionPost>();
            _ctx["Collections"] = postediCollections;
        }

        [When(@"I go to the '(.*)' page")]
        public void WhenIGoToThePage(string pageName)
        {
            if (pageName.Equals("Dashboard"))
            {
                _driver.Navigate().GoToUrl(_hostBaseName + @"Dashboard");
            }
            else
            {
                ScenarioContext.StepIsPending();
            }
        }

        [Then(@"the event will show the profile picture of the user that posted")]
        public void ShowProfilePicture()
        {
            IWebElement avatar = _driver.FindElement(By.ClassName("avatar"));
            Assert.That(avatar.TagName, Is.EqualTo("img"));
        }
    }
}