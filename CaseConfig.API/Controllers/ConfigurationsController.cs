using Autofac;
using CaseConfig.Core.DataAccess.Abstract;
using CaseConfig.Core.IoC;
using CaseConfig.Core.Models.Entities;
using CaseConfig.Core.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseConfig.API.Controllers
{



    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ConfigurationsController : Controller
    {
        private readonly IContainer _container;
        private readonly IConfigurationRepository _configurationRepository;

        public ConfigurationsController()
        {
            _container = DependencyService.Instance.CurrentResolver;
            _configurationRepository = _container.Resolve<IConfigurationRepository>();
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> List(string searchModel)
        {
            var list = await _configurationRepository.GetAll();
            if (!string.IsNullOrEmpty(searchModel))
            {
                list.Data = list.Data.Where(v => v.Name.ToLower().Contains(searchModel.ToLower()));
            }

            var result = list;

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Configuration config)
        {
            var result = await _configurationRepository.Create(config);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Configuration config)
        {
            var result = await _configurationRepository.Update(config);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _configurationRepository.Delete(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
