using CapaDatos.Classes;
using CapaDatos.Controlladores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace webServicesGuarderia.Facturacion
{
    /// <summary>
    /// Descripción breve de WSFactuModule
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
    // [System.Web.Script.Services.ScriptService]
    public class WSFactuModule : System.Web.Services.WebService
    {
        private List<DataTable> result = new List<DataTable>();

        [WebMethod]
        public List<DataTable> crudmfactura(int ID, int IDAbonado, String mes, String FechaCreacion, string op)
        {
            //Hago un objeto de la clase correspondiente
            ClsMfacturas fact = new ClsMfacturas();
            fact.ID = ID;
            fact.IDabonado = IDAbonado;
            fact.Mes = mes;
            fact.FechaCreacion = FechaCreacion;

            //Genero un controladro
            mfacturaController factC = new mfacturaController(fact);
            factC.operacion = op;

            //Realizo la operacion y guardo el mensaje de error en una variable JUNTO CON EL NUMERO DE ERROR
            String mensajeErr = factC.crud();
            int nummErr = factC.numError;

            DataTable infoTable = CreateinfoTable(mensajeErr, nummErr);
            DataTable datos = factC.dataTable;
            datos.TableName = "datos";
            result = new List<DataTable>();
            result.Add(datos);
            result.Add(infoTable);

            return result;
        }

        [WebMethod]
        public List<DataTable> crudDetalleAsistencia(int ID, int IDFactura, int IDAsistencia, String FechaCreacion, string op)
        {
            //Hago un objeto de la clase correspondiente
            ClsDetalleAsistencia deAsis = new ClsDetalleAsistencia();
            deAsis.ID = ID;
            deAsis.IDfactura = IDFactura;
            deAsis.IDasistencia = IDAsistencia;
            deAsis.FechaCreacion = FechaCreacion;

            //Genero un controladro
            detalleAsistenciaController deAsisC = new detalleAsistenciaController(deAsis);
            deAsisC.operacion = op;

            //Realizo la operacion y guardo el mensaje de error en una variable JUNTO CON EL NUMERO DE ERROR
            String mensajeErr = deAsisC.crud();
            int nummErr = deAsisC.numError;

            DataTable infoTable = CreateinfoTable(mensajeErr, nummErr);
            DataTable datos = deAsisC.dataTable;
            datos.TableName = "datos";
            result = new List<DataTable>();
            result.Add(datos);
            result.Add(infoTable);

            return result;
        }

        [WebMethod]
        public List<DataTable> crudAsistencia(int ID, int idChild, String FechaRegistro, string mes,
            string horaEntrada, String horaSalida, string detalles, byte Cancelado, string op)
        {
            //Hago un objeto de la clase correspondiente
            ClsAsisntencias Asis = new ClsAsisntencias();
            Asis.ID = ID;
            Asis.IDchild = idChild;
            Asis.FechaRegistro = FechaRegistro;
            Asis.Mes = mes;
            Asis.HoraEntrada = horaEntrada;
            Asis.HoraSalida = horaSalida;
            Asis.Detalles = detalles;
            Asis.SNCANCELADO = Cancelado;

            //Genero un controladro
            AsistenciaController AsisC = new AsistenciaController(Asis);
            AsisC.operacion = op;

            //Realizo la operacion y guardo el mensaje de error en una variable JUNTO CON EL NUMERO DE ERROR
            String mensajeErr = AsisC.crud();
            int nummErr = AsisC.numError;

            DataTable infoTable = CreateinfoTable(mensajeErr, nummErr);
            DataTable datos = AsisC.dataTable;
            datos.TableName = "datos";
            result = new List<DataTable>();
            result.Add(datos);
            result.Add(infoTable);

            return result;
        }

        [WebMethod]
        public List<DataTable> crudDetalleConsumos(int ID, int IDcosumo, int IDfactura, String fechaCreacion, string op)
        {
            //Hago un objeto de la clase correspondiente
            ClsDetalleConsumos deCon = new ClsDetalleConsumos();
            deCon.ID = ID;
            deCon.IDconsumo = IDcosumo;
            deCon.IDfactura = IDfactura;
            deCon.FechaCreacion = fechaCreacion;

            //Genero un controladro
            detalleConsumoController deConC = new detalleConsumoController(deCon);
            deConC.operacion = op;

            //Realizo la operacion y guardo el mensaje de error en una variable JUNTO CON EL NUMERO DE ERROR
            String mensajeErr = deConC.crud();
            int nummErr = deConC.numError;

            DataTable infoTable = CreateinfoTable(mensajeErr, nummErr);
            DataTable datos = deConC.dataTable;
            datos.TableName = "datos";
            result = new List<DataTable>();
            result.Add(datos);
            result.Add(infoTable);

            return result;
        }

        [WebMethod]
        public List<DataTable> crudConsumos(int ID, int IDChild, int IDMenu, string FechaConsumo, int cancelado, string op)
        {
            //Hago un objeto de la clase correspondiente
            ClsConsumos deCon = new ClsConsumos();
            deCon.ID = ID;
            deCon.IDchild = IDChild;
            deCon.Idmenu = IDMenu;
            deCon.FechaConsumo = FechaConsumo;
            deCon.SnCancelado = cancelado;

            //Genero un controladro
            consumoController deCoC = new consumoController(deCon);
            deCoC.operacion = op;

            //Realizo la operacion y guardo el mensaje de error en una variable JUNTO CON EL NUMERO DE ERROR
            String mensajeErr = deCoC.crud();
            int nummErr = deCoC.numError;

            DataTable infoTable = CreateinfoTable(mensajeErr, nummErr);
            DataTable datos = deCoC.dataTable;
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