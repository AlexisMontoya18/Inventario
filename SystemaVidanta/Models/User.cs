using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SystemaVidanta.Models
{
    public class User
    {
        
        public String ID { get;  set; }
        [Display(Name = "Nombre:")] 
        public String Name { get; set; }
        [Display(Name = "Nombre Usuario:")]
        public String Username { get; set; }
        [Display(Name = "Conraseña:")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        [Display(Name = "Nivel:")]
        public int Level { get; set; }
        [Display(Name = "Activo:")]
        public int Active { get; set; }
        public virtual ICollection<UserRolesMapping> UserRolesMapping { get; set; }
    }
}