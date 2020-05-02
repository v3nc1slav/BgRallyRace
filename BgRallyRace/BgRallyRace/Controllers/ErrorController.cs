namespace BgRallyRace.Controllers
{
    using Microsoft.AspNetCore.Mvc;


    public class ErrorController : Controller
    {
        public IActionResult NotFound()
        {
            Response.StatusCode = 404;

            return View();
        }
    }
}
