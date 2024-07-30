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
    public class KlientaiService : IKlientaiService
    {
        private readonly IKlientaiRepository _klientaiRepository;
        private List<Klientas> Klientai = new List<Klientas>();

        public KlientaiService(IKlientaiRepository klientaiRepository)
        {
            _klientaiRepository = klientaiRepository;
            NuskaitytiIsFailo();
        }

        public List<Klientas> GautiVisusKlientus()
        {
            return Klientai;
        }

        public void IrasytiIFaila(List<Klientas> klientai, bool bTikPrideti)
        {
            _klientaiRepository.IrasytiKlientus(klientai, bTikPrideti);
        }

        public void NuskaitytiIsFailo()
        {
            Klientai = _klientaiRepository.NuskaitytiKlientus();
        }

        public List<Klientas> PaieskaPagalVardaPavarde(string vardas, string pavarde)
        {
            List<Klientas> paieskosRezultatai = new List<Klientas>();
            foreach (Klientas klientas in Klientai)
            {
                if (klientas.Vardas.ToLower() == vardas.ToLower() && klientas.Pavarde.ToLower() == pavarde.ToLower())
                    paieskosRezultatai.Add(klientas);
            }
            return paieskosRezultatai;
        }

        public string PridetiKlienta(Klientas klientas)
        {
            foreach(Klientas tempKlientas in Klientai)
            {
                if(tempKlientas.Vardas == klientas.Vardas && tempKlientas.Pavarde == klientas.Pavarde && tempKlientas.GimimoData == klientas.GimimoData)
                    return "Sis klientas jau yra sarase!";
            }

            Klientai.Add(klientas);
            List<Klientas> klientai = new List<Klientas>();
            klientai.Add(klientas);
            IrasytiIFaila(klientai, true);
            return "Klientas sekmingai pridetas";
        }

        public string IstrintiKlienta(Klientas klientas)
        {
            Klientai.Remove(klientas);
            IrasytiIFaila(Klientai, false);
            return "Klientas sekmingai istrintas";
        }
    }
}
