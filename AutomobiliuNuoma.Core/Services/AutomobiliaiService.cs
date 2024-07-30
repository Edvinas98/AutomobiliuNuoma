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
        private int CurrentID = 0;

        public AutomobiliaiService(IAutomobiliaiRepository automobiliaiRepository)
        {
            _automobiliaiRepository = automobiliaiRepository;
            NuskaitytiIsFailo();
            CurrentID = Automobiliai.Count;
        }
        public List<Automobilis> GautiVisusAutomobilius()
        {
            return Automobiliai;
        }

        public void IrasytiIFaila(List<Automobilis> automobiliai, bool bTikPrideti)
        {
            _automobiliaiRepository.IrasytiAutomobilius(automobiliai, bTikPrideti);
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
            List<Automobilis> automobiliai = new List<Automobilis>();
            automobiliai.Add(automobilis);
            IrasytiIFaila(automobiliai, true);
            return "Automobilis sekmingai pridetas";
        }

        public string IstrintiAutomobili(Automobilis automobilis)
        {
            Automobiliai.Remove(automobilis);
            IrasytiIFaila(Automobiliai, false);
            return "Automobilis sekmingai istrintas";
        }
    }
}
