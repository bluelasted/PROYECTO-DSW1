namespace waProyectoDSW1.Models
{
    public class UsuarioModel
    {
        public int pk_usuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string nombreUsuario { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string rol { get; set; }
        public int fk_doctor { get; set; }
        public bool estado { get; set; }
    }
}
