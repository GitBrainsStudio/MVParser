using MVParser.BLL.SerializeObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MVParser.BLL.Contracts
{
    public interface IConvertService
    {
        SiteMap WriteSiteMapFromXml(XmlDocument xmlDocument);
        IEnumerable<Category> GetUniqCategoriesFromSiteMap(SiteMap siteMap);
        double? ProductPriceConvert(string price);
        string GetSafeFilename(string fileName);
    }
}
