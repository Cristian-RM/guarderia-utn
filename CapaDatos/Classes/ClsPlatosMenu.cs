using System;

namespace CapaDatos.Classes
{
    public class ClsPlatosMenu : Entidad
    {
        public int ID;
        public String NombrePlato;
        public int IDmenu;

        ///Atributos
        ///
        public ClsPlatosMenu()
        {
            this.ID = 0;
            this.NombrePlato = "";
            this.IDmenu = 0;
        }
    }
}