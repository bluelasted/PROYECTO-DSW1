using waProyectoDSW1.Models;

namespace waProyectoDSW1.Interfaces
{
    public interface IHome
    {
        public ResultModel<IEnumerable<EstadoCitaPorCitaModel>> EstadoCitaPorCitas();
        public ResultModel<IEnumerable<ServicioDentalPorCitaModel>> ServicioDentalPorCitas();
    }
}
