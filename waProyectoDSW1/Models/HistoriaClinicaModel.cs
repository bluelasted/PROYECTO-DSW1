namespace waProyectoDSW1.Models
{
    public class HistoriaClinicaModel
    {
        public int pk_historia {  get; set; }
        public int fk_paciente { get; set; }
        public DateTime fechaCreacion { get; set; }
       
    }
}
