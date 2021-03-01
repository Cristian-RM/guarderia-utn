using System;

namespace CapaDatos.Classes
{
    public class ClsIngredientes : Entidad
    {
        public String Nombre { get; set; }

        ///
        public ClsIngredientes()
        {
            this.Nombre = "";
        }

        public ClsIngredientes(String pNombre)
        {
            this.Nombre = pNombre;
        }
    }
}