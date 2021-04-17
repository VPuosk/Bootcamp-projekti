using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Reitinhaku
    {
        // tänne rakennetaan palautusjärjestelmä pyydetylle tiedolle
        public Hakumenetelmä HakuMenetelmä { get; set; }
        public string Palautus { get; set; }

        private void LueKaariLista(string kaariTiedot, Hakutyyppi tyyppi)
        {
            // pilkotaan textarea:n syöte riveittäin
            string[] kaaret = kaariTiedot.Split("\n");
            foreach (string rivi in kaaret)
            {
                // pilkotaan rivit kolmeen komponenttiin
                // 1. alkusolmu
                // 2. loppusolmu
                // 3. paino/virtaus
                // jos ei onnistu, heitetään pyyhe kehään ja lopetetaan.
                string[] tiedot = rivi.Split(" ");

                // jos annetaan tyhjä rivi, niin oletetaan loppuneeksi.
                if (tiedot.Length != 3)
                {
                    Palautus = $"Virheellinen syöte: {tiedot}";
                    return;
                }

                // muutoin syötetään tietoja koneelle
                try
                {
                    string alku = tiedot[0];
                    string loppu = tiedot[1];
                    int paino = Int32.Parse(tiedot[2]);

                    // lisätään (tarvittaessa) solmut
                    HakuMenetelmä.LisääUusiSolmu(alku);
                    HakuMenetelmä.LisääUusiSolmu(loppu);

                    // lisätään itse kaari
                    HakuMenetelmä.LisääUusiKaari(alku, loppu, paino, (int) tyyppi);
                }
                catch (Exception)
                {
                    Palautus = $"Virheellinen syöte: {tiedot}";
                    return;
                }
            }
        }

        // ensimmäinen kutsufunktio - datan validointi tapahtuu täällä
        public string SuoritaHaku(string alku, string loppu, string muoto, string tulostus, string kaaret, string toiminto)
        {

            // valitaan toiminto
            switch (toiminto)
            {
                case "syvyys":
                    HakuMenetelmä = new Syvyyshaku();
                    break;
                case "leveys":
                    HakuMenetelmä = new Leveyshaku();
                    break;
                case "djikstra":
                    HakuMenetelmä = new Djikstra();
                    break;
                case "maksimivirtaus":
                    HakuMenetelmä = new Maksimivirtaus();
                    break;
                case "rakenne":
                    break;
                default:
                    // tämä loppui sitten tähän
                    return "Virheellinen toiminto. Toiminto tulee valita ennen jatkamista.";
            }

            // alettaan täyttää tietokenttiä
            HakuMenetelmä.AlkuSolmu = alku;
            HakuMenetelmä.LoppuSolmu = loppu;

            HakuMenetelmä.LisääUusiSolmu(alku);
            HakuMenetelmä.LisääUusiSolmu(loppu);

            // varmistetaan ettei alku ja loppupiste ole sama.
            if (alku == loppu) return "Virheellinen tieto syötetty. Alkupiste ja loppupiste eivät voi olla sama piste.";

            // varmistetaan ettei hakua voi tapahtua ilman loppusolmua
            if ((toiminto == "rakenne") && (loppu == "")) return "Virheellinen tieto syötetty. Loppupiste on määritettävä.";
            
            Hakutyyppi hakutyyppi;

            switch (muoto)
            {
                case "oletus":
                    hakutyyppi = Hakutyyppi.OLETUS;
                    break;
                case "painotettu":
                    hakutyyppi = Hakutyyppi.PAINOTETTU;
                    break;
                case "suunnattu":
                    hakutyyppi = Hakutyyppi.SUUNNATTU;
                    break;
                case "molemmat":
                    hakutyyppi = Hakutyyppi.MOLEMMAT;
                    break;
                default:
                    // loppuu tähän jos ei mikään aiemmista toimi
                    return "Virheellinen tiedon muoto.";
            }

            // Tiedon tuloksen muoto
            // To-Do!!
            if (tulostus == "laajennettu") HakuMenetelmä.RunsasTulostus = true;

            LueKaariLista(kaaret, hakutyyppi);

            // jos virheitä on havaittu, lopeta suoritus.
            if (Palautus != null) return Palautus;
                
            HakuMenetelmä.Suorita();

            // palautetaan pyydetty tietokenttä
            return HakuMenetelmä.Tulosta();
        }
    }
}
