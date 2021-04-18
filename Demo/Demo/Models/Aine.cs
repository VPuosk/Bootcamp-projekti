using System;
using System.Collections.Generic;

#nullable disable

namespace Demo.Models
{
    public partial class Aine
    {
        public Aine()
        {
            Raakaaines = new HashSet<Raakaaine>();
        }

        public int Id { get; set; }
        public string Nimi { get; set; }

        public virtual ICollection<Raakaaine> Raakaaines { get; set; }
    }
}
