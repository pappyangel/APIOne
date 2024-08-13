using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace frontend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;
        public  string envEnvironment  { get; set; }
        public string envDeploymentDate { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public void OnGet()
        {
            envEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            envDeploymentDate = _config["DeploymentDate"];
            if (envEnvironment == "Development")
            {
                var localNow = DateTimeOffset.Now;
                var usEST = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                envDeploymentDate = TimeZoneInfo.ConvertTime(localNow, usEST).ToString("MM/dd/yyyy hh:mm tt");
                
            }
        }
    }
}
