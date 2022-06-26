using CaseConfig.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CaseConfig.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var configurations = new DataResult<List<Configuration>>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44391/api/Configurations/List"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    configurations = JsonConvert.DeserializeObject<DataResult<List<Configuration>>>(apiResponse);
                }
            }
            return View(configurations.Data);
        }

        public async Task<IActionResult> EditAdd(string id)
        {
            var configuration = new Configuration();
            if (!string.IsNullOrEmpty(id))
            {
                var configurations = new DataResult<List<Configuration>>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:44391/api/Configurations/List"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        configurations = JsonConvert.DeserializeObject<DataResult<List<Configuration>>>(apiResponse);
                    }
                }
                if (configurations.Data != null && configurations.Data.Count > 0)
                {
                    configuration = configurations.Data.FirstOrDefault(x => x.Id == id);
                }
            }
            return View(configuration);
        }

        [HttpPost]
        public async Task<IActionResult> EditAdd(Configuration configuration)
        {
            string url = "https://localhost:44391/api/Configurations/";
            if (string.IsNullOrWhiteSpace(configuration.Id))
            {
                url += "Create";
            }
            else
            {
                url += "Update";
            }
            using (var httpClient = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(configuration);
                await httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
            }
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
