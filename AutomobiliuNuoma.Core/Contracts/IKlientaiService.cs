using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobiliuNuoma.Core.Models;

namespace AutomobiliuNuoma.Core.Contracts
{
    public interface IKlientaiService
    {
        /// <summary>
        /// Nuskaityti klientu sarasa is failo
        /// </summary>
        void NuskaitytiIsFailo();
        /// <summary>
        /// Iraso klientu sarasa i faila
        /// </summary>
        /// <param name="klientai"></param>
        /// <param name="bTikPrideti">Nurodo, ar reikia prideti duomenis prie failo ar istrinti faile turimus duomenis</param>
        void IrasytiIFaila(List<Klientas> klientai, bool bTikPrideti);
        /// <summary>
        /// Grazina klientu sarasa pagal varda ir pavarde
        /// </summary>
        /// <param name="vardas"></param>
        /// <param name="pavarde"></param>
        /// <returns></returns>
        List<Klientas> PaieskaPagalVardaPavarde(string vardas, string pavarde);
        /// <summary>
        /// Grazina visa klientu sarasa
        /// </summary>
        /// <returns></returns>
        List<Klientas> GautiVisusKlientus();
        /// <summary>
        /// Prideti klienta i sarasa
        /// </summary>
        /// <param name="klientas"></param>
        /// <returns></returns>
        string PridetiKlienta(Klientas klientas);
        /// <summary>
        /// Istrinti klienta is saraso
        /// </summary>
        /// <param name="klientas"></param>
        /// <returns></returns>
        string IstrintiKlienta(Klientas klientas);
    }
}
