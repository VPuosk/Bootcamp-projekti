using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Rivilaskuri
    {
        public string Jono { get; set; }

        private Stack<double> LaskuPakka;
        private Stack<string> Operaattorit;
        private string VirheIlmoitus;
        private string[] Merkkijonot;
        private Dictionary<string, int> Prioriteetit;
        
        private string[] PuraMerkkijono(string jono)
        {
            return jono.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        private void AlustaRakenteet()
        {
            LaskuPakka = new();
            Operaattorit = new();
            VirheIlmoitus = "";
            Prioriteetit = new();
            Prioriteetit.Add("*", 2);
            Prioriteetit.Add("/", 2);
            Prioriteetit.Add("+", 1);
            Prioriteetit.Add("-", 1);
            Prioriteetit.Add("(", 0);
            Prioriteetit.Add(")", 0);
            Prioriteetit.Add("lopetus", 0);
        }

        private double SuoritaLasku(double ensimmäinenArvo, double toinenArvo, string toiminto)
        {
            return toiminto switch
            {
                ("*") => (ensimmäinenArvo * toinenArvo),
                ("/") => (ensimmäinenArvo / toinenArvo),
                ("+") => (ensimmäinenArvo + toinenArvo),
                ("-") => (ensimmäinenArvo - toinenArvo),
                _ => 0,// oletettavasti siis virhe
                       // lisää virheilmoitus
            };
        }

        private int HaeNykyinenPrioriteetti()
        {
            if (Operaattorit.Count > 0)
            {
                return Prioriteetit[Operaattorit.Peek()];
            }
            else
            {
                return 0;
            }
        }

        private void Laske()
        {
            double eka;
            double toka;
            string toiminto;
            int nykyinenPrioriteetti;

            foreach (var merkkijono in Merkkijonot)
            {
                nykyinenPrioriteetti = HaeNykyinenPrioriteetti();

                switch (merkkijono)
                {
                    case ("*"):
                    case ("/"):
                    case ("+"):
                    case ("-"):
                    case (")"):
                    
                        // havaittu operaattori
                        int uusiPrioriteetti = Prioriteetit[merkkijono];

                        // jos operaattorin prioriteetti on alhaisempi kuin nykyinen prioriteetti, niin suorita laskua tähän saakka
                        while(uusiPrioriteetti < nykyinenPrioriteetti)
                        {
                            try
                            {
                                toiminto = Operaattorit.Pop();

                                if (LaskuPakka.Count <= 1)
                                {
                                    continue;
                                }
                                toka = LaskuPakka.Pop();
                                eka = LaskuPakka.Pop();
                                
                                LaskuPakka.Push(this.SuoritaLasku(eka, toka, toiminto));

                                nykyinenPrioriteetti = HaeNykyinenPrioriteetti();
                            }
                            catch (Exception)
                            {
                                VirheIlmoitus = "Virhe prosessoitaessa laskua";
                                break;
                            }
                        }

                        if (merkkijono == ")" && Operaattorit.Peek() =="(")
                        {
                            // varmistetaan, ettei ')' lisätä ja '(' poistetaan
                            Operaattorit.Pop();
                            break;
                        }

                        // lisätään uusi operaattori listalle
                        Operaattorit.Push(merkkijono);
                        break;

                    case ("("):
                        Operaattorit.Push(merkkijono);
                        break;
                    case ("lopetus"):
                        while (LaskuPakka.Count > 1)
                        {
                            try
                            {
                                toiminto = Operaattorit.Pop();
                                toka = LaskuPakka.Pop();
                                eka = LaskuPakka.Pop();

                                LaskuPakka.Push(this.SuoritaLasku(eka, toka, toiminto));
                            }
                            catch (Exception)
                            {
                                VirheIlmoitus = "Virhe prosessoitaessa laskua";
                                break;
                            }
                        }
                        break;
                    default:
                        // oletettavasti siis numero
                        try
                        {
                            LaskuPakka.Push(Double.Parse(merkkijono.Replace(".",",")));
                        }
                        catch (Exception)
                        {
                            VirheIlmoitus = "Virhe luettaessa syötettä";
                            break;
                        }

                        break;
                }
            }
        }

        private void TarkistetaanSulkeet()
        {
            int sulkeidenMäärä = 0;

            for (int i = 0; i < Merkkijonot.Length; i++)
            {
                if (Merkkijonot[i] == "(")
                {
                    sulkeidenMäärä++;
                }
                else if (Merkkijonot[i] == ")")
                {
                    sulkeidenMäärä--;
                }

                if (sulkeidenMäärä < 0)
                {
                    VirheIlmoitus = "Virhe sulkujen käytössä.";
                }
            }

            if (sulkeidenMäärä != 0)
            {
                VirheIlmoitus = "Virheellinen määrä sulkeita";
            }
        }

        public string LaskeMerkkiJono()
        {
            // varmista, ettei merkkijono sisällä ennenaikaista lopetusmerkkiä
            if (Jono.Contains("lopetus"))
            {
                return "Virheellinen syöte";
            }

            // lisää oikea lopetusmerkki
            Jono += " lopetus";

            // pura merkkijono array muotoon.
            Merkkijonot = PuraMerkkijono(Jono);

            // nollataan rakenteet
            this.AlustaRakenteet();

            // tarkistetaan sulkeet
            this.TarkistetaanSulkeet();

            // jos on havaittu virheitä, ei lasketa, vaan palautetaan
            if (VirheIlmoitus != "")
            {
                return VirheIlmoitus;
            }

            // lasketaan lasku
            this.Laske();

            // mikäli jokin ei onnistunut, niin tulosta virheilmoitus
            // muutoin tulosta laskupakan viimeinen arvo (jos homma toimi oikein)
            if (VirheIlmoitus != "")
            {
                return VirheIlmoitus;
            } else
            {
                return LaskuPakka.Peek().ToString();
            }
        }
    }
}
