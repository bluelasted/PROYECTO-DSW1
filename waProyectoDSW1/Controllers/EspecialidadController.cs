using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using waProyectoDSW1.Models;
using waProyectoDSW1.Interfaces;
using waProyectoDSW1.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace waProyectoDSW1.Controllers
{
    public class EspecialidadController : Controller
    {
        private readonly IEspecialidad repository;

        public EspecialidadController()
        {
            repository = new EspecialidadRepository();
        }

        public ResultModel<object> Mantenimiento(EspecialidadModel model, int op)
        {
            ResultModel<object> result = null;

            if (model.pk_especialidad == 0)
            {
                result = repository.Especialidad_Mant(model, 1);
            }
            else if (model.pk_especialidad > 0 && op != 3)
            {
                result = repository.Especialidad_Mant(model, 2);
            }
            else
            {
                result = repository.Especialidad_Mant(model, op);
            }

            return result;
        }

        public IEnumerable<EspecialidadModel> ListadoEspecialidad()
        {
            var result = repository.Listado_Especialidades();
            return result.Data;
        }

        public PartialViewResult EspecialidadMant(int pk_especialidad)
        {
            EspecialidadModel model = new EspecialidadModel();

            if (pk_especialidad > 0)
            {
                var result = repository.Buscar_Especialidad(pk_especialidad);
                if (result.success && result.Data != null)
                {
                    model = result.Data;
                }
            }

            return PartialView("_EspecialidadMant", model);
        }

        public IActionResult Index()
        {   
            var Especialidad = ListadoEspecialidad();
            return View(ListadoEspecialidad());
        }
    }
}
