using OpenQA.Selenium;
using System;

namespace Amazon.PageObjects
{
    class AmazonDetailPageObject : AmazonBasePageObject
    {
        // UI elements locators. If they change, there is only one place - here - where we should change them.
        By titleLocator = By.Id("productTitle");
        By subtitleLocator = By.Id("productSubtitle");
        By priceLocator = By.Id("price");
        By badgeLocator = By.XPath("//div[@id='chartsBadge_feature_div']//a[@class='badge-link']");
        By addToBasketLocator = By.Id("add-to-cart-button");
        By notificationAddedToBasketLocator = By.XPath("//div[@id='sw-atc-details-single-container']//span[contains(text(), 'Added to Basket')]");
        By noItemsInBasket = By.Id("nav-cart-count");
        By goToBasketLocator = By.XPath("//span[@id='sw-gtc']//a[contains(text(), 'Go to basket')]");

        public AmazonDetailPageObject(IWebDriver webDriver) : base(webDriver)
        {
        }

        public string CheckTitle()
        {
            IWebElement itemTitle = WaitAndGetElement(titleLocator);

            // We found the title in the detail.
            return itemTitle.Text;
        }

        public string CheckType()
        {
            IWebElement itemSubTitle = WaitAndGetElement(subtitleLocator);

            // We found the subtitle in the detail.
            return itemSubTitle.Text;
        }

        public string CheckPriceIsSameAsInSearch()
        {
            IWebElement itemPrice = WaitAndGetElement(priceLocator);

            return itemPrice.Text;
        }

        public IWebElement CheckDoesNotHaveBadge()
        {
            IWebElement itemBadge = FindOptionalComponent(badgeLocator);
            // We should not have a badge in the details page.
            return itemBadge;
        }

        public void AddItemToBasket()
        {
            // Click the Add to basket button
            WaitAndGetElement(addToBasketLocator).Click();
        }

        public void CheckNotificationIsShown()
        {
            // Check that the notification 'Added to Basket' has appeared.
            WaitAndGetElement(notificationAddedToBasketLocator);
        }

        public int GetNoItemsInBasket()
        {
            IWebElement noItems = WaitAndGetElement(noItemsInBasket);

            return Int32.Parse(noItems.Text);
        }

        public void NavigateToBasket()
        {
            WaitAndGetElement(goToBasketLocator).Click();
        }
    }
}
