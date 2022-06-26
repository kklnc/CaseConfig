using Autofac;
using CaseConfig.Core.DataAccess.Abstract;
using CaseConfig.Core.IoC;
using CaseConfig.Core.Models.Entities;
using CaseConfig.Core.Results;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CaseConfig.Core.DataAccess.Concrete.MongoDB
{
    public class MongoConfigurationRepository : IConfigurationRepository
    {
        private readonly IConfigurationContext _context;

        public MongoConfigurationRepository()
        {
            _context = DependencyService.Instance.CurrentResolver.Resolve<IConfigurationContext>();
        }


        public async Task<IDataResult<IEnumerable<Configuration>>> GetAll()
        {
            //return await _context.Configurations
            //    .Find(_ => true)
            //    .ToListAsync();

            var result =await _context.Configurations.Find(_ => true).ToListAsync();

            
            return new SuccessDataResult<IEnumerable<Configuration>>(result,"Listed");
        }

        public async Task<IDataResult<Configuration>> GetOne(string id)
        {
            FilterDefinition<Configuration> filter = Builders<Configuration>.Filter.Eq(m => m.Id, id);
            var result=await _context
                .Configurations
                .Find(filter)
                .FirstOrDefaultAsync();
            return new SuccessDataResult<Configuration>(result, "Ok");
        }

        public async Task<IResult> Create(Configuration model)
        {
            await _context.Configurations.InsertOneAsync(model);
            return new SuccessResult("Created");
        }

        public async Task<IDataResult<bool>> Update(Configuration model)
        {
            ReplaceOneResult updateResult =
                await _context
                    .Configurations
                    .ReplaceOneAsync(
                        filter: g => g.Id == model.Id,
                        replacement: model);
            var result=updateResult.IsAcknowledged
                   && updateResult.ModifiedCount > 0;
            return new SuccessDataResult<bool>(result,"Updated");
        }

        public async Task<IDataResult<bool>> Delete(string id)
        {
            FilterDefinition<Configuration> filter = Builders<Configuration>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                .Configurations
                .DeleteOneAsync(filter);
            return new SuccessDataResult<bool>(deleteResult.IsAcknowledged
                   && deleteResult.DeletedCount > 0,"Deleted");
        }

       
    }
}
