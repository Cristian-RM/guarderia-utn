using System;

namespace CapaDatos.Classes
{
    public class Entidad
    {
        public Entidad()
        {
        }

        public byte isactivoBYTE(string valor)
        {
            valor = valor.ToLower();
            if (valor == "1" || valor == "s" || valor == "activo" || valor == "enable" || valor == "si")
            {
                return 1;
            }
            return 0;
        }

        public byte isactivoBYTE(bool valor)
        {
            if (valor)
            {
                return 1;
            }
            return 0;
        }

        public Boolean isactivoBOOLEAN(string valor)
        {
            valor = valor.ToLower();
            if (valor == "1" || valor == "s" || valor == "activo" || valor == "enable" || valor == "true")
            {
                return true;
            }
            return false;
        }
    }
}