namespace BgRallyRace.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using BgRallyRace.Models;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.Services;
    using Microsoft.AspNetCore.Http;
    using BgRallyRace.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using BgRallyRace.Services.Competitions;
    using BgRallyRace.Models.Home;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOpinionsServices opinions;
        public readonly ITeamServices team;
        public readonly RoleManager<IdentityRole> roles;
        private readonly UserManager<IdentityUser> user;
        private readonly ICompetitionsServices competitions;

        public HomeController(ILogger<HomeController> logger, ITeamServices teamServices,
            IOpinionsServices opinionsServices, RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager, ICompetitionsServices competitionsServices)
        {
            _logger = logger;
            this.team = teamServices;
            this.opinions = opinionsServices;
            this.roles = roleManager;
            this.user = userManager;
            this.competitions = competitionsServices;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel
            {
                CountNotAuthorization = opinions.GetCountNotAuthorization(),
                StartDate = competitions.GetStartDate().Result,
                Team = team.FindUser(User.Identity.Name)
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> Opinion(int page = 1, string input = null)
        {
            _logger.LogInformation("view opinion");
            competitions.StartRally();
            var viewModel = new OpinionsViewModels
            {
                Opinions = opinions.GetOpinions(page),
                CurrentPage = page,
                Total = opinions.Total(),
                Text = input,
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> FAQ()
        {
            _logger.LogInformation("view FAQ");
            return View();
        }

        public async Task<IActionResult> Gallery()
        {
            _logger.LogInformation("view gallery");
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Opinion(OpinionsViewModels input)
        {
            _logger.LogInformation("add opinion");
            var text = string.Empty;
            if (!this.ModelState.IsValid)
            {
                text = "Неможе да побликувате празно мниние.";
            }
            else
            {
                text = opinions.AddOpinion(input.Opinion, User.Identity.Name);
            }
            return this.RedirectToAction("Opinion", "Home", new {input = text });
        }

        [Authorize]
        public async Task<IActionResult> CreatAdmin()
        {
            _logger.LogInformation("Creat Admin");
            if (User.Identity.Name == "Admin@gmail.com")
            {
                await roles.CreateAsync(new IdentityRole
                {
                    Name = "Admin",
                });

                var userID = await user.GetUserAsync(this.User);

                await user.AddToRoleAsync(userID, "Admin");
                return this.RedirectToAction("Index", "Home");
            }
            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DeleteOpinion(int id)
        {
            _logger.LogInformation("view opinion");
            opinions.DeleteOpinion(id);
            return this.RedirectToAction("Opinion", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
