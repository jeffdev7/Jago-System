using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jago.domain.Core.Entities
{
    public class Passenger : BaseEntity
    {
        public string Name { get; set; }

        public string DocumentNumber { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
        protected Passenger() { }
        public static Passenger Create(string name, string documentNumber, string phone, string email)
            => new()
            {
                Name = name,
                DocumentNumber = documentNumber,
                Phone = phone,
                Email = email
            };
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            builder.Property(j => j.Name).IsRequired();
            builder.Property(j => j.DocumentNumber).IsRequired();
            builder.Property(j => j.Phone).IsRequired();
            builder.Property(j => j.Email).IsRequired();
        }
    }
}
