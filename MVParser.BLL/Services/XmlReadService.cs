using MVParser.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MVParser.BLL.Services
{
    public class XmlReadService : IXmlReadService
    {
        public XmlDocument DownloadXml()
        {
            var xmldoc = new XmlDocument();
            xmldoc.Load("https://www.mvideo.ru/sitemaps/sitemap-collection-www.mvideo.ru-1.xml");
            return xmldoc;
        }

    }
}
