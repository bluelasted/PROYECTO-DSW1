using System.Data.SqlClient;
using System.Data;
using waProyectoDSW1.Models;
using waProyectoDSW1.Interfaces;
using System.Drawing;

namespace waProyectoDSW1.Repositories
{
    public class CitaRepository : ICita
    {
        private readonly string connectionString;

        public CitaRepository()
        {
            connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                .Build().GetConnectionString("cn");
        }

        public ResultModel<CitaModel> Buscar_Cita(int pk_cita)
        {
            var result = new ResultModel<CitaModel>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_BUSCAR_CITA", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pk_cita", pk_cita);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var model = new CitaModel
                                {
                                    pk_cita = Convert.ToInt32(reader["pk_cita"]),
                                    fk_doctor = Convert.ToInt32(reader["fk_doctor"]),
                                    fk_estado = Convert.ToInt32(reader["fk_estadoCita"]),
                                    fk_paciente = Convert.ToInt32(reader["fk_paciente"]),
                                    fk_servicio = Convert.ToInt32(reader["fk_servicio"]),
                                    nombrePaciente = reader["nombrePaciente"].ToString(),
                                    nombreDoctor = reader["nombreDoctor"].ToString(),
                                    nombreServicio = reader["nombreServicio"].ToString(),
                                    nombreEstado = reader["nombreEstado"].ToString(),
                                    horaInicio = (TimeSpan)reader["horaInicio"],
                                    horaFin = (TimeSpan)reader["horaFin"],
                                    observaciones = reader["observaciones"].ToString(),
                                    fechaCita = (DateTime)reader["fechaCita"],
                                    fechaNacimiento = reader["fechaNacimiento"] != DBNull.Value ? (DateTime)reader["fechaNacimiento"] : default(DateTime),
                                    dniPaciente = reader["cedula"].ToString(),
                                    alergias = reader["alergias"].ToString(),
                                    email = reader["email"].ToString(),
                                    direccion = reader["direccion"].ToString(),
                                    precio = reader["precio"] != DBNull.Value ? Convert.ToDecimal(reader["precio"]) : 0m,
                                    duracion = reader["duracion"] != DBNull.Value ? Convert.ToInt32(reader["duracion"]) : 0,
                                    descripcionEspecialidad = reader["descripcionEspecialidad"].ToString()
                                };

                                result.success = true;
                                result.Data = model;
                            }
                            else
                            {
                                result.success = false;
                                result.Message = "Cita no encontrado.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.Message = "Error al buscar la cita: " + ex.Message;
            }

            return result;
        }

        public ResultModel<object> Cita_Mant(CitaModel model, int op)
        {
            var result = new ResultModel<object>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_CITA_MANT", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@op", op);
                        cmd.Parameters.AddWithValue("@pk_cita", model.pk_cita);
                        cmd.Parameters.AddWithValue("@fk_paciente", model.fk_paciente);
                        cmd.Parameters.AddWithValue("@fk_doctor", model.fk_doctor);
                        cmd.Parameters.AddWithValue("@fk_servicio", model.fk_servicio);
                        cmd.Parameters.AddWithValue("@fechaCita",
                            model.fechaCita == DateTime.MinValue ? (object)DBNull.Value : model.fechaCita);
                        cmd.Parameters.AddWithValue("@horaInicio", model.horaInicio);
                        cmd.Parameters.AddWithValue("@horaFin", model.horaFin);
                        cmd.Parameters.AddWithValue("@fk_estadoCita", model.fk_estado);
                        cmd.Parameters.AddWithValue("@observaciones", model.observaciones);

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


        public ResultModel<IEnumerable<CitaModel>> Listado_Cita(int fk_doctor)
        {
            var result = new ResultModel<IEnumerable<CitaModel>>();
            var lista = new List<CitaModel>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_CITA_LISTADO", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@fk_doctor", fk_doctor);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var model = new CitaModel();
                                {
                                    model.pk_cita = Convert.ToInt32(reader["pk_cita"]);
                                    model.fechaCita = Convert.ToDateTime(reader["fechaCita"]);
                                    model.horaInicio = (TimeSpan)reader["horaInicio"];
                                    model.horaFin = (TimeSpan)reader["horaFin"];
                                    model.nombrePaciente = reader["nombrePaciente"].ToString();
                                    model.nombreDoctor = reader["nombreDoctor"].ToString();
                                    model.nombreServicio = reader["nombreServicio"].ToString();
                                    model.nombreEstado = reader["nombreEstado"].ToString();
                                    model.observaciones = reader["observaciones"].ToString();
                                    model.color = reader["color"].ToString();
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
