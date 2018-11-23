using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agenda.Data;
using Agenda.Models.Agenda;

namespace Agenda.Controllers
{
    public class ReservasController : GenericController<Reserva, int?>
    {
        private readonly Services.Generic.IGenericService<Reserva> _serviceReserva;
        private readonly Services.Generic.IGenericService<Local> _serviceLocal;

        public ReservasController(
            Services.Generic.IGenericService<Reserva> serviceReserva,
            Services.Generic.IGenericService<Local> serviceLocal)
            : base (serviceReserva)
        {
            _serviceReserva = serviceReserva;
            _serviceLocal = serviceLocal;
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

    }
}
