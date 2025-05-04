using waProyectoDSW1.Models;

namespace waProyectoDSW1.Interfaces
{
    public interface IDoctor
    {
        public ResultModel<IEnumerable<DoctorModel>> Listado_Doctor();
        public ResultModel<object> Doctor_Mant(DoctorModel model, int op);
        public ResultModel<DoctorModel> Buscar_Doctor(int pk_doctor);
    }
}
