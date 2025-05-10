using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using waProyectoDSW1.Filters;
using waProyectoDSW1.Helpers;
using waProyectoDSW1.Interfaces;
using waProyectoDSW1.Models;
using waProyectoDSW1.Repositories;

namespace waProyectoDSW1.Controllers
{
    [AuthorizeSession]
    public class HomeController : Controller
    {
        private readonly IHome repository;

        public HomeController()
        {
            repository = new HomeRepository();
        }

        [HttpGet]
        public JsonResult GetEstadosCitas()
        {
            var estadosCitas = repository.EstadoCitaPorCitas();  
            return Json(estadosCitas);  
        }

        [HttpGet]
        public JsonResult GetServiciosDentales()
        {
            var serviciosDentales = repository.ServicioDentalPorCitas();  
            return Json(serviciosDentales);  
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
