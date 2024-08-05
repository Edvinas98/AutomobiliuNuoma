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

namespace AutomobiliuNuoma.Core.Repositories
{
    public class DarbuotojaiDbRepository : IDarbuotojaiRepository
    {
        private readonly string _dbConnectionString;
        public DarbuotojaiDbRepository(string connectionString)
        {
            _dbConnectionString = connectionString;
        }

        public void IrasytiDarbuotoja(Darbuotojas darbuotojas)
        {
            string sqlCommand = "INSERT INTO darbuotojai (vardas, pavarde, pareigos) VALUES (@Vardas, @Pavarde, @Pareigos)";

            var parameters = new
            {
                Vardas = darbuotojas.Vardas,
                Pavarde = darbuotojas.Pavarde,
                Pareigos = darbuotojas.Pareigos
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }

        public List<Darbuotojas> NuskaitytiDarbuotojus()
        {
            List<Darbuotojas> tempList = new List<Darbuotojas>();

            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            {
                tempList = dbConnection.
                Query<Darbuotojas>("SELECT vardas, pavarde, pareigos FROM darbuotojai").ToList();
            }
            return tempList;
        }

        public void AtnaujintiDarbuotoja(Darbuotojas senasDarbuotojas, Darbuotojas darbuotojas)
        {
            string sqlCommand = @"UPDATE darbuotojai SET 
                vardas = @NewVardas,
                pavarde = @NewPavarde,
                pareigos = @NewPareigos
                WHERE vardas = @Vardas AND pavarde = @Pavarde AND pareigos = @Pareigos";

            var parameters = new
            {
                Vardas = senasDarbuotojas.Vardas,
                Pavarde = senasDarbuotojas.Pavarde,
                Pareigos = senasDarbuotojas.Pareigos,
                NewVardas = darbuotojas.Vardas,
                NewPavarde = darbuotojas.Pavarde,
                NewPareigos = darbuotojas.Pareigos
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }

        public void IstrintiDarbuotoja(Darbuotojas darbuotojas)
        {
            IstrintiUzsakymus(darbuotojas);

            string sqlCommand = @"DELETE FROM darbuotojai
                WHERE vardas = @Vardas AND pavarde = @Pavarde";

            var parameters = new
            {
                Vardas = darbuotojas.Vardas,
                Pavarde = darbuotojas.Pavarde
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }

        private void IstrintiUzsakymus(Darbuotojas darbuotojas)
        {
            string sqlCommand = @"SELECT item_id FROM darbuotojai WHERE vardas = @Vardas AND pavarde = @Pavarde
                AND pareigos = @Pareigos";

            var parameters1 = new
            {
                Vardas = darbuotojas.Vardas,
                Pavarde = darbuotojas.Pavarde,
                Pareigos = (byte)darbuotojas.Pareigos
            };

            int darbuotojoID;

            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            {
                darbuotojoID = dbConnection.
                QueryFirst<int>(sqlCommand, parameters1);
            }

            sqlCommand = @"DELETE FROM uzsakymai WHERE darbuotojas_ID = @DarbuotojoID";

            var parameters2 = new
            {
                DarbuotojoID = darbuotojoID,
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters2);
            }
        }
    }
}
