using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CursoUdemy.Models
{
    public class PaginaMV
    {
        [Display(Name = "ID")]
        public int iidpagina { get; set; }
        [Display(Name ="Mensaje")]
        [Required]
        [StringLength(50,ErrorMessage ="Máximo 50")]
        public string mensaje { get; set; }
        [Display(Name = "Acción")]
        [Required]
        [StringLength(50, ErrorMessage = "Máximo 50")]
        public string accion { get; set; }
        [Display(Name = "Controlador")]
        [Required]
        [StringLength(100, ErrorMessage = "Máximo 100")]
        public string controlador { get; set; }
        public int bhabilitado { get; set; }
    }
}