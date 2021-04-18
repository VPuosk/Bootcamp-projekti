using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ReitinhaunLuokat
{
    public class Keko
    {
        public List<KekoVertailija> Taulukko { get; set; }
        private int PuunKoko { get; set; }

        // koska haluamme pitää '0' indeksin tyhjänä
        // keon toiminta
        //          1       -> n
        //         / \
        //        2   3     -> (2*n) ja (2*n)+1
        //       /\   /\
        //      4  5 6  7
        // ----------------------

        // konstruktori
        public Keko()
        {
            Taulukko = new();
            Taulukko.Add(new KekoVertailija(-1, -1));
            PuunKoko = 0;
        }

        // Palautetaan ensimmäinen arvo, jos taulukko on tyhjä
        // palautetaan {-1, -1}.
        public int[] LueArvo()
        {
            if (PuunKoko == 0)
            {
                return Taulukko[0].PalautaTaulu();
            }
            else
            {
                return Taulukko[1].PalautaTaulu();
            }
        }

        // Vaihtoehtoinen palautus, vain indeksiarvo
        public int LueIndeksiArvo()
        {
            if (PuunKoko == 0)
            {
                return -1;
            }
            else
            {
                return Taulukko[1].Indeksi;
            }
        }

        // lukumenetelmä, joka heittää edeltävän arvon pois keosta
        // palauttaa taulukko int[2]
        public int[] OtaArvo()
        {
            if (PuunKoko == 0)
            {
                int[] palautus = { -1, -1 };
                return palautus;
            }
            else
            {
                KekoVertailija luettavaArvo = Taulukko[1];
                Taulukko[1] = Taulukko[PuunKoko];
                PuunKoko--;
                JärjestäKekoYlhäältä(1);
                return luettavaArvo.PalautaTaulu();
            }
        }

        // vastaava, mutta palauttaa vain indeksiarvon
        public int OtaIndeksiArvo()
        {
            if (PuunKoko == 0)
            {
                return -1;
            }
            else
            {
                int luettavaArvo = Taulukko[1].Indeksi;
                Taulukko[1] = Taulukko[PuunKoko];
                PuunKoko--;
                JärjestäKekoYlhäältä(1);
                return luettavaArvo;
            }
        }

        public int Koko()
        {
            return PuunKoko;
        }

        public void LisääKekoon(int indeksi, int arvo)
        {
            // lisätään puunkokoa yhdellä
            PuunKoko++;

            // jos taulukko on pienemi -> tehdään uusi olio
            if (Taulukko.Count <= PuunKoko)
            {
                Taulukko.Add(new KekoVertailija(indeksi, arvo));
            }
            else
            {
                //System.Console.WriteLine("DEBUG: 1: " + PuunKoko);
                //System.Console.WriteLine("DEBUG: 2: " + Taulukko.Count);
                Taulukko[PuunKoko] = new KekoVertailija(indeksi, arvo);
            }

            JärjestäKekoYlhäältä(JärjestäKekoAlhaalta(PuunKoko));
        }

        private int JärjestäKekoAlhaalta(int indeksi)
        {
            // jos ollaan jo alkukohdassa, lopetetaan
            if (indeksi == 1) return 1;

            // etsitään solun vanhempi (parent)
            int vanhempi = indeksi / 2;

            if (Taulukko[indeksi].Vertailija < Taulukko[vanhempi].Vertailija)
            {
                // vaihdetaan alkiot
                VaihdaAlkiot(indeksi, vanhempi);
                // jatketaan rekursiota
                return JärjestäKekoAlhaalta(vanhempi);
            }

            return indeksi;
        }

        private void VaihdaAlkiot(int n, int m)
        {
            // System.Console.WriteLine($"Vaihdetaan alkiot: {n} ({Taulukko[n].Vertailija}) ja {m} ({Taulukko[m].Vertailija})");
            KekoVertailija temp = Taulukko[n];
            Taulukko[n] = Taulukko[m];
            Taulukko[m] = temp;
        }

        private void JärjestäKekoYlhäältä(int indeksi)
        {
            int ensimmäinen = indeksi * 2;
            int toinen = ensimmäinen + 1;

            // kumpikaan solmuista ei ole enään keossa
            if (PuunKoko < ensimmäinen)
            {
                // lopeta
                return;
            }

            if (PuunKoko == ensimmäinen)
            {
                // jos nykyinen arvo on pienempi
                // vaihdetaan alkiot, ei tarvitse jatkaa
                if (Taulukko[indeksi].Vertailija > Taulukko[ensimmäinen].Vertailija) VaihdaAlkiot(ensimmäinen, indeksi);
                return;
            }

            // oletustilanne, keko on suurempi
            int pieninSeuraaja = toinen;

            if (Taulukko[ensimmäinen].Vertailija < Taulukko[toinen].Vertailija)
            {
                pieninSeuraaja = ensimmäinen;
            }

            if (Taulukko[indeksi].Vertailija > Taulukko[pieninSeuraaja].Vertailija)
            {
                VaihdaAlkiot(pieninSeuraaja, indeksi);
                JärjestäKekoYlhäältä(pieninSeuraaja);
            }
        }

        public override string ToString()
        {
            StringBuilder riiminRakentaja = new($"PK: {PuunKoko} - ");

            foreach (KekoVertailija olio in Taulukko)
            {
                riiminRakentaja.Append($"{olio.Indeksi}/{olio.Vertailija}  ");
            }
            return riiminRakentaja.ToString();
        }
    }

    public class KekoVertailija
    {
        public int Indeksi { get; set; }
        public int Vertailija { get; set; }

        public KekoVertailija(int arvo)
        {
            Indeksi = arvo;
            Vertailija = arvo;
        }
        public KekoVertailija(int indeksi, int arvo)
        {
            Indeksi = indeksi;
            Vertailija = arvo;
        }

        public void UudetArvot(int indeksi, int arvo)
        {
            Indeksi = indeksi;
            Vertailija = arvo;
        }

        public int[] PalautaTaulu()
        {
            int[] palautusTaulu = { Indeksi, Vertailija };
            return palautusTaulu;
        }
    }
}
