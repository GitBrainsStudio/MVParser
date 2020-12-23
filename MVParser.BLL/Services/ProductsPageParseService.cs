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
        ILogger logger { get; }
        IConvertService convertService { get; }
        IBrowserService browserService { get; }

        public ProductsPageParseService(ILogger logger, IBrowserService browserService, IConvertService convertService)
        {
            this.browserService = browserService;
            this.convertService = convertService;
            this.logger = logger;
        }

        public IEnumerable<Product> GetProducts(string productsPageUrl)
        {
            logger.Event(productsPageUrl + " : Запущен процесс парсинга страницы.");
            this.browserService.Browser.Navigate().GoToUrl(productsPageUrl);
            List<Product> products = new List<Product>();

            int reloadCheckpoint = 0;
            while(true)
            {
                if (reloadCheckpoint == 10000) { this.browserService.Browser.Navigate().Refresh(); reloadCheckpoint = 0; }
                if (this.browserService.Browser.FindElements(ProductsPageProperties.AnimationLoad).Count() == 0 && this.browserService.Browser.FindElements(ProductsPageProperties.SkeletonItem).Count() == 0)
                {
                    try
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
                                products.Add(new Product(status.value, titles.ElementAt(index), urls.ElementAt(index), this.convertService.ProductPriceConvert(actualPrices.ElementAt(index)), this.convertService.ProductPriceConvert(oldPrices.ElementAt(index))));
                            }
                        });

                        productsRows = null;

                        if (!String.IsNullOrEmpty(this.browserService.Browser.FindElements(ProductsPageProperties.NextButton)[1].GetAttribute("href")))
                        {
                            this.browserService.Browser.FindElements(ProductsPageProperties.NextButton)[1].Click();
                        }
                        else
                        {
                            logger.Event(productsPageUrl + " : Парсинг страницы успешно окончен.");
                            break;
                        }

                    }

                    catch (StaleElementReferenceException) { }
                    catch (Exception ex)
                    {
                        logger.Event(productsPageUrl + " :  Ошибка при парсинге страницы. Страница будет пропущена. Содержание исключения записано в файл ошибок.");
                        logger.Error(ex.ToString());
                        break;
                    }
                }
                else reloadCheckpoint++;
            }

            return products;
        }

        private void ScrollToElement(IWebElement webElement)
        {
            Actions actions = new Actions(this.browserService.Browser);
            actions.MoveToElement(webElement);
            actions.Perform();
        }
    }
}
