using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ReitinhaunLuokat
{
    public class Djikstra : Hakumenetelmä, IHaku
    {
        public Keko HakuKeko { get; set; }

        public Djikstra()
        {
            AlustaHakuYleisesti();
            HakuKeko = new();
        }

        public override void Suorita()
        {
            int indeksi = Solmusetti[AlkuSolmu];
            Solmu solmu = Solmulista[indeksi];
            HakuKeko.LisääKekoon(indeksi, 0);
            solmu.KokonaisPaino = 0;
            solmu.Edeltäjä = null;
            TeeDijkstranHaku();
        }

        private void TeeDijkstranHaku()
        {
            int nykyEtäisyys;
            int uusiEtäisyys;
            
            StringBuilder sb = new();

            while (HakuKeko.Koko() > 0)
            {
                int[] tiedot = HakuKeko.OtaArvo();
                int indeksi = tiedot[0];
                Solmu solmu = Solmulista[indeksi];

                if (indeksi == Solmusetti[LoppuSolmu])
                {
                    HakuTullutValmiiksi = true;
                    break;
                }

                // jos tässä solmussa on jo käyty, niin jatka
                if (solmu.Vierailtu) continue;

                //System.Console.WriteLine(solmu);
                // kirjataan lisäteksti osuuteen, että tässä solmussa on nyt käyty:
                if (RunsasTulostus) sb.Append($"{solmu.Nimi} -> ");

                // nyt täällä on käyty
                solmu.Vierailtu = true;

                foreach (Kaari kaari in solmu.Kaaret)
                {
                    // jos kaarella ei ole painoa, ei jatketa
                    if (kaari.Paino <= 0) continue;

                    // muutoin jatketaan lisäämisprosessointia
                    Solmu uusiSolmu = Solmulista[kaari.Loppu];
                    nykyEtäisyys = uusiSolmu.KokonaisPaino;
                    uusiEtäisyys = solmu.KokonaisPaino + kaari.Paino;

                    // eli keossa ei ole tätä ainakaan näin hyvällä vertailuarvolla
                    // lisätään kekoon
                    if (uusiEtäisyys < nykyEtäisyys)
                    {
                        uusiSolmu.KokonaisPaino = uusiEtäisyys;
                        uusiSolmu.Edeltäjä = kaari;
                        HakuKeko.LisääKekoon(kaari.Loppu, uusiEtäisyys);
                        // System.Console.WriteLine($"Lisätty kekoon: {kaari.Loppu} - vertailuarvo: {uusiEtäisyys}");
                        // System.Console.WriteLine(HakuKeko.ToString());
                    }
                }
            }

            // jos halutaan lisää infoa 
            if (RunsasTulostus)
            {
                Tuloste.Add(sb.ToString());
            }
        }

        public override List<string> Tulosta()
        {
            Tuloste.Add(TulostaHaku());
            return Tuloste;
        }
    }
}
