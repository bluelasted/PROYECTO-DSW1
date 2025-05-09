using System.Data.SqlClient;
using System.Data;
using waProyectoDSW1.Interfaces;
using waProyectoDSW1.Models;
using System.Reflection;

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

        public ResultModel<object> Especialidad_Mant(EspecialidadModel model, int op)
        {
            var result = new ResultModel<object>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_ESPECIALIDAD_MANT", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@op", op);
                        cmd.Parameters.AddWithValue("@pk_especialidad", model.pk_especialidad);
                        cmd.Parameters.AddWithValue("@nombre", model.nombre);
                        cmd.Parameters.AddWithValue("@descripcion", model.descripcion);

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

        public ResultModel<EspecialidadModel> Buscar_Especialidad(int pk_especialidad)
        {
            var result = new ResultModel<EspecialidadModel>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_BUSCAR_ESPECIALIDAD", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pk_especialidad", pk_especialidad);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var model = new EspecialidadModel();
                                {
                                    model.pk_especialidad = Convert.ToInt32(reader["pk_especialidad"]);
                                    model.nombre = reader["nombre"].ToString();
                                    model.descripcion = reader["descripcion"].ToString();
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
