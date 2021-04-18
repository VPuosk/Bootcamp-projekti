using System;
using System.Collections.Generic;

#nullable disable

namespace Demo.Models
{
    public partial class Keskustelu
    {
        public Keskustelu()
        {
            Kommenttis = new HashSet<Kommentti>();
        }

        public int Id { get; set; }
        public string Nimi { get; set; }
        public DateTime Luotu { get; set; }

        public virtual ICollection<Kommentti> Kommenttis { get; set; }
    }
}
