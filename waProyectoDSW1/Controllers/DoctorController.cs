using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using waProyectoDSW1.Models;

namespace waProyectoDSW1.Controllers
{
    public class DoctorController : Controller
    {
        


        public IActionResult Index()
        {
            return View();
        }
    }
}
