using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseConfig.Core.DataAccess.Concrete.MongoDB
{
    public class ServerConfig
    {
        public MongoDbConfig MongoDB { get; set; } = new MongoDbConfig();
    }
}
