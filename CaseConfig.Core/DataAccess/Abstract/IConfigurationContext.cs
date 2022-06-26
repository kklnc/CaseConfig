using CaseConfig.Core.Models.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseConfig.Core.DataAccess.Abstract
{
    public interface IConfigurationContext
    {
        IMongoCollection<Configuration> Configurations { get; }
    }
}
