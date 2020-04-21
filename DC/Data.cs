using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;

namespace DC
{
    internal class Data
    {

        internal string Name { get; set; }
        internal string SName { get; set; }
        internal string Proffesion { get; set; }

        internal string Date { get; set; }
        internal string ServisTag { get; set; }


        internal string NarzednikImie(string imie)
        {
            if (imie[imie.Length - 1] != 'a')
            {
                return imie + "em";
            }
            return Chenger(imie);
        }

        internal string NarzednikNazwisko(string nazwisko)
        {
            string[] surnames = nazwisko.Split('-');
            string nazwiska ="";
            List<string> znaczenia = new List<string>();
            foreach(string s in surnames)
            {
                if (s[s.Length - 1] != 'a')
                {
                    znaczenia.Add(s);
                }
                else
                {
                    znaczenia.Add(Chenger(s));
                }
            }
            
            foreach(string s in znaczenia)
            {
                nazwiska += s+"-";
            }
            nazwiska = nazwiska.Remove(nazwiska.Length-1);
            return nazwiska;
        }

        internal string Chenger(string value)
        {
            var nameList = new List<char>();
            foreach (var n in value)
            {
                nameList.Add(n);
            }
            nameList[nameList.Count - 1] = 'ą';
            value = "";
            foreach (var s in nameList)
            {
                value += s;
            }
            return value;
        }

        internal string EndOfWord(string Name)
        {
            if (Name[Name.Length - 1] == 'a')
            {
                return "a ";
            }
            else
            {
                return "ym ";
            }
        }




    }
}
