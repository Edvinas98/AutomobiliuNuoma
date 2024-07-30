using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Models
{
    public class NaftosKuroAutomobilis : Automobilis
    {
        public float DegaluSanaudos {  get; set; }

        public NaftosKuroAutomobilis(string id, string marke, string modelis, decimal nuomosKaina, float degaluSanaudos) : base(id, marke, modelis, nuomosKaina)
        {
            DegaluSanaudos = Convert.ToSingle(Math.Floor(degaluSanaudos * 10.0) * 0.1);
        }

        public override string ToString()
        {
            return base.ToString() + $" Degalu sanaudos: {DegaluSanaudos} L/100km";
        }
    }
}
