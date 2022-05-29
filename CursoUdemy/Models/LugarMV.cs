using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CursoUdemy.Models
{
    public class LugarMV
    {
        [Display(Name ="ID")]
        public int iidlugar { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        [StringLength(50,ErrorMessage ="Máximo 50")]
        public string nombre { get; set; }
        [Required]
        [Display(Name = "Descripción")]
        [StringLength(200, ErrorMessage = "Máximo 200")]
        [DataType(DataType.MultilineText)]
        public string descripcion { get; set; }
        public int bhabilitado { get; set; }
        //Propiedades adicionales
        public string mensajeError { get; set; }
    }
}