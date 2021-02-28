using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    public class ClsConsumos : Entidad
    {
        public int ID;
        public int IDchild;
        public int Idmenu;
        public String FechaConsumo;
        public int SnCancelado;
        ///Atributos
        ///
        public ClsConsumos()
        {
            this.ID = 0;
            this.IDchild = 0;
            this.Idmenu = 0;
            this.FechaConsumo = "";
            this.SnCancelado = 0;
        }


    }
}
