using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jago.domain.Core.Entities
{
    public class Passenger : IEntityTypeConfiguration<Passenger>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        //[RegularExpression(@"(^\d{1,2}).?(\d{3}).?(\d{3})-?(\d{1}|X|x$)", ErrorMessage = " RG inválido.")]
        public string RG { get; set; }

        //[RegularExpression(@"(^\d{3}\.\d{3}\.\d{3}\-\d{2}$)", ErrorMessage = "CPF inválido.")]
        public string CPF { get; set; }
        public string Phone { get; set; }

        //[RegularExpression(@"([a-zA-Z0-9\._]+)@([a-zA-Z0-9])+.([a-z]+)(.[a-z]+)?$", ErrorMessage = "Entre com um email válido")]
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
            builder.HasKey(j => j.Id);
            builder.Property(j => j.Id).IsRequired();
            builder.Property(j => j.Name).IsRequired();
            builder.Property(j => j.RG).IsRequired();
            builder.Property(j => j.Phone).IsRequired();
            builder.Property(j => j.Email).IsRequired();
        }
    }
}
