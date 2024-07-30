using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobiliuNuoma.Core.Models;

namespace AutomobiliuNuoma.Core.Contracts
{
    public interface IKlientaiRepository
    {
        /// <summary>
        /// Nuskaito klientu sarasa is failo ir ji grazina
        /// </summary>
        /// <returns></returns>
        List<Klientas> NuskaitytiKlientus();
        /// <summary>
        /// Iraso klientu sarasa i faila
        /// </summary>
        /// <param name="klientai"></param>
        /// <param name="bTikPrideti">Nurodo, ar reikia prideti duomenis prie failo ar istrinti faile turimus duomenis</param>
        void IrasytiKlientus(List<Klientas> klientai, bool bTikPrideti);
    }
}
