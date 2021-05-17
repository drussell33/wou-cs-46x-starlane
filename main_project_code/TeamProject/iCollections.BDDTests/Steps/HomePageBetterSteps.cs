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

        [When(@"I Click the '(.*)' button")]
        public void WhenIClickTheButton(string buttonId)
        {
            _driver.FindElement(By.Id(buttonId)).Click();
        }
    }
}
