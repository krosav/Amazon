using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Amazon.PageObjects
{
    class AmazonPageObject : AmazonBasePageObject
    {

        private IWebElement? _foundParent;
        private IWebElement? _foundType;

        // UI elements locators. If they change, there is only one place - here - where we should change them.
        private By acceptCookiesBtnBtnLocator = By.Id("sp-cc-accept");
        private By bookSectionBtnLocator = By.XPath("//div[@id='nav-main']//a[contains(@href,'books') and contains(text(), 'Books')]");
        private By searchInputLocator = By.Id("twotabsearchtextbox");
        private By searchMagnifyingGlassLocator = By.Id("nav-search-submit-button");
        private By badgeLocator = By.XPath(".//span[@data-component-type='s-search-results']//span[@data-component-type='s-status-badge-component']");
        private By searchPriceLocator = By.XPath(".//span[@class='a-price']");
        private By searchPriceWholePartLocator = By.XPath(".//span[@class='a-price-whole']");
        private By searchPriceFractionPartLocator = By.XPath(".//span[@class='a-price-fraction']");

        public AmazonPageObject(IWebDriver webDriver) : base(webDriver)
        {
        }

        public void AcceptCookies()
        {
            // This Accept Cookies btn is optional and not necessarily part of the page.
            IWebElement? AcceptCookiesBtn = FindOptionalComponent(acceptCookiesBtnBtnLocator);

            if (AcceptCookiesBtn != null)
                AcceptCookiesBtn.Click(); // If present, click it. If not, we continue;
        }

        public void GoToBooksSection()
        {
            WaitAndGetElement(bookSectionBtnLocator).Click();
        }

        public void EnterTitleInSearchInput(string title)
        {
            IWebElement searchInput = WaitAndGetElement(searchInputLocator);

            searchInput.Clear();   // Clear the input
            searchInput.SendKeys(title); // Type the text

            WaitAndGetElement(searchMagnifyingGlassLocator).Click(); // Click the magnifying glass submit btn to do the search
        }

        public void FindItemWithCorrectTitle(string title)
        {
            WaitAndGetElement(By.XPath("//span[@data-component-type='s-search-results']//div[@data-index]"));
            // In this function, we want to find the book item with the correct title, on the page.
            // We also want to preserve the parent so that later we could look for the presence of a badge
            // So first, find the parent element having such descendent, and eventually having a badge descendent too.
            // The following line will find the i-th result on the page, the span that is the parent of the item with the correct title, and eventually parent to the optional badge too.
            By parentSpanLocator = By.XPath($"//span[@data-component-type='s-search-results']//div[@data-index and descendant::span[contains(text(), '{title}')]]");

            // Keep the parent for the badge check
            _foundParent = WaitAndGetElement(parentSpanLocator);
            // Still here? Good, so the item with the correct title exists
        }

        public void CheckItemDoesNotHaveABadge(string title)
        {
            // Assert that the parent we have found does not have a badge as a child
            Assert.Throws<NoSuchElementException>(() => _foundParent.FindElement(badgeLocator));
        }

        public void CheckItemHasType(string type)
        {
            _foundType = _foundParent.FindElement(By.XPath($".//div/a[contains(text(), '{type}')]"));
            // If we are here, the item does contain the correct type, ex. Paperback or Hardcover.
            // We remember the web element to be used later
        }

        public double GetSearchPrice()
        {
            IWebElement foundPrice = _foundParent.FindElement(searchPriceLocator);
            // The span with the price is found

            IWebElement priceWholePart = foundPrice.FindElement(searchPriceWholePartLocator);  // Whole part of the price, ex. 4
            IWebElement priceFractionPart = foundPrice.FindElement(searchPriceFractionPartLocator); // Fractional part of the price, ex. 17

            int wholePart = Int32.Parse(priceWholePart.Text);
            int fractionalPart = Int32.Parse(priceFractionPart.Text);
            fractionalPart.Should().BeInRange(0, 99);

            return ((double)wholePart + (double)fractionalPart / 100.0); // The price, ex. 4.17
        }

        public void NavigateToDetails()
        {
            _foundType.Click();  // Click on the type, ex. Paperback
        }
    }
}
