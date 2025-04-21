using waProyectoDSW1.Models;

namespace waProyectoDSW1.Interfaces
{
    public interface IUsuario
    {
        public ResultModel<UsuarioModel> ValidarUsuario(string usuario, string password);
    }
}
