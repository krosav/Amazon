using Amazon.Contexts;
using Amazon.PageObjects;
using FluentAssertions;
using System;
using TechTalk.SpecFlow;

namespace Amazon.Steps
{
    [Binding]
    public class AmazonAddToBasketStepDefinitions : IDisposable
    {
        private readonly WebDriverContext _webDriverContext;

        private readonly AmazonDetailPageObject _amazonDetailPageObject;
        private readonly AmazonBasketPageObject _amazonBasketPageObject;

        public AmazonAddToBasketStepDefinitions(WebDriverContext webDriverContext)
        {
            _webDriverContext = webDriverContext;

            _amazonDetailPageObject = new AmazonDetailPageObject(_webDriverContext.Driver);
            _amazonBasketPageObject = new AmazonBasketPageObject(_webDriverContext.Driver);
        }

        [Given(@"I have added the item to the basket")]
        public void GivenIHaveAddedTheItemToTheBasket()
        {
            _amazonDetailPageObject.AddItemToBasket();
        }
        
        [Then(@"the notification is shown")]
        public void ThenTheNotificationIsShown()
        {
            _amazonDetailPageObject.CheckNotificationIsShown();
        }
        
        [Then(@"there is one item in the basket")]
        public void ThenThereIsOneItemInTheBasket()
        {
            int noItems = _amazonDetailPageObject.GetNoItemsInBasket();

            noItems.Should().Be(1); // Assert that we have 1 item in the basket

            // Go to the basket
            _amazonDetailPageObject.NavigateToBasket();
            // Remove the item from the basket (just that. The other scenario checks differents assertions.)
            _amazonBasketPageObject.RemoveItemFromBasket(); 
        }

        public void Dispose()
        {
            _webDriverContext.Driver.Dispose();
        }
    }
}
