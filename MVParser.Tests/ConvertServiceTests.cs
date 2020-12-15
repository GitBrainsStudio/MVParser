using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVParser.BLL.Contracts;
using MVParser.BLL.SerializeObjects;
using MVParser.BLL.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MVParser.Tests
{
    [TestClass]
    public class ConvertServiceTests
    {
        [TestMethod]
        public void WriteSiteMapFromXml_MustCorrectConvertXmlDoc()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + @"\sitemap.xml");

            IConvertService convertService = new ConvertService();

            Assert.IsNotNull(convertService.WriteSiteMapFromXml(xmlDoc));
        }

        [TestMethod]
        public void WriteSiteMapFromXml_MustCorrectConvertXmlDocAndContains()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + @"\sitemap.xml");

            IConvertService convertService = new ConvertService();

            var siteMapModel = convertService.WriteSiteMapFromXml(xmlDoc);

            Assert.IsTrue(siteMapModel.urlset.url.Any(v => v.loc == "https://www.mvideo.ru/prigotovlenie-kofe-29-29/kofemashiny-155/f/collection_top=delonghi-magnifica"));
        }


        [TestMethod]
        public void ConvertLocProperty_MustCorrectlConvertProperty()
        {
            IConvertService convertService = new ConvertService();
            PrivateObject privateObject = new PrivateObject(convertService);

            Assert.AreEqual("https://www.mvideo.ru/prigotovlenie-kofe-29-29/kofemashiny-155", 
                privateObject.Invoke("ConvertLocProperty", "https://www.mvideo.ru/prigotovlenie-kofe-29-29/kofemashiny-155/f/collection_bottom=evidence"));
        }

        [TestMethod]
        public void GetUniqCategoriesFromSiteMap_MustCorrectCreateUniqList()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + @"\sitemap.xml");

            IConvertService convertService = new ConvertService();

            var siteMapModel = convertService.WriteSiteMapFromXml(xmlDoc);

            IEnumerable<Category> uniqCategoriesAfterConvert = convertService.GetUniqCategoriesFromSiteMap(siteMapModel);

            for (var index = 1; index < uniqCategoriesAfterConvert.ToList().Count; index++)
            {
                var previous = uniqCategoriesAfterConvert.ToList()[index - 1];
                var current = uniqCategoriesAfterConvert.ToList()[index];
                if (current == previous)
                    Assert.Fail("Нашёлся дубль в ссылках.");
            }
        }
    }
}
