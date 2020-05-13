using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Input;
using Microsoft.Win32;
using Word = Microsoft.Office.Interop.Word;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;


namespace DC
{
    
    public partial class UserControlCreate : UserControl
    {

        private string saveIn;

        
        public UserControlCreate()
        {
            InitializeComponent();
        }

        public void btn_Save(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Save files in";
            if (openFile.ShowDialog() == true)
            {

                DirectoryInfo directoryInfo = new DirectoryInfo(openFile.FileName);
                saveIn = directoryInfo.FullName;
                string nameOfFile = directoryInfo.Name;
                saveIn = saveIn.Replace(nameOfFile, "");
                saveIn = saveIn.Replace("\\", "/");
                tb_Save.Text = saveIn;
            }

            using(var sw = new StreamWriter("C:/DC/savein.txt"))
            {
                sw.Write(saveIn);
            }
        }


        public void btnSave_Accept(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tb_Save.Text))
            {
                MessageBox.Show("Pole musi byc uzupełnione!");
                return;
            }
            using (var sw = new StreamWriter("C:/DC/savein.txt"))
            {
                tb_Save.Text = saveIn;
                sw.Write(saveIn);
            }
        }



    }
}
