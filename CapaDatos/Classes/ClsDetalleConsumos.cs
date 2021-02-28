using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    public class ClsDetalleConsumos : Entidad
    {
        public int ID;
        public int IDfactura;
        public int IDconsumo;
        public String FechaCreacion;
        ///Atributos
        ///
        public ClsDetalleConsumos()
        {
            this.ID = 0;
            this.IDfactura = 0;
            this.IDconsumo = 0;
            this.FechaCreacion = "";
        }
    }
}
