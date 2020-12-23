using MVParser.BLL.Contracts;
using MVParser.BLL.Models;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVParser.BLL.Services
{
    public class DocumentService : IDocumentService
    {
        public void WriteProductsInExcel(ILogger logger, IEnumerable<Product> products, string fileName)
        {
            try
            {
                logger.Event("Записываем продукты в файл excel..");
                //Create a new workbook
                Workbook workbook = new Workbook();
                //Initialize worksheet        
                Worksheet sheet = workbook.Worksheets[0];

                sheet.Range["A1"].Text = "Ссылка";
                sheet.Range["B1"].Text = "Наименование товара";
                sheet.Range["C1"].Text = "Текущий статус";
                sheet.Range["D1"].Text = "Текущая цена";
                sheet.Range["E1"].Text = "Старая цена";
                sheet.Range["F1"].Text = "Скидка";

                foreach (var product in products.OrderByDescending(p => p.DiscountPercentage).Select((value, i) => new { i, value }))
                {
                    var index = product.i + 2;
                    //Append text
                    sheet.Range["A" + index].Text = product.value.Url;
                    sheet.Range["B" + index].Text = product.value.Title;
                    sheet.Range["C" + index].Text = product.value.Status.ToString() ?? "";
                    sheet.Range["D" + index].Text = product.value?.ActualPrice.ToString() ?? "";
                    sheet.Range["E" + index].Text = product.value?.OldPrice.ToString() ?? "";
                    sheet.Range["F" + index].Text = product.value?.DiscountPercentage.ToString() ?? "";
                }


                //Save it as Excel file
                workbook.SaveToFile(logger.LogDirectory + fileName + ".xls", ExcelVersion.Version97to2003);
                logger.Event("Успешно.");

            }
            catch (Exception ex)
            {
                logger.Event("Ошибка при записи файла.");
                logger.Error(ex.ToString());
            }

        }
    }
}
