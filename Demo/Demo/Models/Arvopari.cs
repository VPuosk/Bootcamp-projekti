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
    }
}
