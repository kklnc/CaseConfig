using Autofac;
using CaseConfig.Core.Abstract;
using CaseConfig.Core.DataAccess.Abstract;
using CaseConfig.Core.IoC;
using CaseConfig.Core.Models.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CaseConfig.Core.Concrete
{
    public class ConfigurationReader : IConfigurationReader
    {
        private ConcurrentBag<Configuration> _confList;

        private readonly IConfigurationRepository _configurationRepository;

        private readonly int _refreshTimerInterval;
        private readonly string _applicationName;
        private readonly string _connectionString;


        public CancellationToken CancellationToken { get; set; }

        public ConfigurationReader(string applicationName, string connectionString, int refreshTimerIntervalInMs)
        {
            _configurationRepository = DependencyService.Instance.CurrentResolver.Resolve<IConfigurationRepository>();
            _confList = new ConcurrentBag<Configuration>();
            _refreshTimerInterval = refreshTimerIntervalInMs;
            _applicationName = applicationName;
            _connectionString = connectionString;

            Task.Run(() => this.CheckDatas()).Wait(); // first call for config datas
            Task.Run(() => this.StartTimer(CancellationToken));
        }

        public async Task CheckDatas()
        {
            var list = await _configurationRepository.GetAll();
            List<Configuration> conflist = list.Data.Where(v => v.ApplicationName == _applicationName && v.IsActive == true).ToList();
            _confList.Clear();
            foreach (Configuration configuration in list.Data)
            {
                _confList.Add(configuration);
            }
        }

        private async Task StartTimer(CancellationToken cancellationToken)
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    await CheckDatas();
                    await Task.Delay(_refreshTimerInterval, cancellationToken);
                    if (cancellationToken.IsCancellationRequested)
                        break;
                }
            }, cancellationToken);
        }

        public T GetValue<T>(string key)
        {
            Configuration configuration = _confList.FirstOrDefault(v => v.Name == key);
            if (configuration == null)
                throw new ArgumentNullException("There is no value for this key");

            Type confType = TypeMap(configuration.Type);

            object value = Convert.ChangeType(configuration.Value, confType);
            return (T)value;
        }

        public Type TypeMap(string type)
        {
            return DataTypeMapper.MapperWithKey()[type];
        }
    }
}
