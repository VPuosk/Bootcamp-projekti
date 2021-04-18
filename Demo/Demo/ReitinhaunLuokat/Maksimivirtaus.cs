using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ReitinhaunLuokat
{
    public class Maksimivirtaus : Hakumenetelmä, IHaku
    {
        // Edmunds-Karp versio (eli leveyshakuun perustuva)
        public int MaksimiVirtaus { get; set; }
        public Queue<int> Solmujono { get; set; }

        public Maksimivirtaus()
        {
            AlustaHakuYleisesti();
            Solmujono = new();
            MaksimiVirtaus = 0;
        }

        // koska tämä rutiini pitää käydä läpi useita kertoja,
        // niin tätä funktiota tarvitaan alustamaan haku uudelleen
        public void NollaaVieraillut()
        {
            foreach (Solmu solmu in Solmulista)
            {
                solmu.Vierailtu = false;
            }
        }

        public void AlustaHakuKierros()
        {
            // varmisteluja
            NollaaVieraillut();
            HakuTullutValmiiksi = false;

            // Laitetaan kaikki valmiiksi 'sorsan' päästä.
            // aloitusindeksi, paikka jonosta, vierailudata, edeltäjä
            int aloitusIndeksi = Solmusetti[AlkuSolmu];
            Solmujono.Enqueue(aloitusIndeksi);
            Solmu solmu = Solmulista[aloitusIndeksi];
            solmu.Vierailtu = true;
            solmu.Edeltäjä = null;

            // varmistetaan että 'sinkin' edeltäjä on null.
            Solmulista[Solmusetti[LoppuSolmu]].Edeltäjä = null;

        }

        public void TeeHakuKierros()
        {
            int seuraavaIndeksi;
            int käsiteltäväIndeksi;
            while (Solmujono.Count > 0)
            {
                käsiteltäväIndeksi = Solmujono.Dequeue();
                Solmu solmu = Solmulista[käsiteltäväIndeksi];

                // tarkistetaan onko saavuttu jo perille
                if (käsiteltäväIndeksi == Solmusetti[LoppuSolmu])
                {
                    // Eli perillä ollaan. Terminoidaan haku.
                    HakuTullutValmiiksi = true;
                    Solmujono.Clear();
                    return;
                }
                // käydään läpi kaaret ja uudet solmut
                foreach (Kaari kaari in solmu.Kaaret)
                {
                    // jos verkko on painotettu tai kaarella ei muutoin ole painoa.. ei jatketa
                    if (kaari.Paino <= 0) continue;

                    seuraavaIndeksi = kaari.Loppu;
                    Solmu seuraavaSolmu = Solmulista[seuraavaIndeksi];

                    // jos solmussa on jo käyty, tai
                    // se on jo lisätty, passaa                    
                    if (seuraavaSolmu.Vierailtu) continue;

                    // lisätään jonoon, ja vierailtaviin
                    Solmujono.Enqueue(seuraavaIndeksi);
                    seuraavaSolmu.Vierailtu = true;
                    seuraavaSolmu.Edeltäjä = kaari;
                }
            }
        }

        // Jotta saadaan reitin pienin virtaus määritettyä
        // Käydään läpi reitti 'takaperin' loppusolmusta
        // koska haku muodostaa spesifisen kaari polun, saadaan
        // näin reitti käytyä läpi.
        public int EtsiPieninVirtausReitillä()
        {
            Solmu solmu = Solmulista[Solmusetti[LoppuSolmu]];
            int alhaisinVirtaus = int.MaxValue;

            while (solmu.Edeltäjä != null)
            {
                Kaari kaari = solmu.Edeltäjä;

                if (kaari.Paino < alhaisinVirtaus)
                {
                    alhaisinVirtaus = kaari.Paino;
                }
                solmu = Solmulista[kaari.Alku];
            }

            return alhaisinVirtaus;
        }

        // Jotta saadaan virtausta vähennettyä reitillä.
        // Toistetaan sama polkukäsittely kuin mitä tehtiin
        // pienintä virtausta haettaessa.
        public void VähennäVirtaustaReitillä(int vähennys)
        {
            Solmu solmu = Solmulista[Solmusetti[LoppuSolmu]];

            while (solmu.Edeltäjä != null)
            {
                Kaari kaari = solmu.Edeltäjä;
                kaari.Paino -= vähennys;
                kaari.VastaKaari.Paino += vähennys;
                solmu = Solmulista[kaari.Alku];
            }

            if (RunsasTulostus)
            {
                Tuloste.Add($"{TulostaHaku()}, reitin virtaus: {vähennys}");
            }
        }

        public override void Suorita()
        {

            int uusiVirtaus;

            // eli niin kauan kun haku muuttaa maksimivirtausta
            // hakua toistetaan
            while (true)
            {
                AlustaHakuKierros();
                TeeHakuKierros();

                uusiVirtaus = EtsiPieninVirtausReitillä();

                // arvo alustetaan int32.maxvalue:n arvoon,
                // joten jos tuo palautetaan voidaan olettaa, että
                // haku ei onnistunut ja uutta kierrosta ei enää tarvita
                if (uusiVirtaus == int.MaxValue)
                {
                    break;
                }
                else
                {
                    VähennäVirtaustaReitillä(uusiVirtaus);
                    MaksimiVirtaus += uusiVirtaus;
                }
            }
        }

        public override List<string> Tulosta()
        {
            Tuloste.Add($"Maksimivirtaus: {MaksimiVirtaus}");
            return Tuloste;
        }
    }
}
