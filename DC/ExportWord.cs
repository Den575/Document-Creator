using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
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
    class ExportWord
    {
        public void CreateWordDocument(User user)
        {

            if (File.Exists($"C:/Users/{Environment.UserName}/Downloads/{user.Name} {user.Surname}.docx"))
            {
                MessageBox.Show($"C:/Users/{Environment.UserName}/Downloads/{user.Name} {user.Surname}.docx już istnieje!");
                return;
            }

            try
            {
                Data data = new Data();

                var wordApp = new Microsoft.Office.Interop.Word.Application();


                wordApp.Visible = false;

                var wordDocument = wordApp.Documents.Open("C:/DC/WZOR.docx");

                ReplaceWordApp("<name>", data.NarzednikImie(user.Name), wordDocument);
                ReplaceWordApp("<stanowisko>", user.Job.ToUpper(), wordDocument);
                ReplaceWordApp("<surname>", data.NarzednikNazwisko(user.Name, user.Surname), wordDocument);
                ReplaceWordApp("<computer>", user.Computer, wordDocument);
                ReplaceWordApp("<date>", user.Date, wordDocument);
                ReplaceWordApp("<date1>", user.Date, wordDocument);
                ReplaceWordApp("<date2>", user.Date, wordDocument);
                ReplaceWordApp("<date3>", user.Date, wordDocument);
                ReplaceWordApp("<name&surname>", $"{user.Name} {user.Surname}", wordDocument);
                ReplaceWordApp("<name&surname1>", $"{user.Name} {user.Surname}", wordDocument);
                ReplaceWordApp("<stag>", user.ServisTag, wordDocument);
                ReplaceWordApp("<endofword> ", data.EndOfWord(user.Name), wordDocument);
                ReplaceWordApp("<anotherinfo>", user.Info.Remove(user.Info.Length-1), wordDocument);
                ReplaceWordApp("<city>", GetCity(user.City), wordDocument);
                ReplaceWordApp("<city2>", user.City, wordDocument);


                wordDocument.SaveAs($"C:/Users/{Environment.UserName}/Downloads/{user.Name} {user.Surname}.docx");
                wordApp.Visible = true;

            }
            catch (System.Runtime.InteropServices.COMException)
            {

                MessageBox.Show("Nie mozna znalezc plik wzorzec. Zrestartuj aplikację!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Proszę zgłosić błąd:\n" + Convert.ToString(ex));
            }
        }


        private void ReplaceWordApp(string stubToReplace, string text, Microsoft.Office.Interop.Word.Document document) //Zamiana
        {
            var range = document.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
        }

        private string GetCity(string city)
        {
            if (city == "Kraków")
            {
                return "Krakowie";
            }
            if(city == "Pruszcz Gdański")
            {
                return "Pruszczu Gdańskim";
            }
            else if(city == "Gdańsk")
            {
                return "Gdańsku";
            }
            return "Krakowie";
        }

    }
}

