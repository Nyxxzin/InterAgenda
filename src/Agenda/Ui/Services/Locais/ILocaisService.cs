namespace Ui.Services.Locais
{
    public interface ILocaisService : Services.Generic.IGenericService<Models.Agenda.Local>
    {
        System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Models.Agenda.Local>> GetAllAsync(object filter);

    }
}
