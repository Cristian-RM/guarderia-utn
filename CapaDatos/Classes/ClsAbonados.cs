using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    public class ClsAbonados : ClsConexion
    {
        public int ID { get; set; }
        public int IDchildRelation { get; set; }
        public String DNI { get; set; }
        public String Nombre { get; set; }
        public String Direccion { get; set; }
        public String Telefono { get; set; }
        public String Banco { get; set; }
        public String CuentaIBAM { get; set; }
        ///Atributos
        ///
        public ClsAbonados()
        {
            this.ID = 0;
            this.IDchildRelation = 0;
            this.DNI = "";
            this.Nombre = "";
            this.Direccion = "";
            this.Telefono = "";
            this.Banco = "";
            this.CuentaIBAM = "";
        }

}
