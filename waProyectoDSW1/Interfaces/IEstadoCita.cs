using waProyectoDSW1.Models;

namespace waProyectoDSW1.Interfaces
{
    public interface IEstadoCita
    {
        public ResultModel<IEnumerable<EstadoCitaModel>> Listado_EstadoCita();
    }
}
