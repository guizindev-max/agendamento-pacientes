using Agendamento.Models;

namespace Agendamento.Data
{
    public class SeedingService
    {
        private readonly AppDbContext _context;

        public SeedingService(AppDbContext context)
        {
            _context = context;
        }

        public void Popula()
        {
            // Cada tabela é verificada separadamente.
            if (!_context.Medicos.Any())
            {
                Medico medico1 = new Medico
                {
                    Nome = "Luiza",
                    Crm = "123456",
                    Especialidade = "Vascular"
                };

                Medico medico2 = new Medico
                {
                    Nome = "João",
                    Crm = "456789",
                    Especialidade = "Ortopedista"
                };

                _context.Medicos.AddRange(medico1, medico2);
            }

            if (!_context.Pacientes.Any())
            {
                // Dados fictícios para demonstrar as telas do trabalho.
                Paciente paciente1 = new Paciente
                {
                    Nome = "Ana Exemplo",
                    Cpf = "01234567890",
                    Telefone = "11900000001",
                    Endereco = "Rua de Exemplo, 100",
                    DataNascimento = new DateTime(1995, 5, 12)
                };

                Paciente paciente2 = new Paciente
                {
                    Nome = "Carlos Exemplo",
                    Cpf = "12345678909",
                    Telefone = "11900000002",
                    Endereco = "Rua de Exemplo, 200",
                    DataNascimento = new DateTime(1988, 10, 3)
                };

                Paciente paciente3 = new Paciente
                {
                    Nome = "Marina Exemplo",
                    Cpf = "98765432100",
                    Telefone = "11900000003",
                    Endereco = "Rua de Exemplo, 300",
                    DataNascimento = new DateTime(2001, 2, 20)
                };

                _context.Pacientes.AddRange(paciente1, paciente2, paciente3);
            }

            _context.SaveChanges();
        }
    }
}
