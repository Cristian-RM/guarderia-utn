using System;

namespace CapaDatos.Classes
{
    public class ClsChildRelations : Entidad
    {
        public int ID { set; get; }
        public int IDchild { set; get; }
        public String TipoRelacion { set; get; }

        ///Atributos
        ///
        public ClsChildRelations()
        {
            this.ID = 0;
            this.IDchild = 0;
            this.TipoRelacion = "";
        }
    }
}