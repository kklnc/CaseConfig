using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseConfig.Core.Abstract
{
    public interface IConfigurationReader
    {
        T GetValue<T>(string key);
        Type TypeMap(string type);
        Task CheckDatas();
    }
}
