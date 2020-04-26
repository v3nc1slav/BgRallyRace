namespace BgRallyRace.ViewComponents
{
    using BgRallyRace.Services;
    using BgRallyRace.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

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
            var viewModel = new OpinionsViewModels
            {
                CountNotAuthorization = opinionsServices.GetCountNotAuthorization()
            };
            return View(viewModel);
        }
    }
}
