using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using waProyectoDSW1.Interfaces;
using waProyectoDSW1.Models;

namespace waProyectoDSW1.Repositories
{
    public class DoctorRepository : IDoctor
    {
        private readonly string connectionString;

        public DoctorRepository()
        {
            connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                .Build().GetConnectionString("cn");
        }

        public ResultModel<object> Doctor_Mant(int? pk_doctor, int op)
        {
            var result = new ResultModel<object>();

            try
            {


                result.success = true;
                result.Message = "Se ha registrado el doctor correctamente";
            }
            catch (Exception ex) 
            {
                result.success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel<IEnumerable<DoctorModel>> Listado_Doctor()
        {
            var result = new ResultModel<IEnumerable<DoctorModel>>();
            var lista = new List<DoctorModel>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_DOCTOR_LISTADO", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var model = new DoctorModel();
                                {
                                    model.pk_doctor = Convert.ToInt32(reader["pk_doctor"]);
                                    model.nombres = reader["nombres"].ToString();
                                    model.apellidos = reader["apellidos"].ToString();
                                    model.dni = reader["dni"].ToString();
                                    model.email = reader["email"].ToString();
                                    model.telefono = reader["telefono"].ToString();
                                    model.fk_especialidad = Convert.ToInt32(reader["fk_especialidad"]);
                                    model.especialidad = reader["especialidad"].ToString();
                                    model.horarioInicio = TimeOnly.FromTimeSpan((TimeSpan)reader["horarioInicio"]);
                                    model.horarioFin = TimeOnly.FromTimeSpan((TimeSpan)reader["horarioFin"]);
                                };

                                lista.Add(model);
                            }
                        }
                    }
                }

                result.success = true;
                result.Data = lista;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.Message = ex.Message;
                result.Data = null;
            }

            return result;
        }
    }
}
