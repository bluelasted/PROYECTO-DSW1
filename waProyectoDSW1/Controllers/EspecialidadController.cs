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

        public IEnumerable<EspecialidadModel> ListadoEspecialidad()
        {
            List<EspecialidadModel> lista = new List<EspecialidadModel>();
            SqlConnection cn = new SqlConnection(configuration["ConnectionStrings:cn"]);
            SqlCommand cmd = new SqlCommand("SP_ESPECIALIDAD_lISTADO", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new EspecialidadModel()
                {
                    pk_especialidad = Convert.ToInt32(reader["pk_especialidad"]),
                    nombre = reader["nombre"].ToString(),
                    descripcion = reader["descripcion"].ToString(),
                    estado = bool.Parse(reader["estado"].ToString()),
                });
            }

            cn.Close();
            return lista;
        }

        public IActionResult Index()
        {   
            var Especialidad = ListadoEspecialidad();
            return View(ListadoEspecialidad());
        }
    }
}
