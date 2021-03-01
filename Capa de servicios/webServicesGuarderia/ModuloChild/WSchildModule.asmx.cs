using CapaDatos.Classes;
using CapaDatos.Controlladores;
using System;
using System.Collections.Generic;
using System.Data;
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

        [WebMethod]
        public List<DataTable> crudChildRelations(int ID, int IDChild, String TipoRelacion, string op)
        {
            //Hago un objeto de la clase correspondiente
            ClsChildRelations childRelation = new ClsChildRelations();
            childRelation.ID = ID;
            childRelation.IDchild = IDChild;
            childRelation.TipoRelacion = TipoRelacion;

            //Genero un controladro
            childRelationController childC = new childRelationController(childRelation);
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

        [WebMethod]
        public List<DataTable> crudEncargados(int ID, int IDChildRelations, String DNI, String Nombre,
            String Direccion, String Telefono, string op)
        {
            //Hago un objeto de la clase correspondiente
            ClsEncargados encargado = new ClsEncargados();
            encargado.ID = ID;
            encargado.DNI = DNI;
            encargado.IDchildRelation = IDChildRelations;
            encargado.Nombre = Nombre;
            encargado.Direccion = Direccion;
            encargado.Telefono = Telefono;

            //Genero un controladro
            encargadoController encargadoC = new encargadoController(encargado);
            encargadoC.operacion = op;

            //Realizo la operacion y guardo el mensaje de error en una variable JUNTO CON EL NUMERO DE ERROR
            String mensajeErr = encargadoC.crud();
            int nummErr = encargadoC.numError;

            DataTable infoTable = CreateinfoTable(mensajeErr, nummErr);
            DataTable datos = encargadoC.dataTable;
            datos.TableName = "datos";

            result = new List<DataTable>();
            result.Add(datos);
            result.Add(infoTable);

            return result;
        }

        [WebMethod]
        public List<DataTable> crudAbonado(int ID, int IDChildRelations,
         String DNI, String Nombre, String Direccion, String Telefono, String Banco, String CuentaIBAM, string op)
        {
            //Hago un objeto de la clase correspondiente
            ClsAbonados abonado = new ClsAbonados();
            abonado.ID = ID;
            abonado.DNI = DNI;
            abonado.IDchildRelation = IDChildRelations;
            abonado.Nombre = Nombre;
            abonado.Direccion = Direccion;
            abonado.Telefono = Telefono;
            abonado.Banco = Banco;
            abonado.CuentaIBAM = CuentaIBAM;

            //Genero un controladro
            abonadosController abonadoC = new abonadosController(abonado);
            abonadoC.operacion = op;

            //Realizo la operacion y guardo el mensaje de error en una variable JUNTO CON EL NUMERO DE ERROR
            String mensajeErr = abonadoC.crud();
            int nummErr = abonadoC.numError;

            DataTable infoTable = CreateinfoTable(mensajeErr, nummErr);
            DataTable datos = abonadoC.dataTable;
            datos.TableName = "datos";

            result = new List<DataTable>();
            result.Add(datos);
            result.Add(infoTable);

            return result;
        }

        [WebMethod]
        public List<DataTable> crudAlergias(int ID, int IDchild, String NombreIngrediente, String op)
        {
            //Hago un objeto de la clase correspondiente
            ClsAlergias alergia = new ClsAlergias();
            alergia.ID = ID;
            alergia.NombreIngrediente = NombreIngrediente;
            alergia.IDchild = IDchild;

            //Genero un controladro
            alergiasController alergiaC = new alergiasController(alergia);
            alergiaC.operacion = op;

            //Realizo la operacion y guardo el mensaje de error en una variable JUNTO CON EL NUMERO DE ERROR
            String mensajeErr = alergiaC.crud();
            int nummErr = alergiaC.numError;

            DataTable infoTable = CreateinfoTable(mensajeErr, nummErr);
            DataTable datos = alergiaC.dataTable;
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