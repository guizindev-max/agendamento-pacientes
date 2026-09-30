using Agendamento.Data;
using Agendamento.Models;

namespace Agendamento.Services
{
    public class PacienteService
    {
        private readonly AppDbContext _context;

        public PacienteService(AppDbContext context)
        {
            _context = context;
        }

        public List<Paciente> Listar()
        {
            return _context.Pacientes.OrderBy(paciente => paciente.Nome).ToList();
        }

        public void Inserir(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            _context.SaveChanges();
        }

        public Paciente? BuscarPorId(int id)
        {
            return _context.Pacientes.Find(id);
        }

        public bool Editar(Paciente paciente)
        {
            var pacienteBanco = BuscarPorId(paciente.Id);

            if (pacienteBanco == null)
            {
                return false;
            }

            pacienteBanco.Nome = paciente.Nome;
            pacienteBanco.Cpf = paciente.Cpf;
            pacienteBanco.Telefone = paciente.Telefone;
            pacienteBanco.Endereco = paciente.Endereco;
            pacienteBanco.DataNascimento = paciente.DataNascimento;

            _context.SaveChanges();
            return true;
        }

        public bool Remover(int id)
        {
            var paciente = BuscarPorId(id);

            if (paciente == null)
            {
                return false;
            }

            _context.Pacientes.Remove(paciente);
            _context.SaveChanges();
            return true;
        }
    }
}
