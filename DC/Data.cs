using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (nazwisko[nazwisko.Length - 1] != 'a')
            {
                return nazwisko;
            }

            return Chenger(nazwisko);
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




    }
}
