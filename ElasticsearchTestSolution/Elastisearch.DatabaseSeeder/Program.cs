using Elasticsearch.Web.Enums;
using Elasticsearch.Web.Models;
using ElasticsearchWrapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elasticsearch.DatabaseSeeder
{
    class Program
    {
        static void Main(string[] args)
        {
            var elasticsearch = new ElasticsearchHelper();

            var vehicles = new List<Vehicle>
            {
                new Vehicle
                {
                    Id = Guid.NewGuid(),
                    Model = "A3",
                    Make = "Audi",
                    YearRegistered = DateTime.Now
                },
                new Vehicle
                {
                    Id = Guid.NewGuid(),
                    Model = "Golf GT TDI",
                    Make = "Volkswagen",
                    YearRegistered = DateTime.Now
                },
                new Vehicle
                {
                    Id = Guid.NewGuid(),
                    Model = "M3",
                    Make = "BMW",
                    YearRegistered = DateTime.Now
                },
                new Vehicle
                {
                    Id = Guid.NewGuid(),
                    Model = "Focus",
                    Make = "Ford",
                    YearRegistered = DateTime.Now
                },
                new Vehicle
                {
                    Id = Guid.NewGuid(),
                    Model = "Mustang Gt",
                    Make = "Ford",
                    YearRegistered = DateTime.Now
                },
                new Vehicle
                {
                    Id = Guid.NewGuid(),
                    Model = "Corsa VXR",
                    Make = "Vauxhall",
                    YearRegistered = DateTime.Now
                },
                new Vehicle
                {
                    Id = Guid.NewGuid(),
                    Model = "Spider",
                    Make = "Ferrari",
                    YearRegistered = DateTime.Now
                },
                new Vehicle
                {
                    Id = Guid.NewGuid(),
                    Model = "Tuscan",
                    Make = "TVR",
                    YearRegistered = DateTime.Now
                },
                new Vehicle
                {
                    Id = Guid.NewGuid(),
                    Model = "Enzo",
                    Make = "Lamborghini",
                    YearRegistered = DateTime.Now
                }
            };

            Task.Run(async () =>
            {
                await elasticsearch.BulkInsertDocuments(vehicles, Indexes.motors.ToString());

            }).GetAwaiter().GetResult();
        }
    }
}