using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseConfig.Core.Concrete
{
    public static class ConfigSettings
    {
        public static string ApplicationName { get; set; } = "ServiceA";
        public static string ConnectionString { get; set; } = "mongodb://localhost:27017";
        public static int RefreshTimerIntervalInMs { get; set; } = 5000;
    }
}
