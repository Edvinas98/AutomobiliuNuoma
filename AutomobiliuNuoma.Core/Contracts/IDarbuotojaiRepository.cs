﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobiliuNuoma.Core.Models;

namespace AutomobiliuNuoma.Core.Contracts
{
    public interface IDarbuotojaiRepository
    {
        /// <summary>
        /// Nuskaito klientu sarasa is failo ir ji grazina
        /// </summary>
        /// <returns></returns>
        List<Darbuotojas> NuskaitytiDarbuotojus();
        /// <summary>
        /// Iraso klientu sarasa i faila
        /// </summary>
        /// <param name="klientai"></param>
        /// <param name="bTikPrideti">Nurodo, ar reikia prideti duomenis prie failo ar istrinti faile turimus duomenis</param>
        void IrasytiDarbuotoja(Darbuotojas darbuotojas);
        void AtnaujintiDarbuotoja(Darbuotojas senasDarbuotojas, Darbuotojas darbuotojas);
        void IstrintiDarbuotoja(Darbuotojas darbuotojas);
    }
}
