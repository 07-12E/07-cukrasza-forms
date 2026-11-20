using System;

namespace Futoverseny
{
    public class Versenyzo
    {
        public string Rajtszam { get; set; }
        public string Nev { get; set; }
        public DateTime SzuletesiDatum { get; set; }
        public string Orszag { get; set; }
        public TimeSpan Idoeredmeny { get; set; }

        public Versenyzo(string sor)
        {
            string[] adatok = sor.Split(';');
            Rajtszam = adatok[0];
            Nev = adatok[1];
            SzuletesiDatum = DateTime.Parse(adatok[2]);
            Orszag = adatok[3];
            Idoeredmeny = TimeSpan.Parse(adatok[4]);
        }

        public int Eletkor
        {
            get
            {
                int kor = DateTime.Now.Year - SzuletesiDatum.Year;
                if (DateTime.Now.DayOfYear < SzuletesiDatum.DayOfYear)
                {
                    kor--;
                }
                return kor;
            }
        }
    }
}