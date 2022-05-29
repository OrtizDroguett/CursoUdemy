using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CursoUdemy.Models
{
    public class MarcaMV
    {
        [Display(Name ="ID")]
        public int iidmarca {get;set;}
        [Required]
        [Display(Name = "Nombre")]
        [StringLength(100,ErrorMessage ="Máximo 100")]
        public string nombre { get; set; }
        [Required]
        [Display(Name = "Descripción")]
        [StringLength(100, ErrorMessage = "Máximo 100")]
        public string descripcion { get; set; }
        public int bhabilitado { get; set; }
    }
}