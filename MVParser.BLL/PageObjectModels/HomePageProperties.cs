using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVParser.BLL.PageObjectModels
{
    public class HomePageProperties
    {
        public static By LocationText => By.ClassName("header-top-line__link-text");
    }
}
