using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using System.Data;
using System.Data.SqlClient;

namespace AutomobiliuNuoma.Core.Repositories
{
    public class AutomobiliaiDbRepository : IAutomobiliaiRepository
    {
        private readonly string _dbConnectionString;
        public AutomobiliaiDbRepository(string connectionString)
        {
            _dbConnectionString = connectionString;
        }
        public void IrasytiAutomobili(Automobilis automobilis)
        {
            if (automobilis is Elektromobilis)
                IrasytiElektromobili(((Elektromobilis)automobilis));
            else if (automobilis is NaftosKuroAutomobilis)
                IrasytiNaftosKuroAutomobili(((NaftosKuroAutomobilis)automobilis));
        }

        private void IrasytiElektromobili(Elektromobilis automobilis)
        {
            string sqlCommand = "INSERT INTO elektromobiliai (valst_nr, marke, modelis, nuomos_kaina, baterijos_talpa, krovimo_laikas) VALUES (@ID, @Marke, @Modelis, @NuomosKaina, @BaterijosTalpa, @KrovimoLaikas)";

            var parameters = new
            {
                ID = automobilis.ID,
                Marke = automobilis.Marke,
                Modelis = automobilis.Modelis,
                NuomosKaina = automobilis.NuomosKaina,
                BaterijosTalpa = automobilis.BaterijosTalpa,
                KrovimoLaikas = automobilis.KrovimoLaikas
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }

        private void IrasytiNaftosKuroAutomobili(NaftosKuroAutomobilis automobilis)
        {
            string sqlCommand = "INSERT INTO naftos_kuro_automobiliai (valst_nr, marke, modelis, nuomos_kaina, degalu_sanaudos) VALUES (@ID, @Marke, @Modelis, @NuomosKaina, @DegaluSanaudos)";

            var parameters = new
            {
                ID = automobilis.ID,
                Marke = automobilis.Marke,
                Modelis = automobilis.Modelis,
                NuomosKaina = automobilis.NuomosKaina,
                DegaluSanaudos = automobilis.DegaluSanaudos
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }

        public List<Automobilis> NuskaitytiAutomobilius()
        {
            List<Automobilis> automobiliai = new List<Automobilis>();
            foreach(Automobilis automobilis in GautiVisusElektromobilius())
            {
                automobiliai.Add(automobilis);
            }
            foreach (Automobilis automobilis in GautiVisusNaftosKuroAutomobilius())
            {
                automobiliai.Add(automobilis);
            }
            return automobiliai;
        }

        private List<Elektromobilis> GautiVisusElektromobilius()
        {
            List<Elektromobilis> tempList = new List<Elektromobilis>();
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            {
                tempList = dbConnection.
                Query<Elektromobilis>(@"SELECT valst_nr AS 'id', marke, modelis, nuomos_kaina AS 'nuomosKaina',
                baterijos_talpa AS 'baterijosTalpa', krovimo_laikas AS 'krovimoLaikas'  FROM elektromobiliai ORDER BY valst_nr").ToList();
            }
            return tempList;
        }

        private List<NaftosKuroAutomobilis> GautiVisusNaftosKuroAutomobilius()
        {
            List<NaftosKuroAutomobilis> tempList = new List<NaftosKuroAutomobilis>();
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            {
                tempList = dbConnection.
                Query<NaftosKuroAutomobilis>(@"SELECT valst_nr AS 'id', marke, modelis, nuomos_kaina AS 'nuomosKaina', 
                degalu_sanaudos AS 'degaluSanaudos' FROM naftos_kuro_automobiliai ORDER BY valst_nr").ToList();
            }
            return tempList;
        }

        public void AtnaujintiAutomobili(Automobilis senasAutomobilis, Automobilis automobilis)
        {
            if(senasAutomobilis.ID != automobilis.ID)
            {
                string sqlCommand = "UPDATE uzsakymai SET valst_nr = @NewID WHERE valst_nr = @OldID";


                var parameters = new
                {
                    NewID = automobilis.ID,
                    OldID = senasAutomobilis.ID,
                };

                using (var connection = new SqlConnection(_dbConnectionString))
                {
                    connection.Execute(sqlCommand, parameters);
                }
            }

            if ((senasAutomobilis is Elektromobilis && automobilis is Elektromobilis) || (senasAutomobilis is NaftosKuroAutomobilis && automobilis is NaftosKuroAutomobilis))
            {
                if (automobilis is Elektromobilis)
                    AtnaujintiElektromobili(senasAutomobilis, ((Elektromobilis)automobilis));
                else
                    AtnaujintiNaftosKuroAutomobili(senasAutomobilis, ((NaftosKuroAutomobilis)automobilis));
            }
            else
            {
                IstrintiAutomobili(senasAutomobilis,false);
                IrasytiAutomobili(automobilis);
            }
        }

        private void AtnaujintiElektromobili(Automobilis senasAutomobilis, Elektromobilis automobilis)
        {
            string sqlCommand = @"UPDATE elektromobiliai SET 
                valst_nr = @NewID,
                marke = @Marke,
                modelis = @Modelis,
                nuomos_kaina = @NuomosKaina,
                baterijos_talpa = @BaterijosTalpa,
                krovimo_laikas = @KrovimoLaikas
                WHERE valst_nr = @ID";

            var parameters = new
            {
                ID = senasAutomobilis.ID,
                NewID = automobilis.ID,
                Marke = automobilis.Marke,
                Modelis = automobilis.Modelis,
                NuomosKaina = automobilis.NuomosKaina,
                BaterijosTalpa = automobilis.BaterijosTalpa,
                KrovimoLaikas = automobilis.KrovimoLaikas
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }

        private void AtnaujintiNaftosKuroAutomobili(Automobilis senasAutomobilis, NaftosKuroAutomobilis automobilis)
        {
            string sqlCommand = @"UPDATE naftos_kuro_automobiliai SET 
                valst_nr = @NewID,
                marke = @Marke,
                modelis = @Modelis,
                nuomos_kaina = @NuomosKaina,
                degalu_sanaudos = @DegaluSanaudos
                WHERE valst_nr = @ID";

            var parameters = new
            {
                ID = senasAutomobilis.ID,
                NewID = automobilis.ID,
                Marke = automobilis.Marke,
                Modelis = automobilis.Modelis,
                NuomosKaina = automobilis.NuomosKaina,
                DegaluSanaudos = automobilis.DegaluSanaudos
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }

        public void IstrintiAutomobili(Automobilis automobilis, bool bTrintiUzsakymus)
        {
            if(bTrintiUzsakymus)
                IstrintiUzsakymus(automobilis);

            if (automobilis is Elektromobilis)
                IstrintiElektromobili(automobilis);
            else if (automobilis is NaftosKuroAutomobilis)
                IstrintiNaftosKuroAutomobili(automobilis);
        }

        private void IstrintiElektromobili(Automobilis automobilis)
        {
            string sqlCommand = "DELETE FROM elektromobiliai WHERE valst_nr = @ID";

            var parameters = new
            {
                ID = automobilis.ID
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }

        private void IstrintiNaftosKuroAutomobili(Automobilis automobilis)
        {
            string sqlCommand = "DELETE FROM naftos_kuro_automobiliai WHERE valst_nr = @ID";

            var parameters = new
            {
                ID = automobilis.ID
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }

        private void IstrintiUzsakymus(Automobilis automobilis)
        {
            string sqlCommand = @"DELETE FROM uzsakymai WHERE valst_nr = @ID";

            var parameters = new
            {
                ID = automobilis.ID,
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }
    }
}
