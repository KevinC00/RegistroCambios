using System;
using System.Collections.Generic;
using System.Text;

namespace Registro.servicios
{
    public class RolService
    {
        public List<string> ObtenerRoles()
        {
            var list = new List<string>();

            list.Add("CLIENTE");
            list.Add("ASESOR");

            return list;
        }
    }
}
