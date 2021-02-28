using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    public class ClsChilds : Entidad
    {
        public int IDmatricula { get; set; }

        public String Nombre { get; set; }
        public String FechaRegistro { get; set; }
        public String FEchaNacimiento { get; set; }

        ///Atributos
        ///
        public ClsChilds()
        {
            this.Nombre = "";
            this.FechaRegistro = "";
            this.FEchaNacimiento = "";
        }
    }
}