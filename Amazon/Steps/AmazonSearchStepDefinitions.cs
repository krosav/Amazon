using Amazon.Contexts;
using Amazon.PageObjects;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Amazon.Steps
{
    [Binding]
    public class AmazonSearchStepDefinitions : IDisposable
    {
        private readonly WebDriverContext _webDriverContext;
        private readonly SearchContext _searchContext;

        private readonly AmazonPageObject _amazonPageObject;

        public AmazonSearchStepDefinitions(WebDriverContext webDriverContext, SearchContext searchContext)
        {
            _webDriverContext = webDriverContext;
            _searchContext = searchContext;
            _amazonPageObject = new AmazonPageObject(_webDriverContext.Driver);
        }

        [Given(@"I have navigated to the Amazon website (.*)")]
        public void GivenIHaveNavigatedToTheAmazonWebsite(string url)
        {
            var driver = _webDriverContext.Driver;

            driver.Navigate().GoToUrl("https://" + url);
            Assert.IsTrue(driver.Title.ToLower().Contains(url));
            driver.Manage().Window.Maximize(); // Maximize the browser window

            _amazonPageObject.AcceptCookies(); // If needed, click on the Accept Cookies btn
            _amazonPageObject.GoToBooksSection(); // Go to the Books section
        }

        [Given(@"I have entered (.*) in the search type is (.*)")]
        public void GivenIHaveEnteredTitleInTheSearch(string title, string type)
        {
            _searchContext.SearchTitle = title; // Remember the book title
            _searchContext.SearchType = type;   // Remember the type, ex. Paperback
            _amazonPageObject.EnterTitleInSearchInput(title);
        }

        [Then(@"we have an item with the correct title")]
        public void ThenWeHaveAnItemWithTheCorrectTitle()
        {
            _amazonPageObject.FindItemWithCorrectTitle(_searchContext.SearchTitle);
        }

        [Then(@"the item does not have a badge")]
        public void ThenTheItemDoesNotHaveABadge()
        {
            _amazonPageObject.CheckItemDoesNotHaveABadge(_searchContext.SearchTitle);
        }

        [Then(@"we have a type (.*)")]
        public void ThenWeHaveAType(string type)
        {
            _amazonPageObject.CheckItemHasType(type);
        }

        public void Dispose()
        {
            _webDriverContext.Driver.Dispose();
        }
    }
}
