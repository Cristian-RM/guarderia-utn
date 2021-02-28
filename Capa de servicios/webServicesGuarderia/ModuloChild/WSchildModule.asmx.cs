using CapaDatos.Classes;
using CapaDatos.Controlladores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace webServicesGuarderia.ModuloChild
{
    /// <summary>
    /// Descripción breve de WSchildModule
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
    // [System.Web.Script.Services.ScriptService]
    public class WSchildModule : System.Web.Services.WebService
    {
        private List<DataTable> result = new List<DataTable>();

        [WebMethod]
        public List<DataTable> crudChild(int IDmatricula, String nombre, String FechaRegistro, String FechaNacimiento, string op)
        {
            //Hago un objeto de la clase correspondiente
            ClsChilds child = new ClsChilds();
            child.IDmatricula = IDmatricula;
            child.Nombre = nombre;
            child.FEchaNacimiento = FechaNacimiento;
            child.FechaRegistro = FechaRegistro;

            //Genero un controladro
            childController childC = new childController(child);
            childC.operacion = op;

            //Realizo la operacion y guardo el mensaje de error en una variable JUNTO CON EL NUMERO DE ERROR
            String mensajeErr = childC.crud();
            int nummErr = childC.numError;

            DataTable infoTable = CreateinfoTable(mensajeErr, nummErr);
            DataTable datos = childC.dataTable;
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