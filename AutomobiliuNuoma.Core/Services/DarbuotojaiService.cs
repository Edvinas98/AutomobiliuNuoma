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
    public class DarbuotojaiService : IDarbuotojaiService
    {
        private readonly IDarbuotojaiRepository _DarbuotojaiRepository;
        private List<Darbuotojas> Darbuotojai = new List<Darbuotojas>();

        public DarbuotojaiService(IDarbuotojaiRepository darbuotojaiRepository)
        {
            _DarbuotojaiRepository = darbuotojaiRepository;
            NuskaitytiIsFailo();
        }

        public List<Darbuotojas> GautiVisusDarbuotojus()
        {
            return Darbuotojai;
        }

        public void IrasytiIFaila(Darbuotojas darbuotojas)
        {
            _DarbuotojaiRepository.IrasytiDarbuotoja(darbuotojas);
        }

        public void IstrintiIsFailo(Darbuotojas darbuotojas)
        {
            _DarbuotojaiRepository.IstrintiDarbuotoja(darbuotojas);
        }

        public void NuskaitytiIsFailo()
        {
            Darbuotojai = _DarbuotojaiRepository.NuskaitytiDarbuotojus();
        }

        public List<Darbuotojas> PaieskaPagalVardaPavarde(string vardas, string pavarde)
        {
            List<Darbuotojas> paieskosRezultatai = new List<Darbuotojas>();
            foreach (Darbuotojas darbuotojas in Darbuotojai)
            {
                if (darbuotojas.Vardas.ToLower() == vardas.ToLower() && darbuotojas.Pavarde.ToLower() == pavarde.ToLower())
                    paieskosRezultatai.Add(darbuotojas);
            }
            return paieskosRezultatai;
        }

        public string PridetiDarbuotoja(Darbuotojas darbuotojas)
        {
            foreach(Darbuotojas tempDarbuotojas in Darbuotojai)
            {
                if(tempDarbuotojas.Vardas.ToLower() == darbuotojas.Vardas.ToLower() && tempDarbuotojas.Pavarde.ToLower() == darbuotojas.Pavarde.ToLower())
                    return "Sis darbuotojas jau yra sarase!";
            }

            Darbuotojai.Add(darbuotojas);
            IrasytiIFaila(darbuotojas);
            return "Darbuotojas sekmingai pridetas";
        }

        public void IstrintiDarbuotoja(Darbuotojas darbuotojas)
        {
            IstrintiIsFailo(darbuotojas);
            Darbuotojai.Remove(darbuotojas);
        }

        public string AtnaujintiDarbuotoja(Darbuotojas darbuotojas, Darbuotojas naujasDarbuotojas, out bool bPavyko)
        {
            foreach (Darbuotojas tempDarbuotojas in Darbuotojai)
            {
                if (tempDarbuotojas != darbuotojas && tempDarbuotojas.Vardas.ToLower() == naujasDarbuotojas.Vardas.ToLower()
                    && tempDarbuotojas.Pavarde.ToLower() == naujasDarbuotojas.Pavarde.ToLower())
                {
                    bPavyko = false;
                    return "Darbuotojas su siuo vardu ir pavarde jau yra sarase!";
                }
            }
            bPavyko = true;
            _DarbuotojaiRepository.AtnaujintiDarbuotoja(darbuotojas, naujasDarbuotojas);
            Darbuotojai[Darbuotojai.IndexOf(darbuotojas)] = naujasDarbuotojas;
            return "Darbuotojo duomenys sekmingai atnaujinti";
        }

        public void PridetiUzsakymaDarbuotoju(Darbuotojas darbuotojas)
        {

        }
    }
}
