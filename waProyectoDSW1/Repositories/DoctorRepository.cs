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

        public ResultModel<DoctorModel> Buscar_Doctor(int pk_doctor)
        {
            var result = new ResultModel<DoctorModel>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_BUSCAR_DOCTOR", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pk_doctor", pk_doctor);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var model = new DoctorModel
                                {
                                    pk_doctor = Convert.ToInt32(reader["pk_doctor"]),
                                    dni = reader["dni"].ToString(),
                                    nombres = reader["nombres"].ToString(),
                                    apellidos = reader["apellidos"].ToString(),
                                    fk_especialidad = Convert.ToInt32(reader["fk_especialidad"]),
                                    especialidad = reader["nombre"].ToString(),
                                    telefono = reader["telefono"].ToString(),
                                    email = reader["email"].ToString(),
                                    direccion = reader["direccion"].ToString(),
                                    horarioInicio = (TimeSpan)reader["horarioInicio"],
                                    horarioFin = (TimeSpan)reader["horarioFin"]
                                };

                                result.success = true;
                                result.Data = model;
                            }
                            else
                            {
                                result.success = false;
                                result.Message = "Doctor no encontrado.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.Message = "Error al buscar el doctor: " + ex.Message;
            }

            return result;
        }

        public ResultModel<object> Doctor_Mant(DoctorModel model, int op)
        {
            var result = new ResultModel<object>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_DOCTOR_MANT", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@op", op);
                        cmd.Parameters.AddWithValue("@pk_doctor", model.pk_doctor);
                        cmd.Parameters.AddWithValue("@nombres", model.nombres);
                        cmd.Parameters.AddWithValue("@apellidos", model.apellidos);
                        cmd.Parameters.AddWithValue("@telefono", model.telefono);
                        cmd.Parameters.AddWithValue("@email", model.email);
                        cmd.Parameters.AddWithValue("@fk_especialidad", model.fk_especialidad);
                        cmd.Parameters.AddWithValue("@horarioInicio", model.horarioInicio);
                        cmd.Parameters.AddWithValue("@horarioFin", model.horarioFin);
                        cmd.Parameters.AddWithValue("@direccion", model.direccion);
                        cmd.Parameters.AddWithValue("@dni", model.dni);

                        cmd.ExecuteNonQuery();
                    }
                }

                result.success = true;
                result.Message = "Operación realizada correctamente.";
            }
            catch (Exception ex)
            {
                result.success = false;
                result.Message = "Error: " + ex.Message;
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
                                    model.especialidad = reader["nombre"].ToString();
                                    model.horarioInicio = (TimeSpan)reader["horarioInicio"];
                                    model.horarioFin = (TimeSpan)reader["horarioFin"];
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
