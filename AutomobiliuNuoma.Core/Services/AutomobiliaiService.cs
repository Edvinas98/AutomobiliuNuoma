using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;

namespace AutomobiliuNuoma.Core.Services
{
    public class AutomobiliaiService : IAutomobiliaiService
    {
        private readonly IAutomobiliaiRepository _automobiliaiRepository;
        private List<Automobilis> Automobiliai = new List<Automobilis>();

        public AutomobiliaiService(IAutomobiliaiRepository automobiliaiRepository)
        {
            _automobiliaiRepository = automobiliaiRepository;
            NuskaitytiIsFailo();
        }
        public List<Automobilis> GautiVisusAutomobilius()
        {
            return Automobiliai;
        }

        public void IrasytiIFaila(Automobilis automobilis)
        {
            _automobiliaiRepository.IrasytiAutomobili(automobilis);
        }

        public void IstrintiIsFailo(Automobilis automobilis, bool bTrintiUzsakymus)
        {
            _automobiliaiRepository.IstrintiAutomobili(automobilis, bTrintiUzsakymus);
        }

        public void NuskaitytiIsFailo()
        {
            Automobiliai = _automobiliaiRepository.NuskaitytiAutomobilius();
        }

        public List<Automobilis> PaieskaPagalMarke(string marke)
        {
            List<Automobilis> paieskosRezultatai = new List<Automobilis>();
            foreach (Automobilis automobilis in Automobiliai)
            {
                if (automobilis.Marke.ToLower() == marke.ToLower())
                    paieskosRezultatai.Add(automobilis);
            }
            return paieskosRezultatai;
        }

        public string PridetiAutomobili(Automobilis automobilis)
        {
            foreach (Automobilis tempauto in Automobiliai)
            {
                if (tempauto.ID == automobilis.ID)
                    return "Automobilis su tokiu valstybiniu numeriu jau yra sarase!";
            }

            Automobiliai.Add(automobilis);
            IrasytiIFaila(automobilis);
            return "Automobilis sekmingai pridetas";
        }

        public void IstrintiAutomobili(Automobilis automobilis, bool bTrintiUzsakymus)
        {
            IstrintiIsFailo(automobilis, bTrintiUzsakymus);
            Automobiliai.Remove(automobilis);
        }

        public string AtnaujintiAutomobili(Automobilis automobilis, Automobilis naujasAutomobilis, out bool bPavyko)
        {
            foreach (Automobilis auto in Automobiliai)
            {
                if (auto != automobilis && auto.ID == naujasAutomobilis.ID)
                {
                    bPavyko = false;
                    return "Automobilis su tokiu valstybiniu numeriu jau yra sarase!";
                }
            }
            bPavyko = true;
            _automobiliaiRepository.AtnaujintiAutomobili(automobilis, naujasAutomobilis);
            Automobiliai[Automobiliai.IndexOf(automobilis)] = naujasAutomobilis;
            return "Automobilio informacija sekmingai atnaujinta";
        }
    }
}
