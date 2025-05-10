using System.Data.SqlClient;
using waProyectoDSW1.Interfaces;
using waProyectoDSW1.Models;

namespace waProyectoDSW1.Repositories
{
    public class EstadoCitaRepository : IEstadoCita
    {
        private readonly string connectionString;

        public EstadoCitaRepository()
        {
            connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                .Build().GetConnectionString("cn");
        }

        public ResultModel<IEnumerable<EstadoCitaModel>> Listado_EstadoCita()
        {
            var result = new ResultModel<IEnumerable<EstadoCitaModel>>();
            var lista = new List<EstadoCitaModel>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_ESTADOCITA_LISTADO", cn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new EstadoCitaModel
                                {
                                    pk_estadoCita = Convert.ToInt32(reader["pk_estadoCita"]),
                                    nombre = reader["nombre"].ToString(),
                                    color = reader["color"].ToString()
                                });
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
            }

            return result;
        }
    }
}
