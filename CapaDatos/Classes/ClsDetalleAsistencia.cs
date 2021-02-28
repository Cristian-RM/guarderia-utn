using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    internal class ClsDetalleAsistencia : Entidad
    {
        public int ID { get; set; }
        public int IDfactura { get; set; }
        public int IDasistencia { get; set; }
        public String FechaCreacion { get; set; }

        ///Atributos
        ///
        public ClsDetalleAsistencia()
        {
            this.ID = 0;
            this.IDfactura = 0;
            this.IDasistencia = 0;
            this.FechaCreacion = "";
        }
    }
}