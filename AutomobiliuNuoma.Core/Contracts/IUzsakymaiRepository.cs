using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobiliuNuoma.Core.Models;

namespace AutomobiliuNuoma.Core.Contracts
{
    public interface IUzsakymaiRepository
    {
        /// <summary>
        /// Nuskaito uzsakymu sarasa is failo ir ji grazina
        /// </summary>
        /// <returns></returns>
        List<NuomosUzsakymas> NuskaitytiUzsakymus(List<Klientas> klientai, List<Darbuotojas> darbuotojai, List<Automobilis> automobiliai);
        /// <summary>
        /// Iraso uzsakymu sarasa i faila
        /// </summary>
        /// <param name="uzsakymai"></param>
        /// <param name="bTikPrideti">Nurodo, ar reikia prideti duomenis prie failo ar istrinti faile turimus duomenis</param>
        void IrasytiUzsakyma(NuomosUzsakymas uzsakymas);
        void IstrintiUzsakyma(NuomosUzsakymas uzsakymas);
        void AtnaujintiUzsakyma(NuomosUzsakymas senasUzsakymas, NuomosUzsakymas uzsakymas);
    }
}
