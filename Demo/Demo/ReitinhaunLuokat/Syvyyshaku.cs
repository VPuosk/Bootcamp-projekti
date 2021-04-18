using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.ReitinhaunLuokat
{
    public class Syvyyshaku : Hakumenetelmä, IHaku
    {
        public Syvyyshaku()
        {
            AlustaHakuYleisesti();
        }

        public override void Suorita()
        {
            Solmu solmu = Solmulista[Solmusetti[AlkuSolmu]];
            solmu.Vierailtu = true;
            solmu.Edeltäjä = null;
            solmu.KokonaisPaino = 0;
            foreach (Kaari kaari in solmu.Kaaret)
            {
                if (kaari.Paino > 0) TeeSyvyysHaku(kaari);
            }
        }

        public override List<string> Tulosta()
        {
            Tuloste.Add(TulostaHaku());
            return Tuloste;
        }

        private void TeeSyvyysHaku(Kaari tuloKaari)
        {
            // jos haku on jo valmis, lopeta heti
            if (HakuTullutValmiiksi) return;

            // jos on jo käyty, ohita
            Solmu käsiteltäväSolmu = Solmulista[tuloKaari.Loppu];
            if (käsiteltäväSolmu.Vierailtu)
            {
                return;
            }

            // merkitään solmu vierailluksi,
            // tallennetaan tulokaari edeltäjä kenttään.
            käsiteltäväSolmu.Vierailtu = true;
            käsiteltäväSolmu.Edeltäjä = tuloKaari;

            // tallennetaan solmun paino
            käsiteltäväSolmu.KokonaisPaino = Solmulista[tuloKaari.Alku].KokonaisPaino + tuloKaari.Paino;

            if (käsiteltäväSolmu.Nimi == LoppuSolmu)
            {
                // saavutettu lopetussolmu, lopetetaan haku
                HakuTullutValmiiksi = true;
            }

            foreach (Kaari kaari in käsiteltäväSolmu.Kaaret)
            {
                if (kaari.Paino > 0) TeeSyvyysHaku(kaari);
            }
        }
    }
}
