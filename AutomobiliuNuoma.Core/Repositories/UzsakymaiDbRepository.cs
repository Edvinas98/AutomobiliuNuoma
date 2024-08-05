using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using Dapper;
using System.Collections;
using System.Reflection;

namespace AutomobiliuNuoma.Core.Repositories
{
    struct Uzsakymai
    {
        public int KlientoID;
        public int DarbuotojoID;
        public string ID;
        public DateTime NuomosPradzia;
        public int DienuKiekis;
    };
    public class UzsakymaiDbRepository : IUzsakymaiRepository
    {
        private readonly string _dbConnectionString;
        public UzsakymaiDbRepository(string connectionString)
        {
            _dbConnectionString = connectionString;
        }

        public void IrasytiUzsakyma(NuomosUzsakymas uzsakymas)
        {
            string sqlCommand = "SELECT item_id FROM klientai WHERE vardas = @Vardas AND pavarde = @Pavarde AND gimimo_data = @GimimoData";

            int klientoID;

            var parameters1 = new
            {
                Vardas = uzsakymas.NuomosKlientas.Vardas,
                Pavarde = uzsakymas.NuomosKlientas.Pavarde,
                GimimoData = uzsakymas.NuomosKlientas.GimimoData.ToDateTime(TimeOnly.Parse("00:00:00"))
            };

            using IDbConnection dbConnection1 = new SqlConnection(_dbConnectionString);
            {
                klientoID = dbConnection1.
                QueryFirst<int>(sqlCommand, parameters1);
            }

            sqlCommand = "SELECT item_id FROM darbuotojai WHERE vardas = @Vardas AND pavarde = @Pavarde AND pareigos = @Pareigos";

            int darbuotojoID;

            var parameters2 = new
            {
                Vardas = uzsakymas.NuomosDarbuotojas.Vardas,
                Pavarde = uzsakymas.NuomosDarbuotojas.Pavarde,
                Pareigos = (byte)uzsakymas.NuomosDarbuotojas.Pareigos
            };

            using IDbConnection dbConnection2 = new SqlConnection(_dbConnectionString);
            {
                darbuotojoID = dbConnection2.
                QueryFirst<int>(sqlCommand, parameters2);
            }

            sqlCommand = "INSERT INTO uzsakymai (klientas_id, darbuotojas_id, valst_nr, nuomos_pradzia, dienu_kiekis) VALUES (@KlientoID, @DarbuotojoID, @ID, @NuomosPradzia, @DienuKiekis)";

            var parameters3 = new
            {
                KlientoID = klientoID,
                DarbuotojoID = darbuotojoID,
                ID = uzsakymas.NuomosAutomobilis.ID,
                NuomosPradzia = uzsakymas.NuomosPradzia,
                DienuKiekis = uzsakymas.DienuKiekis
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters3);
            }
        }

        public List<NuomosUzsakymas> NuskaitytiUzsakymus(List<Klientas> klientai, List<Darbuotojas> darbuotojai, List<Automobilis> automobiliai)
        {
            List<Uzsakymai> uzsakymai = new List<Uzsakymai>();

            using IDbConnection dbConnection1 = new SqlConnection(_dbConnectionString);
            {
                uzsakymai = dbConnection1.
                Query<Uzsakymai>(@"SELECT klientas_id AS 'KlientoID', darbuotojas_id AS 'DarbuotojoID', valst_nr AS 'ID',
                nuomos_pradzia AS 'NuomosPradzia', dienu_kiekis AS 'DienuKiekis' FROM uzsakymai").ToList();
            }

            List<NuomosUzsakymas> nuomosUzsakymai = new List<NuomosUzsakymas>();

            for(int i = 0; i < uzsakymai.Count; i++)
            {
                Klientas klientas;

                var parameters1 = new
                { 
                    KlientoID = uzsakymai[i].KlientoID 
                };

                using IDbConnection dbConnection2 = new SqlConnection(_dbConnectionString);
                {
                    klientas = dbConnection2.
                    QueryFirst<Klientas>("SELECT vardas, pavarde, gimimo_data AS 'gimimoData' FROM klientai WHERE item_id = @KlientoID", parameters1);
                }

               Darbuotojas darbuotojas;

                var parameters2 = new
                {
                    DarbuotojoID = uzsakymai[i].DarbuotojoID
                };

                using IDbConnection dbConnection3 = new SqlConnection(_dbConnectionString);
                {
                    darbuotojas = dbConnection2.
                    QueryFirst<Darbuotojas>("SELECT vardas, pavarde, pareigos FROM darbuotojai WHERE item_id = @DarbuotojoID", parameters2);
                }

                int klientoID = -1;

                for(int j = 0; j < klientai.Count; j++)
                {
                    if (klientas.Vardas == klientai[j].Vardas && klientas.Pavarde == klientai[j].Pavarde && klientas.GimimoData == klientai[j].GimimoData)
                    {
                        klientoID = j;
                        break;
                    }
                }

                int darbuotojoID = -1;

                for (int j = 0; j < darbuotojai.Count; j++)
                {
                    if (darbuotojas.Vardas == darbuotojai[j].Vardas && darbuotojas.Pavarde == darbuotojai[j].Pavarde)
                    {
                        darbuotojoID = j;
                        break;
                    }
                }

                int automobilioID = -1;

                for (int j = 0; j < automobiliai.Count; j++)
                {
                    if (uzsakymai[i].ID == automobiliai[j].ID)
                    {
                        automobilioID = j;
                        break;
                    }
                }

                if(klientoID > -1 && darbuotojoID > -1 && automobilioID > -1)
                    nuomosUzsakymai.Add(new NuomosUzsakymas(klientai[klientoID], darbuotojai[darbuotojoID], automobiliai[automobilioID], uzsakymai[i].NuomosPradzia, uzsakymai[i].DienuKiekis));

            }
            return nuomosUzsakymai;
        }

        public void IstrintiUzsakyma(NuomosUzsakymas uzsakymas)
        {
            string sqlCommand = @"SELECT item_id FROM klientai WHERE vardas = @Vardas AND pavarde = @Pavarde
                AND gimimo_data = @GimimoData";

            var parameters1 = new
            {
                Vardas = uzsakymas.NuomosKlientas.Vardas,
                Pavarde = uzsakymas.NuomosKlientas.Pavarde,
                GimimoData = uzsakymas.NuomosKlientas.GimimoData.ToDateTime(TimeOnly.Parse("00:00:00"))
            };

            int klientoID;

            using IDbConnection dbConnection1 = new SqlConnection(_dbConnectionString);
            {
                klientoID = dbConnection1.
                QueryFirst<int>(sqlCommand, parameters1);
            }

            sqlCommand = @"SELECT item_id FROM darbuotojai WHERE vardas = @Vardas AND pavarde = @Pavarde
                AND pareigos = @Pareigos";

            var parameters2 = new
            {
                Vardas = uzsakymas.NuomosDarbuotojas.Vardas,
                Pavarde = uzsakymas.NuomosDarbuotojas.Pavarde,
                Pareigos = (byte) uzsakymas.NuomosDarbuotojas.Pareigos
            };

            int darbuotojoID;

            using IDbConnection dbConnection2 = new SqlConnection(_dbConnectionString);
            {
                darbuotojoID = dbConnection2.
                QueryFirst<int>(sqlCommand, parameters2);
            }

            sqlCommand = @"DELETE FROM uzsakymai
            WHERE klientas_id = @KlientoID AND darbuotojas_ID = @DarbuotojoID AND valst_nr = @ID
            AND nuomos_pradzia = @NuomosPradzia AND dienu_kiekis = @DienuKiekis";

            var parameters3 = new
            {
                KlientoID = klientoID,
                DarbuotojoID = darbuotojoID,
                ID = uzsakymas.NuomosAutomobilis.ID,
                NuomosPradzia = uzsakymas.NuomosPradzia,
                DienuKiekis = uzsakymas.DienuKiekis
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters3);
            }
        }

        public void AtnaujintiUzsakyma(NuomosUzsakymas senasUzsakymas, NuomosUzsakymas uzsakymas)
        {
            string sqlCommand = @"SELECT item_id FROM klientai WHERE vardas = @Vardas AND pavarde = @Pavarde
                AND gimimo_data = @GimimoData";

            var parameters1 = new
            {
                Vardas = senasUzsakymas.NuomosKlientas.Vardas,
                Pavarde = senasUzsakymas.NuomosKlientas.Pavarde,
                GimimoData = senasUzsakymas.NuomosKlientas.GimimoData.ToDateTime(TimeOnly.Parse("00:00:00"))
            };

            int klientoID;

            using IDbConnection dbConnection1 = new SqlConnection(_dbConnectionString);
            {
                klientoID = dbConnection1.
                QueryFirst<int>(sqlCommand, parameters1);
            }

            sqlCommand = @"SELECT item_id FROM darbuotojai WHERE vardas = @Vardas AND pavarde = @Pavarde
                AND pareigos = @Pareigos";

            var parameters2 = new
            {
                Vardas = senasUzsakymas.NuomosDarbuotojas.Vardas,
                Pavarde = senasUzsakymas.NuomosDarbuotojas.Pavarde,
                Pareigos = (byte)senasUzsakymas.NuomosDarbuotojas.Pareigos
            };

            int darbuotojoID;

            using IDbConnection dbConnection2 = new SqlConnection(_dbConnectionString);
            {
                darbuotojoID = dbConnection2.
                QueryFirst<int>(sqlCommand, parameters2);
            }

            sqlCommand = @"SELECT item_id FROM uzsakymai WHERE klientas_id = @KlientoID AND valst_nr = @ID
                AND nuomos_pradzia = @NuomosPradzia AND dienu_kiekis = @DienuKiekis";

            var parameters3 = new
            {
                KlientoID = klientoID,
                DarbuotojoID = darbuotojoID,
                ID = senasUzsakymas.NuomosAutomobilis.ID,
                NuomosPradzia = senasUzsakymas.NuomosPradzia,
                DienuKiekis = senasUzsakymas.DienuKiekis
            };

            int uzsakymoID;

            using IDbConnection dbConnection3 = new SqlConnection(_dbConnectionString);
            {
                uzsakymoID = dbConnection3.
                QueryFirst<int>(sqlCommand, parameters3);
            }

            int naujasKlientoID = klientoID;

            if (senasUzsakymas.NuomosKlientas != uzsakymas.NuomosKlientas)
            {
                sqlCommand = "SELECT item_id FROM klientai WHERE vardas = @Vardas AND pavarde = @Pavarde AND gimimo_data = @GimimoData";

                var parameters4 = new
                {
                    Vardas = uzsakymas.NuomosKlientas.Vardas,
                    Pavarde = uzsakymas.NuomosKlientas.Pavarde,
                    GimimoData = uzsakymas.NuomosKlientas.GimimoData.ToDateTime(TimeOnly.Parse("00:00:00"))
                };

                using IDbConnection dbConnection4 = new SqlConnection(_dbConnectionString);
                {
                    naujasKlientoID = dbConnection4.
                    QueryFirst<int>(sqlCommand, parameters4);
                }
            }

            int naujasDarbuotojoID = darbuotojoID;

            if (senasUzsakymas.NuomosDarbuotojas != uzsakymas.NuomosDarbuotojas)
            {
                sqlCommand = @"SELECT item_id FROM darbuotojai WHERE vardas = @Vardas AND pavarde = @Pavarde
                AND pareigos = @Pareigos";

                var parameters4 = new
                {
                    Vardas = uzsakymas.NuomosDarbuotojas.Vardas,
                    Pavarde = uzsakymas.NuomosDarbuotojas.Pavarde,
                    Pareigos = (byte)uzsakymas.NuomosDarbuotojas.Pareigos
                };

                using IDbConnection dbConnection4 = new SqlConnection(_dbConnectionString);
                {
                    naujasDarbuotojoID = dbConnection4.
                    QueryFirst<int>(sqlCommand, parameters4);
                }
            }

            sqlCommand = @"UPDATE uzsakymai SET 
                klientas_id = @NewKlientoID,
                darbuotojas_id = @NewDarbuotojoID,
                valst_nr = @NewID,
                nuomos_pradzia = @NewNuomosPradzia,
                dienu_kiekis = @NewDienuKiekis
                WHERE klientas_id = @KlientoID AND darbuotojas_id = @DarbuotojoID AND valst_nr = @ID AND nuomos_pradzia = @NuomosPradzia AND dienu_kiekis = @DienuKiekis";


            var parameters5 = new
            {
                NewKlientoID = naujasKlientoID,
                NewDarbuotojoID = naujasDarbuotojoID,
                NewID = uzsakymas.NuomosAutomobilis.ID,
                NewNuomosPradzia = uzsakymas.NuomosPradzia,
                NewDienuKiekis = uzsakymas.DienuKiekis,

                KlientoID = klientoID,
                DarbuotojoID = darbuotojoID,
                ID = senasUzsakymas.NuomosAutomobilis.ID,
                NuomosPradzia = senasUzsakymas.NuomosPradzia,
                DienuKiekis = senasUzsakymas.DienuKiekis
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters5);
            }
        }
    }
}
