namespace waProyectoDSW1.Models
{
    public class ServicioDentalModel
    {
        public int pk_servicio {  get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int duracion { get; set; }
        public decimal precio { get; set; }
        public int fk_especialidad { get; set; }
        public bool estado { get; set; }
    }
}
