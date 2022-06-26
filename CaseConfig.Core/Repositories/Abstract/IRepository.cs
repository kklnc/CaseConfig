using CaseConfig.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CaseConfig.Core.Repositories.Abstract
{
    public interface IRepository<TModel>
    {
        Task<IDataResult<IEnumerable<TModel>>> GetAll();
        Task<IDataResult<TModel>> GetOne(string id);
        Task<IResult> Create(TModel model);
        Task<IDataResult<bool>> Update(TModel model);
        Task<IDataResult<bool>> Delete(string id);
    }
}
