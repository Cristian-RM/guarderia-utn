using System;

namespace CapaDatos.Classes
{
    public class ClsDetalleConsumos : Entidad
    {
        public int ID;
        public int IDfactura;
        public int IDconsumo;
        public String FechaCreacion;

        ///Atributos
        ///
        public ClsDetalleConsumos()
        {
            this.ID = 0;
            this.IDfactura = 0;
            this.IDconsumo = 0;
            this.FechaCreacion = "";
        }
    }
}