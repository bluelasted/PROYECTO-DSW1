using Microsoft.AspNetCore.Mvc;
using waProyectoDSW1.Helpers;
using waProyectoDSW1.Interfaces;
using waProyectoDSW1.Models;
using waProyectoDSW1.Repositories;

namespace waProyectoDSW1.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuario _usuarioRepo;

        public LoginController()
        {
            _usuarioRepo = new UsuarioRepository();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UsuarioModel model)
        {
            var resultado = _usuarioRepo.ValidarUsuario(model.nombreUsuario, model.password);

            if (resultado.success)
            {
                var usuario = resultado.Data;
                HttpContext.Session.SetInt32("pk_usuario", usuario.pk_usuario);
                HttpContext.Session.SetString("usuario", usuario.nombreUsuario);
                HttpContext.Session.SetString("rol", usuario.rol);
                HttpContext.Session.SetInt32("fk_doctor", usuario.fk_doctor ?? 0);

                SessionHelper.SetUser(HttpContext.Session, usuario);

                if (usuario.rol == "Doctor")
                {
                    return RedirectToAction("Index", "Cita");
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = resultado.Message;
                return View(model);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
