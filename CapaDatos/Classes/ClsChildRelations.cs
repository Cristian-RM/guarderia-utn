using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    public class ClsChildRelations : Entidad
    {
        public int ID { set; get; }
        public int IDchild { set; get; }
        public String TipoRelacion { set; get; }
<<<<<<< HEAD
=======

>>>>>>> e411dc82704a3ccc429ba60007e689012646441c
        ///Atributos
        ///
        public ClsChildRelations()
        {
            this.ID = 0;
            this.IDchild = 0;
            this.TipoRelacion = "";
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> e411dc82704a3ccc429ba60007e689012646441c
