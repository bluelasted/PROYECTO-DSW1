using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Rotativa.AspNetCore;
using waProyectoDSW1.Helpers;
using waProyectoDSW1.Interfaces;
using waProyectoDSW1.Models;
using waProyectoDSW1.Repositories;

namespace waProyectoDSW1.Controllers
{
    public class CitaController : Controller
    {
        private readonly ICita repository;
        private readonly IDoctor repositoryDoc;
        private readonly IPaciente repositoryPac;
        private readonly IEstadoCita repositoryEst;
        private readonly IServicioDental repositorySer;

        private UsuarioModel usuario => SessionHelper.GetUser(HttpContext.Session);

        public CitaController()
        {
            repository = new CitaRepository();
            repositoryDoc = new DoctorRepository();
            repositoryPac = new PacienteRepository();
            repositoryEst = new EstadoCitaRepository();
            repositorySer = new ServicioDentalRepository();
        }

        public ResultModel<object> Mantenimiento(CitaModel model, int op)
        {
            ResultModel<object> result = null;

            if (model.pk_cita == 0)
            {
                result = repository.Cita_Mant(model, 1);
            }
            else if (model.pk_cita > 0 && op != 3)
            {
                result = repository.Cita_Mant(model, 2);
            }
            else
            {
                result = repository.Cita_Mant(model, op);
            }

            return result;
        }

        public IEnumerable<CitaModel> ListadoCita()
        {
            IEnumerable<CitaModel> citas;

            if (usuario.fk_doctor != null && usuario.fk_doctor > 0)
            {
                citas = repository.Listado_Cita(usuario.fk_doctor.Value).Data;
            }
            else
            {
                citas = repository.Listado_Cita().Data;
            }

            return citas;
        }

        public IActionResult Calendario()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CitasCalendario()
        {
            IEnumerable<CitaModel> citas;

            if (usuario.fk_doctor != null && usuario.fk_doctor > 0)
            {
                citas = repository.Listado_Cita(usuario.fk_doctor.Value).Data;
            } else
            {
                citas = repository.Listado_Cita().Data;
            }

            var eventos = citas.Select(c => new {
                title = $"{c.nombrePaciente}\n{c.nombreServicio}\nDoctor: {c.nombreDoctor}", 
                start = c.fechaCita.ToString("yyyy-MM-dd") + "T" + c.horaInicio.ToString(@"hh\:mm"),
                end = c.fechaCita.ToString("yyyy-MM-dd") + "T" + c.horaFin.ToString(@"hh\:mm"),
                description = $"Estado: {c.nombreEstado}\nHora: {c.horaInicio.ToString(@"hh\:mm")} - {c.horaFin.ToString(@"hh\:mm")}"  
            });

            return Json(eventos);
        }

        public PartialViewResult CitaMant(int pk_cita)
        {
            CitaModel model = new CitaModel();
            ViewBag.Doctores = new SelectList(repositoryDoc.Listado_Doctor().Data, "pk_doctor", "nombres");
            ViewBag.Pacientes = new SelectList(repositoryPac.Listado_Paciente().Data, "pk_paciente", "nombres");
            ViewBag.Servicios = new SelectList(repositorySer.Listado_Servicios().Data, "pk_servicio", "nombre");
            ViewBag.EstadosCita = new SelectList(repositoryEst.Listado_EstadoCita().Data, "pk_estadoCita", "nombre");

            if (pk_cita > 0)
            {
                var result = repository.Buscar_Cita(pk_cita);
                if (result.success && result.Data != null)
                {
                    model = result.Data;
                }
            }

            return PartialView("_CitaMant", model);
        }

        public IActionResult GenerarPDF(int pk_cita)
        {
            DateTime fechaHoy = DateTime.Now;

            var model = repository.Buscar_Cita(pk_cita).Data;

            return new ViewAsPdf("GenerarPDF", model)
            {
                FileName = $"cita-{fechaHoy}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }

        public IActionResult Index()
        {
            return View(ListadoCita());
        }
    }
}
