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
        [Display(Name = "Nombre Del Artículo")]
        public string NombreArtículo { get; set; }
       
        [Required]
        public string Descripción { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Modelo { get; set; }
        
        [Display(Name = "Numero De Serie")]
        public string NumSerie { get; set; }
       
        [Display(Name = "Numero Interno")]
        public string  NumInterno { get; set; }

        [Display(Name = "Fecha De Entrada")]
        [DataType(DataType.Date)]
        public string FechaEntrada { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha De Salida")]
        public string FechaSalida { get; set; }


      
    }
}