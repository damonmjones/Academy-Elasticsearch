using Nest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticsearchWrapper.Interfaces
{
    public interface IElasticsearchHelper
    {
        ElasticClient GetElasticClient();
        Task CreateIndex(string indexName);
        Task InsertDocument<T>(T doc, string indexName) where T : class;
        Task BulkInsertDocuments<T>(IList<T> docs, string indexName) where T : class;
        Task<IReadOnlyCollection<T>> GetDocuments<T>(string indexName) where T : class;
        Task<T> GetDocumentById<T>(string id, string indexName) where T : class;
        Task DeleteDocument<T>(string id, string indexName) where T : class;
        Task BulkDeleteDocuments<T>(IList<T> docs, string indexName) where T : class;
    }
}