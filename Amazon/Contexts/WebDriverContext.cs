using OpenQA.Selenium.Chrome;

namespace Amazon.Contexts
{
    public class WebDriverContext
    {
        public WebDriverContext()
        {
            this.Driver = new ChromeDriver();
        }

        public ChromeDriver Driver { get; set; }
    }
}
