using Microsoft.AspNetCore.Mvc;

namespace waProyectoDSW1.Controllers
{
    public class MenuController : Controller
    {
        public readonly IConfiguration configuration;

        public MenuController(IConfiguration config)
        {
            this.configuration = config;
        }

    }
}
