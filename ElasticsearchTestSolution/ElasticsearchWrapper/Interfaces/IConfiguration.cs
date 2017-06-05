using Nest;

namespace ElasticsearchWrapper.Interfaces
{
    public interface IConfiguration
    {
        ElasticClient GetElasticClient();
    }
}
