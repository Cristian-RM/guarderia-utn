using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    public class ClsMenus : Entidad
    {
        public int ID;
        public String Nombre;
        public decimal Precio;
        ///Atributos
        ///
        public ClsMenus()
        {
            this.ID = 0;
            this.Nombre = "";
            this.Precio = 0;
        }
    }
}

