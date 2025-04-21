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
                                    nombreUsuario = reader["nombreUsuario"].ToString(),
                                    rol = reader["rol"].ToString(),
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
