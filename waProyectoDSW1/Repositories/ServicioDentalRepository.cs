using System.Data.SqlClient;
using waProyectoDSW1.Interfaces;
using waProyectoDSW1.Models;

namespace waProyectoDSW1.Repositories
{
    public class ServicioDentalRepository : IServicioDental
    {
        private readonly string connectionString;

        public ServicioDentalRepository()
        {
            connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                .Build().GetConnectionString("cn");
        }

        public ResultModel<IEnumerable<ServicioDentalModel>> Listado_Servicios()
        {
            var result = new ResultModel<IEnumerable<ServicioDentalModel>>();
            var lista = new List<ServicioDentalModel>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_SERVICIODENTAL_LISTADO", cn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new ServicioDentalModel
                                {
                                    pk_servicio = Convert.ToInt32(reader["pk_servicio"]),
                                    nombre = reader["nombre"].ToString(),
                                    descripcion = reader["descripcion"].ToString(),
                                    precio = Convert.ToDecimal(reader["precio"]),
                                    duracion = Convert.ToInt32(reader["duracion"]),
                                    fk_especialidad = Convert.ToInt32(reader["fk_especialidad"]),
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
