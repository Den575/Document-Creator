using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using OfficeOpenXml;
using System.Collections.Generic;

namespace DC
{
    public class ExportExl
    {
        private string path;
        public List<User> data;

        public ExportExl(string path, List<User> data)
        {
            this.path = path;
            this.data = data;
        }


        public void CreateSpreadsheet()
        {
            string spreadsheetPath = path;
            string info = "";
            File.Delete(spreadsheetPath);
            FileInfo spreadsheetInfo = new FileInfo(spreadsheetPath);

            ExcelPackage pck = new ExcelPackage(spreadsheetInfo);
            var activitiesWorksheet = pck.Workbook.Worksheets.Add("Users");
            foreach(var s in data)
            {
                info += $"{s.Name};{s.Surname};{s.Date};{s.Computer};{s.ServisTag};";
            }
            activitiesWorksheet.Cells["A1"].Value = info;
            activitiesWorksheet.Cells["A1:D1"].Style.Font.Bold = false;

            activitiesWorksheet.View.FreezePanes(2, 1);

            pck.Save();

        }

        public void Openfile()
        {
            string mySheet = path;
            var excelApp = new Excel.Application();
            excelApp.Visible = true;
            Excel.Workbooks books = excelApp.Workbooks;
            Excel.Workbook sheet = books.Open(mySheet);
        }

    }
}
