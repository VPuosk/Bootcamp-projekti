using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Solmu
    {
        public List<Kaari> Kaaret { get; set; }
        public Kaari Edeltäjä { get; set; }
        public string Nimi { get; set; }
        public int KokonaisPaino { get; set; }
        public bool Vierailtu { get; set; }

        public Solmu()
        {
            Kaaret = new();
        }

        public Solmu(string nimi)
        {
            Kaaret = new();
            Nimi = nimi;
            KokonaisPaino = Int32.MaxValue;
            Vierailtu = false;
        }

        public void LisääKaari(Kaari kaari)
        {
            this.Kaaret.Add(kaari);
        }

        public override string ToString()
        {
            StringBuilder tuloste = new();
            tuloste.Append($"Solmu: {Nimi}\n\tMatka: {KokonaisPaino}");
            //tuloste.Append("Solmu: " + Nimi + "\n\tKaaret: ");
            //tuloste.AppendJoin("; ", Kaaret);
            tuloste.Append($"\n\tEdeltäjä: {Edeltäjä}\n");
            return tuloste.ToString();
        }
    }
}
