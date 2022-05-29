using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CursoUdemy.Models
{
    public class EmpleadoMV
    {
        public int iidempleado { get; set; }
        public string nombre { get; set; }
        public string appaterno { get; set; }
        public string apmaterno { get; set; }
        public DateTime fechacontrato { get; set; }
        public decimal sueldo { get; set; }
        public int iidtipousuario { get; set; }
        public int iidtipocontrato { get; set; }
        public int iidsexo { get; set; }
        public int bhabilitado { get; set; }
        public int btieneusuario { get; set; }
        public char tipousuario { get; set; }
    }
}