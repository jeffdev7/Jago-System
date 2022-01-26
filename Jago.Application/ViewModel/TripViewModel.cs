using System.ComponentModel.DataAnnotations;

namespace Jago.Application.ViewModel
{
    public class TripViewModel
    {
        [Key]

        public Guid Id { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public Guid PassageiroId { get; set; }
        public string PaxName { get; internal set; }
    }
}
