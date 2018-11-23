namespace Agenda.Data
{
    public partial class ApplicationDbContext
    {
        public Microsoft.EntityFrameworkCore.DbSet<Models.Agenda.Local> Locais { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Models.Agenda.Reserva> Reservas { get; set; }
    }
}
