using System;
using System.Collections.Generic;

#nullable disable

namespace Demo.Models
{
    public partial class Kommentti
    {
        public int Id { get; set; }
        public string Otsikko { get; set; }
        public string Teksti { get; set; }
        public DateTime Luotu { get; set; }
        public string Tekija { get; set; }
        public int Keskustelu { get; set; }

        public virtual Keskustelu KeskusteluNavigation { get; set; }
    }
}
