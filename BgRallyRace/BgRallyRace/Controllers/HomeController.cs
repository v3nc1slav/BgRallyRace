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

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOpinionsServices opinions;
        public readonly ITeamServices team;
        public readonly RoleManager<IdentityRole> roles;
        private readonly UserManager<IdentityUser> user;

        public HomeController(ILogger<HomeController> logger, ITeamServices teamServices,
            IOpinionsServices opinionsServices, RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            this.team = teamServices;
            this.opinions = opinionsServices;
            this.roles = roleManager;
            this.user = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Opinion(int page = 1)
        {
            var viewModel = new OpinionsViewModels
            {
                Opinions = opinions.GetOpinions(page),
                CurrentPage = page,
                Total = opinions.Total(),
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
        public IActionResult Contact(string content)
        {
            opinions.AddOpinionAsync(content, User.Identity.Name);
            return this.RedirectToAction("Opinion", "Home");
        }

        [Authorize]
        public async Task<IActionResult> CreatAdmin()
        {

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
        public IActionResult DeleteOpinion(int id)
        {
            opinions.DeleteOpinion(id);
            return this.RedirectToAction("Opinion", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
