using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    public class ClsMfacturas : ClsConexion
    {
        public int ID { get; set; }
        public int IDabonado { get; set; }
        public String Mes { get; set; }
        public String FechaCreacion { get; set; }

        ///Atributos
        ///
        public ClsMfacturas()
        {
            this.ID = 0;
            this.IDabonado = 0;
            this.Mes = "";
            this.FechaCreacion = "";
        }
    }
}