using Amazon.Contexts;
using Amazon.PageObjects;
using FluentAssertions;
using System;
using TechTalk.SpecFlow;

namespace Amazon.Steps
{
    [Binding]
    public class AmazonClickBasketStepDefinitions : IDisposable
    {
        private readonly WebDriverContext _webDriverContext;
        private readonly SearchContext _searchContext;

        private readonly AmazonDetailPageObject _amazonDetailPageObject;
        private readonly AmazonBasketPageObject _amazonBasketPageObject;

        public AmazonClickBasketStepDefinitions(WebDriverContext webDriverContext, SearchContext searchContext)
        {
            _webDriverContext = webDriverContext;
            _searchContext = searchContext;

            _amazonDetailPageObject = new AmazonDetailPageObject(_webDriverContext.Driver);
            _amazonBasketPageObject = new AmazonBasketPageObject(_webDriverContext.Driver);
        }

        [Given(@"I have clicked the basket")]
        public void GivenIHaveClickedTheBasket()
        {
            _amazonDetailPageObject.NavigateToBasket();
        }
        
        [Then(@"the title of book is correct")]
        public void ThenTheTitleOfBookIsCorrect()
        {
            string itemTitle = _amazonBasketPageObject.FindTitleInBasket();

            itemTitle.Should().Contain(_searchContext.SearchTitle);
        }
        
        [Then(@"the type is correct")]
        public void ThenTheTypeIsCorrect()
        {
            _amazonBasketPageObject.FindTypeInBasket(_searchContext.SearchType);
        }
        
        [Then(@"the quantity is one")]
        public void ThenTheQuantityIsOne()
        {
            int qty = _amazonBasketPageObject.GetQuantityInBasket();

            qty.Should().Be(1);
        }
        
        [Then(@"the price is the same as the one from the searchAnd")]
        public void ThenThePriceIsTheSameAsTheOneFromTheSearchAnd()
        {
            string price = _amazonBasketPageObject.GetPriceInBasket();

            string expectedPrice = String.Format("£{0:0.00}", _searchContext.SearchPrice);
            price.Should().Be(expectedPrice);
        }

        [Then(@"the total price is the same as the price")]
        public void ThenTheTotalPriceIsTheSameAsThePrice()
        {
            string totalPrice = _amazonBasketPageObject.GetTotalPriceInBasket();

            string expectedPrice = String.Format("£{0:0.00}", _searchContext.SearchPrice);
            totalPrice.Should().Be(expectedPrice);

            // Remove the item from the basket (@@@ hook )
            _amazonBasketPageObject.RemoveItemFromBasket();
        }

        public void Dispose()
        {
            _webDriverContext.Driver.Dispose();
        }
    }
}
