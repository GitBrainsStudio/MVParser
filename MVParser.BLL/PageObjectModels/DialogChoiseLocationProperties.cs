using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVParser.BLL.PageObjectModels
{
    public class DialogChoiseLocationProperties
    {
        public static By InputRegionData => By.Id("region-selection-form-city-input");
        public static By DropDownResultDiv => By.CssSelector(".city-selection-popup-results.not-empty-input");
        public static By CityInDropDown => By.ClassName("sel-droplist-cities");
    }
}
