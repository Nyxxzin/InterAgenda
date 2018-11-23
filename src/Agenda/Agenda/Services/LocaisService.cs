using Agenda.Data;
using Agenda.Models.Agenda;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.Services
{
    public class LocaisService : Generic.GenericService<Models.Agenda.Local>
    {
        public LocaisService(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Local>> GetAllByUserIdAsync(string id)
        {
            return await DbSet.Where(l => l.UserId == id)
                .ToListAsync();
        }
    }
}
