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
     
        public string ActualPrice { get; }
        public string OldPrice { get; }

        public Product(string status, string title, string url, string actualPrice, string oldPrice)
        {
            this.Status = status;
            this.Title = title;
            this.Url = url;
            this.ActualPrice = actualPrice;
            this.OldPrice = oldPrice;
        }
    }
}
