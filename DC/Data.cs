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


            string check = "";
            foreach(var n in imie)
            {
                if(n==' ')
                {
                    continue;
                }
                else
                {
                    check += n;
                }
            }
            if (check[check.Length - 1] == 'ł')
            {
                return check;
            }
            if (check[check.Length - 1] != 'a')
            {
                return check + "em";
            }
            return Chenger(check);
        }

        internal string NarzednikNazwisko(string imie, string nazwisko)
        {
            string check = "";
            foreach(var n in nazwisko)
            {
                if(n==' ')
                {
                    continue;
                }
                else
                {
                    check += n;
                }
            }

            string[] surnames = check.Split('-');
            string nazwiska ="";
            List<string> znaczenia = new List<string>();
            foreach(string s in surnames)
            {
                if (imie[imie.Length - 1] == 'a' && s[s.Length-1]=='a')
                {
                    znaczenia.Add(Chenger(s));
                }
                if (imie[imie.Length - 1] == 'a')
                {
                    string spolgloski = "qwrtpsdfghjklzxcvbnmśżńąóo";
                    foreach(char k in spolgloski)
                    {
                        if (s[s.Length - 1] == k)
                        {
                            znaczenia.Add(s);
                        }
                    }
                }
                else if (s[s.Length - 1] == 'i')
                {
                    return s + "m";
                }
                else if (s[s.Length - 1] == 'k')
                {
                    return s + "iem";
                }
                else if (s[s.Length - 1] == 'l')
                {
                    return s + "em";
                }
                else if (s[s.Length - 1] == 'ń')
                {
                    return s.Replace("ń","n") + "em";
                }
                else
                {
                    znaczenia.Add(Chenger(s));
                }
            }
            
            foreach(string s in znaczenia)
            {
                nazwiska += char.ToUpper(s[0])+s.Substring(1).ToLower()+"-";
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
            if (nameList[nameList.Count - 1]!= 'a'){
                return value;
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
