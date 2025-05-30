﻿using waProyectoDSW1.Models;

namespace waProyectoDSW1.Interfaces
{
    public interface IPaciente
    {
        public ResultModel<IEnumerable<PacienteModel>> Listado_Paciente();
        public ResultModel<object> Paciente_Mant(PacienteModel model, int op);
        public ResultModel<PacienteModel> Buscar_Paciente(int pk_paciente);
    }
}
