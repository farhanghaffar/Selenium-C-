using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.API.Test
{
    public static class ExcelHelper
    {
        public static string GetExcelFileDetails(int Row, int Column)
        {
            string cellvalue = string.Empty;

            try
            {
                //create a instance for the Excel object  
                Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();

                //specify the file name where its actually exist  
                string filepath = ConfigurationManager.AppSettings["ExcelLocation"]; ;

                //pass that to workbook object  
                Microsoft.Office.Interop.Excel.Workbook WB = oExcel.Workbooks.Open(filepath);


                // statement get the workbookname  
                string ExcelWorkbookname = WB.Name;

                // statement get the worksheet count  
                int worksheetcount = WB.Worksheets.Count;

                Microsoft.Office.Interop.Excel.Worksheet wks = (Microsoft.Office.Interop.Excel.Worksheet)WB.Worksheets[1];

                // statement get the firstworksheetname  

                string firstworksheetname = wks.Name;

                //statement get the first cell value  
                cellvalue = (string)((Microsoft.Office.Interop.Excel.Range)wks.Cells[Row, Column]).Value;

            }
            catch (Exception ex)
            {

                string error = ex.Message;
            }

            return cellvalue;
        }
    }
}
