using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ui.Data;
using Ui.Models.Agenda;

namespace Ui.Services.Locais
{
    public class LocaisService : Generic.GenericService<Models.Agenda.Local>, ILocaisService
    {
        public LocaisService(ApplicationDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<Local>> GetAllAsync(object filter)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<IEnumerable<Local>> GetAllByUserIdAsync(string id)
        {
            return await DbSet
                .Where(l => l.UserId == id)
                .ToListAsync();
        }
    }
}
