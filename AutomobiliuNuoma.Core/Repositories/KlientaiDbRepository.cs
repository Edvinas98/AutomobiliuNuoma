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
    public class KlientaiDbRepository : IKlientaiRepository
    {
        private readonly string _dbConnectionString;
        public KlientaiDbRepository(string connectionString)
        {
            _dbConnectionString = connectionString;
        }

        public void IrasytiKlienta(Klientas klientas)
        {
            string sqlCommand = "INSERT INTO klientai (vardas, pavarde, gimimo_data) VALUES (@Vardas, @Pavarde, @GimimoData)";

            var parameters = new
            {
                Vardas = klientas.Vardas,
                Pavarde = klientas.Pavarde,
                GimimoData = klientas.GimimoData.ToDateTime(TimeOnly.Parse("00:00:00"))
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }

        public List<Klientas> NuskaitytiKlientus()
        {
            List<Klientas> tempList = new List<Klientas>();

            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            {
                tempList = dbConnection.
                Query<Klientas>("SELECT vardas, pavarde, gimimo_data AS 'gimimoData' FROM klientai").ToList();
            }
            return tempList;
        }

        public void AtnaujintiKlienta(Klientas senasKlientas, Klientas klientas)
        {
            string sqlCommand = @"UPDATE klientai SET 
                vardas = @NewVardas,
                pavarde = @NewPavarde,
                gimimo_data = @NewGimimoData
                WHERE vardas = @Vardas AND pavarde = @Pavarde AND gimimo_data = @GimimoData";

            var parameters = new
            {
                Vardas = senasKlientas.Vardas,
                Pavarde = senasKlientas.Pavarde,
                GimimoData = senasKlientas.GimimoData.ToDateTime(TimeOnly.Parse("00:00:00")),
                NewVardas = klientas.Vardas,
                NewPavarde = klientas.Pavarde,
                NewGimimoData = klientas.GimimoData.ToDateTime(TimeOnly.Parse("00:00:00"))
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }

        public void IstrintiKlienta(Klientas klientas)
        {
            IstrintiUzsakymus(klientas);

            string sqlCommand = @"DELETE FROM klientai
                WHERE vardas = @Vardas AND pavarde = @Pavarde AND gimimo_data = @GimimoData";

            var parameters = new
            {
                Vardas = klientas.Vardas,
                Pavarde = klientas.Pavarde,
                GimimoData = klientas.GimimoData.ToDateTime(TimeOnly.Parse("00:00:00"))
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }

        private void IstrintiUzsakymus(Klientas klientas)
        {
            string sqlCommand = @"SELECT item_id FROM klientai WHERE vardas = @Vardas AND pavarde = @Pavarde
                AND gimimo_data = @GimimoData";

            var parameters1 = new
            {
                Vardas = klientas.Vardas,
                Pavarde = klientas.Pavarde,
                GimimoData = klientas.GimimoData.ToDateTime(TimeOnly.Parse("00:00:00"))
            };

            int klientoID;

            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            {
                klientoID = dbConnection.
                QueryFirst<int>(sqlCommand, parameters1);
            }

            sqlCommand = @"DELETE FROM uzsakymai WHERE klientas_id = @KlientoID";

            var parameters2 = new
            {
                KlientoID = klientoID,
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters2);
            }
        }
    }
}
