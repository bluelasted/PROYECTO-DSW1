using waProyectoDSW1.Models;

namespace waProyectoDSW1.Interfaces
{
    public interface IUsuario
    {
        public ResultModel<UsuarioModel> ValidarUsuario(string usuario, string password);
        public ResultModel<IEnumerable<UsuarioModel>> Listado_Usuario();
        public ResultModel<object> Usuario_Mant(UsuarioModel model, int op);
        public ResultModel<UsuarioModel> Buscar_Usuario(int pk_usuario);
    }
}
