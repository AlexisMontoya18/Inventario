using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SystemaVidanta.Models
{
    public class Resguardo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Numero Colaborador")]
        [Required]
        public string NumColaborador { get; set; }
        public string Nombre { get; set; }
        public string Puesto { get; set; }
        public string UsuarioRecibe { get; set; }
        public string Empresa { get; set; }
      
        [Display(Name = "Folio Resguardo")]
        public string FolioResguardo { get; set; }

        [Display(Name = "Fecha Resguardo")]
        [DataType(DataType.Date)]
        public string FechaResguardo { get; set; }
        [Display(Name = "Fecha Devolución")]
        [DataType(DataType.Date)]
        public string FechaDevolucion { get; set; }
        [Display(Name = "Tipo Movimiento")]
        public string TipoMovimiento { get; set; }

        [Display(Name = "Tipo Prestamo")]
        public string TipoPrestamo { get; set; }
        [Display(Name = "Ubicación")]
        public string Ubicacion { get; set; }

        [Display(Name = "Observaciones Resguardo")]
        public string ObservacionesResguardo { get; set; }
        [Display(Name = "Vo.Bo")]
        public string VoBo { get; set; }
        public string imagen { get; set; }
        public string firmaColaborador { get; set; }
        public string firmaUsuario { get; set; }
        public int Estatus { get; set; }
        public virtual ICollection<ResguardoDetalle> DetallesResguardo {get; set;}

    }
}