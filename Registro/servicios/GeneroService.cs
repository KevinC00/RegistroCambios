using System;
using System.Collections.Generic;
using System.Text;

namespace Registro.servicios
{
    public class GeneroService
    {
        public List<string> ObtenerGeneros()
        {
            var list = new List<string>();

            list.Add("MASCULINO");
            list.Add("FEMENINO");

            return list;
        }
    }
}
