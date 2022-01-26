using System.ComponentModel.DataAnnotations;

namespace Jago.Application.ViewModel
{
    public class PassengerViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
    }
}
