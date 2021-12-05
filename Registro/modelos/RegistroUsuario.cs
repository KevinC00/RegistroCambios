using System;
using System.Collections.Generic;
using System.Text;

namespace Registro.modelos
{
    public class RegistroUsuario
    {
        public string nombre { get; set; }
        public string nombreUsuario { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string sexo { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }
        public string rol { get; set; }
        public object idEmpresa { get; set; }
    }
}
