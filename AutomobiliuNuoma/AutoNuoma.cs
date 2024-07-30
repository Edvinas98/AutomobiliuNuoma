using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using AutomobiliuNuoma.Core.Repositories;
using AutomobiliuNuoma.Core.Services;

namespace AutomobiliuNuoma
{
    internal class AutoNuoma
    {
        static void Main(string[] args)
        {
            IAutonuomaService autonuomaService = SetupDependencies();
            AtidarytiMeniu(autonuomaService);
        }

        public static IAutonuomaService SetupDependencies()
        {
            IKlientaiRepository klientaiRepository = new KlientaiRepository("Klientai.csv");
            IAutomobiliaiRepository automobiliaiRepository = new AutomobiliaiRepository("Automobiliai.csv");
            IKlientaiService klientaiService = new KlientaiService(klientaiRepository);
            IAutomobiliaiService automobiliaiService = new AutomobiliaiService(automobiliaiRepository);
            return new AutonuomosService(klientaiService, automobiliaiService);
        }

        public static void AtidarytiMeniu(IAutonuomaService autonuomaService)
        {
            while (true)
            {
                Console.WriteLine("1. Atidaryti automobiliu meniu");
                Console.WriteLine("2. Atidaryti klientu meniu");
                Console.WriteLine("3. Atidaryti uzsakymu meniu");
                Console.WriteLine("0. Iseiti");
                Console.Write("Pasirinkite veiksmą: ");
                GetInput(out string pasirinkimas);
                switch (pasirinkimas)
                {
                    case "1":
                        Console.WriteLine();
                        AtidarytiAutomobiliuMeniu(autonuomaService);
                        break;
                    case "2":
                        Console.WriteLine();
                        AtidarytiKlientuMeniu(autonuomaService);
                        break;
                    case "3":
                        Console.WriteLine();
                        AtidarytiUzsakymuMeniu(autonuomaService);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Neteisingas pasirinkimas.");
                        Console.WriteLine();
                        break;
                }
            }
        }

        public static void AtidarytiAutomobiliuMeniu(IAutonuomaService autonuomaService)
        {
            while (true)
            {
                Console.WriteLine("1. Rodyti visus automobilius");
                Console.WriteLine("2. Rodyti visus elektromobilius");
                Console.WriteLine("3. Rodyti visus naftos kuro automobilius");
                Console.WriteLine("4. Rodyti visus automobilius pagal marke");
                Console.WriteLine("5. Prideti automobili");
                Console.WriteLine("6. Istrinti automobili");
                Console.WriteLine("0. Grizti");
                Console.Write("Pasirinkite veiksmą: ");
                GetInput(out string pasirinkimas);
                switch (pasirinkimas)
                {
                    case "1":
                        Console.WriteLine();
                        List<Automobilis> automobiliai = autonuomaService.GautiVisusAutomobilius();
                        if (automobiliai.Count == 0)
                        {
                            Console.WriteLine("Automobiliu sarase nera");
                            Console.WriteLine();
                            break;
                        }
                        foreach (Automobilis automobilis in automobiliai)
                        {
                            Console.WriteLine(automobilis);
                        }
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.WriteLine();
                        automobiliai = autonuomaService.GautiVisusElektromobilius();
                        if (automobiliai.Count == 0)
                        {
                            Console.WriteLine("Elektromobiliu sarase nera");
                            Console.WriteLine();
                            break;
                        }
                        foreach (Automobilis automobilis in automobiliai)
                        {
                            Console.WriteLine(automobilis);
                        }
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.WriteLine();
                        automobiliai = autonuomaService.GautiVisusNaftosKuroAutomobilius();
                        if (automobiliai.Count == 0)
                        {
                            Console.WriteLine("Naftos kuro automobiliu sarase nera");
                            Console.WriteLine();
                            break;
                        }
                        foreach (Automobilis automobilis in automobiliai)
                        {
                            Console.WriteLine(automobilis);
                        }
                        Console.WriteLine();
                        break;
                    case "4":
                        Console.Write("Iveskite marke: ");
                        GetInput(out string marke);
                        Console.WriteLine();
                        automobiliai = autonuomaService.PaieskaPagalMarke(marke);
                        if (automobiliai.Count == 0)
                        {
                            Console.WriteLine($"{marke} automobiliu sarase nera");
                            Console.WriteLine();
                            break;
                        }
                        foreach (Automobilis automobilis in automobiliai)
                        {
                            Console.WriteLine(automobilis);
                        }
                        Console.WriteLine();
                        break;
                    case "5":
                        Console.Write("Iveskite ar tai elektromobilis (true/false): ");
                        GetInput(out bool bElektromobilis);
                        Console.Write("Iveskite valstybini numeri: ");
                        GetInput(out string id);
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
                            Console.WriteLine(autonuomaService.PridetiNaujaAutomobili(new Elektromobilis(id, marke, modelis, nuomosKaina, baterijosTalpa, krovimoLaikas)));
                        }
                        else
                        {
                            Console.Write("Iveskite degalu sanaudas L/100km: ");
                            GetInput(out float degaluSanaudos);
                            Console.WriteLine();
                            Console.WriteLine(autonuomaService.PridetiNaujaAutomobili(new NaftosKuroAutomobilis(id, marke, modelis, nuomosKaina, degaluSanaudos)));
                        }
                        Console.WriteLine();
                        break;
                    case "6":
                        Console.WriteLine();
                        automobiliai = autonuomaService.GautiVisusAutomobilius();
                        if (automobiliai.Count == 0)
                        {
                            Console.WriteLine("Automobiliu sarase nera");
                            Console.WriteLine();
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
                            Console.WriteLine(autonuomaService.IstrintiAutomobili(automobiliai[index - 1]));
                        }
                        Console.WriteLine();
                        break;
                    case "0":
                        Console.WriteLine();
                        return;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Neteisingas pasirinkimas.");
                        Console.WriteLine();
                        break;
                }
            }
        }

        public static void AtidarytiKlientuMeniu(IAutonuomaService autonuomaService)
        {
            while (true)
            {
                Console.WriteLine("1. Rodyti visus klientus");
                Console.WriteLine("2. Ieskoti kliento pagal varda ir pavarde");
                Console.WriteLine("3. Prideti klienta");
                Console.WriteLine("4. Istrinti klienta");
                Console.WriteLine("0. Grizti");
                Console.Write("Pasirinkite veiksmą: ");
                GetInput(out string pasirinkimas);
                switch (pasirinkimas)
                {
                    case "1":
                        Console.WriteLine();
                        List<Klientas> klientai = autonuomaService.GautiVisusKlientus();
                        if (klientai.Count == 0)
                        {
                            Console.WriteLine("Klientu sarase nera");
                            Console.WriteLine();
                            break;
                        }
                        foreach (Klientas klientas in klientai)
                        {
                            Console.WriteLine(klientas);
                        }
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.Write("Iveskite kliento varda: ");
                        GetInput(out string vardas);
                        Console.Write("Iveskite kliento pavarde: ");
                        GetInput(out string pavarde);
                        Console.WriteLine();
                        klientai = autonuomaService.PaieskaPagalVardaIrPavarde(vardas, pavarde);
                        if (klientai.Count == 0)
                        {
                            Console.WriteLine("Klientu pagal varda ir pavarde nerasta");
                            Console.WriteLine();
                            break;
                        }
                        foreach (Klientas klientas in klientai)
                        {
                            Console.WriteLine(klientas);
                        }
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.Write("Iveskite kliento varda: ");
                        GetInput(out vardas);
                        Console.Write("Iveskite kliento pavarde: ");
                        GetInput(out pavarde);
                        Console.Write("Iveskite kliento gimimo data: ");
                        GetInput(out DateOnly gimimoData);
                        Console.WriteLine();
                        Console.WriteLine(autonuomaService.PridetiNaujaKlienta(new Klientas(vardas, pavarde, gimimoData)));
                        Console.WriteLine();
                        break;
                    case "4":
                        Console.WriteLine();
                        klientai = autonuomaService.GautiVisusKlientus();
                        if (klientai.Count == 0)
                        {
                            Console.WriteLine("Klientu sarase nera");
                            Console.WriteLine();
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
                            Console.WriteLine(autonuomaService.IstrintiKlienta(klientai[index - 1]));
                        }
                        Console.WriteLine();
                        break;
                    case "0":
                        Console.WriteLine();
                        return;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Neteisingas pasirinkimas.");
                        Console.WriteLine();
                        break;
                }
            }
        }

        public static void AtidarytiUzsakymuMeniu(IAutonuomaService autonuomaService)
        {
            while (true)
            {
                Console.WriteLine("1. Rodyti visus nuomos uzsakymus");
                Console.WriteLine("2. Ieskoti uzsakymu pagal klienta");
                Console.WriteLine("3. Rodyti laisvus automobilius");
                Console.WriteLine("4. Apskaiciuoti nuomos kaina");
                Console.WriteLine("5. Sukurti nuomos uzsakyma");
                Console.WriteLine("6. Istrinti nuomos uzsakyma");
                Console.WriteLine("0. Grizti");
                Console.Write("Pasirinkite veiksmą: ");
                GetInput(out string pasirinkimas);
                switch (pasirinkimas)
                {
                    case "1":
                        Console.WriteLine();
                        List<NuomosUzsakymas> uzsakymai = autonuomaService.GautiVisusUzsakymus();
                        if (uzsakymai.Count == 0)
                        {
                            Console.WriteLine("Nuomos uzsakymu nera");
                            Console.WriteLine();
                            break;
                        }
                        foreach (NuomosUzsakymas uzsakymas in uzsakymai)
                        {
                            Console.WriteLine(uzsakymas);
                            Console.WriteLine();
                        }
                        break;
                    case "2":
                        Console.WriteLine();
                        List<Klientas> klientai = autonuomaService.GautiVisusKlientus();
                        if (klientai.Count == 0)
                        {
                            Console.WriteLine("Klientu sarasas yra tuscias!");
                            Console.WriteLine();
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
                            Console.WriteLine();
                            break;
                        }
                        Console.WriteLine();
                        uzsakymai = autonuomaService.GautiUzsakymusPagalKlienta(klientai[index - 1]);
                        if (uzsakymai.Count == 0)
                        {
                            Console.WriteLine("Nuomos uzsakymu nerasta");
                            Console.WriteLine();
                            break;
                        }
                        foreach (NuomosUzsakymas uzsakymas in uzsakymai)
                        {
                            Console.WriteLine(uzsakymas);
                        }
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.WriteLine();
                        List<Automobilis> automobiliai = autonuomaService.GautiVisusLaisvusAutomobilius();
                        if (automobiliai.Count == 0)
                        {
                            Console.WriteLine("Laisvu automobiliu sarase nera");
                            Console.WriteLine();
                            break;
                        }
                        foreach (Automobilis automobilis in automobiliai)
                        {
                            Console.WriteLine(automobilis);
                        }
                        Console.WriteLine();
                        break;
                    case "4":
                        Console.WriteLine();
                        automobiliai = autonuomaService.GautiVisusAutomobilius();
                        if (automobiliai.Count == 0)
                        {
                            Console.WriteLine("Nera automobiliu sarase!");
                            Console.WriteLine();
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
                            Console.WriteLine();
                            break;
                        }
                        Automobilis nuomosAutomobilis = automobiliai[index - 1];
                        Console.Write("Iveskite dienu kieki: ");
                        GetInput(out int dienuKiekis, 1);
                        Console.WriteLine();
                        Console.WriteLine(autonuomaService.SkaiciuotiBendraNuomosKaina(nuomosAutomobilis, dienuKiekis));
                        Console.WriteLine();
                        break;
                    case "5":
                        Console.WriteLine();
                        klientai = autonuomaService.GautiVisusKlientus();
                        if (klientai.Count == 0)
                        {
                            Console.WriteLine("Nepavyko sukurti uzsakymo, nes klientu sarasas yra tuscias");
                            Console.WriteLine();
                            break;
                        }
                        automobiliai = autonuomaService.GautiVisusLaisvusAutomobilius();
                        if (automobiliai.Count == 0)
                        {
                            Console.WriteLine("Nepavyko sukurti uzsakymo, nes nera laisvu automobiliu");
                            Console.WriteLine();
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
                            Console.WriteLine();
                            break;
                        }
                        Klientas nuomosKlientas = klientai[index - 1];
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
                            Console.WriteLine();
                            break;
                        }
                        nuomosAutomobilis = automobiliai[index - 1];
                        Console.Write("Iveskite dienu kieki: ");
                        GetInput(out dienuKiekis, 1);
                        Console.WriteLine();
                        Console.WriteLine(autonuomaService.SukurtiUzsakyma(new NuomosUzsakymas(nuomosKlientas, nuomosAutomobilis, dienuKiekis)));
                        Console.WriteLine();
                        break;
                    case "6":
                        uzsakymai = autonuomaService.GautiVisusUzsakymus();
                        if (uzsakymai.Count == 0)
                        {
                            Console.WriteLine("Uzsakymu sarase nera");
                            Console.WriteLine();
                            break;
                        }
                        for (int i = 0; i < uzsakymai.Count; i++)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {uzsakymai[i]}");
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
                            Console.WriteLine(autonuomaService.IstrintiUzsakyma(uzsakymai[index - 1]));
                        }
                        Console.WriteLine();
                        break;
                    case "0":
                        Console.WriteLine();
                        return;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Neteisingas pasirinkimas.");
                        Console.WriteLine();
                        break;
                }
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
    }
}
