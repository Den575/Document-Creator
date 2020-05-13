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
            //try
            //{
                Data data = new Data();

                var wordApp = new Microsoft.Office.Interop.Word.Application();


                wordApp.Visible = false;

                var wordDocument = wordApp.Documents.Open("C:/DC/WZOR.docx");



                ReplaceWordApp("<name>", data.NarzednikImie(user.Name), wordDocument);
                ReplaceWordApp("<stanowisko>", user.Job, wordDocument);
                ReplaceWordApp("<surname>", data.NarzednikNazwisko(user.Surname), wordDocument);
                ReplaceWordApp("<computer>", user.Computer, wordDocument);
                ReplaceWordApp("<date>", user.Date, wordDocument);
                ReplaceWordApp("<date1>", user.Date, wordDocument);
                ReplaceWordApp("<date2>", user.Date, wordDocument);
                ReplaceWordApp("<name&surname> ", $"{user.Name} {user.Surname}", wordDocument);
                ReplaceWordApp("<name&surname1> ", $"{user.Surname} {user.Surname}", wordDocument);
                ReplaceWordApp("<stag> ", user.ServisTag, wordDocument);
                ReplaceWordApp("<endofword> ", data.EndOfWord(user.Name), wordDocument);
                ReplaceWordApp("<anotherinfo>", user.Info, wordDocument);


                wordDocument.SaveAs($"C:/DC/{user.Name} {user.Surname}.docx");
                wordApp.Visible = true;
           
            //}
            //catch (System.Runtime.InteropServices.COMException)
            //{

            //    MessageBox.Show("Nie mozna znalezc plik wzorzec. Zrestartuj aplikację!");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Proszę zgłosić błąd:\n" + Convert.ToString(ex));
            //}
        }


        private void ReplaceWordApp(string stubToReplace, string text, Microsoft.Office.Interop.Word.Document document) //Zamiana
        {
            var range = document.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
        }

    }
}

