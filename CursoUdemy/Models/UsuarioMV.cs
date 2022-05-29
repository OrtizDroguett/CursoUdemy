using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CursoUdemy.Models
{
    public class UsuarioMV
    {
        [Display(Name ="ID Usuario")]
        public int iidusuario { get; set; }
        [Display(Name = "Usuario")]
        [Required]
        [StringLength(100,ErrorMessage ="Limite máximo es de 100")]
        public string nombreusuario { get; set; }
        [Display(Name = "Contraseña")]
        [Required]
        [StringLength(100, ErrorMessage = "Limite máximo es de 100")]
        public string contra { get; set; }
        [Display(Name = "TipoUsuario")]
        [Required]
        public char tipoUsuario { get; set; }
        [Display(Name = "ID")]
        [Required]
        public int iid { get; set; }
        [Display(Name = "ID rol")]
        [Required]
        public int iidrol { get; set; }
        public int bhabilitado { get; set; }
        //Propiedad adicional
        public string nombrePersona { get; set; } 
        public string nombreTipoUsuario { get; set; }
        public string nombreRol { get; set; }
    }
}