using CapaDatos.Classes;
using CapaDatos.Controlladores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;

namespace webServicesGuarderia.ModuloComidas
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        private List<DataTable> result = new List<DataTable>();

        [WebMethod]
        public List<DataTable> crudMenus(int ID, String nombre, Decimal Precio, string op)
        {
            //Hago un objeto de la clase correspondiente
            ClsMenus menu = new ClsMenus();
            menu.ID = ID;
            menu.Nombre = nombre;
            menu.Precio = Precio;

            //Genero un controladro
            menusController menuC = new menusController(menu);
            menuC.operacion = op;

            //Realizo la operacion y guardo el mensaje de error en una variable JUNTO CON EL NUMERO DE ERROR
            String mensajeErr = menuC.crud();
            int nummErr = menuC.numError;

            DataTable infoTable = CreateinfoTable(mensajeErr, nummErr);
            DataTable datos = menuC.dataTable;
            datos.TableName = "datos";
            result = new List<DataTable>();
            result.Add(datos);
            result.Add(infoTable);

            return result;
        }

        [WebMethod]
        public List<DataTable> crudIngredientes(String nombre, string op)
        {
            //Hago un objeto de la clase correspondiente
            ClsIngredientes ingre = new ClsIngredientes();
            ingre.Nombre = nombre;

            //Genero un controladro
            ingredienteController ingreC = new ingredienteController(ingre);
            ingreC.operacion = op;

            //Realizo la operacion y guardo el mensaje de error en una variable JUNTO CON EL NUMERO DE ERROR
            String mensajeErr = ingreC.crud();
            int nummErr = ingreC.numError;

            DataTable infoTable = CreateinfoTable(mensajeErr, nummErr);
            DataTable datos = ingreC.dataTable;
            datos.TableName = "datos";
            result = new List<DataTable>();
            result.Add(datos);
            result.Add(infoTable);

            return result;
        }

        [WebMethod]
        public List<DataTable> crudIngredientePlato(int ID, String nombrePlato, string nombreIngrediente, string op)
        {
            //Hago un objeto de la clase correspondiente
            ClsIngredientePlato ingrePla = new ClsIngredientePlato();
            ingrePla.ID = ID;
            ingrePla.NombrePlato = nombrePlato;
            ingrePla.nombreIngrediente = nombreIngrediente;

            //Genero un controladro
            ingredientePlatoController ingrePlaC = new ingredientePlatoController(ingrePla);
            ingrePlaC.operacion = op;

            //Realizo la operacion y guardo el mensaje de error en una variable JUNTO CON EL NUMERO DE ERROR
            String mensajeErr = ingrePlaC.crud();
            int nummErr = ingrePlaC.numError;

            DataTable infoTable = CreateinfoTable(mensajeErr, nummErr);
            DataTable datos = ingrePlaC.dataTable;
            datos.TableName = "datos";
            result = new List<DataTable>();
            result.Add(datos);
            result.Add(infoTable);

            return result;
        }

        [WebMethod]
        public List<DataTable> crudPlatos(String nombre, string op)
        {
            //Hago un objeto de la clase correspondiente
            ClasPlato plato = new ClasPlato();
            plato.Nombre = nombre;

            //Genero un controladro
            platosController platoC = new platosController(plato);
            platoC.operacion = op;

            //Realizo la operacion y guardo el mensaje de error en una variable JUNTO CON EL NUMERO DE ERROR
            String mensajeErr = platoC.crud();
            int nummErr = platoC.numError;

            DataTable infoTable = CreateinfoTable(mensajeErr, nummErr);
            DataTable datos = platoC.dataTable;
            datos.TableName = "datos";
            result = new List<DataTable>();
            result.Add(datos);
            result.Add(infoTable);

            return result;
        }

        [WebMethod]
        public List<DataTable> crudPlatosMenu(int ID, String nombrePlato, int IDmenu, string op)
        {
            //Hago un objeto de la clase correspondiente
            ClsPlatosMenu platomenu = new ClsPlatosMenu();
            platomenu.ID = ID;
            platomenu.NombrePlato = nombrePlato;
            platomenu.IDmenu = IDmenu;

            //Genero un controladro
            platoMenuController platomenuC = new platoMenuController(platomenu);
            platomenuC.operacion = op;

            //Realizo la operacion y guardo el mensaje de error en una variable JUNTO CON EL NUMERO DE ERROR
            String mensajeErr = platomenuC.crud();
            int nummErr = platomenuC.numError;

            DataTable infoTable = CreateinfoTable(mensajeErr, nummErr);
            DataTable datos = platomenuC.dataTable;
            datos.TableName = "datos";
            result = new List<DataTable>();
            result.Add(datos);
            result.Add(infoTable);

            return result;
        }

        private DataTable CreateinfoTable(string mensajeErr, int numErr)
        {
            DataTable infoTable = new DataTable("infoTable");
            //Añadimos una columna para el mensaje de error
            infoTable.Columns.Add("mensajeDeError");
            //Añadimos una columna para el número de error
            infoTable.Columns.Add("numeroDeError");

            DataRow row = infoTable.NewRow();
            row["mensajeDeError"] = mensajeErr;
            row["numeroDeError"] = numErr;
            infoTable.Rows.Add(row);

            return infoTable;
        }
    }
}