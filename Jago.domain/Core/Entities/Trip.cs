using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jago.domain.Core.Entities
{
    public class Trip: IEntityTypeConfiguration<Trip>
    {
        public Guid Id { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public virtual Passenger Passenger { get; set; }
        public Guid PassengerId { get; set; }
        public Trip() { }

        public Trip(string origem, string destino, DateTime departure, DateTime arrival, Guid passengerId)
        {
            Origem = origem;
            Destino = destino;
            Departure = departure;
            Arrival = arrival;
            PassengerId = passengerId;
        }
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(j => j.Id);
            builder.Property(j => j.Id).IsRequired();
            builder.Property(j => j.Origem).IsRequired();
            builder.Property(j => j.Destino).IsRequired();
            builder.Property(j => j.Departure).IsRequired();
        }
    }
}
