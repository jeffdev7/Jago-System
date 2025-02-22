using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jago.domain.Core.Entities
{
    public class Passenger : BaseEntity
    {
        public string Name { get; set; }

        public string RG { get; set; }

        public string CPF { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }
        protected Passenger() { }
        public static Passenger Create(string name, string rg, string cpf, string phone, string email)
            => new()
            {
                Name = name,
                RG = rg,
                CPF = cpf,
                Phone = phone,
                Email = email
            };
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            builder.Property(j => j.Name).IsRequired();
            builder.Property(j => j.RG).IsRequired();
            builder.Property(j => j.Phone).IsRequired();
            builder.Property(j => j.Email).IsRequired();
        }
    }
}
