using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;

namespace AutomobiliuNuoma.Core.Repositories
{
    public class KlientaiRepository : IKlientaiRepository
    {
        private readonly string _filePath;
        public KlientaiRepository(string klientaiFilePath)
        {
            _filePath = klientaiFilePath;
        }

        public void IrasytiKlientus(List<Klientas> klientai, bool bTikPrideti)
        {
            foreach (Klientas klientas in klientai)
            {
                using (StreamWriter sw = new StreamWriter(_filePath, bTikPrideti))
                {
                    if (!bTikPrideti)
                        bTikPrideti = true;
                    sw.WriteLine($"{klientas.Vardas},{klientas.Pavarde},{klientas.GimimoData}");
                }
            }
        }

        public List<Klientas> NuskaitytiKlientus()
        {
            List<Klientas> klientai = new List<Klientas>();

            using (StreamReader sr = new StreamReader(_filePath))
            {
                while (!sr.EndOfStream)
                {
                    string eilute = sr.ReadLine() ?? string.Empty;
                    string[] vertes = eilute.Split(',');
                    if(vertes.Length == 3)
                        klientai.Add(new Klientas(vertes[0], vertes[1], DateOnly.Parse(vertes[2])));
                }
            }
            return klientai;
        }
    }
}
