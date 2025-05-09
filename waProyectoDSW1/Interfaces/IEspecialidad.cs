using waProyectoDSW1.Models;

namespace waProyectoDSW1.Interfaces
{
    public interface IEspecialidad
    {
        public ResultModel<IEnumerable<EspecialidadModel>> Listado_Especialidades();
        public ResultModel<object> Especialidad_Mant(EspecialidadModel model, int op);
        public ResultModel<EspecialidadModel> Buscar_Especialidad(int pk_especialidad);
    }
}
