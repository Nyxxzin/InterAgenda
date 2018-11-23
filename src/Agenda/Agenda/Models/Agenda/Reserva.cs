namespace Agenda.Models.Agenda
{
    public class Reserva
    {
        public int Id { get; set; }
        public System.DateTime Data { get; set; }

        public int LocalId { get; set; }
        public Local Local { get; set; }

        public string UserId { get; set; }
    }
}
