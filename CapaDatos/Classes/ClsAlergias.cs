using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    public class ClsAlergias : Entidad
    {
        public int ID { get; set; }
        public String NombreIngrediente { get; set; }
        public int IDchild { get; set; }

        ///Atributos
        ///
        public ClsAlergias()
        {
            this.ID = 0;
            this.NombreIngrediente = "";
            this.IDchild = 0;
        }
    }
}

