using waProyectoDSW1.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace waProyectoDSW1.Helpers
{
    public static class SessionHelper
    {
        private const string UsuarioKey = "usuarioCompleto";

        public static void SetUser(ISession session, Models.UsuarioModel usuario)
        {
            var json = JsonSerializer.Serialize(usuario);
            session.SetString(UsuarioKey, json);
        }

        public static Models.UsuarioModel GetUser(ISession session)
        {
            var json = session.GetString(UsuarioKey);
            if (string.IsNullOrEmpty(json))
                return null;

            return JsonSerializer.Deserialize<Models.UsuarioModel>(json);
        }
    }
}
