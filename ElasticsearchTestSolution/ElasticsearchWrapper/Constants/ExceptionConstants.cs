namespace ElasticsearchWrapper.Constants
{
    public class ExceptionConstants
    {
        public const string IndexIsNullOrEmpty = "The provided index name is null or empty";
        public const string DocumentIsNull = "The provided document is null";
        public const string DocumentsListIsEmpty = "The provided documents list is empty";
        public const string IdParameterMissing = "The provided ID parameter is missing";
        public const string ElasticClientError = "Status code {0} : {1}";
        public const string DocumentNotFound = "Document not found";
    }
}
