using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    internal class ClsIngredientes : ClsConexion
    {
        public String Nombre { get; set; }

        ///
        public ClsIngredientes()
        {
            this.Nombre = "";
        }

        public ClsIngredientes(String pNombre)
        {
            this.Nombre = pNombre;
        }
    }
}