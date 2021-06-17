using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SystemaVidanta.Models
{
    public class Bitacora
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Accion { get; set; }
        public string Tabla { get; set; }
        [Display(Name = "Modificación")]
        public string PropertyModified { get; set; }
        [Display(Name = "Llave primaria ")]
        public string PrimaryKeyValue { get; set; }
        [Display(Name = "Valor Antiguo")]
        public string OldValue { get; set; }
        [Display(Name = "Nuevo Valor")]
        public string NewValue { get; set; }
        [Display(Name = "ID Usario")]
        public string IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
    }
}