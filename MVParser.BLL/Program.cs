using MVParser.BLL.Contracts;
using MVParser.BLL.Logging;
using MVParser.BLL.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVParser.BLL
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new Logger();
            IBrowserService browserService = new BrowserPoolService();
            IConvertService convertService = new ConvertService();
            IDocumentService documentService = new DocumentService();
            IXmlReadService xmlReadService = new XmlReadService();

            IProductsPageParseService productsPageParseService = new ProductsPageParseService(logger, browserService, convertService);

            IRegionChangeService regionChangeService = new RegionChangeService(browserService);
            regionChangeService.ChangeRegion(ConfigurationManager.AppSettings["regionname"]);

            convertService.GetUniqCategoriesFromSiteMap(convertService.WriteSiteMapFromXml(xmlReadService.DownloadXml())).ToList().ForEach(c =>
            {
                documentService.WriteProductsInExcel(logger, productsPageParseService.GetProducts(c.loc), convertService.GetSafeFilename(c.loc));
            });
 
        }
    }
}
