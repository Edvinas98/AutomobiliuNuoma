using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobiliuNuoma.Core.Models;
using AutomobiliuNuoma.Core.Repositories;
using AutomobiliuNuoma.Core.Services;

namespace AutomobiliuNuoma.Core.Contracts
{
    public interface IAutonuomaService
    {
        /// <summary>
        /// Grazina visu automobiliu sarasa
        /// </summary>
        /// <returns></returns>
        List<Automobilis> GautiVisusAutomobilius();
        /// <summary>
        /// Grazina visu automobiliu, kurie yra elektromobiliai, sarasa
        /// </summary>
        /// <returns></returns>
        List<Automobilis> GautiVisusElektromobilius();
        /// <summary>
        /// Grazina visu automobiliu, kurie yra naftos kuro automobiliai, sarasa
        /// </summary>
        /// <returns></returns>
        List<Automobilis> GautiVisusNaftosKuroAutomobilius();
        /// <summary>
        /// Grazina automobiliu sarasa pagal nurodyta marke
        /// </summary>
        /// <param name="marke"></param>
        /// <returns></returns>
        List<Automobilis> PaieskaPagalMarke(string marke);
        /// <summary>
        /// Prideda nauja automobili i automobiliu sarasa
        /// </summary>
        /// <param name="automobilis"></param>
        /// <returns></returns>
        string PridetiNaujaAutomobili(Automobilis automobilis);
        /// <summary>
        /// Istrina automobili is automobiliu saraso
        /// </summary>
        /// <param name="automobilis"></param>
        /// <returns></returns>
        string IstrintiAutomobili(Automobilis automobilis, bool bTrintiUzsakymus);
        /// <summary>
        /// Grazina visu klientu sarasa
        /// </summary>
        /// <returns></returns>
        List<Klientas> GautiVisusKlientus();
        /// <summary>
        /// Grazina klientu sarasa pagal varda ir pavarde
        /// </summary>
        /// <param name="vardas"></param>
        /// <param name="pavarde"></param>
        /// <returns></returns>
        List<Klientas> PaieskaPagalVardaIrPavarde(string vardas, string pavarde);
        /// <summary>
        /// Prideda nauja klienta i klientu sarasa
        /// </summary>
        /// <param name="klientas"></param>
        /// <returns></returns>
        string PridetiNaujaKlienta(Klientas klientas);
        /// <summary>
        /// Istrina klienta is klientu saraso
        /// </summary>
        /// <param name="klientas"></param>
        /// <returns></returns>
        string IstrintiKlienta(Klientas klientas);
        /// <summary>
        /// Grazina visu nuomos uzsakymu sarasa
        /// </summary>
        /// <returns></returns>
        List<NuomosUzsakymas> GautiVisusUzsakymus();
        /// <summary>
        /// Grazina nuomos uzsakymu sarasa pagal nurodyta klienta
        /// </summary>
        /// <param name="klientas"></param>
        /// <returns></returns>
        List<NuomosUzsakymas> GautiUzsakymusPagalKlienta(Klientas klientas);
        /// <summary>
        /// Grazina dirbtini nuomos uzsakyma pagal nurodyta automobili ir dienu kieki
        /// </summary>
        /// <param name="automobilis"></param>
        /// <param name="dienuKiekis"></param>
        /// <returns></returns>
        List<NuomosUzsakymas> GautiUzsakymusPagalDarbuotoja(Darbuotojas darbuotojas);
        NuomosUzsakymas SkaiciuotiBendraNuomosKaina(Automobilis automobilis, int dienuKiekis);
        /// <summary>
        /// Prideda nuomos uzsakyma i uzsakymu sarasa
        /// </summary>
        /// <param name="uzsakymas"></param>
        /// <returns></returns>
        string SukurtiUzsakyma(NuomosUzsakymas uzsakymas);
        /// <summary>
        /// Istrina nuomos uzsakyma is uzsakymu saraso
        /// </summary>
        /// <param name="uzsakymas"></param>
        /// <returns></returns>
        string IstrintiUzsakyma(NuomosUzsakymas uzsakymas);
        void IrasytiIFaila(NuomosUzsakymas uzsakymas);
        void IstrintiIsFailo(NuomosUzsakymas uzsakymas);
        void NuskaitytiIsFailo();
        string AtnaujintiAutomobili(Automobilis automobilis, Automobilis naujasAutomobilis, out bool bPavyko);
        string AtnaujintiKlienta(Klientas klientas, Klientas naujasKlientas, out bool bPavyko);
        string AtnaujintiUzsakyma(NuomosUzsakymas uzsakymas, NuomosUzsakymas naujasUzsakymas);
        List<Darbuotojas> GautiVisusDarbuotojus();
        List<Darbuotojas> DarbuotojoPaieskaPagalVardaIrPavarde(string vardas, string pavarde);
        string PridetiNaujaDarbuotoja(Darbuotojas darbuotojas);
        string IstrintiDarbuotoja(Darbuotojas darbuotojas);
        string AtnaujintiDarbuotoja(Darbuotojas darbuotojas, Darbuotojas naujasDarbuotojas, out bool bPavyko);
    }
}
