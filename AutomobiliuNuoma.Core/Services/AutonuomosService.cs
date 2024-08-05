using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using AutomobiliuNuoma.Core.Repositories;

namespace AutomobiliuNuoma.Core.Services
{
    public class AutonuomosService : IAutonuomaService
    {
        private readonly IKlientaiService _klientaiService;
        private readonly IAutomobiliaiService _automobiliaiService;
        private readonly IDarbuotojaiService _darbuotojaiService;
        private readonly IUzsakymaiRepository _uzsakymaiRepository;
        private List<NuomosUzsakymas> Uzsakymai = new List<NuomosUzsakymas>();

        public AutonuomosService(IKlientaiService klientaiService, IAutomobiliaiService automobiliaiService, IDarbuotojaiService darbuotojaiService, IUzsakymaiRepository uzsakymaiRepository)
        {
            _automobiliaiService = automobiliaiService;
            _klientaiService = klientaiService;
            _darbuotojaiService = darbuotojaiService;
            _uzsakymaiRepository = uzsakymaiRepository;
            NuskaitytiIsFailo();
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
            _klientaiService.IstrintiKlienta(klientas);
            List<NuomosUzsakymas> uzsakymai = new List<NuomosUzsakymas>();
            foreach (NuomosUzsakymas uzsakymas in Uzsakymai)
            {
                if (uzsakymas.NuomosKlientas != klientas)
                    uzsakymai.Add(uzsakymas);
            }
            Uzsakymai = uzsakymai;
            return "Klientas sekmingai istrintas";
        }

        public List<Darbuotojas> GautiVisusDarbuotojus()
        {
            return _darbuotojaiService.GautiVisusDarbuotojus();
        }

        public List<Darbuotojas> DarbuotojoPaieskaPagalVardaIrPavarde(string vardas, string pavarde)
        {
            return _darbuotojaiService.PaieskaPagalVardaPavarde(vardas, pavarde);
        }

        public string PridetiNaujaDarbuotoja(Darbuotojas darbuotojas)
        {
            return _darbuotojaiService.PridetiDarbuotoja(darbuotojas);
        }

        public string IstrintiDarbuotoja(Darbuotojas darbuotojas)
        {
            _darbuotojaiService.IstrintiDarbuotoja(darbuotojas);
            List<NuomosUzsakymas> uzsakymai = new List<NuomosUzsakymas>();
            foreach (NuomosUzsakymas uzsakymas in Uzsakymai)
            {
                if (uzsakymas.NuomosDarbuotojas != darbuotojas)
                    uzsakymai.Add(uzsakymas);
            }
            Uzsakymai = uzsakymai;
            return "Darbuotojas sekmingai istrintas";
        }

        public string AtnaujintiDarbuotoja(Darbuotojas darbuotojas, Darbuotojas naujasDarbuotojas, out bool bPavyko)
        {
            return _darbuotojaiService.AtnaujintiDarbuotoja(darbuotojas, naujasDarbuotojas, out bPavyko);
        }

        public string PridetiNaujaAutomobili(Automobilis automobilis)
        {
            return _automobiliaiService.PridetiAutomobili(automobilis);
        }

        public string IstrintiAutomobili(Automobilis automobilis, bool bTrintiUzsakymus)
        {
            _automobiliaiService.IstrintiAutomobili(automobilis, bTrintiUzsakymus);
            if (bTrintiUzsakymus)
            {
                List<NuomosUzsakymas> uzsakymai = new List<NuomosUzsakymas>();
                foreach (NuomosUzsakymas uzsakymas in Uzsakymai)
                {
                    if (uzsakymas.NuomosAutomobilis != automobilis)
                        uzsakymai.Add(uzsakymas);
                }
                Uzsakymai = uzsakymai;
            }
            return "Automobilis sekmingai istrintas";
        }

        public string AtnaujintiAutomobili(Automobilis automobilis, Automobilis naujasAutomobilis, out bool bPavyko)
        {
            string rezultatas = _automobiliaiService.AtnaujintiAutomobili(automobilis, naujasAutomobilis, out bPavyko);
            if (bPavyko)
            {
                foreach (NuomosUzsakymas uzsakymas in Uzsakymai)
                {
                    if (uzsakymas.NuomosAutomobilis == automobilis)
                        uzsakymas.NuomosAutomobilis = naujasAutomobilis;
                }
            }
            return rezultatas;
        }

        public string AtnaujintiKlienta(Klientas klientas, Klientas naujasKlientas, out bool bPavyko)
        {
            return _klientaiService.AtnaujintiKlienta(klientas, naujasKlientas, out bPavyko);
        }

        public string AtnaujintiUzsakyma(NuomosUzsakymas uzsakymas, NuomosUzsakymas naujasUzsakymas)
        {
            _uzsakymaiRepository.AtnaujintiUzsakyma(uzsakymas, naujasUzsakymas);
            Uzsakymai[Uzsakymai.IndexOf(uzsakymas)] = naujasUzsakymas;
            return "Uzsakymo duomenys sekmingai atnaujinti";
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

        public List<NuomosUzsakymas> GautiUzsakymusPagalDarbuotoja(Darbuotojas darbuotojas)
        {
            List<NuomosUzsakymas> paieskosRezultatai = new List<NuomosUzsakymas>();
            foreach (NuomosUzsakymas uzsakymas in Uzsakymai)
            {
                if (uzsakymas.NuomosDarbuotojas == darbuotojas)
                    paieskosRezultatai.Add(uzsakymas);
            }
            return paieskosRezultatai;
        }

        public NuomosUzsakymas SkaiciuotiBendraNuomosKaina(Automobilis automobilis, int dienuKiekis)
        {
            return new NuomosUzsakymas(new Klientas("Vardenis", "Pavardenis", DateOnly.Parse("1900-01-01")),new Darbuotojas("Vardenis", "Pavardenis", 1), automobilis, dienuKiekis);
        }

        public string SukurtiUzsakyma(NuomosUzsakymas uzsakymas)
        {
            Uzsakymai.Add(uzsakymas);
            IrasytiIFaila(uzsakymas);
            _darbuotojaiService.PridetiUzsakymaDarbuotoju(uzsakymas.NuomosDarbuotojas);
            return "Uzsakymas sekmingai sukurtas";
        }

        public string IstrintiUzsakyma(NuomosUzsakymas uzsakymas)
        {
            IstrintiIsFailo(uzsakymas);
            Uzsakymai.Remove(uzsakymas);
            return "Uzsakymas sekmingai istrintas";
        }

        public void IrasytiIFaila(NuomosUzsakymas uzsakymas)
        {
            _uzsakymaiRepository.IrasytiUzsakyma(uzsakymas);
        }

        public void IstrintiIsFailo(NuomosUzsakymas uzsakymas)
        {
            _uzsakymaiRepository.IstrintiUzsakyma(uzsakymas);
        }

        public void NuskaitytiIsFailo()
        {
            Uzsakymai = _uzsakymaiRepository.NuskaitytiUzsakymus(_klientaiService.GautiVisusKlientus(), _darbuotojaiService.GautiVisusDarbuotojus(), _automobiliaiService.GautiVisusAutomobilius());
        }
    }
}
