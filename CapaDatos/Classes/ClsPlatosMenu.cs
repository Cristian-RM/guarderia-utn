using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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