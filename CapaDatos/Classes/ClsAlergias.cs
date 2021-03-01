using System;

namespace CapaDatos.Classes
{
    public class ClsAlergias : Entidad
    {
        public int ID { get; set; }
        public String NombreIngrediente { get; set; }
        public int IDchild { get; set; }

        ///Atributos
        ///
        public ClsAlergias()
        {
            this.ID = 0;
            this.NombreIngrediente = "";
            this.IDchild = 0;
        }
    }
}