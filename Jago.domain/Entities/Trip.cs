using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jago.domain.Entities
{
    public class Trip : BaseEntity
    {
        public string Origin { get; set; }
        public string Destine { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public virtual Passenger Passenger { get; set; }
        public Guid PassengerId { get; set; }
        public string UserId { get; set; }

        protected Trip() { }

        public static Trip Create(string origin, string destine, DateTime departure, DateTime arrival, Guid passengerId)
            => new()
            {
                Origin = origin,
                Destine = destine,
                Departure = departure,
                Arrival = arrival,
                PassengerId = passengerId
            };
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.Property(j => j.Origin).IsRequired();
            builder.Property(j => j.Destine).IsRequired();
            builder.Property(j => j.Departure).IsRequired();
        }
    }
}
