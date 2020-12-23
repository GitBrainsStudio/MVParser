using MVParser.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVParser.BLL.Contracts
{
    public interface IDocumentService
    {
        void WriteProductsInExcel(ILogger logger, IEnumerable<Product> products, string fileName);
    }
}
