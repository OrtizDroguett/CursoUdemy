using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CursoUdemy.Models
{
    public class ModeloMV
    {
        [Display(Name ="ID")]
        public int iidmodelo { get; set; }
        [Display(Name = "Nombre")]
        [Required]
        [StringLength(100, ErrorMessage ="Máximo 100")]
        public string nombre { get; set; }
        [Display(Name = "Descripción")]
        [Required]
        [StringLength(200,ErrorMessage ="Máximo 200")]
        public string descripcion { get; set; }
        public int bhabilitado { get; set; } 
        //propiedad adicional
        public string mensajeError { get; set; }
    }
}