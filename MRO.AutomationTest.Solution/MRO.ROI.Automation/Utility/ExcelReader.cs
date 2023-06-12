using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.IO;
using System.Net;

namespace DataDrivenProject
{
    public class ExcelReaderFile
    {
        public FileStream fs = null;
        private IWorkbook workbook = null;
        private ISheet sheet = null;
        private IRow row = null;
        private ICell cell = null;
        Microsoft.Office.Interop.Excel.Application xlApp;
        Microsoft.Office.Interop.Excel.Workbook xlWorkbook;
        Microsoft.Office.Interop.Excel.Range usedRange;
        Microsoft.Office.Interop.Excel.Range xlCells;
        public string path = string.Empty;

        public ExcelReaderFile()
        {
        }

        

        public ExcelReaderFile(string path)
        {

            this.path = path;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                workbook = new XSSFWorkbook(fs);
                sheet = workbook.GetSheetAt(0);
                fs.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Inner Exception: " + ex.InnerException.Message);
            }
        }

        public string ReadExcelCellData(string path, int rowNumber, int colNumber)
        {
            string value = string.Empty;
            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();

                if (null == xlApp)
                {
                    throw new Exception("Excel could not be started. Check that your " +
                      "office installation and project references are correct.");
                }

                bool openReadOnly = true;
                xlWorkbook = xlApp.Workbooks.Open(path,
                    0, openReadOnly, 5, "", "", false,
                    Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);

                if (null == xlWorkbook)
                {
                    throw new Exception(string.Format(
                      "Excel Workbook '{0}' could not be opened.", path));
                }

                foreach (Microsoft.Office.Interop.Excel.Worksheet xlSheet in xlWorkbook.Worksheets)
                {
                    if (null != xlSheet)
                    {
                        usedRange = xlSheet.UsedRange;
                        if ((null != usedRange) && (null != usedRange.Cells))
                        {
                            xlCells = usedRange.Cells;

                            if(xlCells==null)
                            {
                                throw new Exception("No data found to return");
                            }

                            dynamic cell = xlCells[rowNumber, colNumber];
                            if (null != cell)
                            {
                                  value = GetValue(cell);

                            }

                        }
                        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet);
                    }
                }
                xlWorkbook.Close(Type.Missing, Type.Missing, Type.Missing);
                xlApp.Quit();
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException)
                {
                   throw new Exception("Inner Exception: " + ex.InnerException.Message);
                }
            }
            finally
            {
                if (null != usedRange) { System.Runtime.InteropServices.Marshal.FinalReleaseComObject(usedRange); usedRange = null; }
                if (null != xlCells) { System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlCells); xlCells = null; }
                if (null != xlWorkbook) { System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlWorkbook); xlWorkbook = null; }
                if (null != xlApp) { System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp); xlApp = null; }
            }

            return value;
        }


        public string ConvertXLS_XLSX(string file)
        {
            try
            {
                var app = new Microsoft.Office.Interop.Excel.Application();
                var xlsFile = file;
                var wb = app.Workbooks.Open(xlsFile);
                var xlsxFile = xlsFile + "x";
                wb.SaveAs(Filename: xlsxFile, FileFormat: Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
                wb.Close();
                app.Quit();
                return xlsxFile;

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to convert the excel from xls to xlsx");
            }
        }

        public static string DownloadPDFFileFromUrl(string pdfUrl)
        {
            string pdfContent = string.Empty;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(pdfUrl, $"{downloadFolder}Temp.pdf");
                }
                pdfContent = ExtractTextFromPdf($"{downloadFolder}Temp.pdf");
            }
            catch (Exception ex)
            {
                File.Delete($"{downloadFolder}Temp.pdf");
            }
            File.Delete($"{downloadFolder}Temp.pdf");
            return pdfContent;
        }

        private static string ExtractTextFromPdf(string path)
        {
            org.apache.pdfbox.pdmodel.PDDocument doc = null;
            try
            {
                doc = org.apache.pdfbox.pdmodel.PDDocument.load(path);
                org.apache.pdfbox.util.PDFTextStripper stripper = new org.apache.pdfbox.util.PDFTextStripper();
                return stripper.getText(doc);
            }
            finally
            {
                if (doc != null)
                {
                    doc.close();
                }
            }
        }

        private static string GetValue(dynamic cell)
        {
            string ret = string.Empty;
            if (null == cell) { return ret; }
            Microsoft.Office.Interop.Excel.Range singleCell = cell as Microsoft.Office.Interop.Excel.Range;
            if (null == singleCell) { return ret; }
            if (null != singleCell.Text)
            {
                ret = singleCell.Text as string;
            }
            if (null == ret) { return string.Empty; }
            return ret.Replace("\n", " ");
        }

        public int getRowCount(string sheetName)
        {
            int index = workbook.GetSheetIndex(sheetName);
            if (index == -1)
                return 0;
            else
            {
                sheet = workbook.GetSheetAt(index);
                int number = sheet.LastRowNum + 1;
                return number;
            }
        }

        public int getColumnCount(string sheetName, int rowNumer =0)
        {
            if (!isSheetExist(sheetName))
                return -1;
            sheet = workbook.GetSheet(sheetName);
            row = sheet.GetRow(rowNumer);
            if (row == null)
                return -1;
            return row.LastCellNum;
        }

        public int getRowNumber(string sheetName, int colNum, string value)
        {
            fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            workbook = new HSSFWorkbook(fs);
            int index = workbook.GetSheetIndex(sheetName);
            if (index == -1)
                return 0;

            sheet = workbook.GetSheetAt(index);
            for (int rw = 0; rw < sheet.LastRowNum; rw++)
            {
                if (sheet.GetRow(rw) != null)
                {
                    row = sheet.GetRow(rw);
                }

            }

            return row.RowNum;


        }

        public string getCellData(string sheetName, int colNum, int rowNum)
        {

            if (rowNum <= 0)
                return "";
            int index = workbook.GetSheetIndex(sheetName);
            if (index == -1)
                return "";
            sheet = workbook.GetSheetAt(index);
            row = sheet.GetRow(rowNum - 1);
            if (row == null)
                return "";
            cell = row.GetCell(colNum);
            if (cell == null)
                return "";
            if (cell.CellType == CellType.String)
                return cell.StringCellValue;
            else if (cell.CellType == CellType.Numeric || cell.CellType == CellType.Formula)
            {
                string cellText = Convert.ToString(cell.NumericCellValue);
                return cellText;
            }
            else if (cell.CellType == CellType.Blank)
                return "";
            else
                return cell.BooleanCellValue.ToString();

        }

        public string getCellData(string sheetName, string colName, int rowNum)
        {
            if (rowNum <= 0)
                return "";
            int colNum = -1;
            int index = workbook.GetSheetIndex(sheetName);
            if (index == -1)
                return "";
            sheet = workbook.GetSheetAt(index);
            row = sheet.GetRow(0);
            for (int i = 0; i < row.LastCellNum; i++)
            {
                if (row.GetCell(i).StringCellValue.Trim().Equals(colName.Trim()))
                {
                    colNum = i;
                }
            }
            if (colNum == -1)
                return "";
            sheet = workbook.GetSheetAt(index);
            row = sheet.GetRow(rowNum - 1);
            if (row == null)
                return "";
            cell = row.GetCell(colNum);
            if (cell == null)
                return "";
            if (cell.CellType == CellType.String)
                return cell.StringCellValue;
            else if (cell.CellType == CellType.Numeric || cell.CellType == CellType.Formula)
            {
                string cellText = Convert.ToString(cell.NumericCellValue);
                return cellText;
            }
            else if (cell.CellType == CellType.Blank)
                return "";
            else
                return cell.BooleanCellValue.ToString();
        }

        public string setCellData(string sheetName, string colName, int rowNum, string data)
        {
            fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            workbook = new HSSFWorkbook(fs);
            if (rowNum <= 0)
                return "";
            int colNum = -1;
            int index = workbook.GetSheetIndex(sheetName);
            if (index == -1)
                return "";
            sheet = workbook.GetSheetAt(index);
            row = sheet.GetRow(0);
            for (int i = 0; i < row.LastCellNum; i++)
            {
                if (row.GetCell(i).StringCellValue.Equals(colName))
                {
                    colNum = i;
                }
            }
            if (colNum == -1)
                return "";
            row = sheet.GetRow(rowNum - 1);
            if (row == null)
                row = sheet.CreateRow(rowNum - 1);
            cell = row.GetCell(colNum - 1);
            if (cell == null)
                cell = row.CreateCell(colNum);

            ICellStyle cs = workbook.CreateCellStyle();
            cs.WrapText = true;
            cell.CellStyle = cs;
            cell.SetCellValue(data);

            FileStream f = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            workbook.Write(f);
            f.Close();
            fs.Close();

            return data;

        }

        public string setCellData(string sheetName, int colNum, int rowNum, string data)
        {
            fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            workbook = new HSSFWorkbook(fs);
            if (rowNum <= 0)
                return "";
            int index = workbook.GetSheetIndex(sheetName);
            if (index == -1)
                return "";
            sheet = workbook.GetSheetAt(index);
            row = sheet.GetRow(rowNum - 1);
            if (row == null)
                row = sheet.CreateRow(rowNum - 1);
            cell = row.GetCell(colNum - 1);
            if (cell == null)
                cell = row.CreateCell(colNum - 1);

            ICellStyle cs = workbook.CreateCellStyle();
            cs.WrapText = true;
            cell.CellStyle = cs;
            cell.SetCellValue(data);

            FileStream f = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            workbook.Write(f);
            f.Close();
            fs.Close();

            return data;
        }

        public bool addSheet(string sheetName)
        {
            try
            {
                workbook.CreateSheet(sheetName);
                fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                workbook.Write(fs);
                fs.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool removeSheet(string sheetName)
        {
            int index = workbook.GetSheetIndex(sheetName);
            if (index == -1)
                return false;
            try
            {
                workbook.RemoveSheetAt(index);
                fs = new FileStream(path, FileMode.Truncate);
                workbook.Write(fs);
                fs.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool addColumn(string sheetName, string colName)
        {
            fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            workbook = new HSSFWorkbook(fs);
            int index = workbook.GetSheetIndex(sheetName);
            if (index == -1)
                return false;
            ICellStyle cs = workbook.CreateCellStyle();
            sheet = workbook.GetSheetAt(index);
            row = sheet.GetRow(0);
            if (row == null)
                row = sheet.CreateRow(0);
            cell = row.GetCell(0);
            if (cell == null)
                cell = row.CreateCell(0);
            else
                cell = row.CreateCell(row.LastCellNum);
            cell.SetCellValue(colName);
            cell.CellStyle = cs;

            FileStream f = new FileStream(path, FileMode.Create);
            workbook.Write(f);
            f.Close();
            fs.Close();
            return true;
        }

        public bool removeColumn(string sheetName, int colNum)
        {
            if (!isSheetExist(sheetName))
                return false;
            fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            workbook = new HSSFWorkbook(fs);
            sheet = workbook.GetSheet(sheetName);
            ICellStyle cs = workbook.CreateCellStyle();
            for (int i = 0; i < getRowCount(sheetName); i++)
            {
                row = sheet.GetRow(i);
                if (row != null)
                {
                    cell = row.GetCell(colNum - 1);
                    if (cell != null)
                    {
                        cell.CellStyle = cs;
                        row.RemoveCell(cell);
                    }
                }
            }
            FileStream f = new FileStream(path, FileMode.Truncate);
            workbook.Write(f);
            f.Close();
            fs.Close();
            return true;
        }

        public bool isSheetExist(string sheetName)
        {
            int index = workbook.GetSheetIndex(sheetName);
            if (index == -1)
            {
                index = workbook.GetSheetIndex(sheetName.ToUpper());
                if (index == -1)
                    return false;
                else
                    return true;
            }
            else
                return true;
        }

        public int getCellRowNum(string sheetName, string colName, string cellValue)
        {
            for (int i = 0; i < getRowCount(sheetName); i++)
            {
                if (getCellData(sheetName, colName, i).Equals(cellValue))
                {
                    return i;
                }
            }
            return -1;
        }

        public static void KillPDFProcess()
        {
            try
            {
                System.Diagnostics.Process[] processlist = System.Diagnostics.Process.GetProcesses();

                foreach (System.Diagnostics.Process theprocess in processlist)
                {

                    if (theprocess.ProcessName.Equals("testhost.x86.exe"))
                    {
                        theprocess.Kill();
                        break;
                    }
                }

            }
            catch
            {
            }
        }

        public static void DeleteExistingFiles(string path, string extension, string fileName)
        {
            try
            {
                string[] filePaths = Directory.GetFiles(path, "*");
                foreach (string filePath in filePaths)
                {
                    string ext = filePath.Split('.')?[1];
                    if (ext == extension)
                    {
                        File.Delete(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public string ReadExcelCellDataBySheet(string path, int sheetIndex, int rowNumber, int colNumber)
        {
            string value = string.Empty;
            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();

                if (null == xlApp)
                {
                    throw new Exception("Excel could not be started. Check that your " +
                      "office installation and project references are correct.");
                }

                bool openReadOnly = true;
                xlWorkbook = xlApp.Workbooks.Open(path,
                    0, openReadOnly, 5, "", "", false,
                    Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);

                if (null == xlWorkbook)
                {
                    throw new Exception(string.Format(
                      "Excel Workbook '{0}' could not be opened.", path));
                }

                Console.WriteLine();
                Microsoft.Office.Interop.Excel.Worksheet xlSheet = xlWorkbook.Worksheets[sheetIndex];
                if (null != xlSheet)
                {
                    usedRange = xlSheet.UsedRange;
                    if ((null != usedRange) && (null != usedRange.Cells))
                    {
                        xlCells = usedRange.Cells;

                        if (xlCells == null)
                        {
                            throw new Exception("No data found to return");
                        }

                        dynamic cell = xlCells[rowNumber, colNumber];
                        if (null != cell)
                        {
                            value = GetValue(cell);

                        }

                    }
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet);
                }

                xlWorkbook.Close(Type.Missing, Type.Missing, Type.Missing);
                xlApp.Quit();
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException)
                {
                    throw new Exception("Inner Exception: " + ex.InnerException.Message);
                }
            }
            finally
            {
                if (null != usedRange) { System.Runtime.InteropServices.Marshal.FinalReleaseComObject(usedRange); usedRange = null; }
                if (null != xlCells) { System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlCells); xlCells = null; }
                if (null != xlWorkbook) { System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlWorkbook); xlWorkbook = null; }
                if (null != xlApp) { System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp); xlApp = null; }
            }

            return value;
        }

        public bool SearchTextinExcelBySheetIndex(string path, int sheetIndex, string searchText)
        {
            bool isValueExists = false;
            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();

                if (null == xlApp)
                {
                    throw new Exception("Excel could not be started. Check that your " +
                      "office installation and project references are correct.");
                }

                xlWorkbook = xlApp.Workbooks.Open(path);

                if (null == xlWorkbook)
                {
                    throw new Exception(string.Format(
                      "Excel Workbook '{0}' could not be opened.", path));
                }

                Console.WriteLine();
                Microsoft.Office.Interop.Excel.Worksheet xlSheet = xlWorkbook.Worksheets[sheetIndex];
                if (null != xlSheet)
                {
                    usedRange = xlSheet.UsedRange;
                    if (usedRange != null)
                    {
                        var currentFind = usedRange.Find(searchText);
                        if (currentFind != null)
                        {
                            isValueExists = true;
                        }
                    }
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet);
                }

                xlWorkbook.Close(Type.Missing, Type.Missing, Type.Missing);
                xlApp.Quit();
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException)
                {
                    throw new Exception("Inner Exception: " + ex.InnerException.Message);
                }
            }
            finally
            {
                if (null != usedRange) { System.Runtime.InteropServices.Marshal.FinalReleaseComObject(usedRange); usedRange = null; }
                if (null != xlCells) { System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlCells); xlCells = null; }
                if (null != xlWorkbook) { System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlWorkbook); xlWorkbook = null; }
                if (null != xlApp) { System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp); xlApp = null; }
            }

            return isValueExists;
        }

        public Microsoft.Office.Interop.Excel.Range ReturnAllRowsOfExcel(string path)
        {
            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();

                if (null == xlApp)
                {
                    throw new Exception("Excel could not be started. Check that your " +
                      "office installation and project references are correct.");
                }

                bool openReadOnly = true;
                xlWorkbook = xlApp.Workbooks.Open(path,
                    0, openReadOnly, 5, "", "", false,
                    Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);

                if (null == xlWorkbook)
                {
                    throw new Exception(string.Format(
                      "Excel Workbook '{0}' could not be opened.", path));
                }

                Console.WriteLine();
                foreach (Microsoft.Office.Interop.Excel.Worksheet xlSheet in xlWorkbook.Worksheets)
                {
                    if (null != xlSheet)
                    {
                        usedRange = xlSheet.UsedRange;
                        if ((null != usedRange) && (null != usedRange.Cells))
                        {
                            xlCells = usedRange.Cells;

                            if (xlCells == null)
                            {
                                throw new Exception("No data found to return");
                            }

                        }
                        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet);
                    }
                }
                xlWorkbook.Close(Type.Missing, Type.Missing, Type.Missing);
                xlApp.Quit();
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException)
                {
                    throw new Exception("Inner Exception: " + ex.InnerException.Message);
                }
            }
            finally
            {
                if (null != usedRange) { System.Runtime.InteropServices.Marshal.FinalReleaseComObject(usedRange); usedRange = null; }
                if (null != xlCells) { System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlCells); xlCells = null; }
                if (null != xlWorkbook) { System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlWorkbook); xlWorkbook = null; }
                if (null != xlApp) { System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp); xlApp = null; }
            }
            return xlCells;
        }

        public string GetSpecificFileLocation(string path, string extension, string fileName)
        {
            string _filePath = string.Empty;
            try
            {
                string[] filePaths = Directory.GetFiles(path, "*");
                foreach (string filePath in filePaths)
                {
                    string ext = filePath.Split('.')?[1];
                    if (ext == extension && filePath.Contains(fileName))
                    {
                        _filePath = filePath;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to delete the file");
            }
            return _filePath;
        }
    }
}
