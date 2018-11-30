using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ui.Data;
using Ui.Models.Agenda;

namespace Ui.Services.Reservas
{
    public class ReservasService : Generic.GenericService<Models.Agenda.Reserva>
    {
        public ReservasService(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Reserva>> GetAllByUserIdAsync(string userId)
        {
            var query = DbSet
                .Where(r => string.IsNullOrWhiteSpace(userId) || r.UserId == userId)
                .Include(p => p.Local);

            return await query.ToListAsync();
        }

       /* public override async Task<IEnumerable<Reserva>> GetAllByUserIdAsync(string nome)
        {
            return await DbSet
                .Where(r => r.UserId == nome)
                .ToListAsync();
        }*/
    }
}
