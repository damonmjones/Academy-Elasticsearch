using Elasticsearch.CVLMigrator.Domain;
using ElasticsearchWrapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Elasticsearch.CVLMigrator
{
    class Program
    {
        public static List<Vehicle> Vehicles = new List<Vehicle>();
        public static ElasticsearchHelper ElasticsearchHelper = new ElasticsearchHelper();

        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();

            using (var conn = new SqlConnection())
            {
                Console.WriteLine("Gathering data...");
                stopwatch.Start();

                conn.ConnectionString = "Server=ANS-A358;Database=CVL;Trusted_Connection=True;";
                conn.Open();

                var command = new SqlCommand("SELECT veh_id, veh_reg, veh_desc FROM dbo.vehicles", conn);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vehicle = new Vehicle
                        {
                            Id = (int)reader[0],
                            RegistrationNumber = (string)reader[1],
                            Description = (string)reader[2]
                        };

                        Vehicle
                            s.Add(vehicle);
                    }
                }

                conn.Close();

                stopwatch.Stop();
                Console.WriteLine($"Data gathered, time elaspsed {stopwatch.ElapsedMilliseconds} milliseconds");
                stopwatch.Reset();
            }

            stopwatch.Start();
            Console.WriteLine($"Elasticsearch migration in progress");

            Task.Run(async () =>
            {
                await ElasticsearchHelper.BulkInsertDocuments(Vehicles, "dummy_vehicle");

            }).GetAwaiter().GetResult();

            stopwatch.Stop();
            Console.WriteLine($"Migration complete, time elaspsed {stopwatch.ElapsedMilliseconds} milliseconds");
            Console.WriteLine("Press enter to close the application");
            Console.ReadLine();
        }
    }
}
