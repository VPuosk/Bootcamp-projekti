using System;
using System.Collections.Generic;

#nullable disable

namespace Demo.Models
{
    public partial class Resepti
    {
        public Resepti()
        {
            Raakaaines = new HashSet<Raakaaine>();
        }

        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Ohje { get; set; }
        public DateTime Luotu { get; set; }
        public int Aika { get; set; }

        public virtual ICollection<Raakaaine> Raakaaines { get; set; }
    }
}
