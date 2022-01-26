using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Jago.domain.Core.Entities
{
    public class Passenger: IEntityTypeConfiguration<Passenger>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [RegularExpression(@"(^\d{1,2}).?(\d{3}).?(\d{3})-?(\d{1}|X|x$)", ErrorMessage = " RG inválido.")]
        public string RG { get; set; }

        [RegularExpression(@"(^\d{3}\.\d{3}\.\d{3}\-\d{2}$)", ErrorMessage = "CPF inválido.")]
        public string CPF { get; set; }
        public string Celular { get; set; }

        [RegularExpression(@"([a-zA-Z0-9\._]+)@([a-zA-Z0-9])+.([a-z]+)(.[a-z]+)?$", ErrorMessage = "Entre com um email válido")]
        public string Email { get; set; }
        public Passenger() { }
        public Passenger(string name, string rg, string cpf, string celular, string email)
        {
            Name = name;
            RG = rg;
            CPF = cpf;
            Celular = celular;
            Email = email;
        }
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            builder.HasKey(j => j.Id);
            builder.Property(j => j.Id).IsRequired();
            builder.Property(j => j.Name).IsRequired();
            builder.Property(j => j.RG).IsRequired();
            builder.Property(j => j.Celular).IsRequired();
            builder.Property(j => j.Email).IsRequired();
        }
    }
}
