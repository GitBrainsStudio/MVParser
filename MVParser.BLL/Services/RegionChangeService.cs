using MVParser.BLL.Contracts;
using MVParser.BLL.Exceptions;
using MVParser.BLL.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MVParser.BLL.Services
{
    public class RegionChangeService : IRegionChangeService
    {
        IBrowserService browserService { get; }

        public RegionChangeService(IBrowserService browserService)
        {
            this.browserService = browserService;
        }

        public void ChangeRegion(string regionName)
        {
            this.browserService.Browser.Navigate().GoToUrl("https://www.mvideo.ru/");

            this.browserService.Browser.FindElement(HomePageProperties.LocationText).Click();

            this.browserService.BrowserWait(10000).Until(d => d.FindElement(DialogChoiseLocationProperties.InputRegionData)).Click();

            this.browserService.BrowserWait(10000).Until(d => d.FindElement(DialogChoiseLocationProperties.InputRegionData)).SendKeys("");

            this.browserService.BrowserWait(10000).Until(d => d.FindElement(DialogChoiseLocationProperties.InputRegionData)).SendKeys(regionName);

            var dropDownResultDiv = this.browserService.BrowserWait(10000).Until(d => d.FindElement(DialogChoiseLocationProperties.DropDownResultDiv));


            if (browserService.BrowserWaitCountChange(10000, DialogChoiseLocationProperties.CityInDropDown))
            {
                var citiesInDropDown = dropDownResultDiv.FindElements(DialogChoiseLocationProperties.CityInDropDown);

                IWebElement cityElement = null;
                citiesInDropDown.ToList().ForEach(e =>
                {
                    e.Text.Split(' ').ToList().ForEach(wordPart =>
                    {
                        if (wordPart.Replace(",", "") == regionName)
                        {
                            cityElement = e;
                        }
                    });

                });
                
                if (cityElement != null)
                {
                    cityElement.Click();
                }
                else throw new RegionNotFoundException();
            }

            else throw new RegionNotFoundException();
        }


    }
}
