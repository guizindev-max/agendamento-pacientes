using Agendamento.Models;
using Agendamento.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.Controllers
{
    public class PacienteController : Controller
    {
        private readonly PacienteService _pacienteService;

        public PacienteController(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        public IActionResult Index()
        {
            var listaPacientes = _pacienteService.Listar();
            return View(listaPacientes);
        }

        [HttpGet]
        public IActionResult Inserir()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Inserir([Bind("Nome,Cpf,Telefone,Endereco,DataNascimento")] Paciente paciente)
        {
            ValidarDataNascimento(paciente);

            if (!ModelState.IsValid)
            {
                return View(paciente);
            }

            _pacienteService.Inserir(paciente);
            return RedirectToAction(nameof(Index));
        }

        private void ValidarDataNascimento(Paciente paciente)
        {
            if (paciente.DataNascimento.HasValue &&
                (paciente.DataNascimento.Value.Date > DateTime.Today ||
                 paciente.DataNascimento.Value.Date < new DateTime(1900, 1, 1)))
            {
                ModelState.AddModelError(nameof(Paciente.DataNascimento),
                    "A data de nascimento deve estar entre 01/01/1900 e hoje.");
            }
        }
    }
}
