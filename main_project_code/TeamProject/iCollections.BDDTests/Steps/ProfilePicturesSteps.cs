using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuji.BDDTests.Steps;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace iCollections.BDDTests.Steps
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
        // private string _hostBaseName = @"https://icollections.azurewebsites.net/";
        private string _hostBaseName = @"https://localhost:5001/";
        //private string _hostBaseName = @"https://localhost:44372/";
        

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
            string firstName = (string)_ctx["FirstName"];
            IEnumerable<TestUser> users = (IEnumerable<TestUser>)_ctx["Users"];
            TestUser u = users.Where(u => u.FirstName == firstName).FirstOrDefault();
            _driver.Navigate().GoToUrl(_hostBaseName + @"Identity/Account/Login");
            _driver.FindElement(By.Id("Input_Email")).SendKeys(u.Email);
            _driver.FindElement(By.Id("Input_Password")).SendKeys(u.Password);
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
            IWebElement avatar = _driver.FindElement(By.ClassName("profile-pic"));
            Assert.That(avatar.TagName, Is.EqualTo("img"));
        }

        [Given(@"I am on '(.*)' profile page")]
        public void IAmOnUserProfilePage(string User)
        {
            _driver.Navigate().GoToUrl(_hostBaseName + @"userpage/" + User);
        }

        [When(@"I go to '(.*)' following page")]
        public void IGoToUserFollowingPage(string User)
        {
            _driver.Navigate().GoToUrl(_hostBaseName + @"userpage/" + User + "/following");
        }

        [Then(@"the followees profile pictures show")]
        public void ShowFolloweesPictures()
        {
            IEnumerable<IWebElement> avatar = _driver.FindElements(By.ClassName("profile-pic"));
            Assert.That(avatar.First().TagName, Is.EqualTo("img"));
        }

        [When(@"I go to '(.*)' follower page")]
        public void IGoToUserFollowerPage(string User)
        {
            _driver.Navigate().GoToUrl(_hostBaseName + @"userpage/" + User + "/followers");
        }

        [Then(@"the followers profile pictures show")]
        public void ShowFollowersPictures()
        {
            IEnumerable<IWebElement> avatar = _driver.FindElements(By.ClassName("profile-pic"));
            Assert.That(avatar.First().TagName, Is.EqualTo("img"));
        }
    }
}