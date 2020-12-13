using MVParser.BLL.Contracts;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVParser.BLL.Services
{
    public class BrowserService : IBrowserService
    {
        public IWebDriver Browser { get; }

        public BrowserService()
        {
            this.Browser = new ChromeDriver();
        }

        public WebDriverWait BrowserWait(int ms) => new WebDriverWait(this.Browser, TimeSpan.FromMilliseconds(ms));

        public bool BrowserWaitCountChange(int ms, By by)
        {
            try
            {
                return new WebDriverWait(this.Browser, TimeSpan.FromMilliseconds(ms)).Until(d => d.FindElements(by).Count() > 0) ? true : false;
            }

            catch(WebDriverTimeoutException)
            {
                return false;
            }
            
        }
    }
}
