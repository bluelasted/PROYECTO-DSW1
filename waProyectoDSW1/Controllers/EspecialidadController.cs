using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using waProyectoDSW1.Models;

namespace waProyectoDSW1.Controllers
{
    public class EspecialidadController : Controller
    {
        public readonly IConfiguration configuration;

        public EspecialidadController(IConfiguration config)
        {
            this.configuration = config;
        }

        public IEnumerable<EspecialidadModel> LISTADO_ESPECIALIDAD()
        {
            List<EspecialidadModel> lista = new List<EspecialidadModel>();
            SqlConnection cn = new SqlConnection(configuration["ConnectionStrings:cn"]);
            SqlCommand cmd = new SqlCommand("SP_ESPECIALIDAD_lISTADO", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new EspecialidadModel()
                {
                    pk_especialidad = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString(),
                    descripcion = dr[2].ToString(),
                    estado = bool.Parse(dr[3].ToString()),
                });
            }

            cn.Close();
            return lista;
        }

        public IActionResult Index()
        {   
            var Especialidad = LISTADO_ESPECIALIDAD();
            return View(LISTADO_ESPECIALIDAD());
        }
    }
}
