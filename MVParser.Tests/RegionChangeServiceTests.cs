using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVParser.BLL.Contracts;
using MVParser.BLL.Exceptions;
using MVParser.BLL.PageObjectModels;
using MVParser.BLL.Services;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MVParser.Tests
{
    [TestClass]
    public class RegionChangeServiceTests
    {
        [TestMethod]
        public void ChangeRegion_MustChangeRegionInMVideoSite()
        {
            IBrowserService browserService = new BrowserService();

            var regionChangeService = new RegionChangeService(browserService);

            regionChangeService.ChangeRegion("Екатеринбург");

            Assert.AreEqual(browserService.Browser.FindElement(HomePageProperties.LocationText).Text, "Екатеринбург");

            browserService.Browser.Close();
            browserService.Browser.Dispose();
        }

        [TestMethod]
        public void ChangeRegion_MustReturnRegionNotFoundException()
        {
            IBrowserService browserService = new BrowserService();

            var regionChangeService = new RegionChangeService(browserService);

            Assert.ThrowsException<RegionNotFoundException>(() => regionChangeService.ChangeRegion("monako"));

            browserService.Browser.Close();
            browserService.Browser.Dispose();
        }

    }
}
