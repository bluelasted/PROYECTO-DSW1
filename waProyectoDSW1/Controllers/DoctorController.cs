using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using waProyectoDSW1.Models;
using waProyectoDSW1.Interfaces;
using waProyectoDSW1.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace waProyectoDSW1.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctor repository;
        private readonly IEspecialidad repositoryEsp;

        public DoctorController()
        {
            repository = new DoctorRepository();
            repositoryEsp = new EspecialidadRepository();
        }

        public ResultModel<object> Mantenimiento(DoctorModel model, int op)
        {
            ResultModel<object> result = null; 

            if (model.pk_doctor == 0)
            {
                result = repository.Doctor_Mant(model, 1);
            }
            else if (model.pk_doctor > 0 && op != 3)
            {
                result = repository.Doctor_Mant(model, 2);
            }
            else
            {
                result = repository.Doctor_Mant(model, op);
            }

            return result; 
        }

        public IEnumerable<DoctorModel> ListadoDoctor()
        {
            var result = repository.Listado_Doctor();
            return result.Data;
        }

        public PartialViewResult DoctorMant(int pk_doctor)
        {
            DoctorModel model = new DoctorModel();
            ViewBag.Especialidades = new SelectList(repositoryEsp.Listado_Especialidades().Data, "pk_especialidad", "nombre");

            if (pk_doctor > 0)
            {
                var result = repository.Buscar_Doctor(pk_doctor);
                if (result.success && result.Data != null)
                {
                    model = result.Data;
                }
            }

            return PartialView("_DoctorMant", model);
        }

        public IActionResult Index()
        {
            ViewBag.Especialidades = new SelectList(repositoryEsp.Listado_Especialidades().Data, "pk_especialidad", "nombre");
            return View(ListadoDoctor());
        }
    }
}
