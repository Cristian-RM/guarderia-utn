namespace CapaDatos.Classes
{
    public class clsOperacion
    {
        public string insertar { get; }
        public string delete { get; }
        public string update { get; }
        public string gets { get; }
        public string list { get; }
        public string listarDetalles { get; }
        public string listarPedientes { get; }
        public string buscar { get; }

        public clsOperacion()
        {
            insertar = "i";
            delete = "d";
            update = "u";
            gets = "g";
            list = "l";
            buscar = "b";
            listarDetalles = "lb";
            listarPedientes = "lp";
        }

        public bool isconsulta(string op)
        {
            if (op == "i" || op == "u" || op == "d")
            {
                return false;
            }
            else
            if (op == "g" || op == "b" || op == "l" || op == "lb" || op == "lp")
            {
                return true;
            }
            return false;
        }
    }
}