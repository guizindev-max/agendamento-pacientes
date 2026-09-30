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
            if (!ModelState.IsValid)
            {
                return View(paciente);
            }

            _pacienteService.Inserir(paciente);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var paciente = _pacienteService.BuscarPorId(id);

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar([FromRoute] int id,
            [Bind("Id,Nome,Cpf,Telefone,Endereco,DataNascimento")] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return BadRequest();
            }

            if (_pacienteService.BuscarPorId(id) == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(paciente);
            }

            if (!_pacienteService.Editar(paciente))
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Remover(int id)
        {
            var paciente = _pacienteService.BuscarPorId(id);

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        [HttpPost, ActionName("Remover")]
        [ValidateAntiForgeryToken]
        public IActionResult RemoverConfirmado([FromRoute] int id)
        {
            if (!_pacienteService.Remover(id))
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
