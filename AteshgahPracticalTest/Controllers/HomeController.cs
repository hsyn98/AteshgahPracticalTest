using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LoanPracticalTest.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LoanPracticalTest.Models;
using Microsoft.Extensions.Hosting;

namespace LoanPracticalTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHostApplicationLifetime _lifetime;

        public HomeController(ILogger<HomeController> logger, IHostApplicationLifetime lifetime)
        {
            _logger = logger;
            _lifetime = lifetime;
        }
        
        public void ExitApp()
        {
            _lifetime.StopApplication();
        }
    }
}