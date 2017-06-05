using ElasticsearchWrapper.Interfaces;
using Nest;
using System;

namespace ElasticsearchWrapper
{
    public class Configuration : IConfiguration
    {
        private static readonly ElasticClient ElasticClient = CreateElasticClient();

        private static ElasticClient CreateElasticClient()
        {
            // Get node and apply any relevant settings for ElasticClient
            var node = new Uri(Constants.ConfigurationConstants.Connection);
            var settings = new ConnectionSettings(node)
                .BasicAuthentication(Constants.ConfigurationConstants.Username, Constants.ConfigurationConstants.Password);

            var elasticClient = new ElasticClient(settings);

            return elasticClient;
        }

        public ElasticClient GetElasticClient()
        {
            return ElasticClient;
        }
    }
}