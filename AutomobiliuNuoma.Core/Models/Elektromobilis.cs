using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Models
{
    public class Elektromobilis : Automobilis
    {
        public float BaterijosTalpa {  get; set; }
        public int KrovimoLaikas { get; set; }

        public Elektromobilis(string id, string marke, string modelis, decimal nuomosKaina, float baterijosTalpa, int krovimoLaikas) : base(id, marke, modelis, nuomosKaina)
        {
            BaterijosTalpa = Convert.ToSingle(Math.Floor(baterijosTalpa * 10.0) * 0.1);
            KrovimoLaikas = krovimoLaikas;
        }

        public override string ToString()
        {
            return base.ToString() + $" Baterijos talpa: {BaterijosTalpa} kWh ".PadRight(31) + $" Krovimo laikas: {KrovimoLaikas} minutes";
        }
    }
}
