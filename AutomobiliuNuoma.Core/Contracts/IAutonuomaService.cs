using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobiliuNuoma.Core.Models;
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
        string IstrintiAutomobili(Automobilis automobilis);
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
        /// Grazina automobiliu, kurie nera isnuomoti, sarasa
        /// </summary>
        /// <returns></returns>
        List<Automobilis> GautiVisusLaisvusAutomobilius();
        /// <summary>
        /// Grazina dirbtini nuomos uzsakyma pagal nurodyta automobili ir dienu kieki
        /// </summary>
        /// <param name="automobilis"></param>
        /// <param name="dienuKiekis"></param>
        /// <returns></returns>
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
    }
}
