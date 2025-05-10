using System.Data.SqlClient;
using System.Data;
using waProyectoDSW1.Interfaces;
using waProyectoDSW1.Models;

namespace waProyectoDSW1.Repositories
{
    public class HomeRepository : IHome
    {
        private readonly string connectionString;

        public HomeRepository()
        {
            connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                .Build().GetConnectionString("cn");
        }

        public ResultModel<IEnumerable<EstadoCitaPorCitaModel>> EstadoCitaPorCitas()
        {
            var result = new ResultModel<IEnumerable<EstadoCitaPorCitaModel>>();
            var lista = new List<EstadoCitaPorCitaModel>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_OBTENER_ESTADOCITAPORCITA", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var model = new EstadoCitaPorCitaModel();
                                {
                                    model.estadoCita = reader["EstadoCita"].ToString();
                                    model.numeroCitas = (int)reader["NumeroDeCitas"];
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

        public ResultModel<IEnumerable<ServicioDentalPorCitaModel>> ServicioDentalPorCitas()
        {
            var result = new ResultModel<IEnumerable<ServicioDentalPorCitaModel>>();
            var lista = new List<ServicioDentalPorCitaModel>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_OBTENER_SERVICIODENTALPORCITA", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var model = new ServicioDentalPorCitaModel();
                                {
                                    model.servicioDental = reader["ServicioDental"].ToString();
                                    model.numeroCitas = (int)reader["NumeroDeCitas"];
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
