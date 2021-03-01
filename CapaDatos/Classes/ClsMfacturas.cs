using System;

namespace CapaDatos.Classes
{
    public class ClsMfacturas : ClsConexion
    {
        public int ID { get; set; }
        public int IDabonado { get; set; }
        public String Mes { get; set; }
        public String FechaCreacion { get; set; }

        ///Atributos
        ///
        public ClsMfacturas()
        {
            this.ID = 0;
            this.IDabonado = 0;
            this.Mes = "";
            this.FechaCreacion = "";
        }
    }
}