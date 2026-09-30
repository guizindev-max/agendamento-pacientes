using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agendamento.Models
{
    public class Paciente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do paciente.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o CPF.")]
        [Display(Name = "CPF")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 dígitos, sem pontos ou traço.")]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Digite apenas os 11 números do CPF.")]
        public string Cpf { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o telefone.")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "O telefone deve ter 10 ou 11 dígitos, incluindo o DDD.")]
        [RegularExpression(@"^[0-9]{10,11}$", ErrorMessage = "Digite o DDD e o telefone, somente números.")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o endereço.")]
        [Display(Name = "Endereço")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "O endereço deve ter entre 5 e 200 caracteres.")]
        public string Endereco { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a data de nascimento.")]
        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime? DataNascimento { get; set; }
    }
}
