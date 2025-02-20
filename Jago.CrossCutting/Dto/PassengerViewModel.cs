using System.ComponentModel.DataAnnotations;

namespace Jago.CrossCutting.Dto
{
    public class PassengerViewModel 
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    };//rg and cpf = documentnumber
}
