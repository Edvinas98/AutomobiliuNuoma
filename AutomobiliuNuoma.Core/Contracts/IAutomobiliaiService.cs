﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobiliuNuoma.Core.Models;

namespace AutomobiliuNuoma.Core.Contracts
{
    public interface IAutomobiliaiService
    {
        /// <summary>
        /// Nuskaityti automobiliu sarasa is failo
        /// </summary>
        void NuskaitytiIsFailo();
        /// <summary>
        /// Irasyti automobiliu sarasa i faila
        /// </summary>
        /// <param name="automobiliai"></param>
        /// <param name="bTikPrideti">Nurodo, ar reikia prideti duomenis prie failo ar istrinti faile turimus duomenis</param>
        void IrasytiIFaila(List<Automobilis> automobiliai, bool bTikPrideti);
        /// <summary>
        /// Prideti automobili i sarasa
        /// </summary>
        /// <param name="automobilis"></param>
        /// <returns></returns>
        string PridetiAutomobili(Automobilis automobilis);
        /// <summary>
        /// Istrinti automobili is saraso
        /// </summary>
        /// <param name="automobilis"></param>
        /// <returns></returns>
        string IstrintiAutomobili(Automobilis automobilis);
        /// <summary>
        /// Grazina automobiliu sarasa pagal nurodyta marke
        /// </summary>
        /// <param name="marke"></param>
        /// <returns></returns>
        List<Automobilis> PaieskaPagalMarke(string marke);
        /// <summary>
        /// Grazina visa automobiliu sarasa
        /// </summary>
        /// <returns></returns>
        List<Automobilis> GautiVisusAutomobilius();
    }
}
