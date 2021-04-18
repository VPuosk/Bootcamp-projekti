using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ReitinhaunLuokat
{
    enum Hakutyyppi
    {
        OLETUS,
        SUUNNATTU,
        PAINOTETTU,
        MOLEMMAT
    }

    public class Hakumenetelmä : IHaku
    {
        public List<Solmu> Solmulista { get; set; }
        public string AlkuSolmu { get; set; }
        public string LoppuSolmu { get; set; }
        public Dictionary<string, int> Solmusetti { get; set; }
        public int SolmujenMäärä { get; set; }
        public bool HakuTullutValmiiksi { get; set; }
        public bool RunsasTulostus { get; set; }
        public List<string> Tuloste { get; set; }

        public void AlustaHakuYleisesti()
        {
            Solmulista = new();
            Solmusetti = new();
            SolmujenMäärä = 0;
            HakuTullutValmiiksi = false;
            RunsasTulostus = false;
            Tuloste = new();
        }

        public virtual void Suorita()
        {
            // vain templaattipohja
        }

        public virtual List<string> Tulosta()
        {
            // vain templaattipohja
            return Tuloste;
        }

        public void LisääUusiSolmu(string solmunNimi)
        {
            // jos solmu on jo lisätty, palataan
            if (Solmusetti.ContainsKey(solmunNimi)) return;

            // muutoin lisätään uusi solmu
            Solmulista.Add(new(solmunNimi));
            Solmusetti.Add(solmunNimi, SolmujenMäärä);
            SolmujenMäärä++;
        }

        public void LisääUusiKaari(string alku, string loppu, int paino, int muoto)
        {
            int alkuIndeksi = Solmusetti[alku];
            int loppuIndeksi = Solmusetti[loppu];

            Kaari kaari = new();
            Kaari vastaKaari = new();

            switch (muoto)
            {
                case (int)Hakutyyppi.OLETUS:
                    kaari.AsetaKaarenArvot(alkuIndeksi, loppuIndeksi, 1);
                    vastaKaari.AsetaKaarenArvot(alkuIndeksi, loppuIndeksi, 1);
                    break;
                case (int)Hakutyyppi.PAINOTETTU:
                    kaari.AsetaKaarenArvot(alkuIndeksi, loppuIndeksi, paino);
                    vastaKaari.AsetaKaarenArvot(alkuIndeksi, loppuIndeksi, paino);
                    break;
                case (int)Hakutyyppi.SUUNNATTU:
                    kaari.AsetaKaarenArvot(alkuIndeksi, loppuIndeksi, 1);
                    vastaKaari.AsetaKaarenArvot(alkuIndeksi, loppuIndeksi, 0);
                    break;
                case (int)Hakutyyppi.MOLEMMAT:
                    kaari.AsetaKaarenArvot(alkuIndeksi, loppuIndeksi, paino);
                    vastaKaari.AsetaKaarenArvot(alkuIndeksi, loppuIndeksi, 0);
                    break;
                default:
                    break;
            }

            kaari.AsetaVastaKaari(vastaKaari);
            vastaKaari.AsetaVastaKaari(kaari);

            Solmulista[alkuIndeksi].LisääKaari(kaari);
            Solmulista[loppuIndeksi].LisääKaari(vastaKaari);
        }

        public string TulostaHaku()
        {
            if (HakuTullutValmiiksi == false)
            {
                return "Haku ei ole onnistunut";
            }

            Solmu solmu = Solmulista[Solmusetti[LoppuSolmu]];
            List<Solmu> tulosteLista = new();
            StringBuilder tuloste = new();

            // luodaan kuljetun polun reitti loppusolmusta alkusolmuun
            while (solmu.Edeltäjä != null)
            {
                tulosteLista.Add(solmu);
                solmu = Solmulista[solmu.Edeltäjä.Alku];
            }

            // lisätään aloitussolmu (jolla siis edeltäjä on aina 'null'
            tulosteLista.Add(Solmulista[Solmusetti[AlkuSolmu]]);

            // tulostetaan kuljetun polun reitti alkusolmusta loppusolmuun
            for (int i = tulosteLista.Count - 1; i >= 0; i--)
            {
                tuloste.Append(tulosteLista[i].Nimi);

                if (i > 0)
                {
                    tuloste.Append(" -> \n");
                }
            }

            return tuloste.ToString();
        }
    }
}
