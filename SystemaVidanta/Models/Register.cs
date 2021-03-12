using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SystemaVidanta.Models
{
    public class Register
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Nombre { get; set; }

        public string ApellidoP { get; set; }

        public string ApellidoM { get; set; }
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }

        public string Colaborador { get; set; }
        public string Puesto { get; set; }

        public string Telefono { get; set; }

        public string Departamento { get; set; }


    }
}