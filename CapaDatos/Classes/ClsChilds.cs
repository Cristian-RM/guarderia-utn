using System;

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