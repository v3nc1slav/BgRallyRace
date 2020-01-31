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
using BgRallyRace.Services;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace BgRallyRace.Controllers
{
    public class HomeController : Controller
    {
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

        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }

        [Authorize]
        public IActionResult Contact()
        {
            var db = new ApplicationDbContext();
            var opinions = new OpinionsServices(db);
            var result = Request.Body.ToString().Split('&').ToList();
            //opinions.AddOpinion(result);
            return RedirectToAction("Opinion", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
