using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Enums;
using AutomobiliuNuoma.Core.Models;
using AutomobiliuNuoma.Core.Repositories;
using AutomobiliuNuoma.Core.Services;
using AutomobiliuNuoma.Services;

namespace AutomobiliuNuoma
{
    internal class AutoNuoma
    {
        static void Main(string[] args)
        {
            IAutonuomaService autonuomaService = SetupDependenciesForDb();
            MeniuUI meniu = new MeniuUI(autonuomaService);
            meniu.AtidarytiMeniu();
        }

        public static IAutonuomaService SetupDependenciesForDb()
        {
            string dataBaseString = "Server=localhost;Database=autonuoma;Trusted_Connection=True;";

            IKlientaiRepository klientaiRepository = new KlientaiDbRepository(dataBaseString);
            IAutomobiliaiRepository automobiliaiRepository = new AutomobiliaiDbRepository(dataBaseString);
            IDarbuotojaiRepository darbuotojaiRepository = new DarbuotojaiDbRepository(dataBaseString);
            IUzsakymaiRepository uzsakymaiRepository = new UzsakymaiDbRepository(dataBaseString);
            IKlientaiService klientaiService = new KlientaiService(klientaiRepository);
            IDarbuotojaiService darbuotojaiService = new DarbuotojaiService(darbuotojaiRepository);
            IAutomobiliaiService automobiliaiService = new AutomobiliaiService(automobiliaiRepository);
            return new AutonuomosService(klientaiService, automobiliaiService, darbuotojaiService, uzsakymaiRepository);
        }
    }
}
