using System;

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