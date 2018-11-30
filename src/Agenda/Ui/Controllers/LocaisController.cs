using Ui.Models.Agenda;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ui.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LocaisController : GenericController<Local, int?>
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<Models.ApplicationUser> _userManager;
        

        public LocaisController(
            Services.Locais.ILocaisService service,
            Microsoft.AspNetCore.Identity.UserManager<Models.ApplicationUser> userManager
            )
            : base (service)
        {
            _userManager = userManager;
         
        }

        protected override async Task<IEnumerable<Local>> GetAll()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            return await Service.GetAllByUserIdAsync(currentUser.Id);
        }

        public override async Task<IActionResult> Create(Local resource)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            resource.UserId = currentUser.Id;

            return await base.Create(resource);
        }
    }
}
