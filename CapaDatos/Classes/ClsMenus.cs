using System;

namespace CapaDatos.Classes
{
    public class ClsMenus : Entidad
    {
        public int ID;
        public String Nombre;
        public decimal Precio;

        ///Atributos
        ///
        public ClsMenus()
        {
            this.ID = 0;
            this.Nombre = "";
            this.Precio = 0;
        }
    }
}