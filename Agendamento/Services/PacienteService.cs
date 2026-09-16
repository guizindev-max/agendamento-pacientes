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
    }
}
