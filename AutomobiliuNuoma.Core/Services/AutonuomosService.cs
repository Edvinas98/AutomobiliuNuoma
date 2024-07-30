using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;

namespace AutomobiliuNuoma.Core.Services
{
    public class AutonuomosService : IAutonuomaService
    {
        private readonly IKlientaiService _klientaiService;
        private readonly IAutomobiliaiService _automobiliaiService;
        private List<NuomosUzsakymas> Uzsakymai = new List<NuomosUzsakymas>();

        public AutonuomosService(IKlientaiService klientaiService, IAutomobiliaiService automobiliaiService)
        {
            _automobiliaiService = automobiliaiService;
            _klientaiService = klientaiService;
        }

        public List<Automobilis> GautiVisusAutomobilius()
        {
            return _automobiliaiService.GautiVisusAutomobilius();
        }
        public List<Automobilis> PaieskaPagalMarke(string marke)
        {
            return _automobiliaiService.PaieskaPagalMarke(marke);
        }

        public List<Automobilis> GautiVisusElektromobilius()
        {
            List<Automobilis> automobiliai = new List<Automobilis>();
            foreach (Automobilis automobilis in _automobiliaiService.GautiVisusAutomobilius())
            {
                if(automobilis is Elektromobilis)
                    automobiliai.Add(automobilis);
            }
            return automobiliai;
        }

        public List<Automobilis> GautiVisusNaftosKuroAutomobilius()
        {
            List<Automobilis> automobiliai = new List<Automobilis>();
            foreach (Automobilis automobilis in _automobiliaiService.GautiVisusAutomobilius())
            {
                if (automobilis is NaftosKuroAutomobilis)
                    automobiliai.Add(automobilis);
            }
            return automobiliai;
        }

        public List<Klientas> GautiVisusKlientus()
        {
            return _klientaiService.GautiVisusKlientus();
        }

        public List<Klientas> PaieskaPagalVardaIrPavarde(string vardas, string pavarde)
        {
            return _klientaiService.PaieskaPagalVardaPavarde(vardas, pavarde);
        }

        public string PridetiNaujaKlienta(Klientas klientas)
        {
            return _klientaiService.PridetiKlienta(klientas);
        }

        public string IstrintiKlienta(Klientas klientas)
        {
            foreach(NuomosUzsakymas uzsakymas in Uzsakymai)
            {
                if(uzsakymas.NuomosKlientas == klientas)
                {
                    IstrintiUzsakyma(uzsakymas);
                    break;
                }
            }
            return _klientaiService.IstrintiKlienta(klientas);
        }

        public string PridetiNaujaAutomobili(Automobilis automobilis)
        {
            return _automobiliaiService.PridetiAutomobili(automobilis);
        }

        public string IstrintiAutomobili(Automobilis automobilis)
        {
            foreach (NuomosUzsakymas uzsakymas in Uzsakymai)
            {
                if (uzsakymas.NuomosAutomobilis == automobilis)
                {
                    IstrintiUzsakyma(uzsakymas);
                    break;
                }
            }

            return _automobiliaiService.IstrintiAutomobili(automobilis);
        }

        public List<NuomosUzsakymas> GautiVisusUzsakymus()
        {
            return Uzsakymai;
        }

        public List<NuomosUzsakymas> GautiUzsakymusPagalKlienta(Klientas klientas)
        {
            List<NuomosUzsakymas> paieskosRezultatai = new List<NuomosUzsakymas>();
            foreach (NuomosUzsakymas uzsakymas in Uzsakymai)
            {
                if (uzsakymas.NuomosKlientas == klientas)
                    paieskosRezultatai.Add(uzsakymas);
            }
            return paieskosRezultatai;
        }

        public List<Automobilis> GautiVisusLaisvusAutomobilius()
        {
            List<Automobilis> laisviAuto = new List<Automobilis>();
            List <Automobilis> automobiliai = _automobiliaiService.GautiVisusAutomobilius();
            foreach(Automobilis automobilis in automobiliai)
            {
                bool bRastas = false;
                foreach (NuomosUzsakymas uzsakymas in Uzsakymai)
                {
                    if (automobilis == uzsakymas.NuomosAutomobilis)
                    {
                        bRastas = true;
                        break;
                    }
                }
                if (!bRastas)
                    laisviAuto.Add(automobilis);
            }
            return laisviAuto;
        }

        public NuomosUzsakymas SkaiciuotiBendraNuomosKaina(Automobilis automobilis, int dienuKiekis)
        {
            return new NuomosUzsakymas(new Klientas("Vardenis", "Pavardenis", DateOnly.Parse("1900-01-01")), automobilis, dienuKiekis);
        }

        public string SukurtiUzsakyma(NuomosUzsakymas uzsakymas)
        {
            foreach (NuomosUzsakymas tempUzsakymas in Uzsakymai)
            {
                if (tempUzsakymas.NuomosKlientas == uzsakymas.NuomosKlientas)
                    return "Sis klientas jau yra issinuomaves viena automobili!";
            }
            Uzsakymai.Add(uzsakymas);
            return "Uzsakymas sekmingai sukurtas";
        }

        public string IstrintiUzsakyma(NuomosUzsakymas uzsakymas)
        {
            Uzsakymai.Remove(uzsakymas);
            return "Uzsakymas sekmingai istrintas";
        }
    }
}
