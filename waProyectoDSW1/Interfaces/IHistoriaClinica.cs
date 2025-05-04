using waProyectoDSW1.Models;

namespace waProyectoDSW1.Interfaces
{
    public interface IHistoriaClinica
    {
        public ResultModel<IEnumerable<HistoriaClinicaModel>> Listado_HistorialClinico();
        public ResultModel<object> HistorialClinico_Mant();
        public ResultModel<HistoriaClinicaModel> Buscar_HistorialClinico(int pk_doctor);
    }
}
