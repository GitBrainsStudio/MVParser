using MVParser.BLL.Contracts;
using MVParser.BLL.SerializeObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MVParser.BLL.Services
{
    public class ConvertService : IConvertService
    {
        public SiteMap WriteSiteMapFromXml(XmlDocument xmlDocument)
        {
            var fromXml = JsonConvert.SerializeXmlNode(xmlDocument);
            return JsonConvert.DeserializeObject<SiteMap>(fromXml);
        }

        public IEnumerable<Category> GetUniqCategoriesFromSiteMap(SiteMap siteMap)
        {
            var list = new List<Category>();
            siteMap.urlset.url.ForEach(category =>
            {
                category.loc = ConvertLocProperty(category.loc);
                list.Add(category);
            });

            return list.GroupBy(v => v.loc).ToList().Select(v => v.First()).ToList();
        }

        private string ConvertLocProperty(string property)
        {
            return property.Replace("/f/", "$").Split('$').First();
        }
    }
}
