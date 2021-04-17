﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Leveyshaku : Hakumenetelmä, IHaku
    {
        public Queue<int> Solmujono { get; set; }

        public Leveyshaku()
        {
            AlustaHakuYleisesti();
            Solmujono = new();
        }

        public override void Suorita()
        {
            // alustetaan ensimmäinen solmu ja laitetaan
            // leveyshaku liikkeelle.
            int aloitusIndeksi = Solmusetti[AlkuSolmu];
            Solmujono.Enqueue(aloitusIndeksi);
            Solmu solmu = Solmulista[aloitusIndeksi];
            solmu.Vierailtu = true;
            solmu.KokonaisPaino = 0;
            solmu.Edeltäjä = null;
            TeeLeveyshaku();
        }

        private void TeeLeveyshaku()
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
                    seuraavaSolmu.KokonaisPaino = solmu.KokonaisPaino + kaari.Paino;
                    seuraavaSolmu.Edeltäjä = kaari;
                }
            }
        }

        public override string Tulosta()
        {
            return TulostaHaku();
        }
    }
}
