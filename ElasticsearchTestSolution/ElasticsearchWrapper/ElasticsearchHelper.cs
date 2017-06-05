using Elasticsearch.Net;
using ElasticsearchWrapper.Constants;
using ElasticsearchWrapper.Interfaces;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticsearchWrapper
{
    public class ElasticsearchHelper : IElasticsearchHelper
    {
        private static readonly Configuration Configuration = new Configuration();
        private static readonly ElasticClient ElasticClient = Configuration.GetElasticClient();

        public ElasticClient GetElasticClient()
        {
            return ElasticClient;
        }

        public async Task CreateIndex(string indexName)
        {
            if (string.IsNullOrEmpty(indexName))
            {
                throw new Exception(ExceptionConstants.IndexIsNullOrEmpty);
            }

            // Attempt to create new index and check response
            var response = await ElasticClient.CreateIndexAsync(indexName);

            if (!response.IsValid)
            {
                throw new ElasticsearchClientException(string.Format(ExceptionConstants.ElasticClientError,
                    response.ServerError.Status, response.ServerError.Error));
            }
        }

        public async Task InsertDocument<T>(T doc, string indexName) where T : class
        {
            if (string.IsNullOrEmpty(indexName))
            {
                throw new Exception(ExceptionConstants.IndexIsNullOrEmpty);
            }

            if (doc == null)
            {
                throw new Exception(ExceptionConstants.DocumentIsNull);
            }

            // Attempt to create new document and check response
            var response = await ElasticClient.IndexAsync(doc, i => i.Index(indexName));

            if (!response.IsValid)
            {
                throw new ElasticsearchClientException(string.Format(ExceptionConstants.ElasticClientError,
                    response.ServerError.Status, response.ServerError.Error));
            }
        }

        public async Task BulkInsertDocuments<T>(IList<T> docs, string indexName) where T : class
        {
            if (string.IsNullOrEmpty(indexName))
            {
                throw new Exception(ExceptionConstants.IndexIsNullOrEmpty);
            }

            if (!docs.Any())
            {
                throw new Exception(ExceptionConstants.DocumentsListIsEmpty);
            }

            // Attempt to create multiple new documents and check response
            var response = await ElasticClient.IndexManyAsync(docs, indexName);

            if (!response.IsValid)
            {
                throw new ElasticsearchClientException(string.Format(ExceptionConstants.ElasticClientError,
                    response.ServerError.Status, response.ServerError.Error));
            }
        }

        public async Task<IReadOnlyCollection<T>> GetDocuments<T>(string indexName) where T : class
        {
            if (string.IsNullOrEmpty(indexName))
            {
                throw new Exception(ExceptionConstants.IndexIsNullOrEmpty);
            }

            // Attempt to retrieve all document and check response
            var response = await ElasticClient.SearchAsync<T>(i => i.Index(indexName));

            if (!response.IsValid)
            {
                throw new ElasticsearchClientException(string.Format(ExceptionConstants.ElasticClientError,
                    response.ServerError.Status, response.ServerError.Error));
            }

            var data = response.Documents;

            return data;
        }

        public async Task<T> GetDocumentById<T>(string id, string indexName) where T : class
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception(ExceptionConstants.IdParameterMissing);
            }

            if (string.IsNullOrEmpty(indexName))
            {
                throw new Exception(ExceptionConstants.IndexIsNullOrEmpty);
            }

            // Attempt to get document by provided id and check response
            var response = await ElasticClient.GetAsync<T>(id, i => i.Index(indexName));

            if (!response.IsValid)
            {
                throw new ElasticsearchClientException(string.Format(ExceptionConstants.ElasticClientError,
                    response.ServerError.Status, response.ServerError.Error));
            }

            var data = response.Source;

            return data;
        }

        public async Task DeleteDocument<T>(string id, string indexName) where T : class
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception(ExceptionConstants.IdParameterMissing);
            }

            if (string.IsNullOrEmpty(indexName))
            {
                throw new Exception(ExceptionConstants.IndexIsNullOrEmpty);
            }

            // Attempt to delete a document by provided id and check response
            var response = await ElasticClient.DeleteAsync<T>(id, i => i.Index(indexName));

            if (!response.Found)
            {
                throw new ElasticsearchClientException(ExceptionConstants.DocumentNotFound);
            }

            if (!response.IsValid)
            {
                throw new ElasticsearchClientException(string.Format(ExceptionConstants.ElasticClientError,
                    response.ServerError.Status, response.ServerError.Error));
            }
        }

        public async Task BulkDeleteDocuments<T>(IList<T> docs, string indexName) where T : class
        {
            if (string.IsNullOrEmpty(indexName))
            {
                throw new Exception(ExceptionConstants.IndexIsNullOrEmpty);
            }

            if (!docs.Any())
            {
                throw new Exception(ExceptionConstants.DocumentsListIsEmpty);
            }

            // Attempt to delete multiple documents and check response
            var response = await ElasticClient.DeleteManyAsync(docs, indexName);

            if (!response.IsValid)
            {
                throw new ElasticsearchClientException(string.Format(ExceptionConstants.ElasticClientError,
                    response.ServerError.Status, response.ServerError.Error));
            }
        }
    }
}