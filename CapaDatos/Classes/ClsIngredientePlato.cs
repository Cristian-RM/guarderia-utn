using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    public class ClsIngredientePlato : ClsConexion
    {
        public int ID { get; set; }
        public String NombrePlato { get; set; }
        public String nombreIngrediente { get; set; }

        ///Atributos
        ///
        public ClsIngredientePlato()
        {
            this.NombrePlato = "";
            this.nombreIngrediente = "";
        }

        public ClsIngredientePlato(String pNombrePlato, String pnombreIngrediente)
        {
            this.NombrePlato = pNombrePlato;
            this.nombreIngrediente = pnombreIngrediente;
        }
    }
}