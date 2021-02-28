using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    public class ClsEncargados : Entidad
    {
        public int ID { get; set; }
        public int IDchildRelation { get; set; }
        public String DNI { get; set; }
        public String Nombre { get; set; }
        public String Direccion { get; set; }
        public String Telefono { get; set; }
        ///Atributos
        ///
        public ClsEncargados()
        {
            this.ID = 0;
            this.IDchildRelation = 0;
            this.DNI = "";
            this.Nombre = "";
            this.Direccion = "";
            this.Telefono = "";
        }
    }
}
