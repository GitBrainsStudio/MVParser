using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVParser.BLL.Contracts
{
    public interface IBrowserService
    {
        IWebDriver Browser { get; }
        WebDriverWait BrowserWait(int ms);
        bool BrowserWaitCountChange(int ms, By by);
    }
}
