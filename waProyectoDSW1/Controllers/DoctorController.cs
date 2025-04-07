using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using waProyectoDSW1.Models;

namespace waProyectoDSW1.Controllers
{
    public class DoctorController : Controller
    {
        public readonly IConfiguration configuration;

        public DoctorController(IConfiguration config)
        {
            this.configuration = config;
        }

        public IEnumerable<DoctorModel> LISTADO_DOCTORES()
        {
            List<DoctorModel> lista = new List<DoctorModel>();
            SqlConnection cn = new SqlConnection(configuration["ConnectionStrings:cn"]);
            SqlCommand cmd = new SqlCommand("SP_DOCTOR_LISTADO", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new DoctorModel()
                {
                    pk_doctor = int.Parse(dr[0].ToString()),
                    nombres = dr[1].ToString(),
                    apellidos = dr[2].ToString(),
                    dni = dr[3].ToString(),
                    telefono = dr[4].ToString(),
                    direccion = dr[5].ToString(),
                    fk_especialidad = int.Parse(dr[6].ToString()),
                    especialidad = dr[7].ToString(),
                    horarioInicio = TimeOnly.Parse(dr[8].ToString()), 
                    horarioFin = TimeOnly.Parse(dr[9].ToString())
                });
            }

            cn.Close();
            return lista;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
