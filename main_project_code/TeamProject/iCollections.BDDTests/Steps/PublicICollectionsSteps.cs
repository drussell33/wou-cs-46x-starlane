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
    public sealed class PublicICollectionSteps
    {

        private readonly ScenarioContext _ctx;
        // private string _hostBaseName = @"https://icollections.azurewebsites.net/";
        private string _hostBaseName = @"https://localhost:5001/";

        private readonly IWebDriver _driver;

        public PublicICollectionSteps(ScenarioContext scenarioContext, IWebDriver driver)
        {
            _driver = driver;
            _ctx = scenarioContext;
        }

        [When(@"I click on the '(.*)'")]
        public void WhenIClickOn(string Link)
        {
            _driver.FindElement(By.LinkText(Link)).Click();
        }

        [Then(@"I can see a registered user's public iCollections")]
        public void SeePublicICollections()
        {
            var body = _driver.FindElement(By.TagName("tbody"));
            Assert.That(body,Is.Not.Null);
        }
    }
}