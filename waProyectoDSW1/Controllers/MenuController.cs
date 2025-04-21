using Microsoft.AspNetCore.Mvc;
using waProyectoDSW1.Interfaces;
using waProyectoDSW1.Repositories;

namespace waProyectoDSW1.Controllers
{
    public class MenuController : Controller
    {
        private readonly IUsuario _usuarioRepo;

        public LoginController()
        {
            _usuarioRepo = new UsuarioRepository();
        }



    }
}
