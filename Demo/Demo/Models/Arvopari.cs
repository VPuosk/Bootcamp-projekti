// luodaan C# luokka laskutehtäviä varten

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Arvopari
    {
        public int A { get; set; }
        public int B { get; set; }
        public string Toiminto { get; set; }

        public int Summa()
        {
            return A + B;
        }

        public int Erotus()
        {
            return A - B;
        }

        public int Kertolasku()
        {
            return A * B;
        }

        public int Jakolasku()
        {
            return A / B;
        }
        
        public int Modulo()
        {
            return A % B;
        }

        public int Laske()
        {
            return Toiminto switch
            {
                "Summa" => this.Summa(),
                "Erotus" => this.Erotus(),
                "Kerto" => this.Kertolasku(),
                "Jako" => this.Jakolasku(),
                "Modulo" => this.Modulo(),
                _ => 0,
            };
        }
    }
    public enum Laskutoimitus
    {
        Summa,
        Erotus,
        Kerto,
        Jako,
        Modulo
    }
}
