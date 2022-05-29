using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CursoUdemy.Models
{
    public class ViajeMV
    {
        public int iidviaje { get; set; }
        [Required]
        public int iidlugarorigen { get; set; }
        [Required]
        public int iidlugardestino { get; set; }
        [Required]
        public decimal precio { get; set; }
        [Required]
        public DateTime fechaviaje { get; set; }
        [Required]
        public int iidbus { get; set; }
        [Required]
        public int numeroasientosdisponibles { get; set; }
        public int bhabilitado { get; set; }
        //Propiedades adicionales
        public string nombrefoto { get; set; }

        

        public string nombreOrigen { get; set; }
        public string nombreDestino { get; set; }
        [Display(Name ="Placa")]
        public string nombreBus { get; set; }
    }
}