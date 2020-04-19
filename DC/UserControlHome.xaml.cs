using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using Microsoft.Office.Interop.Word;

namespace DC
{

    public partial class UserControlHome : UserControl
    {
        private static string ComputerName { get; set; }
        private static string Сity { get; set; }

        private string SaveIn { get; set; }
        private string OpenIn { get; set; }

        private static string Date = DateTime.Now.ToShortDateString();


        public UserControlHome()
        {
            InitializeComponent();
        }

        private void ComboBox_Selected(object sender, RoutedEventArgs e) //Lista Computer
        {
            try
            {
                ComputerName = (ComboBox1.SelectedItem as ComboBoxItem).Content.ToString();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Null"); //TODO dopracowac
            }

        }

        private void ComboBox_SelectedCity(object sender, RoutedEventArgs e) //Lista Miasto
        {
            try
            {
                Сity = (ComboBox2.SelectedItem as ComboBoxItem).Content.ToString();
                if (Сity == "Krakow") {
                    Сity = "Krakowie";
                }
                else if (Сity == "Gdańsk")
                {
                    Сity = "Gdańsku";
                }
                else if(Сity== "Pruszcz Gdański")
                {
                    Сity = "Pruszczu Gdańskim";
                }
                else
                {
                    Сity = "Kraków";
                }
            }
            catch
            {
                MessageBox.Show("Null"); //TODO dopracowac
            }

        }

        private void mycalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) //Calendar
        {
            DateTime calendarDate = (DateTime)mycalendar.SelectedDate;
            Date = calendarDate.ToShortDateString();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //Button
        {

            if (FileExists() == false)
            {
                MessageBox.Show("Aplikacja nie zostala skonfigurowana");
                return;
            }

            XDocument xdoc = XDocument.Load("C:/DC/config.xml");
            XElement root = xdoc.Element("computers");

            using (var sw = new StreamReader("C:/DC/openin.txt"))
            {
                OpenIn = sw.ReadToEnd();
            }

            btnCreate.Cursor = Cursors.Wait;

            if (CheckedValues())
            {
                return;
            }
            try
            {
                Data data = new Data() { Name = tbName.Text, SName = tbSName.Text };

                var wordApp = new Microsoft.Office.Interop.Word.Application();


                wordApp.Visible = false;

                var wordDocument = wordApp.Documents.Open(OpenIn);

                ReplaceWordApp("<name>", data.NarzednikImie(data.Name), wordDocument);
                ReplaceWordApp("<surname>", data.NarzednikNazwisko(data.SName), wordDocument);
                ReplaceWordApp("<computer>", ComputerName, wordDocument);
                ReplaceWordApp("<date>", Date, wordDocument);
                ReplaceWordApp("<date1>", Date, wordDocument);
                ReplaceWordApp("<date2>", Date, wordDocument);
                ReplaceWordApp("<name&surname> ", $"{data.Name} {data.SName}", wordDocument);
                ReplaceWordApp("<name&surname1> ", $"{data.Name} {data.SName}", wordDocument);



                root.Add(new XElement("user",
                    new XAttribute("name", data.Name),
                    new XAttribute("surname", data.SName),
                    new XElement("computer", ComputerName),
                    new XElement("date", Date),
                    new XElement("servistag", "TAG")));
                xdoc.Save("C:/DC/config.xml");


                using (var sr = new StreamReader("C:/DC/savein.txt"))
                {
                    SaveIn = sr.ReadToEnd();
                }

                wordDocument.SaveAs($"{SaveIn}{data.Name} {data.SName}.docx"); //TODO: Niepoprawnie 
                wordApp.Visible = true;


                btnCreate.Cursor = Cursors.Hand;




            }
            catch (System.Runtime.InteropServices.COMException)
            {

                MessageBox.Show("Nie mozna znalezc pliku");
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }



        private void ReplaceWordApp(string stubToReplace, string text, Microsoft.Office.Interop.Word.Document document) //Zamiena
        {
            var range = document.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
        }

        public bool CheckedValues() //Sprawdzenie 
        {
            string inccorectValues = "@!#%&*()/\\}{[]`<>\";~+-=^$?.,";
            if (string.IsNullOrWhiteSpace(tbName.Text) || string.IsNullOrWhiteSpace(tbSName.Text))
            {
                MessageBox.Show("Podaj imie i nazwisko");
                return true;
            }
            string nameAndSurname = tbName.Text + tbSName.Text;
            for (int i =0;i<=nameAndSurname.Length-1;i++)
            {
                for (int j = 0;j<=inccorectValues.Length-1;j++)
                {
                    if (nameAndSurname[i] == inccorectValues[j])
                    {
                        MessageBox.Show("Imie i nazwisko nie moze zawierac nastepujacych znakow @!#%&*()/\\}{[]`<>\";~+-=^$?.,");
                        return true;
                    }
                }

                
            }

        return false;
        }

        private bool FileExists()
        {
            if (File.Exists("C:/DC/savein.txt") && File.Exists("C:/DC/openin.txt"))
            {
                return true;
            }
            return false;
        }
    }
}
