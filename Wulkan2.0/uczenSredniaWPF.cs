using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace uczenSredniaWPF
{
    internal class Uczen
    {
        private string imie;
        private string nazwisko;
        private string klasa;
        private int semestr;
        private double srednia;
        private string pesel;
        private DateTime data_ur;
        private List<int> oceny;

        public Uczen(string pesel, string imie, string zdjecie, string nazwisko, string klasa, int semestr, List<int> oceny)
        {
            this.pesel = pesel;
            this.imie = imie;
            this.nazwisko = nazwisko;
            this.klasa = klasa;
            this.Zdjecie = zdjecie;
            this.semestr = semestr;
            this.oceny = oceny;
            this.data_ur = Data_ur();
            this.srednia = SredniaOcenUcznia();
        }

        public string Zdjecie { get; set; }

        public string Srednia
        {
            get { return Math.Round(srednia, 2).ToString("F2"); }
            private set { srednia = double.Parse(value); }
        }

        public int IloscOcen()
        {
            return oceny.Count;
        }

        public double SredniaOcenUcznia()
        {
            double srednia = 0;
            int sumaOcen = 0;

            foreach (int i in oceny)
            {
                sumaOcen += i;
            }
            srednia = (double)sumaOcen / IloscOcen();

            Srednia = srednia.ToString();

            return Math.Round(srednia, 2);
        }


        public DateTime Data_ur()
        {
            int rok = int.Parse(pesel.Substring(0, 2));
            int miesiac = int.Parse(pesel.Substring(2, 2));
            int dzien = int.Parse(pesel.Substring(4, 2));

            if (miesiac >= 81 && miesiac <= 92)
            {
                rok += 1800;
                miesiac -= 80;
            }
            else if (miesiac >= 1 && miesiac <= 12)
            {
                rok += 1900;
            }
            else if (miesiac >= 21 && miesiac <= 32)
            {
                rok += 2000;
                miesiac -= 20;
            }
            else if (miesiac >= 41 && miesiac <= 52)
            {
                rok += 2100;
                miesiac -= 40;
            }
            else if (miesiac >= 61 && miesiac <= 72)
            {
                rok += 2200;
                miesiac -= 60;
            }

            return new DateTime(rok, miesiac, dzien);
        }

        public string Data
        {
            get { return data_ur.ToShortDateString(); }
        }

        public string Imie
        {
            get { return imie; }
        }

        public string Nazwisko
        {
            get { return nazwisko; }
        }

        public string Klasa
        {
            get { return klasa; }
        }

        public string Pesel
        {
            get { return pesel; }
        }

        public int Semestr
        {
            get { return semestr; }
        }

        public string Oceny()
        {
            string oceny = string.Join(" ", this.oceny);
            return oceny;
        }


    }
}