using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ui.Data;
using Ui.Models.Agenda;

namespace Ui.Controllers
{
    public class ReservasController : GenericController<Reserva, int?>
    {
        private readonly Services.Generic.IGenericService<Reserva> _serviceReserva;
        private readonly Services.Generic.IGenericService<Local> _serviceLocal;
        private readonly Microsoft.AspNetCore.Identity.UserManager<Models.ApplicationUser> _userManager;

        public ReservasController(
            Services.Generic.IGenericService<Reserva> serviceReserva,
            Services.Generic.IGenericService<Local> serviceLocal,
            Microsoft.AspNetCore.Identity.UserManager<Models.ApplicationUser> userManager)
            : base(serviceReserva)
        {
            _serviceReserva = serviceReserva;
            _serviceLocal = serviceLocal;
            _userManager = userManager;
        }
        protected override async System.Threading.Tasks.Task<object> AddViewDataAsync(
            Models.Agenda.Reserva resource = null
            )
        {
            ViewData["LocalId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                await _serviceLocal.GetAllAsync(),
                nameof(Models.Agenda.Local.Id),
                nameof(Models.Agenda.Local.Nome),
                resource?.LocalId);

            return base.AddViewDataAsync(resource);
        }

        public override async Task<IActionResult> Create(Reserva resource)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            resource.UserId = currentUser.Id;

            return await base.Create(resource);
        }

        protected override async Task<IEnumerable<Reserva>> GetAll()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var id = currentUser.Id;
            // se admin não passar o id
            var isAdmin = currentUser != null && await _userManager.IsInRoleAsync(currentUser, Constants.AdministratorRole);
            if (isAdmin)
                id = string.Empty;

            return await Service.GetAllByUserIdAsync(id);
        }
    }
}
