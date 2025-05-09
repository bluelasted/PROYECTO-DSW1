using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using waProyectoDSW1.Interfaces;
using waProyectoDSW1.Models;
using waProyectoDSW1.Repositories;

namespace waProyectoDSW1.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IPaciente repository;

        public PacienteController()
        {
            repository = new PacienteRepository();
        }

        public ResultModel<object> Mantenimiento(PacienteModel model, int op)
        {
            ResultModel<object> result = null;

            if (model.pk_paciente == 0)
            {
                result = repository.Paciente_Mant(model, 1);
            }
            else if (model.pk_paciente > 0 && op != 3)
            {
                result = repository.Paciente_Mant(model, 2);
            }
            else
            {
                result = repository.Paciente_Mant(model, op);
            }

            return result;
        }

        public IEnumerable<PacienteModel> ListadoPaciente()
        {
            var result = repository.Listado_Paciente();
            return result.Data;
        }

        public PartialViewResult PacienteMant(int pk_paciente)
        {
            PacienteModel model = new PacienteModel();

            if (pk_paciente > 0)
            {
                var result = repository.Buscar_Paciente(pk_paciente);
                if (result.success && result.Data != null)
                {
                    model = result.Data;
                }
            }

            return PartialView("_PacienteMant", model);
        }

        public IActionResult Index()
        {
            return View(ListadoPaciente());
        }
    }
}
