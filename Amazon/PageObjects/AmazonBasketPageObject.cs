using OpenQA.Selenium;
using System;

namespace Amazon.PageObjects
{
    class AmazonBasketPageObject : AmazonBasePageObject
    {
        // UI elements locators. If they change, there is only one place - here - where we should change them.
        By deleteBtnLocator = By.XPath("//span[@data-action='delete' and @data-feature-id='delete']//input[@value='Delete']");
        By cartEmptyLocator = By.XPath("//div[@id='sc-active-cart']//h1[contains(text(), 'Your Amazon Cart is empty.')]");
        By titleLocator = By.XPath($"//div[@data-name='Active Items']//span[@class='a-truncate-cut']");
        By quantityBtnLocator = By.XPath("//span[@data-feature-id='sc-update-quantity-select']//span[@class='a-dropdown-prompt']");
        By itemPriceLocator = By.XPath("//p[@class='a-spacing-mini']/span[@class='a-size-medium a-color-base sc-price sc-white-space-nowrap sc-product-price a-text-bold']");
        By totalPriceLocator = By.XPath("//span[@id='sc-subtotal-amount-activecart']/span[@class='a-size-medium a-color-base sc-price sc-white-space-nowrap']");

        public AmazonBasketPageObject(IWebDriver webDriver) : base(webDriver)
        {
        }

        public string FindTitleInBasket()
        {
            IWebElement itemTitle = WaitAndGetElement(titleLocator);

            return itemTitle.Text;
        }

        public void FindTypeInBasket(string type)
        {
            By typeBasketItemLocator = By.XPath($"//div[@data-name='Active Items']//span[contains(text(), '{type}')]");

            // Find the type in the basket
            WaitAndGetElement(typeBasketItemLocator);
        }

        public int GetQuantityInBasket()
        {
            IWebElement qty = WaitAndGetElement(quantityBtnLocator);

            // Return the quantity of the item in the basket
            return Int32.Parse(qty.Text);
        }

        public string GetPriceInBasket()
        {
            IWebElement price = WaitAndGetElement(itemPriceLocator);

            // Return the price of the item in the basket
            return price.Text;
        }

        public string GetTotalPriceInBasket()
        {
            IWebElement totalPrice = WaitAndGetElement(totalPriceLocator);

            // Return the price of the item in the basket
            return totalPrice.Text;
        }

        public void RemoveItemFromBasket()
        {
            // Click the delete btn to remove it from the basket.
            WaitAndGetElement(deleteBtnLocator).Click();

            // Wait for the label that the cart is empty to appear.
            WaitAndGetElement(cartEmptyLocator);
        }
    }
}
