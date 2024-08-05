using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Enums;
using AutomobiliuNuoma.Core.Models;

namespace AutomobiliuNuoma.Services
{
    public class MeniuUI
    {
        private IAutonuomaService _autonuomaService;
        public MeniuUI(IAutonuomaService autonuomaService)
        {
            _autonuomaService = autonuomaService;
        }
        public void AtidarytiMeniu()
        {
            while (true)
            {
                Console.WriteLine("1. Atidaryti automobiliu meniu");
                Console.WriteLine("2. Atidaryti klientu meniu");
                Console.WriteLine("3. Atidaryti darbuotoju meniu");
                Console.WriteLine("4. Atidaryti uzsakymu meniu");
                Console.WriteLine("0. Iseiti");
                Console.Write("Pasirinkite veiksmą: ");
                GetInput(out string pasirinkimas);
                Console.WriteLine();
                switch (pasirinkimas)
                {
                    case "1":
                        AtidarytiAutomobiliuMeniu();
                        break;
                    case "2":
                        AtidarytiKlientuMeniu();
                        break;
                    case "3":
                        AtidarytiDarbuotojuMeniu();
                        break;
                    case "4":
                        AtidarytiUzsakymuMeniu();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Neteisingas pasirinkimas.");
                        Console.WriteLine();
                        break;
                }
            }
        }

        public void AtidarytiAutomobiliuMeniu()
        {
            while (true)
            {
                Console.WriteLine("1. Rodyti visus automobilius");
                Console.WriteLine("2. Rodyti visus elektromobilius");
                Console.WriteLine("3. Rodyti visus naftos kuro automobilius");
                Console.WriteLine("4. Rodyti visus automobilius pagal marke");
                Console.WriteLine("5. Prideti automobili");
                Console.WriteLine("6. Istrinti elektromobili");
                Console.WriteLine("7. Istrinti naftos kuro automobili");
                Console.WriteLine("8. Redaguoti elektromobili");
                Console.WriteLine("9. Redaguoti naftos kuro automobili");
                Console.WriteLine("0. Grizti");
                Console.Write("Pasirinkite veiksmą: ");
                GetInput(out string pasirinkimas);
                Console.WriteLine();
                switch (pasirinkimas)
                {
                    case "1":
                        List<Automobilis> automobiliai = _autonuomaService.GautiVisusAutomobilius();
                        if (automobiliai.Count == 0)
                        {
                            Console.WriteLine("Automobiliu sarase nera");
                            break;
                        }
                        foreach (Automobilis automobilis in automobiliai)
                        {
                            Console.WriteLine(automobilis);
                        }
                        break;
                    case "2":
                        automobiliai = _autonuomaService.GautiVisusElektromobilius();
                        if (automobiliai.Count == 0)
                        {
                            Console.WriteLine("Elektromobiliu sarase nera");
                            break;
                        }
                        foreach (Automobilis automobilis in automobiliai)
                        {
                            Console.WriteLine(automobilis);
                        }
                        break;
                    case "3":
                        automobiliai = _autonuomaService.GautiVisusNaftosKuroAutomobilius();
                        if (automobiliai.Count == 0)
                        {
                            Console.WriteLine("Naftos kuro automobiliu sarase nera");
                            break;
                        }
                        foreach (Automobilis automobilis in automobiliai)
                        {
                            Console.WriteLine(automobilis);
                        }
                        break;
                    case "4":
                        Console.Write("Iveskite marke: ");
                        GetInput(out string marke);
                        Console.WriteLine();
                        automobiliai = _autonuomaService.PaieskaPagalMarke(marke);
                        if (automobiliai.Count == 0)
                        {
                            Console.WriteLine($"{marke} automobiliu sarase nera");
                            break;
                        }
                        foreach (Automobilis automobilis in automobiliai)
                        {
                            Console.WriteLine(automobilis);
                        }
                        break;
                    case "5":
                        Console.Write("Iveskite ar tai elektromobilis (true/false): ");
                        GetInput(out bool bElektromobilis);
                        Console.Write("Iveskite valstybini numeri XXX 000: ");
                        GetInput(out string id, 7);
                        Console.Write("Iveskite marke: ");
                        GetInput(out marke);
                        Console.Write("Iveskite modeli: ");
                        GetInput(out string modelis);
                        Console.Write("Iveskite nuomos kaina Eur: ");
                        GetInput(out decimal nuomosKaina);
                        if (bElektromobilis)
                        {
                            Console.Write("Iveskite baterijos talpa kWh: ");
                            GetInput(out float baterijosTalpa);
                            Console.Write("Iveskite ikrovimo laika minutemis: ");
                            GetInput(out int krovimoLaikas, 1);
                            Console.WriteLine();
                            Console.WriteLine(_autonuomaService.PridetiNaujaAutomobili(new Elektromobilis(id, marke, modelis, nuomosKaina, baterijosTalpa, krovimoLaikas)));
                        }
                        else
                        {
                            Console.Write("Iveskite degalu sanaudas L/100km: ");
                            GetInput(out float degaluSanaudos);
                            Console.WriteLine();
                            Console.WriteLine(_autonuomaService.PridetiNaujaAutomobili(new NaftosKuroAutomobilis(id, marke, modelis, nuomosKaina, degaluSanaudos)));
                        }
                        break;
                    case "6":
                        automobiliai = _autonuomaService.GautiVisusElektromobilius();
                        if (automobiliai.Count == 0)
                        {
                            Console.WriteLine("Elektromobiliu sarase nera");
                            break;
                        }
                        for (int i = 0; i < automobiliai.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {automobiliai[i]}");
                        }
                        Console.WriteLine("0. Atsaukti");
                        Console.Write("Pasirinkite, kuri automobili norite istrinti: ");
                        GetInput(out int index, 0);
                        if (index == 0 || index > automobiliai.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Automobilio istrynimas atsauktas");
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(_autonuomaService.IstrintiAutomobili(automobiliai[index - 1], true));
                        }
                        break;
                    case "7":
                        automobiliai = _autonuomaService.GautiVisusNaftosKuroAutomobilius();
                        if (automobiliai.Count == 0)
                        {
                            Console.WriteLine("Naftos kuro automobiliu sarase nera");
                            break;
                        }
                        for (int i = 0; i < automobiliai.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {automobiliai[i]}");
                        }
                        Console.WriteLine("0. Atsaukti");
                        Console.Write("Pasirinkite, kuri automobili norite istrinti: ");
                        GetInput(out index, 0);
                        if (index == 0 || index > automobiliai.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Automobilio istrynimas atsauktas");
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(_autonuomaService.IstrintiAutomobili(automobiliai[index - 1], true));
                        }
                        break;
                    case "8":
                        automobiliai = _autonuomaService.GautiVisusElektromobilius();
                        if (automobiliai.Count == 0)
                        {
                            Console.WriteLine("Elektromobiliu sarase nera");
                            Console.WriteLine();
                            break;
                        }
                        for (int i = 0; i < automobiliai.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {automobiliai[i]}");
                        }
                        Console.WriteLine("0. Atsaukti");
                        Console.Write("Pasirinkite, kuri automobili norite redaguoti: ");
                        GetInput(out index, 0);
                        if (index == 0 || index > automobiliai.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Automobilio redagavimas atsauktas");
                        }
                        else
                        {
                            Console.WriteLine();
                            AtidarytiAutoRedagavimoMeniu(automobiliai[index - 1]);
                        }
                        break;
                    case "9":
                        automobiliai = _autonuomaService.GautiVisusNaftosKuroAutomobilius();
                        if (automobiliai.Count == 0)
                        {
                            Console.WriteLine("Naftos kuro automobiliu sarase nera");
                            Console.WriteLine();
                            break;
                        }
                        for (int i = 0; i < automobiliai.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {automobiliai[i]}");
                        }
                        Console.WriteLine("0. Atsaukti");
                        Console.Write("Pasirinkite, kuri automobili norite redaguoti: ");
                        GetInput(out index, 0);
                        if (index == 0 || index > automobiliai.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Automobilio redagavimas atsauktas");
                        }
                        else
                        {
                            Console.WriteLine();
                            AtidarytiAutoRedagavimoMeniu(automobiliai[index - 1]);
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Neteisingas pasirinkimas.");
                        break;
                }
                Console.WriteLine();
            }
        }

        public void AtidarytiAutoRedagavimoMeniu(Automobilis automobilis)
        {
            Automobilis naujasAuto = automobilis;
            if (automobilis is Elektromobilis)
                naujasAuto = new Elektromobilis(automobilis.ID, automobilis.Marke, automobilis.Modelis, automobilis.NuomosKaina, ((Elektromobilis)automobilis).BaterijosTalpa, ((Elektromobilis)automobilis).KrovimoLaikas);
            else if (automobilis is NaftosKuroAutomobilis)
                naujasAuto = new NaftosKuroAutomobilis(automobilis.ID, automobilis.Marke, automobilis.Modelis, automobilis.NuomosKaina, ((NaftosKuroAutomobilis)automobilis).DegaluSanaudos);
            else
            {
                Console.WriteLine("Pasirinkto automobilio tipas nerastas");
                return;
            }
            bool bPakeista = false;
            while (true)
            {
                Console.WriteLine("Pasirinkto automobilio duomenys:");
                Console.WriteLine();
                Console.WriteLine(naujasAuto);
                Console.WriteLine();
                Console.WriteLine($"1. Pakeisti automobili i {(naujasAuto is Elektromobilis ? "naftos kuro automobili" : "elektromobili")}");
                Console.WriteLine("2. Pakeisti valstybini numeri");
                Console.WriteLine("3. Pakeisti marke");
                Console.WriteLine("4. Pakeisti modeli");
                Console.WriteLine("5. Pakeisti nuomos kaina");
                if (naujasAuto is Elektromobilis)
                {
                    Console.WriteLine("6. Pakeisti baterijos talpa");
                    Console.WriteLine("7. Pakeisti krovimo laika");
                    if (bPakeista)
                        Console.WriteLine("8. Issaugoti pakeitimus");
                }
                else
                {
                    Console.WriteLine("6. Pakeisti degalu sanaudas");
                    if (bPakeista)
                        Console.WriteLine("7. Issaugoti pakeitimus");
                }
                Console.WriteLine("0. Atsaukti");
                Console.Write("Pasirinkite veiksmą: ");
                GetInput(out string pasirinkimas);
                Console.WriteLine();
                switch (pasirinkimas)
                {
                    case "1":
                        if (naujasAuto is Elektromobilis)
                        {
                            naujasAuto = new NaftosKuroAutomobilis(naujasAuto.ID, naujasAuto.Marke, naujasAuto.Modelis, naujasAuto.NuomosKaina, 0);
                        }
                        else
                        {
                            naujasAuto = new Elektromobilis(naujasAuto.ID, naujasAuto.Marke, naujasAuto.Modelis, naujasAuto.NuomosKaina, 0, 0);
                        }
                        bPakeista = true;
                        break;
                    case "2":
                        Console.Write("Iveskite nauja valstybini numeri XXX 000: ");
                        GetInput(out string newString, 7);
                        naujasAuto.ID = newString;
                        bPakeista = true;
                        break;
                    case "3":
                        Console.Write("Iveskite nauja marke: ");
                        GetInput(out newString);
                        naujasAuto.Marke = newString;
                        bPakeista = true;
                        break;
                    case "4":
                        Console.Write("Iveskite nauja modeli: ");
                        GetInput(out newString);
                        naujasAuto.Modelis = newString;
                        bPakeista = true;
                        break;
                    case "5":
                        Console.Write("Iveskite nauja nuomos kaina: ");
                        GetInput(out decimal newDecimal);
                        naujasAuto.NuomosKaina = newDecimal;
                        bPakeista = true;
                        break;
                    case "6":
                        if (naujasAuto is Elektromobilis)
                        {
                            Console.Write("Iveskite nauja baterijos talpa: ");
                            GetInput(out float newFloat);
                            ((Elektromobilis)naujasAuto).BaterijosTalpa = newFloat;
                        }
                        else
                        {
                            Console.Write("Iveskite naujas degalu sanaudas: ");
                            GetInput(out float newFloat);
                            ((NaftosKuroAutomobilis)naujasAuto).DegaluSanaudos = newFloat;
                        }
                        bPakeista = true;
                        break;
                    case "7":
                        if (naujasAuto is Elektromobilis)
                        {
                            Console.Write("Iveskite nauja krovimo laika: ");
                            GetInput(out int newInt, 1);
                            ((Elektromobilis)naujasAuto).KrovimoLaikas = newInt;
                            bPakeista = true;
                        }
                        else if (bPakeista)
                        {
                            Console.WriteLine(_autonuomaService.AtnaujintiAutomobili(automobilis, naujasAuto, out bool bPavyko));
                            if (bPavyko)
                                return;
                        }
                        else
                            Console.WriteLine("Neteisingas pasirinkimas.");
                        break;
                    case "8":
                        if (naujasAuto is Elektromobilis && bPakeista)
                        {
                            Console.WriteLine(_autonuomaService.AtnaujintiAutomobili(automobilis, naujasAuto, out bool bPavyko));
                            if (bPavyko)
                                return;
                        }
                        else
                            Console.WriteLine("Neteisingas pasirinkimas.");
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Neteisingas pasirinkimas.");
                        break;
                }
                Console.WriteLine();
            }
        }

        public void AtidarytiKlientuMeniu()
        {
            while (true)
            {
                Console.WriteLine("1. Rodyti visus klientus");
                Console.WriteLine("2. Ieskoti kliento pagal varda ir pavarde");
                Console.WriteLine("3. Prideti klienta");
                Console.WriteLine("4. Istrinti klienta");
                Console.WriteLine("5. Redaguoti klienta");
                Console.WriteLine("0. Grizti");
                Console.Write("Pasirinkite veiksmą: ");
                GetInput(out string pasirinkimas);
                Console.WriteLine();
                switch (pasirinkimas)
                {
                    case "1":
                        List<Klientas> klientai = _autonuomaService.GautiVisusKlientus();
                        if (klientai.Count == 0)
                        {
                            Console.WriteLine("Klientu sarase nera");
                            break;
                        }
                        foreach (Klientas klientas in klientai)
                        {
                            Console.WriteLine(klientas);
                        }
                        break;
                    case "2":
                        Console.Write("Iveskite kliento varda: ");
                        GetInput(out string vardas);
                        Console.Write("Iveskite kliento pavarde: ");
                        GetInput(out string pavarde);
                        Console.WriteLine();
                        klientai = _autonuomaService.PaieskaPagalVardaIrPavarde(vardas, pavarde);
                        if (klientai.Count == 0)
                        {
                            Console.WriteLine("Klientu pagal varda ir pavarde nerasta");
                            break;
                        }
                        foreach (Klientas klientas in klientai)
                        {
                            Console.WriteLine(klientas);
                        }
                        break;
                    case "3":
                        Console.Write("Iveskite kliento varda: ");
                        GetInput(out vardas);
                        Console.Write("Iveskite kliento pavarde: ");
                        GetInput(out pavarde);
                        Console.Write("Iveskite kliento gimimo data: ");
                        GetInput(out DateOnly gimimoData);
                        Console.WriteLine();
                        Console.WriteLine(_autonuomaService.PridetiNaujaKlienta(new Klientas(vardas, pavarde, gimimoData)));
                        break;
                    case "4":
                        klientai = _autonuomaService.GautiVisusKlientus();
                        if (klientai.Count == 0)
                        {
                            Console.WriteLine("Klientu sarase nera");
                            break;
                        }
                        for (int i = 0; i < klientai.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {klientai[i]}");
                        }
                        Console.WriteLine("0. Atsaukti");
                        Console.Write("Pasirinkite, kuri klienta norite istrinti: ");
                        GetInput(out int index, 0);
                        if (index == 0 || index > klientai.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Kliento istrynimas atsauktas");
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(_autonuomaService.IstrintiKlienta(klientai[index - 1]));
                        }
                        break;
                    case "5":
                        klientai = _autonuomaService.GautiVisusKlientus();
                        if (klientai.Count == 0)
                        {
                            Console.WriteLine("Klientu sarase nera");
                            break;
                        }
                        for (int i = 0; i < klientai.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {klientai[i]}");
                        }
                        Console.WriteLine("0. Atsaukti");
                        Console.Write("Pasirinkite, kuri klienta norite redaguoti: ");
                        GetInput(out index, 0);
                        if (index == 0 || index > klientai.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Kliento redagavimas atsauktas");
                        }
                        else
                        {
                            Console.WriteLine();
                            AtidarytiKlientoRedagavimoMeniu(klientai[index - 1]);
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Neteisingas pasirinkimas.");
                        break;
                }
                Console.WriteLine();
            }
        }

        public void AtidarytiKlientoRedagavimoMeniu(Klientas klientas)
        {
            Klientas naujasKlientas = new Klientas(klientas.Vardas, klientas.Pavarde, klientas.GimimoData);
            bool bPakeista = false;
            while (true)
            {
                Console.WriteLine("Pasirinkto kliento duomenys:");
                Console.WriteLine();
                Console.WriteLine(naujasKlientas);
                Console.WriteLine();
                Console.WriteLine("1. Pakeisti kliento varda");
                Console.WriteLine("2. Pakeisti kliento pavarde");
                Console.WriteLine("3. Pakeisti kliento gimimo data");
                if (bPakeista)
                    Console.WriteLine("4. Issaugoti pakeitimus");
                Console.WriteLine("0. Atsaukti");
                Console.Write("Pasirinkite veiksmą: ");
                GetInput(out string pasirinkimas);
                Console.WriteLine();
                switch (pasirinkimas)
                {
                    case "1":
                        Console.Write("Iveskite nauja kliento varda: ");
                        GetInput(out string newString);
                        naujasKlientas.Vardas = newString;
                        bPakeista = true;
                        break;
                    case "2":
                        Console.Write("Iveskite nauja kliento pavarde: ");
                        GetInput(out newString);
                        naujasKlientas.Pavarde = newString;
                        bPakeista = true;
                        break;
                    case "3":
                        Console.Write("Iveskite nauja kliento gimimo data: ");
                        GetInput(out DateOnly newDateOnly);
                        naujasKlientas.GimimoData = newDateOnly;
                        bPakeista = true;
                        break;
                    case "4":
                        if (bPakeista)
                        {
                            Console.WriteLine(_autonuomaService.AtnaujintiKlienta(klientas, naujasKlientas, out bool bPavyko));
                            if (bPavyko)
                                return;
                        }
                        else
                            Console.WriteLine("Neteisingas pasirinkimas.");
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Neteisingas pasirinkimas.");
                        break;
                }
                Console.WriteLine();
            }
        }

        public void AtidarytiDarbuotojuMeniu()
        {
            while (true)
            {
                Console.WriteLine("1. Rodyti visus darbuotojus");
                Console.WriteLine("2. Ieskoti darbuotojo pagal varda ir pavarde");
                Console.WriteLine("3. Prideti darbuotoja");
                Console.WriteLine("4. Istrinti darbuotoja");
                Console.WriteLine("5. Redaguoti darbuotoja");
                Console.WriteLine("0. Grizti");
                Console.Write("Pasirinkite veiksmą: ");
                GetInput(out string pasirinkimas);
                Console.WriteLine();
                switch (pasirinkimas)
                {
                    case "1":
                        List<Darbuotojas> darbuotojai = _autonuomaService.GautiVisusDarbuotojus();
                        if (darbuotojai.Count == 0)
                        {
                            Console.WriteLine("Darbuotoju sarase nera");
                            break;
                        }
                        foreach (Darbuotojas darbuotojas in darbuotojai)
                        {
                            Console.WriteLine(darbuotojas);
                        }
                        break;
                    case "2":
                        Console.Write("Iveskite darbuotojo varda: ");
                        GetInput(out string vardas);
                        Console.Write("Iveskite Darbuotojo pavarde: ");
                        GetInput(out string pavarde);
                        Console.WriteLine();
                        darbuotojai = _autonuomaService.DarbuotojoPaieskaPagalVardaIrPavarde(vardas, pavarde);
                        if (darbuotojai.Count == 0)
                        {
                            Console.WriteLine("Darbuotoju pagal varda ir pavarde nerasta");
                            break;
                        }
                        foreach (Darbuotojas darbuotojas in darbuotojai)
                        {
                            Console.WriteLine(darbuotojas);
                        }
                        break;
                    case "3":
                        Console.Write("Iveskite darbuotojo varda: ");
                        GetInput(out vardas);
                        Console.Write("Iveskite darbuotojo pavarde: ");
                        GetInput(out pavarde);
                        Console.Write("Pasirinkite darbuotojo pareigas: 1 - direktorius, 2 - vadybininkas, 3 - mechanikas: ");
                        GetInput(out byte pareigos);
                        Console.WriteLine();
                        Console.WriteLine(_autonuomaService.PridetiNaujaDarbuotoja(new Darbuotojas(vardas, pavarde, pareigos)));
                        break;
                    case "4":
                        darbuotojai = _autonuomaService.GautiVisusDarbuotojus();
                        if (darbuotojai.Count == 0)
                        {
                            Console.WriteLine("Darbuotoju sarase nera");
                            break;
                        }
                        for (int i = 0; i < darbuotojai.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {darbuotojai[i]}");
                        }
                        Console.WriteLine("0. Atsaukti");
                        Console.Write("Pasirinkite, kuri darbuotoja norite istrinti: ");
                        GetInput(out int index, 0);
                        if (index == 0 || index > darbuotojai.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Darbuotojo istrynimas atsauktas");
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(_autonuomaService.IstrintiDarbuotoja(darbuotojai[index - 1]));
                        }
                        break;
                    case "5":
                        darbuotojai = _autonuomaService.GautiVisusDarbuotojus();
                        if (darbuotojai.Count == 0)
                        {
                            Console.WriteLine("Darbuotoju sarase nera");
                            break;
                        }
                        for (int i = 0; i < darbuotojai.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {darbuotojai[i]}");
                        }
                        Console.WriteLine("0. Atsaukti");
                        Console.Write("Pasirinkite, kuri darbuotoja norite redaguoti: ");
                        GetInput(out index, 0);
                        if (index == 0 || index > darbuotojai.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Darbuotojo redagavimas atsauktas");
                        }
                        else
                        {
                            Console.WriteLine();
                            AtidarytiDarbuotojoRedagavimoMeniu(darbuotojai[index - 1]);
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Neteisingas pasirinkimas.");
                        break;
                }
                Console.WriteLine();
            }
        }

        public void AtidarytiDarbuotojoRedagavimoMeniu(Darbuotojas darbuotojas)
        {
            Darbuotojas naujasDarbuotojas = new Darbuotojas(darbuotojas.Vardas, darbuotojas.Pavarde, (Byte)darbuotojas.Pareigos);
            bool bPakeista = false;
            while (true)
            {
                Console.WriteLine("Pasirinkto darbuotojo duomenys:");
                Console.WriteLine();
                Console.WriteLine(naujasDarbuotojas);
                Console.WriteLine();
                Console.WriteLine("1. Pakeisti darbuotojo varda");
                Console.WriteLine("2. Pakeisti darbuotojo pavarde");
                Console.WriteLine("3. Pakeisti darbuotojo pareigas");
                if (bPakeista)
                    Console.WriteLine("4. Issaugoti pakeitimus");
                Console.WriteLine("0. Atsaukti");
                Console.Write("Pasirinkite veiksmą: ");
                GetInput(out string pasirinkimas);
                Console.WriteLine();
                switch (pasirinkimas)
                {
                    case "1":
                        Console.Write("Iveskite nauja darbuotojo varda: ");
                        GetInput(out string newString);
                        naujasDarbuotojas.Vardas = newString;
                        bPakeista = true;
                        break;
                    case "2":
                        Console.Write("Iveskite nauja darbuotojo pavarde: ");
                        GetInput(out newString);
                        naujasDarbuotojas.Pavarde = newString;
                        bPakeista = true;
                        break;
                    case "3":
                        Console.Write("Iveskite naujas darbuotojo pareigas: ");
                        GetInput(out byte newByte);
                        naujasDarbuotojas.Pareigos = (DarbuotojasPareigos)newByte;
                        bPakeista = true;
                        break;
                    case "4":
                        if (bPakeista)
                        {
                            Console.WriteLine(_autonuomaService.AtnaujintiDarbuotoja(darbuotojas, naujasDarbuotojas, out bool bPavyko));
                            if (bPavyko)
                                return;
                        }
                        else
                            Console.WriteLine("Neteisingas pasirinkimas.");
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Neteisingas pasirinkimas.");
                        break;
                }
                Console.WriteLine();
            }
        }

        public void AtidarytiUzsakymuMeniu()
        {
            while (true)
            {
                Console.WriteLine("1. Rodyti visus nuomos uzsakymus");
                Console.WriteLine("2. Ieskoti uzsakymu pagal klienta");
                Console.WriteLine("3. Ieskoti uzsakymu pagal darbuotoja");
                Console.WriteLine("4. Apskaiciuoti nuomos kaina");
                Console.WriteLine("5. Sukurti nuomos uzsakyma");
                Console.WriteLine("6. Istrinti nuomos uzsakyma");
                Console.WriteLine("7. Redaguoti nuomos uzsakyma");
                Console.WriteLine("0. Grizti");
                Console.Write("Pasirinkite veiksmą: ");
                GetInput(out string pasirinkimas);
                Console.WriteLine();
                switch (pasirinkimas)
                {
                    case "1":
                        List<NuomosUzsakymas> uzsakymai = _autonuomaService.GautiVisusUzsakymus();
                        if (uzsakymai.Count == 0)
                        {
                            Console.WriteLine("Nuomos uzsakymu nera");
                            break;
                        }
                        foreach (NuomosUzsakymas uzsakymas in uzsakymai)
                        {
                            Console.WriteLine(uzsakymas);
                            Console.WriteLine();
                        }
                        break;
                    case "2":
                        List<Klientas> klientai = _autonuomaService.GautiVisusKlientus();
                        if (klientai.Count == 0)
                        {
                            Console.WriteLine("Klientu sarasas yra tuscias!");
                            break;
                        }
                        for (int i = 0; i < klientai.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {klientai[i]}");
                        }
                        Console.Write("Pasirinkite, kurio kliento uzsakymus norite rasti: ");
                        GetInput(out int index, 0);
                        if (index == 0 || index > klientai.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Pasirinktas klientas nerastas");
                            break;
                        }
                        Console.WriteLine();
                        uzsakymai = _autonuomaService.GautiUzsakymusPagalKlienta(klientai[index - 1]);
                        if (uzsakymai.Count == 0)
                        {
                            Console.WriteLine("Nuomos uzsakymu nerasta");
                            break;
                        }
                        foreach (NuomosUzsakymas uzsakymas in uzsakymai)
                        {
                            Console.WriteLine(uzsakymas);
                            Console.WriteLine();
                        }
                        break;
                    case "3":
                        List<Darbuotojas> darbuotojai = _autonuomaService.GautiVisusDarbuotojus();
                        if (darbuotojai.Count == 0)
                        {
                            Console.WriteLine("Darbuotoju sarasas yra tuscias!");
                            break;
                        }
                        for (int i = 0; i < darbuotojai.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {darbuotojai[i]}");
                        }
                        Console.Write("Pasirinkite, kurio darbuotojo uzsakymus norite rasti: ");
                        GetInput(out index, 0);
                        if (index == 0 || index > darbuotojai.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Pasirinktas darbuotojas nerastas");
                            break;
                        }
                        Console.WriteLine();
                        uzsakymai = _autonuomaService.GautiUzsakymusPagalDarbuotoja(darbuotojai[index - 1]);
                        if (uzsakymai.Count == 0)
                        {
                            Console.WriteLine("Nuomos uzsakymu nerasta");
                            break;
                        }
                        foreach (NuomosUzsakymas uzsakymas in uzsakymai)
                        {
                            Console.WriteLine(uzsakymas);
                            Console.WriteLine();
                        }
                        break;
                    case "4":
                        List<Automobilis> automobiliai = _autonuomaService.GautiVisusAutomobilius();
                        if (automobiliai.Count == 0)
                        {
                            Console.WriteLine("Nera automobiliu sarase!");
                            break;
                        }
                        for (int i = 0; i < automobiliai.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {automobiliai[i]}");
                        }
                        Console.Write("Pasirinkite automobili: ");
                        GetInput(out index, 0);
                        if (index == 0 || index > automobiliai.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Pasirinktas automobilis nerastas");
                            break;
                        }
                        Automobilis nuomosAutomobilis = automobiliai[index - 1];
                        Console.Write("Iveskite dienu kieki: ");
                        GetInput(out int dienuKiekis, 1);
                        Console.WriteLine();
                        Console.WriteLine(_autonuomaService.SkaiciuotiBendraNuomosKaina(nuomosAutomobilis, dienuKiekis));
                        break;
                    case "5":
                        klientai = _autonuomaService.GautiVisusKlientus();
                        if (klientai.Count == 0)
                        {
                            Console.WriteLine("Nepavyko sukurti uzsakymo, nes klientu sarasas yra tuscias");
                            break;
                        }
                        darbuotojai = _autonuomaService.GautiVisusDarbuotojus();
                        if (darbuotojai.Count == 0)
                        {
                            Console.WriteLine("Nepavyko sukurti uzsakymo, nes darbuotoju sarasas yra tuscias");
                            break;
                        }
                        automobiliai = _autonuomaService.GautiVisusAutomobilius();
                        if (automobiliai.Count == 0)
                        {
                            Console.WriteLine("Nepavyko sukurti uzsakymo, nes automobiliu sarasas yra tuscias");
                            break;
                        }
                        for (int i = 0; i < klientai.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {klientai[i]}");
                        }
                        Console.Write("Pasirinkite, kuriam klientui norite sukurti uzsakyma: ");
                        GetInput(out index, 0);
                        if (index == 0 || index > klientai.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Pasirinktas klientas nerastas");
                            break;
                        }
                        Klientas nuomosKlientas = klientai[index - 1];
                        Console.WriteLine();
                        for (int i = 0; i < darbuotojai.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {darbuotojai[i]}");
                        }
                        Console.Write("Pasirinkite darbuotoja, kuris atliko si uzsakyma: ");
                        GetInput(out index, 0);
                        if (index == 0 || index > darbuotojai.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Pasirinktas darbuotojas nerastas");
                            break;
                        }
                        Darbuotojas nuomosDarbuotojas = darbuotojai[index - 1];
                        Console.WriteLine();
                        for (int i = 0; i < automobiliai.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {automobiliai[i]}");
                        }
                        Console.Write("Pasirinkite, kuri automobili norite isnuomoti: ");
                        GetInput(out index, 0);
                        if (index == 0 || index > automobiliai.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Pasirinktas automobilis nerastas");
                            break;
                        }
                        nuomosAutomobilis = automobiliai[index - 1];
                        Console.Write("Iveskite dienu kieki: ");
                        GetInput(out dienuKiekis, 1);
                        Console.WriteLine();
                        Console.WriteLine(_autonuomaService.SukurtiUzsakyma(new NuomosUzsakymas(nuomosKlientas, nuomosDarbuotojas, nuomosAutomobilis, dienuKiekis)));
                        break;
                    case "6":
                        uzsakymai = _autonuomaService.GautiVisusUzsakymus();
                        if (uzsakymai.Count == 0)
                        {
                            Console.WriteLine("Uzsakymu sarase nera");
                            break;
                        }
                        for (int i = 0; i < uzsakymai.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {uzsakymai[i]}");
                            Console.WriteLine();
                        }
                        Console.WriteLine("0. Atsaukti");
                        Console.Write("Pasirinkite, kuri uzsakyma norite istrinti: ");
                        GetInput(out index, 0);
                        if (index == 0 || index > uzsakymai.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Uzsakymo istrynimas atsauktas");
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(_autonuomaService.IstrintiUzsakyma(uzsakymai[index - 1]));
                        }
                        break;
                    case "7":
                        uzsakymai = _autonuomaService.GautiVisusUzsakymus();
                        if (uzsakymai.Count == 0)
                        {
                            Console.WriteLine("Nuomos uzsakymu sarase nera");
                            break;
                        }
                        for (int i = 0; i < uzsakymai.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {uzsakymai[i]}");
                            Console.WriteLine();
                        }
                        Console.WriteLine("0. Atsaukti");
                        Console.Write("Pasirinkite, kuri uzsakyma norite redaguoti: ");
                        GetInput(out index, 0);
                        if (index == 0 || index > uzsakymai.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Uzsakymo redagavimas atsauktas");
                        }
                        else
                        {
                            Console.WriteLine();
                            AtidarytiUzsakymoRedagavimoMeniu(uzsakymai[index - 1]);
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Neteisingas pasirinkimas.");
                        break;
                }
                Console.WriteLine();
            }
        }

        public void AtidarytiUzsakymoRedagavimoMeniu(NuomosUzsakymas uzsakymas)
        {
            NuomosUzsakymas naujasUzsakymas = new NuomosUzsakymas(uzsakymas.NuomosKlientas, uzsakymas.NuomosDarbuotojas, uzsakymas.NuomosAutomobilis, uzsakymas.DienuKiekis);
            bool bPakeista = false;
            while (true)
            {
                Console.WriteLine("Pasirinkto uzsakymo duomenys:");
                Console.WriteLine();
                Console.WriteLine(naujasUzsakymas);
                Console.WriteLine();
                Console.WriteLine("1. Pakeisti uzsakymo klienta");
                Console.WriteLine("2. Pakeisti uzsakymo darbuotoja");
                Console.WriteLine("3. Pakeisti uzsakymo automobili");
                Console.WriteLine("4. Pakeisti uzsakymo dienu kieki");
                if (bPakeista)
                    Console.WriteLine("5. Issaugoti pakeitimus");
                Console.WriteLine("0. Atsaukti");
                Console.Write("Pasirinkite veiksmą: ");
                GetInput(out string pasirinkimas);
                Console.WriteLine();
                switch (pasirinkimas)
                {
                    case "1":
                        List<Klientas> klientai = _autonuomaService.GautiVisusKlientus();
                        if (klientai.Count == 0)
                        {
                            Console.WriteLine("Klientu sarasas yra tuscias!");
                            break;
                        }
                        for (int i = 0; i < klientai.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {klientai[i]}");
                        }
                        Console.Write("Pasirinkite nauja uzsakymo klienta: ");
                        GetInput(out int index, 0);
                        if (index == 0 || index > klientai.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Pasirinktas klientas nerastas");
                            break;
                        }
                        naujasUzsakymas.NuomosKlientas = klientai[index - 1];
                        bPakeista = true;
                        break;
                    case "2":
                        List<Darbuotojas> darbuotojai = _autonuomaService.GautiVisusDarbuotojus();
                        if (darbuotojai.Count == 0)
                        {
                            Console.WriteLine("Darbuotoju sarasas yra tuscias!");
                            break;
                        }
                        for (int i = 0; i < darbuotojai.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {darbuotojai[i]}");
                        }
                        Console.Write("Pasirinkite nauja uzsakymo darbuotoja: ");
                        GetInput(out index, 0);
                        if (index == 0 || index > darbuotojai.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Pasirinktas darbuotojas nerastas");
                            break;
                        }
                        naujasUzsakymas.NuomosDarbuotojas = darbuotojai[index - 1];
                        bPakeista = true;
                        break;
                    case "3":
                        List<Automobilis> automobiliai = _autonuomaService.GautiVisusAutomobilius();
                        if (automobiliai.Count == 0)
                        {
                            Console.WriteLine("Nera automobiliu sarase!");
                            break;
                        }
                        for (int i = 0; i < automobiliai.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {automobiliai[i]}");
                        }
                        Console.Write("Pasirinkite nauja uzsakymo automobili: ");
                        GetInput(out index, 0);
                        if (index == 0 || index > automobiliai.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Pasirinktas automobilis nerastas");
                            break;
                        }
                        naujasUzsakymas.NuomosAutomobilis = automobiliai[index - 1];
                        bPakeista = true;
                        break;
                    case "4":
                        Console.Write("Iveskite nauja uzsakymo dienu kieki: ");
                        GetInput(out int dienuKiekis, 1);
                        naujasUzsakymas.DienuKiekis = dienuKiekis;
                        bPakeista = true;
                        break;
                    case "5":
                        if (bPakeista)
                        {
                            Console.WriteLine(_autonuomaService.AtnaujintiUzsakyma(uzsakymas, naujasUzsakymas));
                            return;
                        }
                        else
                            Console.WriteLine("Neteisingas pasirinkimas.");
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Neteisingas pasirinkimas.");
                        break;
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Nuskaito vartotojo ivesti ir grazina string tipo rezultata
        /// </summary>
        /// <returns></returns>
        public static void GetInput(out string input)
        {
            while (true)
            {
                input = Console.ReadLine() ?? string.Empty;
                if (input != "")
                    return;
                else
                    Console.Write("Klaida, bandykite ivesti is naujo: ");
            }
        }

        public static void GetInput(out string input, byte length)
        {
            while (true)
            {
                input = Console.ReadLine() ?? string.Empty;
                if (input.Length == length)
                    return;
                else
                    Console.Write("Klaida, bandykite ivesti is naujo: ");
            }
        }

        /// <summary>
        /// Nuskaito vartotojo ivesti ir grazina decimal tipo rezultata
        /// </summary>
        /// <returns></returns>
        public static void GetInput(out decimal input)
        {
            while (true)
            {
                if (!decimal.TryParse(Console.ReadLine(), out input) || input <= 0)
                    Console.Write("Klaida, bandykite ivesti is naujo: ");
                else
                    return;
            }
        }

        /// <summary>
        /// Nuskaito vartotojo ivesti ir grazina bool tipo rezultata
        /// </summary>
        /// <returns></returns>
        public static void GetInput(out bool input)
        {
            while (true)
            {
                if (!bool.TryParse(Console.ReadLine(), out input))
                    Console.Write("Klaida, bandykite ivesti is naujo: ");
                else
                    return;
            }
        }

        /// <summary>
        /// Nuskaito vartotojo ivesti ir grazina int tipo rezultata
        /// </summary>
        /// <returns></returns>
        public static void GetInput(out int input, int minVerte)
        {
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out input) || input < minVerte)
                    Console.Write("Klaida, bandykite ivesti is naujo: ");
                else
                    return;
            }
        }

        /// <summary>
        /// Nuskaito vartotojo ivesti ir grazina float tipo rezultata
        /// </summary>
        /// <returns></returns>
        public static void GetInput(out float input)
        {
            while (true)
            {
                if (!float.TryParse(Console.ReadLine(), out input) || input <= 0)
                    Console.Write("Klaida, bandykite ivesti is naujo: ");
                else
                    return;
            }
        }

        /// <summary>
        /// Nuskaito vartotojo ivesti ir grazina DateOnly tipo rezultata
        /// </summary>
        /// <returns></returns>
        public static void GetInput(out DateOnly input)
        {
            while (true)
            {
                if (!DateOnly.TryParse(Console.ReadLine(), out input))
                    Console.Write("Klaida, bandykite ivesti is naujo: ");
                else
                    return;
            }
        }

        /// <summary>
        /// Nuskaito vartotojo ivesti ir grazina Byte tipo rezultata
        /// </summary>
        /// <returns></returns>
        public static void GetInput(out byte input)
        {
            while (true)
            {
                if (!byte.TryParse(Console.ReadLine(), out input) || input <= 0)
                    Console.Write("Klaida, bandykite ivesti is naujo: ");
                else
                    return;
            }
        }
    }
}
