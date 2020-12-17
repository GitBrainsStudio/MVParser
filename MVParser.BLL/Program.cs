using MVParser.BLL.Contracts;
using MVParser.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVParser.BLL
{
    class Program
    {
        static void Main(string[] args)
        {
            IBrowserService browserService = new BrowserService();
            

            //IRegionChangeService regionChangeService = new RegionChangeService(browserService);

            //regionChangeService.ChangeRegion("Уфа");

            IProductsPageParseService productsPageParseService = new ProductsPageParseService(browserService);

            productsPageParseService.StartParsing("https://www.mvideo.ru/noutbuki-planshety-komputery-8/noutbuki-118?from=under_search");

        }
    }
}
