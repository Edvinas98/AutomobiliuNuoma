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

        public void IrasytiIFaila(Klientas klientas)
        {
            _klientaiRepository.IrasytiKlienta(klientas);
        }

        public void IstrintiIsFailo(Klientas klientas)
        {
            _klientaiRepository.IstrintiKlienta(klientas);
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
                if(tempKlientas.Vardas.ToLower() == klientas.Vardas.ToLower() && tempKlientas.Pavarde.ToLower() == klientas.Pavarde.ToLower()
                    && tempKlientas.GimimoData == klientas.GimimoData)
                    return "Sis klientas jau yra sarase!";
            }

            Klientai.Add(klientas);
            IrasytiIFaila(klientas);
            return "Klientas sekmingai pridetas";
        }

        public void IstrintiKlienta(Klientas klientas)
        {
            IstrintiIsFailo(klientas);
            Klientai.Remove(klientas);
        }

        public string AtnaujintiKlienta(Klientas klientas, Klientas naujasKlientas, out bool bPavyko)
        {
            foreach (Klientas tempKlientas in Klientai)
            {
                if (tempKlientas != klientas && tempKlientas.Vardas.ToLower() == naujasKlientas.Vardas.ToLower()
                    && tempKlientas.Pavarde.ToLower() == naujasKlientas.Pavarde.ToLower() && tempKlientas.GimimoData == naujasKlientas.GimimoData)
                {
                    bPavyko = false;
                    return "Klientas su identiskais duomenimis jau yra sarase!";
                }
            }
            bPavyko = true;
            _klientaiRepository.AtnaujintiKlienta(klientas, naujasKlientas);
            Klientai[Klientai.IndexOf(klientas)] = naujasKlientas;
            return "Kliento duomenys sekmingai atnaujinti";
        }
    }
}
