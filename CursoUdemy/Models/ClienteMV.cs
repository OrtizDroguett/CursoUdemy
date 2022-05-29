using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CursoUdemy.Models
{
    public class ClienteMV
    {
        public int iidcliente { get; set; }
        public string nombre { get; set; }
        public string appaterno { get; set; }
        public string apmaterno { get; set; }
        public string email { get; set; }
        public string direccion { get; set; }
        public int iidsexo { get; set; }
        public string telefonofijo { get; set; }
        public string telefonocelular { get; set; }
        public int bhabilitado { get; set; }
        public int btieneusuario { get; set; }
        public char tipousuario { get; set; }

}
}