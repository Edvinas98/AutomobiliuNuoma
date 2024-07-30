using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Models
{
    public class Klientas
    {
        public string Vardas {  get; set; }
        public string Pavarde { get; set; }
        public DateOnly GimimoData { get; set; }

        public Klientas(string vardas, string pavarde, DateOnly gimimoData)
        {
            Vardas = vardas;
            Pavarde = pavarde;
            GimimoData = gimimoData;
        }

        public override string ToString()
        {
            return $"{Vardas} {Pavarde}".PadRight(30) + $" Gimimo metai: {GimimoData}";
        }
    }
}
