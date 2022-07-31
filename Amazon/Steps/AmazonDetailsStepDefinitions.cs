using Amazon.Contexts;
using Amazon.PageObjects;
using FluentAssertions;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace Amazon.Steps
{
    [Binding]
    public class AmazonDetailsStepDefinitions : IDisposable
    {
        private readonly WebDriverContext _webDriverContext;
        private readonly SearchContext _searchContext;

        private readonly AmazonPageObject _amazonPageObject;
        private readonly AmazonDetailPageObject _amazonDetailPageObject;

        public AmazonDetailsStepDefinitions(WebDriverContext webDriverContext, SearchContext searchContext)
        {
            _webDriverContext = webDriverContext;
            _searchContext = searchContext;

            _amazonPageObject = new AmazonPageObject(_webDriverContext.Driver);
            _amazonDetailPageObject = new AmazonDetailPageObject(_webDriverContext.Driver);
        }

        [Given(@"I have navigated to the detail of the item")]
        public void GivenIHaveNavigatedToTheDetailOfThe()
        {
            // Get the item displayed in the search page
            _amazonPageObject.FindItemWithCorrectTitle(_searchContext.SearchTitle);

            // Before navigating to the detail, find the price and check that the price is > 0
            _searchContext.SearchPrice = _amazonPageObject.GetSearchPrice();
            // We cannot make a more precise assert, the start and end of promotions can change the price.
            // But we will check that in details page and in the basket the price is the same.
            _searchContext.SearchPrice.Should().BeGreaterThan(0.0);

            // Find the web element corresponding to the correct type. ex. Paperback
            _amazonPageObject.CheckItemHasType(_searchContext.SearchType);

            _amazonPageObject.NavigateToDetails();
        }

        [Then(@"the detailed item has the correct title")]
        public void ThenTheDetailedItemHasTheCorrectTitle()
        {
            string title = _amazonDetailPageObject.CheckTitle();
            // Get the title and assert it contains the one from the search.
            title.Should().Contain(_searchContext.SearchTitle);
        }

        [Then(@"the detailed item has the correct type")]
        public void ThenTheDetailedItemHasTheCorrectType()
        {
            string type = _amazonDetailPageObject.CheckType();
            // Get the type and assert it contains the one from the search.
            type.Should().Contain(_searchContext.SearchType);
        }

        [Then(@"the detailed item does not have a best-seller badge")]
        public void ThenTheDetailedItemDoesNotHaveABest_SellerBadge()
        {
            IWebElement badge = _amazonDetailPageObject.CheckDoesNotHaveBadge();
            // There should not be a badge.
            badge.Should().BeNull();
        }

        [Then(@"the price is the same as the one from the search")]
        public void ThenThePriceIsTheSameAsTheOneFromTheSearch()
        {
            string price = _amazonDetailPageObject.CheckPriceIsSameAsInSearch();

            string expectedPrice = String.Format("£{0:0.00}", _searchContext.SearchPrice);
            price.Should().Be(expectedPrice);
        }

        public void Dispose()
        {
            _webDriverContext.Driver.Dispose();
        }
    }
}
