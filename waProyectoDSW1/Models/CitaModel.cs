namespace waProyectoDSW1.Models
{
    public class CitaModel
    {
        public int pk_cita { get; set; }
        public int fk_paciente { get; set; }
        public string nombrePaciente {get; set; }
        public int fk_doctor { get; set; }
        public string nombreDoctor { get; set; }
        public int fk_servicio { get; set; }
        public string nombreServicio { get; set; }
        public DateTime fechaCita { get; set; }
        public TimeSpan horaInicio {  get; set; }
        public TimeSpan horaFin {  get; set; }
        public int fk_estado { get; set; }
        public string nombreEstado { get; set; }    
        public string observaciones { get; set; }
        public string color { get; set; }
        public DateTime fechaCreacion { get; set; }

    }
}
