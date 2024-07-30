using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Models
{
    public class Automobilis
    {
        public string ID;
        public string Marke { get; set; }
        public string Modelis { get; set; }
        public decimal NuomosKaina { get; set; }

        public Automobilis(string id, string marke, string modelis, decimal nuomosKaina)
        {
            ID = id;
            Marke = marke;
            Modelis = modelis;
            NuomosKaina = Math.Floor(nuomosKaina * 100M) * 0.01M;
        }

        public override string ToString()
        {
            string aprasymas = ID.PadRight(8);
            aprasymas += " Marke: " + Marke.PadRight(10);
            aprasymas += " Modelis: " + Modelis.PadRight(12);
            aprasymas += $" Nuomos kaina: {NuomosKaina} Eur".PadRight(27);

            return aprasymas;
        }
    }
}
