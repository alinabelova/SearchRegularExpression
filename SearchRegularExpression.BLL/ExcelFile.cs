using NLog;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchRegularExpression.BLL
{
    public class ExcelFile : File
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ExcelFile(string file) : base(file)
        {
            logger.Info("Excel Object was created");
        }

        private static DataTable WorksheetToDataTable(ExcelWorksheet oSheet)
        {
            try
            {
                int totalRows = oSheet.Dimension.End.Row;
                int totalCols = oSheet.Dimension.End.Column;
                DataTable dt = new DataTable(oSheet.Name);
                DataRow dr = null;
                for (int i = 1; i <= totalRows; i++)
                {
                    if (i > 1) dr = dt.Rows.Add();
                    for (int j = 1; j <= totalCols; j++)
                    {
                        if (i == 1)
                            dt.Columns.Add(oSheet.Cells[i, j].Value?.ToString());
                        else
                            dr[j - 1] = oSheet.Cells[i, j].Value?.ToString();
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
        }
        public override int Recognition(string regex)
        {
            try
            {
                logger.Info("Start read excel file");
                byte[] file = System.IO.File.ReadAllBytes(FileLocation);
                if (file == null)
                    return 0;
                using (MemoryStream ms = new MemoryStream(file))
                using (ExcelPackage package = new ExcelPackage(ms))
                {
                    if (package.Workbook.Worksheets.Count == 0)
                        Console.WriteLine("Your Excel file does not contain any work sheets");
                    else
                    {
                        foreach (ExcelWorksheet worksheet in package.Workbook.Worksheets)
                        {
                            var dt = WorksheetToDataTable(worksheet);
                            if (dt == null)
                                break;
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                Count += dt.AsEnumerable().Count(a => Regex.IsMatch(a[i].ToString(), regex, RegexOptions.IgnoreCase));
                            }
                        }
                    }
                    logger.Info("End read excel file");
                }
                logger.Info("Number of matches: " + Count);
                return Count;
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);                
                return 0;
            }                    
        }
    }
}
