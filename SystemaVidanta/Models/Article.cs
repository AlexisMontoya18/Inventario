using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SystemaVidanta.Models
{
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Nombre Artículo")]
        public string NombreArtículo { get; set; }
       
        [Required]
        public string Descripción { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        [Display(Name = "Fecha Entrada")]
        [DataType(DataType.Date)]
        public string FechaEntrada { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Salida")]
        public string FechaSalida { get; set; }


      
    }
}