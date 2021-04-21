using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.ReitinhaunLuokat
{
    public class Reitinhaku
    {
        public Reitinhaku()
        {
            Palautus = new();
        }

        // tänne rakennetaan palautusjärjestelmä pyydetylle tiedolle
        public Hakumenetelmä HakuMenetelmä { get; set; }
        public List<string> Palautus { get; set; }

        private void LueKaariLista(string kaariTiedot, Hakutyyppi tyyppi)
        {
            // pilkotaan textarea:n syöte riveittäin tai pikkuun (vast) asti
            char[] katkaisijat = { ',', ';', '.', '\n' };
            string[] kaaret = kaariTiedot.Split(katkaisijat);
            foreach (string rivi in kaaret)
            {

                // jos tulee täysin tyhjä syöte, niin ohitetaan
                if (rivi == "")
                {
                    continue;
                }

                // poistetaan mahdolliset rivin alussa ja/tai lopuuss olevat tyhjät merkit
                string trimmattuRivi = rivi.Trim(' ');
                // pilkotaan rivit kolmeen komponenttiin
                // 1. alkusolmu
                // 2. loppusolmu
                // 3. paino/virtaus
                // jos ei onnistu, heitetään pyyhe kehään ja lopetetaan.
                string[] tiedot = trimmattuRivi.Split(" ");

                // jos annetaan outoa tai väärää tietoa, niin oletetaan loppuneeksi.
                if (tiedot.Length != 3)
                {
                    Palautus.Add($"Virheellinen syöte: {tiedot}");
                    return;
                }

                // muutoin syötetään tietoja koneelle
                try
                {
                    string alku = tiedot[0];
                    string loppu = tiedot[1];
                    int paino = int.Parse(tiedot[2]);

                    // lisätään (tarvittaessa) solmut
                    HakuMenetelmä.LisääUusiSolmu(alku);
                    HakuMenetelmä.LisääUusiSolmu(loppu);

                    // lisätään itse kaari
                    HakuMenetelmä.LisääUusiKaari(alku, loppu, paino, (int)tyyppi);
                }
                catch (Exception)
                {
                    Palautus.Add($"Virheellinen syöte: {tiedot}");
                    return;
                }
            }
        }

        // ensimmäinen kutsufunktio - datan validointi tapahtuu täällä
        public List<string> SuoritaHaku(string alku, string loppu, string muoto, string tulostus, string kaaret, string toiminto)
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
                case "dijkstra":
                    HakuMenetelmä = new Djikstra();
                    break;
                case "maksimivirtaus":
                    HakuMenetelmä = new Maksimivirtaus();
                    break;
                case "rakenne":
                    HakuMenetelmä = new Rakennehaku();
                    break;
                default:
                    // tämä loppui sitten tähän
                    Palautus.Add("Virheellinen toiminto. Toiminto tulee valita ennen jatkamista.");
                    return Palautus;
            }

            // alettaan täyttää tietokenttiä
            HakuMenetelmä.AlkuSolmu = alku;
            HakuMenetelmä.LoppuSolmu = loppu;

            HakuMenetelmä.LisääUusiSolmu(alku);
            HakuMenetelmä.LisääUusiSolmu(loppu);

            // varmistetaan ettei alku ja loppupiste ole sama.
            if (alku == loppu)
            {
                Palautus.Add("Virheellinen tieto syötetty. Alkupiste ja loppupiste eivät voi olla sama piste.");
                return Palautus;
            }

            // varmistetaan ettei hakua voi tapahtua ilman loppusolmua
            if (toiminto == "rakenne" && loppu == "")
            {
                Palautus.Add("Virheellinen tieto syötetty. Loppupiste on määritettävä.");
                return Palautus;
            }

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
                    Palautus.Add("Virheellinen tiedon muoto.");
                    return Palautus;
            }

            // Tiedon tuloksen muoto
            // To-Do!!
            if (tulostus == "laajennettu") HakuMenetelmä.RunsasTulostus = true;

            LueKaariLista(kaaret, hakutyyppi);

            // jos virheitä on havaittu, lopeta suoritus.
            if (Palautus.Count != 0) return Palautus;

            HakuMenetelmä.Suorita();

            // palautetaan pyydetty tietokenttä
            return HakuMenetelmä.Tulosta();
        }
    }
}
