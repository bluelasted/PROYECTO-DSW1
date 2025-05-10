using waProyectoDSW1.Models;

namespace waProyectoDSW1.Interfaces
{
    public interface IServicioDental
    {
        public ResultModel<IEnumerable<ServicioDentalModel>> Listado_Servicios();
    }
}
