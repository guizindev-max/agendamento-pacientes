using System.ComponentModel.DataAnnotations;

namespace Agendamento.Models.Validations
{
    // Valida o formato e os dígitos verificadores; não consulta a situação cadastral.
    public class CpfAttribute : ValidationAttribute
    {
        public CpfAttribute() : base("Informe um CPF válido.")
        {
        }

        public override bool IsValid(object? value)
        {
            // A obrigatoriedade é tratada pelo atributo Required.
            if (value == null || value is string { Length: 0 })
            {
                return true;
            }

            if (value is not string cpf || cpf.Length != 11 ||
                cpf.Any(c => c < '0' || c > '9') || cpf.All(c => c == cpf[0]))
            {
                return false;
            }

            for (int tamanho = 9; tamanho <= 10; tamanho++)
            {
                int soma = 0;
                for (int i = 0; i < tamanho; i++)
                {
                    soma += (cpf[i] - '0') * (tamanho + 1 - i);
                }

                int resto = soma % 11;
                int digito = resto < 2 ? 0 : 11 - resto;
                if (cpf[tamanho] - '0' != digito)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
