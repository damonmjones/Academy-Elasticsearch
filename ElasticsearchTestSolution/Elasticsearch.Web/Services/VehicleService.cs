using Elasticsearch.Web.Enums;
using Elasticsearch.Web.Interfaces.Services;
using Elasticsearch.Web.Models;
using ElasticsearchWrapper.Constants;
using ElasticsearchWrapper.Interfaces;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elasticsearch.Web.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IElasticsearchHelper _elasticsearchHelper;

        public VehicleService(IElasticsearchHelper elasticsearchHelper)
        {
            _elasticsearchHelper = elasticsearchHelper;
        }

        public async Task<List<Vehicle>> GetVehicles()
        {
            var vehicles = await _elasticsearchHelper.GetDocuments<Vehicle>(Indexes.motors.ToString());

            return vehicles.ToList();
        }

        public async Task<Vehicle> GetVehicleById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception(ExceptionConstants.IdParameterMissing);
            }

            var vehicle = await _elasticsearchHelper.GetDocumentById<Vehicle>(id, Indexes.motors.ToString());

            return vehicle;
        }

        public async Task<List<Vehicle>> SearchVehicles(string term)
        {
            // Basic search with paging using lambda expression
            //var response = await _elasticsearchHelper.ElasticClient.SearchAsync<Vehicle>(s => s
            //    .Index(Indexes.motors.ToString())
            //    .From(0)
            //    .Size(1)
            //    .Query(q => q
            //        .Match(m => m
            //            .Field(f => f.Make)
            //            .Query(term)
            //        )
            //    )
            //);

            // Basic search with paging using Object Initializer
            //var searchRequest = new SearchRequest<Vehicle>(Indexes.motors.ToString(), Enums.Types.vehicle.ToString())
            //{
            //    From = 0,
            //    Size = 1,
            //    Query = new MatchQuery
            //    {
            //        Field = Infer.Field<Vehicle>(v => v.Make),
            //        Query = term
            //    }
            //};

            // MultiMatch search using lambda expression
            var response = await _elasticsearchHelper.GetElasticClient().SearchAsync<Vehicle>(s => s.Index(Indexes.motors.ToString())
                .Query(q => q
                    .MultiMatch(m => m
                        .Fields(f => f.Field(v => v.Make)
                        .Field(v => v.Model))
                        .Query(term)
                    )
                )
            );

            // MultiMatch search using Object Initializer
            //var searchRequest = new SearchRequest<Vehicle>(Indexes.motors.ToString(), Enums.Types.vehicle.ToString())
            //{
            //    Query = new MultiMatchQuery
            //    {
            //        Fields = Infer.Field<Vehicle>(v => v.Make).And(Infer.Field<Vehicle>(v => v.Model)),
            //        Query = term
            //    }
            //};

            //var response = await _elasticsearchHelper.ElasticClient.SearchAsync<Vehicle>(searchRequest);

            var vehicles = response.Documents;

            return vehicles.ToList();
        }

        public async Task<bool> UpdateVehicle(Vehicle updatedVehicle)
        {
            if (updatedVehicle.Id.Equals(default(Guid)))
            {
                throw new Exception(ExceptionConstants.IdParameterMissing);
            }

            var updateResponse = await _elasticsearchHelper.GetElasticClient().UpdateAsync<Vehicle>(updatedVehicle, u => u
                .Index(Indexes.motors.ToString())
                .Doc(updatedVehicle)
                .RetryOnConflict(1));

            switch (updateResponse.Result)
            {
                case Result.Updated:
                    return true;
                case Result.NotFound:
                    return false;
                case Result.Created:
                    return false;
                case Result.Deleted:
                    return false;
                case Result.Noop:
                    return false;
                default:
                    return false;
            }
        }
    }
}