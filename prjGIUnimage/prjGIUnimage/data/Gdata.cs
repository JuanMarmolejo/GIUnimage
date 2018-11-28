using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.data
{
    public abstract class Gdata
    {
        #region "Declaración de Variables"

        protected string MServidor = "";
        protected string MBase = "";
        protected string MUsuario = "";
        protected string MPassword = "";
        protected string MCadenaConexion = "";
        protected IDbConnection MConexion;

        #endregion

        #region "Setters y Getters"

        // Nombre del equipo servidor de datos. 
        public string Servidor
        {
            get { return MServidor; }
            set { MServidor = value; }
        } // end Servidor

        // Nombre de la base de datos a utilizar.
        public string Base
        {
            get { return MBase; }
            set { MBase = value; }
        } // end Base

        // Nombre del Usuario de la BD. 
        public string Usuario
        {
            get { return MUsuario; }
            set { MUsuario = value; }
        } // end Usuario

        // Password del Usuario de la BD. 
        public string Password
        {
            get { return MPassword; }
            set { MPassword = value; }
        } // end Password

        // Cadena de conexión completa a la base.
        public abstract string CadenaConexion
        { get; set; }

        #endregion

        #region "Privadas"

        // Crea u obtiene un objeto para conectarse a la base de datos. 
        protected IDbConnection Conexion
        {
            get
            {
                // si aun no tiene asignada la cadena de conexion lo hace
                if (MConexion == null)
                    MConexion = CrearConexion(CadenaConexion);

                // si no esta abierta aun la conexion, lo abre
                if (MConexion.State != ConnectionState.Open)
                    MConexion.Open();

                // retorna la conexion en modo interfaz, para que se adapte a cualquier implementacion de los distintos fabricantes de motores de bases de datos
                return MConexion;
            } // end get
        } // end Conexion

        #endregion

        #region "Lecturas"

        // Executes a Stored Procedure with parameters.
        public void ExecuteStoredProcedure(string storedProcedure, params Object[] args)
        {
            // assign the sql string to the command
            var com = Comando(storedProcedure);
            // load the SP parameters
            CargarParametros(com, args);
            // execute the command
            com.ExecuteNonQuery();

        } // end ExecuteStoredProcedure

        // Obtiene un DataSet a partir de un Procedimiento Almacenado.
        public DataSet GetDataSet(string procedimientoAlmacenado)
        {
            var mDataSet = new DataSet();
            CrearDataAdapter(procedimientoAlmacenado).Fill(mDataSet);
            return mDataSet;
        } // end TraerDataset

        //Obtiene un DataSet a partir de un Procedimiento Almacenado y sus parámetros. 
        public DataSet GetDataSet(string procedimientoAlmacenado, params Object[] args)
        {
            var mDataSet = new DataSet();
            CrearDataAdapter(procedimientoAlmacenado, args).Fill(mDataSet);
            return mDataSet;
        } // end TraerDataset

        // Obtiene un DataSet a partir de un Query Sql.
        public DataSet GetDataSetSql(string comandoSql)
        {
            var mDataSet = new DataSet();
            CrearDataAdapterSql(comandoSql).Fill(mDataSet);
            return mDataSet;
        } // end TraerDataSetSql

        // Obtiene un DataTable a partir de un Procedimiento Almacenado.
        public DataTable GetDataTable(string procedimientoAlmacenado)
        { return GetDataSet(procedimientoAlmacenado).Tables[0].Copy(); } // end TraerDataTable


        //Obtiene un DataSet a partir de un Procedimiento Almacenado y sus parámetros. 
        public DataTable GetDataTable(string procedimientoAlmacenado, params Object[] args)
        { return GetDataSet(procedimientoAlmacenado, args).Tables[0].Copy(); } // end TraerDataTable

        //Obtiene un DataTable a partir de un Query SQL
        public DataTable GetDataTableSql(string comandoSql)
        { return GetDataSetSql(comandoSql).Tables[0].Copy(); } // end TraerDataTableSql

        // Obtiene un DataReader a partir de un Procedimiento Almacenado. 
        public IDataReader GetDataReader(string procedimientoAlmacenado)
        {
            var com = Comando(procedimientoAlmacenado);
            return com.ExecuteReader();
        } // end TraerDataReader 

        // Obtiene un DataReader a partir de un Procedimiento Almacenado y sus parámetros. 
        public IDataReader GetDataReader(string procedimientoAlmacenado, params object[] args)
        {
            var com = Comando(procedimientoAlmacenado);
            CargarParametros(com, args);
            return com.ExecuteReader();
        } // end TraerDataReader

        // Obtiene un DataReader a partir de un Procedimiento Almacenado. 
        public IDataReader GetDataReaderSql(string comandoSql)
        {
            var com = ComandoSql(comandoSql);
            return com.ExecuteReader();
        } // end TraerDataReaderSql 

        // Obtiene un Valor Escalar a partir de un Procedimiento Almacenado. Solo funciona con SP's que tengan
        // definida variables de tipo output, para funciones escalares mas abajo se declara un metodo
        public object GetValueOutput(string procedimientoAlmacenado)
        {
            // asignar el string sql al command
            var com = Comando(procedimientoAlmacenado);
            // ejecutar el command
            com.ExecuteNonQuery();
            // declarar variable de retorno
            Object resp = null;

            // recorrer los parametros del SP
            foreach (IDbDataParameter par in com.Parameters)
                // si tiene parametros de tipo IO/Output retornar ese valor
                if (par.Direction == ParameterDirection.InputOutput || par.Direction == ParameterDirection.Output)
                    resp = par.Value;
            return resp;
        } // end TraerValor

        // Obtiene un Valor a partir de un Procedimiento Almacenado, y sus parámetros. 
        public object GetValueOutput(string procedimientoAlmacenado, params Object[] args)
        {
            // asignar el string sql al command
            var com = Comando(procedimientoAlmacenado);
            // cargar los parametros del SP
            CargarParametros(com, args);
            // ejecutar el command
            com.ExecuteNonQuery();
            // declarar variable de retorno
            Object resp = null;

            // recorrer los parametros del SP
            foreach (IDbDataParameter par in com.Parameters)
                // si tiene parametros de tipo IO/Output retornar ese valor
                if (par.Direction == ParameterDirection.InputOutput || par.Direction == ParameterDirection.Output)
                    resp = par.Value;
            return resp;
        } // end TraerValor

        // Obtiene un Valor Escalar a partir de un Procedimiento Almacenado. 
        public object GetValueOutputSql(string comadoSql)
        {
            // asignar el string sql al command
            var com = ComandoSql(comadoSql);
            // ejecutar el command
            com.ExecuteNonQuery();
            // declarar variable de retorno
            Object resp = null;

            // recorrer los parametros del Query (uso tipico envio de varias sentencias sql en el mismo command)
            foreach (IDbDataParameter par in com.Parameters)
                // si tiene parametros de tipo IO/Output retornar ese valor
                if (par.Direction == ParameterDirection.InputOutput || par.Direction == ParameterDirection.Output)
                    resp = par.Value;
            return resp;
        } // end TraerValor

        // Obtiene un Valor de una funcion Escalar a partir de un Procedimiento Almacenado. 
        public object GetScalarValue(string procedimientoAlmacenado)
        {
            var com = Comando(procedimientoAlmacenado);
            return com.ExecuteScalar();
        } // end TraerValorEscalar

        /// Obtiene un Valor de una funcion Escalar a partir de un Procedimiento Almacenado, con Params de Entrada
        public object GetScalarValue(string procedimientoAlmacenado, params object[] args)
        {
            var com = Comando(procedimientoAlmacenado);
            CargarParametros(com, args);
            return com.ExecuteScalar();
        } // end TraerValorEscalar

        // Obtiene un Valor de una funcion Escalar a partir de un Query SQL
        public object GetScalarValueSql(string comandoSql)
        {
            var com = ComandoSql(comandoSql);
            return com.ExecuteScalar();
        } // end TraerValorEscalarSql

        #endregion

        #region "Acciones"

        protected abstract IDbConnection CrearConexion(string cadena);
        protected abstract IDbCommand Comando(string procedimientoAlmacenado);
        protected abstract IDbCommand ComandoSql(string comandoSql);
        protected abstract IDataAdapter CrearDataAdapter(string procedimientoAlmacenado, params Object[] args);
        protected abstract IDataAdapter CrearDataAdapterSql(string comandoSql);
        protected abstract void CargarParametros(IDbCommand comando, Object[] args);

        // metodo sobrecargado para autenticarse contra el motor de BBDD
        public bool Autenticar()
        {
            if (Conexion.State != ConnectionState.Open)
                Conexion.Open();
            return true;
        }// end Autenticar

        // metodo sobrecargado para autenticarse contra el motor de BBDD
        public bool Autenticar(string vUsuario, string vPassword)
        {
            MUsuario = vUsuario;
            MPassword = vPassword;
            MConexion = CrearConexion(CadenaConexion);

            MConexion.Open();
            return true;
        }// end Autenticar

        // cerrar conexion
        public void CerrarConexion()
        {
            if (Conexion.State != ConnectionState.Closed)
                MConexion.Close();
        }

        // end CerrarConexion

        // Ejecuta un Procedimiento Almacenado en la base. 
        public int Ejecutar(string procedimientoAlmacenado)
        { return Comando(procedimientoAlmacenado).ExecuteNonQuery(); } // end Ejecutar

        // Ejecuta un query sql
        public int RunSql(string comandoSql)
        { return ComandoSql(comandoSql).ExecuteNonQuery(); } // end Ejecutar

        //Ejecuta un Procedimiento Almacenado en la base, utilizando los parámetros. 
        public int Ejecutar(string procedimientoAlmacenado, params Object[] args)
        {
            var com = Comando(procedimientoAlmacenado);
            CargarParametros(com, args);
            var resp = com.ExecuteNonQuery();
            for (var i = 0; i < com.Parameters.Count; i++)
            {
                var par = (IDbDataParameter)com.Parameters[i];
                if (par.Direction == ParameterDirection.InputOutput || par.Direction == ParameterDirection.Output)
                    args.SetValue(par.Value, i - 1);
            }// end for
            return resp;
        } // end Ejecutar

        #endregion

        #region "Transacciones"

        protected IDbTransaction MTransaccion;
        protected bool EnTransaccion;

        //Comienza una Transacción en la base en uso. 
        public void IniciarTransaccion()
        {
            try
            {
                MTransaccion = Conexion.BeginTransaction();
                EnTransaccion = true;
            }// end try
            finally
            { EnTransaccion = false; }
        }// end IniciarTransaccion


        //Confirma la transacción activa. 
        public void TerminarTransaccion()
        {
            try
            { MTransaccion.Commit(); }
            finally
            {
                MTransaccion = null;
                EnTransaccion = false;
            }// end finally
        }// end TerminarTransaccion


        //Cancela la transacción activa.
        public void AbortarTransaccion()
        {
            try
            { MTransaccion.Rollback(); }
            finally
            {
                MTransaccion = null;
                EnTransaccion = false;
            }// end finally
        }// end AbortarTransaccion


        #endregion

    }// end class gDatos

    //public abstract class Gdata
    //{
    //    protected string MCadenaConexion = "";
    //    protected IDbConnection MConexion;

    //    // Complete connection string to the base.
    //    public abstract string CadenaConexion
    //    { get; set; }

    //    // Create or obtain an object to connect to the database.
    //    protected IDbConnection Conexion
    //    {
    //        get
    //        {
    //            // If you have not yet assigned the connection string, it does
    //            if (MConexion == null)
    //                MConexion = CreateConnection(CadenaConexion);

    //            // if the connection is not open, open it
    //            if (MConexion.State != ConnectionState.Open)
    //                MConexion.Open();

    //            // returns the connection in interface mode, so that it adapts to any implementation of the different database engine manufacturers
    //            return MConexion;
    //        } // end get
    //    } // end Conexion

    //    // Obtain a DataSet from a Stored Procedure.
    //    public DataSet BringDataSet(string StoredProcedure)
    //    {
    //        var mDataSet = new DataSet();
    //        CreateDataAdapterSql(StoredProcedure).Fill(mDataSet);
    //        return mDataSet;
    //    } // end TraerDataset

    //    // Executes a Stored Procedure with parameters.
    //    public void ExecuteStoredProcedure(string storedProcedure, params Object[] args)
    //    {
    //        // assign the sql string to the command
    //        var com = Command(storedProcedure);
    //        // load the SP parameters
    //        LoadParameters(com, args);
    //        // execute the command
    //        com.ExecuteNonQuery();

    //    } // end ExecuteStoredProcedure

    //    internal DataTable GetDataTableSql(string comandoSql)
    //    {
    //        return BringDataSetSql(comandoSql).Tables[0].Copy();
    //    }

    //    private DataSet BringDataSetSql(string comandoSql)
    //    {
    //        var mDataSet = new DataSet();
    //        CreateDataAdapterSql(comandoSql).Fill(mDataSet);
    //        return mDataSet;
    //    }

    //    protected abstract IDbConnection CreateConnection(string cadena);
    //    protected abstract IDbCommand Command(string storedProcedure);
    //    protected abstract IDbCommand CommandSql(string commandSql);
    //    protected abstract IDataAdapter CreateDataAdapterSql(string commandSql);
    //    protected abstract void LoadParameters(IDbCommand command, Object[] args);

    //    // overloaded method to authenticate against the DB engine
    //    public bool Authenticate()
    //    {
    //        if (Conexion.State != ConnectionState.Open)
    //            Conexion.Open();
    //        return true;
    //    }// end Authenticate

    //    // cerrar conexion
    //    public void CloseConexion()
    //    {
    //        if (Conexion.State != ConnectionState.Closed)
    //            MConexion.Close();
    //    }

    //    // end CerrarConexion

    //    // Ejecuta un query sql

    //    public int RunSql(string comandoSql)
    //    { return CommandSql(comandoSql).ExecuteNonQuery(); } // end Ejecutar

    //    // Obtiene un Valor de una funcion Escalar a partir de un Query SQL
    //    public object GetScalarValueSql(string comandoSql)
    //    {
    //        var com = CommandSql(comandoSql);
    //        return com.ExecuteScalar();
    //    } // end TraerValorEscalarSql

    //    protected IDbTransaction MTransaccion;

    //    internal DataSet BringDataSet(string storedProcedure, params Object[] args)
    //    {
    //        var com = Command(storedProcedure);
    //        var myDataSet = new DataSet();
    //        IDbDataAdapter myAd = (IDbDataAdapter)CreateDataAdapterSql("");
    //        // load the SP parameters
    //        LoadParameters(com, args);
    //        myAd.SelectCommand = com;
    //        myAd.Fill(myDataSet);
    //        return myDataSet;
    //    }
    //}
}
