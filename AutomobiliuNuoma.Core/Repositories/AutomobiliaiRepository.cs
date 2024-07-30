using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;

namespace AutomobiliuNuoma.Core.Repositories
{
    public class AutomobiliaiRepository : IAutomobiliaiRepository
    {
        private readonly string _filePath;
        public AutomobiliaiRepository(string autoFilePath)
        {
            _filePath = autoFilePath;
        }
        public void IrasytiAutomobilius(List<Automobilis> automobiliai, bool bTikPrideti)
        {
            foreach (Automobilis automobilis in automobiliai)
            {
                using (StreamWriter sw = new StreamWriter(_filePath, bTikPrideti))
                {
                    if (!bTikPrideti)
                        bTikPrideti = true;
                    if (automobilis is Elektromobilis)
                        sw.WriteLine($"{automobilis.ID},{automobilis.Marke},{automobilis.Modelis},{automobilis.NuomosKaina},{((Elektromobilis)automobilis).BaterijosTalpa},{((Elektromobilis)automobilis).KrovimoLaikas}");
                    else if (automobilis is NaftosKuroAutomobilis)
                        sw.WriteLine($"{automobilis.ID},{automobilis.Marke},{automobilis.Modelis},{automobilis.NuomosKaina},{((NaftosKuroAutomobilis)automobilis).DegaluSanaudos}");
                    else 
                        sw.WriteLine($"{automobilis.ID},{automobilis.Marke},{automobilis.Modelis},{automobilis.NuomosKaina}");
                }
            }
        }

        public List<Automobilis> NuskaitytiAutomobilius()
        {
            List<Automobilis> automobiliai = new List<Automobilis>();

            using (StreamReader sr = new StreamReader(_filePath))
            {
                while (!sr.EndOfStream)
                {
                    string eilute = sr.ReadLine() ?? string.Empty;
                    string[] vertes = eilute.Split(',');
                    if (vertes.Length >= 4)
                    {
                        if (!decimal.TryParse(vertes[3], out decimal kaina) || kaina <= 0)
                            continue;
                        if (vertes.Length == 6)
                        {
                            if (!float.TryParse(vertes[4], out float baterijostalpa) || baterijostalpa <= 0)
                                continue;
                            if (!int.TryParse(vertes[5], out int krovimolaikas) || krovimolaikas <= 0)
                                continue;
                            automobiliai.Add(new Elektromobilis(vertes[0], vertes[1], vertes[2], kaina, baterijostalpa, krovimolaikas));
                        }
                        else if(vertes.Length == 5)
                        {
                            if (!float.TryParse(vertes[4], out float degaluSanaudos) || degaluSanaudos <= 0)
                                continue;
                            automobiliai.Add(new NaftosKuroAutomobilis(vertes[0], vertes[1], vertes[2], kaina, degaluSanaudos));
                        }
                        else
                            automobiliai.Add(new Automobilis(vertes[0], vertes[1], vertes[2], kaina));
                    }
                }
            }
            return automobiliai;
        }
    }
}
