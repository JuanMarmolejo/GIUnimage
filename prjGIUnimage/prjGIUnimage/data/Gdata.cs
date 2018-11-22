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
        protected string MCadenaConexion = "";
        protected IDbConnection MConexion;

        // Complete connection string to the base.
        public abstract string CadenaConexion
        { get; set; }

        // Create or obtain an object to connect to the database.
        protected IDbConnection Conexion
        {
            get
            {
                // If you have not yet assigned the connection string, it does
                if (MConexion == null)
                    MConexion = CreateConnection(CadenaConexion);

                // if the connection is not open, open it
                if (MConexion.State != ConnectionState.Open)
                    MConexion.Open();

                // returns the connection in interface mode, so that it adapts to any implementation of the different database engine manufacturers
                return MConexion;
            } // end get
        } // end Conexion

        // Obtain a DataSet from a Stored Procedure.
        public DataSet BringDataSet(string StoredProcedure)
        {
            var mDataSet = new DataSet();
            CreateDataAdapterSql(StoredProcedure).Fill(mDataSet);
            return mDataSet;
        } // end TraerDataset

        // Executes a Stored Procedure with parameters.
        public void ExecuteStoredProcedure(string storedProcedure, params Object[] args)
        {
            // assign the sql string to the command
            var com = Command(storedProcedure);
            // load the SP parameters
            LoadParameters(com, args);
            // execute the command
            com.ExecuteNonQuery();

        } // end ExecuteStoredProcedure

        internal DataTable BringDataTableSql(string comandoSql)
        {
            return BringDataSetSql(comandoSql).Tables[0].Copy();
        }

        private DataSet BringDataSetSql(string comandoSql)
        {
            var mDataSet = new DataSet();
            CreateDataAdapterSql(comandoSql).Fill(mDataSet);
            return mDataSet;
        }

        protected abstract IDbConnection CreateConnection(string cadena);
        protected abstract IDbCommand Command(string storedProcedure);
        protected abstract IDbCommand CommandSql(string commandSql);
        protected abstract IDataAdapter CreateDataAdapterSql(string commandSql);
        protected abstract void LoadParameters(IDbCommand command, Object[] args);

        // overloaded method to authenticate against the DB engine
        public bool Authenticate()
        {
            if (Conexion.State != ConnectionState.Open)
                Conexion.Open();
            return true;
        }// end Authenticate

        // cerrar conexion
        public void CloseConexion()
        {
            if (Conexion.State != ConnectionState.Closed)
                MConexion.Close();
        }

        // end CerrarConexion

        // Ejecuta un query sql

        public int RunSql(string comandoSql)
        { return CommandSql(comandoSql).ExecuteNonQuery(); } // end Ejecutar

        // Obtiene un Valor de una funcion Escalar a partir de un Query SQL
        public object BringScalarValueSql(string comandoSql)
        {
            var com = CommandSql(comandoSql);
            return com.ExecuteScalar();
        } // end TraerValorEscalarSql

        protected IDbTransaction MTransaccion;

        internal DataSet BringDataSet(string storedProcedure, params Object[] args)
        {
            var com = Command(storedProcedure);
            var myDataSet = new DataSet();
            IDbDataAdapter myAd = (IDbDataAdapter)CreateDataAdapterSql("");
            // load the SP parameters
            LoadParameters(com, args);
            myAd.SelectCommand = com;
            myAd.Fill(myDataSet);
            return myDataSet;
        }
    }
}
