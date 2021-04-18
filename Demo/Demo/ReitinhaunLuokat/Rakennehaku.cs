using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ReitinhaunLuokat
{
    // käytännössä kopio syvyyshausta, mutta sillä erotuksella, että tämä hakua koluaa koko puun.
    public class Rakennehaku : Hakumenetelmä, IHaku
    {
        public StringBuilder sb { get; set; }

        public Rakennehaku()
        {
            AlustaHakuYleisesti();
            sb = new();
        }

        public override void Suorita()
        {
            Solmu solmu = Solmulista[Solmusetti[AlkuSolmu]];
            solmu.Vierailtu = false;
            solmu.KokonaisPaino = 0;
            sb.Clear();
            sb.Append($"{solmu.Nimi} - ");
            foreach (Kaari kaari in solmu.Kaaret)
            {
                if (kaari.Paino > 0)
                {
                    TeeSyvyysHaku(kaari);
                    sb.Append($"{solmu.Nimi} - ");
                }
            }
        }

        public override List<string> Tulosta()
        {
            Tuloste.Add(sb.ToString());
            return Tuloste;
        }

        private void TeeSyvyysHaku(Kaari tuloKaari)
        {
            // jos on jo käyty, ohita
            Solmu käsiteltäväSolmu = Solmulista[tuloKaari.Loppu];

            käsiteltäväSolmu.Vierailtu = true;

            sb.Append($"{käsiteltäväSolmu.Nimi} - ");

            foreach (Kaari kaari in käsiteltäväSolmu.Kaaret)
            {
                if ((kaari.Paino > 0) && (Solmulista[kaari.Loppu].Vierailtu == false))
                {
                    TeeSyvyysHaku(kaari);
                    sb.Append($"{käsiteltäväSolmu.Nimi} - ");
                }
            }
        }
    }
}
