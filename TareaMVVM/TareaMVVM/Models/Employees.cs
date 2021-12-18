using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TareaMVVM.Models
{
   

   public class Employees
    {
       [PrimaryKey,AutoIncrement]
        public int IdEmployee { get; set; }
        [MaxLength(40)]
       public string Nombre { get; set; }
        [MaxLength(60)]
       public string Apellido { get; set; }
         
        public string Edad { get; set; }
        [MaxLength(80)]
        public string Direccion { get; set; }
        public string Cargo { get; set; }
    }
}
