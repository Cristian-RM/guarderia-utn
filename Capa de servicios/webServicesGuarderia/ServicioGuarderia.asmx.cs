using CapaDatos.Classes;
using CapaDatos.Controlladores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace webServicesGuarderia
{
    /// <summary>
    /// Descripción breve de ServicioGuarderia
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
    // [System.Web.Script.Services.ScriptService]

    public class ServicioGuarderia : System.Web.Services.WebService
    {
        public String mensajeErr = "";
        public int nummErr = 0;

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
            mensajeErr = factC.crud();
            nummErr = factC.numError;

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

        [WebMethod]
        public DataTable crudChild(int IDmatricula, String nombre, String FechaRegistro, String FechaNacimiento, string op)
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

            DataTable datos = new DataTable();
            if (childC.isconsulta(op))
            {
                datos = childC.dataTable;
                datos.TableName = "datos";
            }
            else
            {
                datos = CreateinfoTable(mensajeErr, nummErr);
            }

            return datos;
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
            row["numeroDeError"] = Convert.ToString(numErr);
            infoTable.Rows.Add(row);

            return infoTable;
        }
    }
}