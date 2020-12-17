using MVParser.BLL.Contracts;
using MVParser.BLL.Models;
using MVParser.BLL.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVParser.BLL.Services
{
    public class ProductsPageParseService : IProductsPageParseService
    {
        IBrowserService browserService { get; }

        public ProductsPageParseService(IBrowserService browserService)
        {
            this.browserService = browserService;
        }

        public void StartParsing(string productsPageUrl)
        {
            this.browserService.Browser.Navigate().GoToUrl(productsPageUrl);
            List<Product> products = new List<Product>();

            while(true)
            {
                if (this.browserService.Browser.FindElements(ProductsPageProperties.AnimationLoad).Count() == 0)
                {

                    IList<IWebElement> productsRows = this.browserService.Browser.FindElements(ProductsPageProperties.ProductsRow);

                    productsRows.ToList().ForEach(productRow =>
                    {
                        this.ScrollToElement(productRow);

                        IEnumerable<string> statuses = productRow.FindElements(ProductsPageProperties.NotificationBlock)
                        .Select(e =>
                        {
                            try 
                            { 
                                return e.FindElement(ProductsPageProperties.NotificationStatus).Text; 
                            }
                            catch (NoSuchElementException)
                            { 
                                return ""; 
                            }
                        });

                        IEnumerable<string> titles = productRow.FindElements(ProductsPageProperties.Title).Select(e => e.Text);

                        IEnumerable<string> urls = productRow.FindElements(ProductsPageProperties.Title).Select(e => e.GetAttribute("href"));

                        IEnumerable<string> actualPrices = productRow.FindElements(ProductsPageProperties.PriceBlock)
                        .Select(e =>
                        {
                            try
                            {
                                return e.FindElement(ProductsPageProperties.ActualPrice).Text;
                            }

                            catch (NoSuchElementException)
                            {
                                return "";
                            } 
                        });

                        IEnumerable<string> oldPrices = productRow.FindElements(ProductsPageProperties.PriceBlock)
                        .Select(e =>
                        {
                            try
                            {
                                return e.FindElement(ProductsPageProperties.OldPrice).Text;
                            }
                            catch (NoSuchElementException)
                            {
                                return "";
                            }
                        });

                        foreach (var status in statuses.Select((value, i) => new { i, value }))
                        {
                            var index = status.i;
                            products.Add(new Product(status.value, titles.ElementAt(index), urls.ElementAt(index), actualPrices.ElementAt(index), oldPrices.ElementAt(index)));
                        }
                     
                    });
                    break;
                }
            }
        }

        private void ScrollToElement(IWebElement webElement)
        {
            Actions actions = new Actions(this.browserService.Browser);
            actions.MoveToElement(webElement);
            actions.Perform();
        }
    }
}
