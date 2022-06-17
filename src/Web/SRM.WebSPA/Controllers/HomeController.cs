using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using SRM.WebSPA.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace SRM.WebSPA.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptionsSnapshot<AppSettings> _settings;
        private readonly IHostEnvironment env;

        public HomeController(IOptionsSnapshot<AppSettings> settings, IHostEnvironment env)
        {
            _settings = settings;
            this.env = env;
        }

        public async Task<IActionResult> Configuration()
        {
            try
            {
                if (env.IsDevelopment())
                {
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync($"{_settings.Value.ApplicationUrl}/api/Authentication/GetApplicationSettings");
                    response.EnsureSuccessStatusCode();
                    // Deserialize the updated product from the response body.
                    var configJson = await response.Content.ReadAsStringAsync();
                    dynamic settings = JObject.Parse(configJson);
                    var result = settings.SelectToken("resultValue");
                    _settings.Value.ApplicationName += " " + result.SelectToken("applicationName");
                    return Json(_settings.Value);
                }
                else
                {
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync($"{_settings.Value.ApplicationUrl}/api/Authentication/GetApplicationSettings");
                    response.EnsureSuccessStatusCode();
                    // Deserialize the updated product from the response body.
                    var configJson = await response.Content.ReadAsStringAsync();
                    dynamic settings = JObject.Parse(configJson);
                    return Json(settings.resultValue);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Json(_settings.Value);

            }
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
