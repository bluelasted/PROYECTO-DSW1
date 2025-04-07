namespace waProyectoDSW1.Models
{
    public class PagoModel
    {
        public int pk_pago { get; set; }
        public int fk_cita { get; set; }
        public int fk_paciente { get; set; }
        public DateTime fechaPago  { get; set; }
        public double monto { get; set; }
        public string metodoPago {  get; set; }
        public string estado { get; set; }
        public string observaciones { get; set; }
    }
}
