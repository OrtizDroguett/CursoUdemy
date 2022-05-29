using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CursoUdemy.Models
{
    public class RolPaginaMV
    {
        [Display(Name ="ID Rol Pagina")]
        
        public int iidrolpagina { get; set; }
        [Required(ErrorMessage ="El campo Rol es requerido")]
        [Display(Name = "ID Rol")]
        public int iidrol { get; set; }
        [Required(ErrorMessage = "El campo Pagina es requerido")]
        [Display(Name = "ID Pagina")]
        public int iidpagina { get; set; }
        public int bhabilitado { get; set; }
        //Propiedades adicionales
        [Display(Name ="Rol")]
        public string nombreRol { get; set; }
        [Display(Name ="Página")]
        public string nombrePagina { get; set; }
    }
}