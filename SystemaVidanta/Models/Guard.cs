using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SystemaVidanta.Models
{
    public class Guard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string NumColaborador { get; set; }
        [Required]
        public string Empresa { get; set; }
        
        [Required]
        public string FolioResguardo { get; set; }
       
        [Required]
        [DataType(DataType.Date)]
        public string FechaResguardo { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string FechaDevolución { get; set; }
        [Required]
        public string TipoMovimiento { get; set; }

        [Required]
        public string TipoPrestamo { get; set; }
        [Required]
        public string Ubicación { get; set; }
      
        [Required]
        public string ObservacionesResguardo { get; set; }
        [Required]
        public string VoBo { get; set; }


    }
}