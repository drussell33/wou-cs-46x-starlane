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
    public class CreateCollectionBetterSteps
    {
        private readonly ScenarioContext _ctx;
        //private Table _userTable;
        private string _hostBaseName = @"https://localhost:5001/";//5001/";
        private readonly IWebDriver _driver;

        public CreateCollectionBetterSteps(ScenarioContext scenarioContext, IWebDriver driver)
        {
            _driver = driver;
            _ctx = scenarioContext;
        }

     
        [Given(@"I am on the '(.*)' page")]
        [When(@"I am on the '(.*)' page")]
        public void WhenIAmOnThePage(string pageName)
        {
            //IWebDriver driver = (IWebDriver)_ctx["WebDriver"];
            if (pageName.Equals("EnvironmentSelection"))
            {
                _driver.Navigate().GoToUrl(_hostBaseName + @"CreateCollection/EnvironmentSelection");
            }
            else
            {
                ScenarioContext.StepIsPending();
            }
        }

        [Given(@"I select the '(.*)' checkbox")]
        [When(@"I select the '(.*)' checkbox")]
        public void GivenISelectTheCheckbox(string buttonId)
        {
            if(buttonId != "null")
            {
                _driver.FindElement(By.Id(buttonId)).Click();
            }
            else
            {
                
            }
        }

        [When(@"I view the gallery iCollection with '(.*)' as the Id")]
        public void IViewThegalleryiCollectionWithAstheId(int collectionID)
        {
            _driver.Navigate().GoToUrl(_hostBaseName + @"gallery_environment?collectionID=" + collectionID);
        }

        [When(@"I view the ocean iCollection with '(.*)' as the Id")]
        public void IViewTheoceaniCollectionWithAstheId(int collectionID)
        {
            _driver.Navigate().GoToUrl(_hostBaseName + @"ocean_environment?collectionID=" + collectionID);
        }

        [Then(@"I can view the '(.*)' title on the page")]
        public void ICanViewThetitleOnThePage(string collectionTitle)
        {
            var thatTitle = _driver.FindElement(By.Id("iCollection-title"));
            if (thatTitle != null)
            {
                Assert.That(thatTitle.Text, Is.EqualTo(collectionTitle));
            }
        }

        [Then(@"I can view the '(.*)' description on the page")]
        public void ICanViewThedescriptionOnThePage(string collectionTitle)
        {
            var thatTitle = _driver.FindElement(By.Id("iCollection-description"));
            if (thatTitle != null)
            {
                Assert.That(thatTitle.Text, Is.EqualTo(collectionTitle));
            }
        }
    }
}
