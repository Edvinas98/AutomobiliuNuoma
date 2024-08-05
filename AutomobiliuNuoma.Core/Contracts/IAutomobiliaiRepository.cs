using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobiliuNuoma.Core.Models;

namespace AutomobiliuNuoma.Core.Contracts
{
    public interface IAutomobiliaiRepository
    {
        /// <summary>
        /// Nuskaito automobiliu sarasa is failo ir ji grazina
        /// </summary>
        /// <returns></returns>
        List<Automobilis> NuskaitytiAutomobilius();
        /// <summary>
        /// Iraso automobiliu sarasa i faila
        /// </summary>
        /// <param name="automobiliai"></param>
        /// <param name="bTikPrideti">Nurodo, ar reikia prideti duomenis prie failo ar istrinti faile turimus duomenis</param>
        void IrasytiAutomobili(Automobilis automobiliai);

        void AtnaujintiAutomobili(Automobilis senasAutomobilis, Automobilis automobilis);
        void IstrintiAutomobili(Automobilis automobilis, bool bTrintiUzsakymus);
    }
}
