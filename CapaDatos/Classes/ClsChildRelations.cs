using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    class ClsChildRelations : ClsConexion
    {
        public int IDchild { set; get; }
        public String TipoRelacion { set; get; }
        ///Atributos
        ///
        public ClsChildRelations()
        {
            this.IDchild = 0;
            this.TipoRelacion = "";
        }

    } 
}
