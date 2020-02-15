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
using Microsoft.AspNetCore.Identity;
using BgRallyRace.ViewModels;
using BgRallyRace.Models.RandomName;

namespace BgRallyRace.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext db { get; set; } = new ApplicationDbContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task <IActionResult> Index()
        {
            MoneyAccountServices money = new MoneyAccountServices(db);
            if (money.FindUserAsync(User.Identity.Name) == null)
            {
                await money.CreateMoneyAccount(User.Identity.Name);
            }

           // var viewModel = new MoneyAccountViewModels
           // {
           //     Balance = money.GetBalanceAsync(User.Identity.Name)
           // };

            return View();
        }

        public IActionResult Opinion()
        {
            var opinion = new OpinionsServices(db);
            var viewModel = new OpinionsViewModels
            {
                Opinions = opinion.GetOpinions()
            };
            return this.View(viewModel);
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
        [HttpPost]
        public async Task<IActionResult> Contact(string content)
        {
            var opinions = new OpinionsServices(db);
            await opinions.AddOpinionAsync(content, User.Identity.Name);
            return RedirectToAction("Opinion", "Home");
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
