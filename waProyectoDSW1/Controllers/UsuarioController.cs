using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using waProyectoDSW1.Interfaces;
using waProyectoDSW1.Models;
using waProyectoDSW1.Repositories;

namespace waProyectoDSW1.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuario repository;

        public UsuarioController()
        {
            repository = new UsuarioRepository();
        }

        public ResultModel<object> Mantenimiento(UsuarioModel model, int op)
        {
            ResultModel<object> result = null;

            if (model.pk_usuario == 0)
            {
                result = repository.Usuario_Mant(model, 1);
            }
            else if (model.pk_usuario > 0 && op != 3)
            {
                result = repository.Usuario_Mant(model, 2);
            }
            else
            {
                result = repository.Usuario_Mant(model, op);
            }

            return result;
        }

        public IEnumerable<UsuarioModel> ListadoUsuario()
        {
            var result = repository.Listado_Usuario();
            return result.Data;
        }

        public PartialViewResult UsuarioMant(int pk_usuario)
        {
            UsuarioModel model = new UsuarioModel();

            if (pk_usuario > 0)
            {
                var result = repository.Buscar_Usuario(pk_usuario);
                if (result.success && result.Data != null)
                {
                    model = result.Data;
                }
            }

            return PartialView("_UsuarioMant", model);
        }

        public IActionResult Index()
        {
            return View(ListadoUsuario());
        }
    }
}
