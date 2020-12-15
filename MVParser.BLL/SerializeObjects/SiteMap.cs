using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVParser.BLL.SerializeObjects
{
    public class SiteMap
    {
        public UrlSet urlset { get; set; }
    }

    public class UrlSet
    {
        public List<Category> url { get; set; }
    }

    public class Category
    {
        public string loc { get; set; }
        public string lastmod { get; set; }
        public string changefreq { get; set; }
        public string priority { get; set; }
    }
}
