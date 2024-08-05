using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobiliuNuoma.Core.Models;

namespace AutomobiliuNuoma.Core.Contracts
{
    public interface IDarbuotojaiService
    {
        /// <summary>
        /// Nuskaityti darbuotoju sarasa is failo
        /// </summary>
        void NuskaitytiIsFailo();
        /// <summary>
        /// Iraso darbuotoju sarasa i faila
        /// </summary>
        /// <param name="klientai"></param>
        /// <param name="bTikPrideti">Nurodo, ar reikia prideti duomenis prie failo ar istrinti faile turimus duomenis</param>
        void IrasytiIFaila(Darbuotojas darbuotojas);
        /// <summary>
        /// Grazina klientu sarasa pagal varda ir pavarde
        /// </summary>
        /// <param name="vardas"></param>
        /// <param name="pavarde"></param>
        /// <returns></returns>
        List<Darbuotojas> PaieskaPagalVardaPavarde(string vardas, string pavarde);
        /// <summary>
        /// Grazina visa klientu sarasa
        /// </summary>
        /// <returns></returns>
        List<Darbuotojas> GautiVisusDarbuotojus();
        /// <summary>
        /// Prideti klienta i sarasa
        /// </summary>
        /// <param name="klientas"></param>
        /// <returns></returns>
        string PridetiDarbuotoja(Darbuotojas darbuotojas);
        /// <summary>
        /// Istrinti klienta is saraso
        /// </summary>
        /// <param name="klientas"></param>
        /// <returns></returns>
        void IstrintiDarbuotoja(Darbuotojas darbuotojas);
        string AtnaujintiDarbuotoja(Darbuotojas darbuotojas, Darbuotojas naujasDarbuotojas, out bool bPavyko);
        void PridetiUzsakymaDarbuotoju(Darbuotojas darbuotojas);
    }
}
