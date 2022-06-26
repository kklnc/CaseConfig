using Autofac;
using CaseConfig.Core.Abstract;
using CaseConfig.Core.Concrete;
using CaseConfig.Core.DataAccess.Abstract;
using CaseConfig.Core.DataAccess.Concrete.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseConfig.Core.IoC
{
    public static class AutofacResolver
    {
        public static IContainer Bind(this ContainerBuilder builder)
        {

            var configParameters = new NamedParameter[]
            {
                new NamedParameter("applicationName", ConfigSettings.ApplicationName),
                new NamedParameter("connectionString", ConfigSettings.ConnectionString),
                new NamedParameter("refreshTimerIntervalInMs", ConfigSettings.RefreshTimerIntervalInMs),

            };

            builder.RegisterType<ConfigurationReader>().As<IConfigurationReader>()
                .WithParameters(configParameters).SingleInstance();

            builder.RegisterType<ConfigurationContext>().As<IConfigurationContext>().SingleInstance();

            builder.RegisterType<MongoDbConfig>();

            var config = new ServerConfig();
            builder.RegisterInstance(config);

            builder.RegisterType<MongoConfigurationRepository>().As<IConfigurationRepository>();

            return builder.Build();
        }


    }
}
