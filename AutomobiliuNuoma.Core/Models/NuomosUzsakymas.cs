using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Models
{
    public class NuomosUzsakymas
    {
        public Klientas NuomosKlientas { get; set; }
        public Darbuotojas NuomosDarbuotojas { get; set; }
        public Automobilis NuomosAutomobilis { get; set; }
        public DateTime NuomosPradzia { get; set; }
        public int DienuKiekis { get; set; }

        public NuomosUzsakymas(Klientas klientas, Darbuotojas darbuotojas, Automobilis automobilis, int dienuKiekis)
        {
            NuomosKlientas = klientas;
            NuomosDarbuotojas = darbuotojas;
            NuomosAutomobilis = automobilis;
            NuomosPradzia = DateTime.Now;
            DienuKiekis = dienuKiekis;
        }

        public NuomosUzsakymas(Klientas klientas, Darbuotojas darbuotojas, Automobilis automobilis, DateTime nuomosPradzia, int dienuKiekis)
        {
            NuomosKlientas = klientas;
            NuomosDarbuotojas = darbuotojas;
            NuomosAutomobilis = automobilis;
            NuomosPradzia = nuomosPradzia;
            DienuKiekis = dienuKiekis;
        }

        public override string ToString()
        {
            return $"Klientas: {NuomosKlientas}\n{NuomosAutomobilis}\n" + $"Nuomos pradzia: {NuomosPradzia}".PadRight(40) + $" Dienu kiekis: {DienuKiekis}".PadRight(19) + $"Bendra uzsakymo kaina: {NuomosAutomobilis.NuomosKaina * Convert.ToDecimal(DienuKiekis)} Eur\nDarbuotojas: {NuomosDarbuotojas}";
        }
    }
}
