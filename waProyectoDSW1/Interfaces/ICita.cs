using waProyectoDSW1.Models;

namespace waProyectoDSW1.Interfaces
{
    public interface ICita
    {
        public ResultModel<IEnumerable<CitaModel>> Listado_Cita(int fk_doctor = 0);
        public ResultModel<object> Cita_Mant(CitaModel model, int op);
        public ResultModel<CitaModel> Buscar_Cita(int pk_cita);
    }
}
