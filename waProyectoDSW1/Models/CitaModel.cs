namespace waProyectoDSW1.Models
{
    public class CitaModel
    {
        public int pk_cita { get; set; }
        public int fk_paciente { get; set; }
        public string nombrePaciente { get; set; }
        public int fk_doctor { get; set; }
        public string nombreDoctor { get; set; }
        public int fk_servicio { get; set; }
        public string nombreServicio { get; set; }
        public DateTime fechaCita { get; set; }
        public TimeSpan horaInicio { get; set; }
        public TimeSpan horaFin { get; set; }
        public int fk_estado { get; set; }
        public string nombreEstado { get; set; }
        public string observaciones { get; set; }
        public string color { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string dniPaciente { get; set; }
        public string alergias { get; set; }
        public string email { get; set; }
        public string direccion { get; set; }
        public decimal precio { get; set; }
        public int duracion { get; set; }
        public string descripcionEspecialidad { get; set; }
        public int edad
        {
            get
            {
                var edad = DateTime.Now.Year - fechaNacimiento.Year;
                if (DateTime.Now.Date < fechaNacimiento.AddYears(edad).Date)
                {
                    edad--;
                }
                return edad;
            }
        }
    }
}
