using waProyectoDSW1.Models;

namespace waProyectoDSW1.Interfaces
{
    public interface IDoctor
    {
        public ResultModel<IEnumerable<DoctorModel>> Listado_Doctor();
        public ResultModel<object> Doctor_Mant(int? pk_doctor, int op);
    }
}
