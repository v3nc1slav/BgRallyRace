using BgRallyRace.Data;
using BgRallyRace.Services;
using BgRallyRace.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.ViewComponents
{
    [ViewComponent(Name = "Opinions")]
    public class OpinionsViewComponent : ViewComponent
    {
        private readonly IOpinionsServices opinionsServices;

        public OpinionsViewComponent( IOpinionsServices opinionsServices)
        {
            this.opinionsServices = opinionsServices;
        }

        public async Task< IViewComponentResult> InvokeAsync()
        {
            var viewModel = new OpinionsViewModels();
            viewModel.CountNotAuthorization = opinionsServices.GetCountNotAuthorization();
            return View(viewModel);
        }
    }
}
