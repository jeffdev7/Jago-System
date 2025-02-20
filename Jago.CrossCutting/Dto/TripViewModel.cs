using System.ComponentModel.DataAnnotations;

namespace Jago.CrossCutting.Dto
{
    public class TripViewModel 
    {
        [Key]

        public Guid Id { get; set; }
        public string Origin { get; set; }
        public string Destine { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public Guid PassengerId { get; set; }
        public string PaxName { get; internal set; }
    } 
}
