using System.ComponentModel.DataAnnotations;

namespace Agendamento.Models.Validations
{
    public class DataNascimentoAttribute : ValidationAttribute
    {
        public DataNascimentoAttribute()
            : base("A data de nascimento deve estar entre 01/01/1900 e hoje.")
        {
        }

        public override bool IsValid(object? value)
        {
            // Required valida a ausência; a regra também vale fora do controller.
            return value == null || value is DateTime data &&
                data.Date >= new DateTime(1900, 1, 1) && data.Date <= DateTime.Today;
        }
    }
}
