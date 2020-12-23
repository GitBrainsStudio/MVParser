using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVParser.BLL.Models
{
    public class Product
    {
        public string Status { get; }
        public string Title { get; }
        public string Url { get; }
     
        public double? ActualPrice { get; }
        public double? OldPrice { get; }

        public double? DiscountPercentage => (OldPrice - ActualPrice) * 100 / OldPrice;

        public Product(string status, string title, string url, double? actualPrice, double? oldPrice)
        {
            this.Status = status;
            this.Title = title;
            this.Url = url;
            this.ActualPrice = actualPrice;
            this.OldPrice = oldPrice;
        }
    }
}
