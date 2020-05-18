using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Windows;
using System;

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

        public void Openfile()
        {
            string mySheet = path;
            var excelApp = new Excel.Application();
            excelApp.Visible = true;
            Excel.Workbooks books = excelApp.Workbooks;
            Excel.Workbook sheet = books.Open(mySheet);
        }



        public void CreateSpreadsheet1(List<User> users)
        {
            try
            {
                string spreadsheetPath = $"C:/Users/{Environment.UserName}/Downloads/data.xlsx";
                File.Delete(spreadsheetPath);
                FileInfo spreadsheetInfo = new FileInfo(spreadsheetPath);

                ExcelPackage pck = new ExcelPackage(spreadsheetInfo);
                var activitiesWorksheet = pck.Workbook.Worksheets.Add("Users");
                activitiesWorksheet.Cells["A1"].Value = "Name";
                activitiesWorksheet.Cells["B1"].Value = "Surname";
                activitiesWorksheet.Cells["C1"].Value = "Date";
                activitiesWorksheet.Cells["D1"].Value = "Computer";
                activitiesWorksheet.Cells["E1"].Value = "Servis Tag";
                activitiesWorksheet.Cells["A1:E1"].Style.Font.Bold = true;

                // populate spreadsheet with data
                int currentRow = 2;
                foreach (var user in users)
                {
                    activitiesWorksheet.Cells["A" + currentRow.ToString()].Value = user.Name;
                    activitiesWorksheet.Cells["B" + currentRow.ToString()].Value = user.Surname;
                    activitiesWorksheet.Cells["C" + currentRow.ToString()].Value = user.Date;
                    activitiesWorksheet.Cells["D" + currentRow.ToString()].Value = user.Computer;
                    activitiesWorksheet.Cells["E" + currentRow.ToString()].Value = user.ServisTag;

                    currentRow++;
                }
                pck.Save();
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Plik już został otwarty!");
            }   
        }
    }
}
