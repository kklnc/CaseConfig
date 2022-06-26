using System;
using Autofac;
using CaseConfig.Core.Abstract;
using CaseConfig.Core.IoC;
using Xunit;

namespace CaseConfig.Test
{
    public class ConfigurationTest
    {
        private IContainer _container;

        [Fact]
        public void Is_Type_Mapper_Work()
        {
            _container = (IContainer)DependencyService.Instance.CurrentResolver;
            var _reader = _container.Resolve<IConfigurationReader>();

            Type stringType = _reader.TypeMap("string");
            Type floatType = _reader.TypeMap("float");

            Assert.Equal(typeof(string), stringType);
            Assert.Equal(typeof(float), floatType);
        }

        [Fact]
        public void IsListWorking()
        {
            _container = DependencyService.Instance.CurrentResolver;
            IConfigurationReader _reader = _container.Resolve<IConfigurationReader>();
            _reader.CheckDatas();
        }
    }
}
