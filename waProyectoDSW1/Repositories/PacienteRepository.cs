using System.Data.SqlClient;
using System.Data;
using waProyectoDSW1.Interfaces;
using waProyectoDSW1.Models;

namespace waProyectoDSW1.Repositories
{
    public class PacienteRepository : IPaciente
    {
        private readonly string connectionString;

        public PacienteRepository()
        {
            connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                .Build().GetConnectionString("cn");
        }

        public ResultModel<PacienteModel> Buscar_Paciente(int pk_paciente)
        {
            var result = new ResultModel<PacienteModel>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_BUSCAR_PACIENTE", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pk_paciente", pk_paciente);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var model = new PacienteModel
                                {
                                    pk_paciente = Convert.ToInt32(reader["pk_paciente"]),
                                    nombres = reader["nombres"].ToString(),
                                    apellidos = reader["apellidos"].ToString(),
                                    cedula = reader["cedula"].ToString(),
                                    email = reader["email"].ToString(),
                                    telefono = reader["telefono"].ToString(),
                                    direccion = reader["direccion"].ToString(),
                                    fechaNacimiento = (DateTime)reader["fechaNacimiento"],
                                    genero = reader["genero"].ToString(),
                                    alergias = reader["alergias"].ToString(),
                                    fechaRegistro = (DateTime)reader["fechaRegistro"],
                                    fechaUltimaVisita = (DateTime)reader["fechaUltimaVisita"]
                                };

                                result.Data = model;
                            }
                            else
                            {
                                result.success = false;
                                result.Message = "Paciente no encontrado.";
                                return result;
                            }
                        }
                    }
                }

                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.Message = "Error al buscar paciente: " + ex.Message;
            }

            return result;
        }


        public ResultModel<IEnumerable<PacienteModel>> Listado_Paciente()
        {
            var result = new ResultModel<IEnumerable<PacienteModel>>();
            var lista = new List<PacienteModel>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PACIENTE_LISTADO", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var model = new PacienteModel();
                                {
                                    model.pk_paciente = Convert.ToInt32(reader["pk_paciente"]);
                                    model.nombres = reader["nombres"].ToString();
                                    model.apellidos = reader["apellidos"].ToString();
                                    model.cedula = reader["cedula"].ToString();
                                    model.email = reader["email"].ToString();
                                    model.telefono = reader["telefono"].ToString();
                                    model.direccion = reader["direccion"].ToString();
                                    model.fechaNacimiento = (DateTime)reader["fechaNacimiento"];
                                    model.genero = reader["genero"].ToString();
                                    model.alergias = reader["alergias"].ToString();
                                    model.fechaRegistro = (DateTime)reader["fechaRegistro"];
                                    model.fechaUltimaVisita = (DateTime)reader["fechaUltimaVisita"];
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

        public ResultModel<object> Paciente_Mant()
        {
            throw new NotImplementedException();
        }
    }
}

