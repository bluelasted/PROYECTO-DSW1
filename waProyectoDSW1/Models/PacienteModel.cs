namespace waProyectoDSW1.Models
{
    public class PacienteModel
    {
        public int pk_paciente {  get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string cedula { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string direccion { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string genero { get; set; }
        public string alergias { get; set; }
        public bool estado { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaUltimaVisita { get; set; }
    }
}
