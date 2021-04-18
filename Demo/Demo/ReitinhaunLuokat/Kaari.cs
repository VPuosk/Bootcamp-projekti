using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.ReitinhaunLuokat
{
    public class Kaari
    {

        public int Alku { get; set; }
        public int Loppu { get; set; }
        public int Paino { get; set; }
        public int AlkuPaino { get; set; }
        public Kaari VastaKaari { get; set; }

        public Kaari()
        {

        }

        public Kaari(int alku, int loppu, int paino)
        {
            Alku = alku;
            Loppu = loppu;
            Paino = paino;
            AlkuPaino = paino;
            VastaKaari = null;
        }

        public void AsetaKaarenArvot(int alku, int loppu, int paino)
        {
            Alku = alku;
            Loppu = loppu;
            Paino = paino;
            AlkuPaino = paino;
            VastaKaari = null;
        }

        public void AsetaVastaKaari(Kaari vastaKaari)
        {
            VastaKaari = vastaKaari;
        }

        public override string ToString()
        {
            return Alku + ", " + Loppu + ": " + Paino;
        }
    }
}
