using System;

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