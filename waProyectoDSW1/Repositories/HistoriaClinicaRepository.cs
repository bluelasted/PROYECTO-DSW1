using waProyectoDSW1.Interfaces;
using waProyectoDSW1.Models;

namespace waProyectoDSW1.Repositories
{
    public class HistoriaClinicaRepository : IHistoriaClinica
    {
        private readonly string connectionString;

        public HistoriaClinicaRepository()
        {
            connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                .Build().GetConnectionString("cn");
        }

        public ResultModel<HistoriaClinicaModel> Buscar_HistorialClinico(int pk_doctor)
        {
            throw new NotImplementedException();
        }

        public ResultModel<object> HistorialClinico_Mant()
        {
            throw new NotImplementedException();
        }

        public ResultModel<IEnumerable<HistoriaClinicaModel>> Listado_HistorialClinico()
        {
            throw new NotImplementedException();
        }
    }
}
