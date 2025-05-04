using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace waProyectoDSW1.Models
{
    public class DoctorModel
    {
        public int pk_doctor { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string dni { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string direccion { get; set; }
        public int fk_especialidad { get; set; }
        public string especialidad { get; set; }
        public TimeSpan horarioInicio { get; set; }
        public TimeSpan horarioFin { get; set; }
        public bool estado { get; set; }
    }
}
