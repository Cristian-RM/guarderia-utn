using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    public class clsOperacion
    {
        public string insertar { get; }
        public string delete { get; }
        public string update { get; }
        public string gets { get; }
        public string list { get; }
        public string listarContrataciones { get; }
        public string buscar { get; }

        public clsOperacion()
        {
            insertar = "i";
            delete = "d";
            update = "u";
            gets = "g";
            list = "l";
            buscar = "b";
            listarContrataciones = "lb";
        }
    }
}