using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MiljoBoven.Models; // Pizzakväll

namespace MiljoBoven.Components
{
    public class ShowOneErrand : ViewComponent // ShowOneTeacher Ärver från ViewComponent 
    {
        private IEnvironmentCrimeRepository repository;

        public ShowOneErrand(IEnvironmentCrimeRepository repo)
        {
            repository = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id) // Läggs på kö och kör när det passar
        {
            var errandDetail = await repository.GetErrandDetail(id);
            return View(errandDetail);
        }

    }
}