using System.Data;
using System.Data.SqlClient;
using waProyectoDSW1.Interfaces;
using waProyectoDSW1.Models;

namespace waProyectoDSW1.Repositories
{
    public class UsuarioRepository : IUsuario
    {
        private readonly string connectionString;

        public UsuarioRepository()
        {
            connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                .Build().GetConnectionString("cn");
        }

        public ResultModel<UsuarioModel> Buscar_Usuario(int pk_usuario)
        {
            var result = new ResultModel<UsuarioModel>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_BUSCAR_USUARIO", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pk_usuario", pk_usuario);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var model = new UsuarioModel
                                {
                                    pk_usuario = Convert.ToInt32(reader["pk_usuario"]),
                                    nombre = reader["nombre"].ToString(),
                                    apellido = reader["apellido"].ToString(),
                                    email = reader["email"].ToString(),
                                    nombreUsuario = reader["nombreUsuario"].ToString(),
                                    password = reader["password"].ToString(),
                                    rol = reader["rol"].ToString()
                                };

                                result.success = true;
                                result.Data = model;
                            }
                            else
                            {
                                result.success = false;
                                result.Message = "Usuario no encontrado.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.Message = "Usuario al buscar el doctor: " + ex.Message;
            }

            return result;
        }

        public ResultModel<IEnumerable<UsuarioModel>> Listado_Usuario()
        {
            var result = new ResultModel<IEnumerable<UsuarioModel>>();
            var lista = new List<UsuarioModel>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_USUARIO_LISTADO", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var model = new UsuarioModel();
                                {
                                    model.pk_usuario = Convert.ToInt32(reader["pk_usuario"]);
                                    model.nombre = reader["nombre"].ToString();
                                    model.apellido = reader["apellido"].ToString();
                                    model.nombreUsuario = reader["nombreUsuario"].ToString();
                                    model.email = reader["email"].ToString();
                                    model.password = reader["password"].ToString();
                                    model.rol = reader["rol"].ToString();
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

        public ResultModel<object> Usuario_Mant(UsuarioModel model, int op)
        {
            var result = new ResultModel<object>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_USUARIO_MANT", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@op", op);
                        cmd.Parameters.AddWithValue("@pk_usuario", model.pk_usuario);
                        cmd.Parameters.AddWithValue("@nombre", model.nombre);
                        cmd.Parameters.AddWithValue("@apellido", model.apellido);
                        cmd.Parameters.AddWithValue("@nombreUsuario", model.nombreUsuario);
                        cmd.Parameters.AddWithValue("@email", model.email);
                        cmd.Parameters.AddWithValue("@password", model.password);
                        cmd.Parameters.AddWithValue("@rol", model.rol);

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

        public ResultModel<UsuarioModel> ValidarUsuario(string usuario, string password)
        {
            var result = new ResultModel<UsuarioModel>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_VALIDAR_USUARIO", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var model = new UsuarioModel
                                {
                                    pk_usuario = Convert.ToInt32(reader["pk_usuario"]),
                                    nombre = reader["nombre"].ToString(),
                                    apellido = reader["apellido"].ToString(),
                                    nombreUsuario = reader["nombreUsuario"].ToString(),
                                    email = reader["email"].ToString(),
                                    rol = reader["rol"].ToString(),
                                    fk_doctor = reader.IsDBNull(reader.GetOrdinal("fk_doctor")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("fk_doctor")),
                                    estado = Convert.ToBoolean(reader["estado"])
                                };

                                result.success = true;
                                result.Message = "Login exitoso";
                                result.Data = model;
                            } else
                            {
                                result.success = false;
                                result.Message = "Usuario o contraseña incorrectos.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.Message = $"Error al validar usuario: {ex.Message}";
                result.Data = null;
            }

            return result;
        }
        
    }
}
