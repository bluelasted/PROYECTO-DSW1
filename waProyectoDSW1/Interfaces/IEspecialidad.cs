using waProyectoDSW1.Models;

namespace waProyectoDSW1.Interfaces
{
    public interface IEspecialidad
    {
        public ResultModel<IEnumerable<EspecialidadModel>> Listado_Especialidades();
        public ResultModel<object> Especialidad_Mant();
    }
}
