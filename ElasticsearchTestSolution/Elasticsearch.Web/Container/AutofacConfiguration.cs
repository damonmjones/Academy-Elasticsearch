using Autofac;
using Elasticsearch.Web.Interfaces.Services;
using Elasticsearch.Web.Services;
using ElasticsearchWrapper;
using ElasticsearchWrapper.Interfaces;

namespace Elasticsearch.Web.Container
{
    public static class AutofacConfiguration
    {
        private static IContainer _container;
        private static ContainerBuilder _builder;

        public static IContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = Builder.Build();
                }

                return _container;
            }
        }

        public static ContainerBuilder Builder
        {
            get
            {
                if (_builder == null)
                {
                    _builder = new ContainerBuilder();

                    RegisterTypes(_builder);
                }

                return _builder;
            }
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            // Register services, business or data components
            builder.RegisterType<ElasticsearchHelper>().As<IElasticsearchHelper>();
            builder.RegisterType<VehicleService>().As<IVehicleService>();
        }
    }
}