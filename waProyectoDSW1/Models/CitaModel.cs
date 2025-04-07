namespace waProyectoDSW1.Models
{
    public class CitaModel
    {
        public int pk_cita { get; set; }
        public int fk_paciente { get; set; }
        public int fk_doctor { get; set; }
        public int fk_servicio { get; set; }
        public DateTime fechaCita { get; set; }
        public TimeOnly horaInicio {  get; set; }
        public TimeOnly horaFin {  get; set; }
        public int fk_estado { get; set; }
        public string observaciones { get; set; }
        public DateTime fechaCreacion { get; set; }

    }
}
