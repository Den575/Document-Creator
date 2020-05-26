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
        private static string ComputerName { get; set; } = "Lenovo ThinkBook 13s";
        private static string Сity { get; set; } = "Krakowie";
        private static string СityForm2 { get; set; } = "Kraków";

        private string SaveIn { get; set; }
        private string OpenIn { get; } = "C:/DC/WZOR.docx";

        private static string Date = DateTime.Now.ToShortDateString();


        public UserControlHome()
        {
            InitializeComponent();
            ComboBox1.Text = "Select computer";
            ComboBox2.Text = "Select city";

        }

        private void ComboBox_Selected(object sender, RoutedEventArgs e) //Lista Computer
        {
            try
            {
                ComputerName = (ComboBox1.SelectedItem as ComboBoxItem).Content.ToString();
            }
            catch (NullReferenceException)
            {
                ComputerName = "Lenovo ThinkBook 13s";
                return;
            }

        }

        private void ComboBox_SelectedCity(object sender, RoutedEventArgs e) //Lista Miasto
        {
            try
            {
                Сity = (ComboBox2.SelectedItem as ComboBoxItem).Content.ToString();
                if (Сity == "Krakow") {
                    Сity = "Krakowie";
                    СityForm2 = "Kraków";
                }
                else if (Сity == "Gdańsk")
                {
                    Сity = "Gdańsku";
                    СityForm2 = "Gdańsk";
                }
                else if(Сity== "Pruszcz Gdański")
                {
                    Сity = "Pruszczu Gdańskim";
                    СityForm2 = "Przuszcz Gdański";
                }
                else
                {
                    
                }
            }
            catch
            {
                Сity = "Krakowie";
                СityForm2 = "Kraków";
                return;
            }

        }

        private void mycalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) //Calendar
        {
            DateTime calendarDate = (DateTime)mycalendar.SelectedDate;
            Date = calendarDate.ToShortDateString();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //Button
        {


            if (CheckedValues())
            {
                return;
            }

            Data data = new Data() { Name = char.ToUpper(tbName.Text[0]) + tbName.Text.Substring(1).ToLower(), SName = char.ToUpper(tbSName.Text[0]) + tbSName.Text.Substring(1), Proffesion = tbPosition.Text.ToUpper(), ServisTag = tbServisTag.Text.ToUpper() };


            try
            {
                using (var sr = new StreamReader("C:/DC/savein.txt"))
                {
                    SaveIn = sr.ReadToEnd();
                }
            }
            catch
            {
                SaveIn = $"C:/Users/{Environment.UserName}/Downloads/";
            }
           

            if (File.Exists($"{SaveIn}{data.Name} {data.SName}.docx"))
            {
                MessageBox.Show($"Plik {SaveIn}{data.Name} {data.SName}.docx już istnieje!");
                return;
            }


            XDocument xdoc = XDocument.Load("C:/DC/config.xml");
            XElement root = xdoc.Element("computers");


            btnCreate.Cursor = Cursors.Wait;


            try
            {

                var wordApp = new Microsoft.Office.Interop.Word.Application();


                wordApp.Visible = false;

                var wordDocument = wordApp.Documents.Open(OpenIn);



                ReplaceWordApp("<name>", data.NarzednikImie(data.Name), wordDocument);
                ReplaceWordApp("<stanowisko>", data.Proffesion, wordDocument);
                ReplaceWordApp("<surname>", data.NarzednikNazwisko(data.Name, data.SName), wordDocument); ;
                ReplaceWordApp("<computer>", ComputerName, wordDocument);
                ReplaceWordApp("<date>", Date, wordDocument);
                ReplaceWordApp("<date1>", Date, wordDocument);
                ReplaceWordApp("<date2>", Date, wordDocument);
                ReplaceWordApp("<date3>", Date, wordDocument);
                ReplaceWordApp("<name&surname>", $"{data.Name} {data.SName}", wordDocument);
                ReplaceWordApp("<name&surname1>", $"{data.Name} {data.SName}", wordDocument);
                ReplaceWordApp("<stag>", data.ServisTag, wordDocument);
                ReplaceWordApp("<endofword> ", data.EndOfWord(data.Name), wordDocument);
                ReplaceWordApp("<anotherinfo>", RichTextB(), wordDocument);
                ReplaceWordApp("<city>", Сity, wordDocument);
                ReplaceWordApp("<city2>", СityForm2, wordDocument);



                root.Add(new XElement("user",
                    new XAttribute("name", data.Name),
                    new XAttribute("surname", data.SName),
                    new XElement("computer", ComputerName),
                    new XElement("date", Date),
                    new XElement("profession", data.Proffesion.ToUpper()),
                    new XElement("Info", RichTextB()),
                    new XElement("city", Сity),
                    new XElement("servistag", data.ServisTag)));
                xdoc.Save("C:/DC/config.xml");

                wordDocument.SaveAs($"{SaveIn}{data.Name} {data.SName}.docx");
                wordApp.Visible = true;

                DeleteAllData();

                


                btnCreate.Cursor = Cursors.Hand;
        }
            catch (System.Runtime.InteropServices.COMException)
            {

                MessageBox.Show("Nie mozna znalezc plik wzorzec. Zrestartuj aplikację!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Proszę zgłosić błąd:\n"+Convert.ToString(ex));
            }
        }



        private void ReplaceWordApp(string stubToReplace, string text, Microsoft.Office.Interop.Word.Document document) //Zamiana
        {
            var range = document.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
        }

        public bool CheckedValues() //Sprawdzenie 
        {
           
            if (string.IsNullOrWhiteSpace(tbName.Text) || string.IsNullOrWhiteSpace(tbSName.Text) || string.IsNullOrWhiteSpace(tbPosition.Text) || string.IsNullOrWhiteSpace(tbServisTag.Text))
            {
                MessageBox.Show("Wszystkie pola muszą być uzupełnione!");
                return true;
            }

            string inccorectValues = "@!#%&*()/\\}{[]<>\";~+=^$?.,1234567890";
            string nameAndSurname = tbName.Text + tbSName.Text;
            for (int i =0;i<=nameAndSurname.Length-1;i++)
            {
                for (int j = 0;j<=inccorectValues.Length-1;j++)
                {
                    if (nameAndSurname[i] == inccorectValues[j])
                    {
                        MessageBox.Show("Pola nie mogą zawierac nastepujacych znakow @!#%&*()/\\}{[]<>\";~+=^$?.,1234567890");
                        return true;
                    }
                }

                
            }

        return false;
        }

        private string RichTextB() // Text Info
        {
            TextRange textRange = new TextRange(rtbInfo.Document.ContentStart, rtbInfo.Document.ContentEnd);
            if (textRange.Text == "")
            {
                return "";
            }
            string infoText = textRange.Text.Remove(textRange.Text.Length-1);
            return infoText;
        }


        private bool FileExists()
        {
            if (!File.Exists("C:/DC/savein.txt") || !File.Exists("C:/DC/WZOR.docx"))
            {
                return true;
            }

            if (!File.Exists("C:/DC/config.xml"))
            {
                MessageBox.Show("Brak pliku C:/DC/config.xml. Zrestartuj aplikację.");
                return true;
            }

            return false;
        }

        public void DeleteAllData()
        {
            tbName.Text = "";
            tbPosition.Text = "";
            tbServisTag.Text = "";
            tbSName.Text = "";
            TextRange textRange = new TextRange(rtbInfo.Document.ContentStart, rtbInfo.Document.ContentEnd);
            textRange.Text = "";
            ComboBox1.Text = "";
            ComboBox2.Text = "";
        }
    }
}
