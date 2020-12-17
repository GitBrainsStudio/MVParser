using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVParser.BLL.PageObjectModels
{
    public class ProductsPageProperties
    {
        public static By ProductsRow => By.ClassName("product-cards-row");
        public static By Title => By.CssSelector(".product-title__text.product-title--clamp");

        public static By NotificationBlock => By.CssSelector(".product-card__notification.ng-star-inserted");
        public static By NotificationStatus => By.ClassName("product-notification");

        public static By PriceBlock => By.CssSelector(".product-card__price-block.ng-star-inserted");
        public static By ActualPrice => By.CssSelector(".price__main-value.ng-star-inserted");
        public static By OldPrice => By.CssSelector(".price__sale-value.ng-star-inserted");

        public static By AnimationLoad => By.ClassName("animated-background");
    }
}
