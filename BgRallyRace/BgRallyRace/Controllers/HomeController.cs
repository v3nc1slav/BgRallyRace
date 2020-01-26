using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BgRallyRace.Models;
using Microsoft.AspNetCore.Authorization;
using BgRallyRace.Data;
using BgRallyRace.Models.Home;

namespace BgRallyRace.Controllers
{
    public class HomeController : Controller
    {
     //private readonly ApplicationDbContext dbContext;
     //public HomeController(ApplicationDbContext dbContext)
     //{
     //    this.dbContext = dbContext;
     //}
      
         private readonly ILogger<HomeController> _logger;
        
         public HomeController(ILogger<HomeController> logger)
         {
             _logger = logger;
         }
   
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Opinion()
        {
            return View();
           
        }

        //[Authorize]

        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }
     
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
