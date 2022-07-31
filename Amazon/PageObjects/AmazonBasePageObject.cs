using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Amazon.PageObjects
{
    class AmazonBasePageObject
    {
        //The Selenium web driver to automate the browser
        private readonly IWebDriver _webDriver;
        private WebDriverWait _wait;
        private const int DefaultWaitInSeconds = 5;

        public AmazonBasePageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
        }

        protected IWebElement FindOptionalComponent(By byLocator)
        {
            // This element is optional and not necessarily part of the page.
            IWebElement? optionalElement;

            // We will wait up to 3 secs for it, and if present, we'll click it.
            // If not, we continue;
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(byLocator));
            }
            catch (WebDriverTimeoutException)
            {  // The timeout is OK, just continue
            }

            try
            {
                optionalElement = _webDriver.FindElement(byLocator);
            }
            catch (NoSuchElementException)
            {
                optionalElement = null;
            }
            return optionalElement;
        }

        protected IWebElement WaitAndGetElement(By byLocator)
        {
            // Wait for some seconds for the element to appear.
            _wait.Until(ExpectedConditions.ElementIsVisible(byLocator));
            IWebElement element = _webDriver.FindElement(byLocator);
            return element;
        }
    }
}
