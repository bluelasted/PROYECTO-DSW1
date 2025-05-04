using System.Data.SqlClient;
using System.Data;
using waProyectoDSW1.Interfaces;
using waProyectoDSW1.Models;

namespace waProyectoDSW1.Repositories
{
    public class EspecialidadRepository : IEspecialidad
    {
        private readonly string connectionString;

        public EspecialidadRepository()
        {
            connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                .Build().GetConnectionString("cn");
        }

        public ResultModel<object> Especialidad_Mant()
        {
            throw new NotImplementedException();
        }

        public ResultModel<IEnumerable<EspecialidadModel>> Listado_Especialidades()
        {
            var result = new ResultModel<IEnumerable<EspecialidadModel>>();
            var lista = new List<EspecialidadModel>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_ESPECIALIDAD_LISTADO", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var model = new EspecialidadModel();
                                {
                                    model.pk_especialidad = Convert.ToInt32(reader["pk_especialidad"]);
                                    model.nombre = reader["nombre"].ToString();
                                    model.descripcion = reader["descripcion"].ToString();
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
