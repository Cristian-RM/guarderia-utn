using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    public class ClsAsisntencias : Entidad
    {
        public int ID { get; set; }
        public int IDchild { get; set; }
        public String FechaRegistro { get; set; }
        public String Mes { get; set; }
        public String HoraEntrada { get; set; }
        public String HoraSalida { get; set; }
        public String Detalles { get; set; }
        public Byte SNCANCELADO { get; set; }
        ///Atributos
        ///
        public ClsAsisntencias()
        {
            this.ID = 0;
            this.IDchild = 0;
            this.FechaRegistro = "";
            this.Mes = "";
            this.HoraEntrada = "";
            this.HoraSalida = "";
            this.Detalles = "";
            this.SNCANCELADO = 1;
        }
    }
}
