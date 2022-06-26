using Autofac;
using CaseConfig.Core.DataAccess.Abstract;
using CaseConfig.Core.IoC;
using CaseConfig.Core.Models.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseConfig.Core.DataAccess.Concrete.MongoDB
{
    public class ConfigurationContext : IConfigurationContext
    {
        private readonly IMongoDatabase _db;

        public ConfigurationContext()
        {
            MongoDbConfig config = DependencyService.Instance.CurrentResolver.Resolve<MongoDbConfig>();
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }


        public IMongoCollection<Configuration> Configurations => _db.GetCollection<Configuration>("Configurations");
    }
}
