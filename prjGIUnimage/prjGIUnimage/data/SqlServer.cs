using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.data
{
    class SqlServer : Gdata
    {
        static readonly System.Collections.Hashtable ColComandos = new System.Collections.Hashtable();
        public override string CadenaConexion { get => MCadenaConexion; set => MCadenaConexion = value; }

        // A constructor that supports the complete connection string is defined.

        public SqlServer()
        {
            
        }// end DatosSQLServer


        public SqlServer(string cadenaConexion)
        {
            CadenaConexion = cadenaConexion;
        }// end DatosSQLServer

        /* 
         * Then we will implement CreateConnection, where a new instance of the SqlClient Connection object is simply returned, 
         * using the connection string of the object.
         */
        protected override System.Data.IDbConnection CreateConnection(string cadenaConexion)
        { return new System.Data.SqlClient.SqlConnection(cadenaConexion); }

        protected override System.Data.IDbCommand CommandSql(string commandSql)
        {
            var com = new System.Data.SqlClient.SqlCommand(commandSql, (System.Data.SqlClient.SqlConnection)Conexion, (System.Data.SqlClient.SqlTransaction)MTransaccion);
            return com;
        }// end Comando

        //CreateDataAdapterSql is defined, which uses the Command method to create the necessary command.
        protected override System.Data.IDataAdapter CreateDataAdapterSql(string comandoSql)
        {
            var da = new System.Data.SqlClient.SqlDataAdapter((System.Data.SqlClient.SqlCommand)CommandSql(comandoSql));
            return da;
        } // end CrearDataAdapterSql

        protected override IDbCommand Command(string storedProcedure)
        {
            System.Data.SqlClient.SqlCommand com;
            if (ColComandos.Contains(storedProcedure))
            {
                com = (System.Data.SqlClient.SqlCommand)ColComandos[storedProcedure];
            }
            else
            {
                var con2 = new System.Data.SqlClient.SqlConnection(CadenaConexion);
                con2.Open();
                com = new System.Data.SqlClient.SqlCommand(storedProcedure, con2) { CommandType = System.Data.CommandType.StoredProcedure };
                System.Data.SqlClient.SqlCommandBuilder.DeriveParameters(com);
                con2.Close();
                con2.Dispose();
                ColComandos.Add(storedProcedure, com);
            }//end else
            com.CommandTimeout = 600;
            com.Connection = (System.Data.SqlClient.SqlConnection)Conexion;
            com.Transaction = (System.Data.SqlClient.SqlTransaction)MTransaccion;
            return com;
        }

        protected override void LoadParameters(IDbCommand com, object[] args)
        {
            for (int i = 1; i < com.Parameters.Count; i++)
            {
                var p = (System.Data.SqlClient.SqlParameter)com.Parameters[i];
                p.Value = i <= args.Length ? args[i - 1] ?? DBNull.Value : null;
            } // end for
        }
    }
}
