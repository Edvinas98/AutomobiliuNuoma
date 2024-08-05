using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobiliuNuoma.Core.Enums;

namespace AutomobiliuNuoma.Core.Models
{
    public class Darbuotojas
    {
        public string Vardas { get; set; }
        public string Pavarde { get; set; }
        public DarbuotojasPareigos Pareigos { get; set; }

        public Darbuotojas(string vardas, string pavarde, byte pareigos)
        {
            Vardas = vardas;
            Pavarde = pavarde;
            Pareigos = (DarbuotojasPareigos) pareigos;
        }

        public decimal ApskaiciuotiAtlyginima(decimal BazineAlga, int AtliktiUzsakymai)
        {
            if (Pareigos == DarbuotojasPareigos.Vadybininkas)
                return BazineAlga + AtliktiUzsakymai * 7;
            else return BazineAlga;
        }

        public override string ToString()
        {
            return $"{Vardas} {Pavarde}".PadRight(30) + $"Pareigos: {Pareigos}";
        }
    }
}
