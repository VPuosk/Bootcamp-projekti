using System;
using System.Collections.Generic;

#nullable disable

namespace Demo.Models
{
    public partial class Raakaaine
    {
        public int Id { get; set; }
        public int Maara { get; set; }
        public string Yksikko { get; set; }
        public int Resepti { get; set; }
        public int Ainesosa { get; set; }

        public virtual Aine AinesosaNavigation { get; set; }
        public virtual Resepti ReseptiNavigation { get; set; }
    }
}
