using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVParser.BLL.Contracts;
using MVParser.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVParser.Tests
{
    [TestClass]
    public class XmlReadServiceTests
    {

        [TestMethod]
        public void ReadXml_MustReturnNotNull()
        {
            IXmlReadService xmlReadService = new XmlReadService();
            Assert.IsNotNull(xmlReadService.DownloadXml());
        }

    }
}
