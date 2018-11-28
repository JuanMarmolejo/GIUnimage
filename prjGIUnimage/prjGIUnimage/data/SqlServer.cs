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

        /*
         * Continuaremos con el método Comando, procediendo de igual forma que en los anteriores. 
         * En este caso, además, implementaremos un mecanismo de “preservación” de los Comandos creados,
         * para acelerar su utilización. Esto es, cada procedimiento que sea accedido, se guardará 
         * en memoria hasta que la instancia del objeto se destruya. Para ello, declararemos una variable 
         * como HashTable para la clase, con el modificador Shared (compartida) que permite 
         * persistir la misma entre creaciones de objetos
         */
        static readonly System.Collections.Hashtable ColComandos = new System.Collections.Hashtable();


        public override sealed string CadenaConexion
        {
            get
            {
                if (MCadenaConexion.Length == 0)
                {
                    if (MBase.Length != 0 && MServidor.Length != 0)
                    {
                        var sCadena = new System.Text.StringBuilder("");
                        sCadena.Append("data source=<SERVIDOR>;");
                        sCadena.Append("initial catalog=<BASE>;");
                        sCadena.Append("user id=<USER>;");
                        sCadena.Append("password=<PASSWORD>;");
                        sCadena.Append("persist security info=True;");
                        sCadena.Append("user id=sa;packet size=4096");
                        sCadena.Replace("<SERVIDOR>", Servidor);
                        sCadena.Replace("<BASE>", Base);
                        sCadena.Replace("<USER>", Usuario);
                        sCadena.Replace("<PASSWORD>", Password);

                        return sCadena.ToString();
                    }
                    throw new Exception("No se puede establecer la cadena de conexión en la clase DatosSQLServer");
                }
                return MCadenaConexion = CadenaConexion;

            }// end get
            set
            { MCadenaConexion = value; } // end set
        }// end CadenaConexion


        /*	
         * Agregue ahora la definición del procedimiento CargarParametros, el cual deberá asignar cada valor 
         * al parámetro que corresponda (considerando que, en el caso de SQLServer©, el parameter 0 
         * siempre corresponde al “return Value” del Procedimiento Almacenado). Por otra parte, en algunos casos,
         * como la ejecución de procedimientos almacenados que devuelven un valor como parámetro de salida, 
         * la cantidad de elementos en el vector de argumentos, puede no corresponder con la cantidad de parámetros. 
         * Por ello, se decide comparar el indicador con la cantidad de argumentos recibidos, antes de asignar el valor.
         * protected override void CargarParametros(System.Data.IDbCommand Com, System.Object[] Args)
         */
        protected override void CargarParametros(System.Data.IDbCommand com, Object[] args)
        {
            for (int i = 1; i < com.Parameters.Count; i++)
            {
                var p = (System.Data.SqlClient.SqlParameter)com.Parameters[i];
                p.Value = i <= args.Length ? args[i - 1] ?? DBNull.Value : null;
            } // end for
        } // end CargarParametros


        /*
         * En el procedimiento Comando, se buscará primero si ya existe el comando en dicha Hashtable para retornarla 
         * (convertida en el tipo correcto). Caso contrario, se procederá a la creación del mismo, 
         * y su agregado en el repositorio. Dado que cabe la posibilidad de que ya estemos dentro de una transacción,
         * es necesario abrir una segunda conexión a la base de datos, para obtener la definición de los parámetros 
         * del procedimiento Almacenado (caso contrario da error, por intentar leer sin tener asignado el
         * objeto Transaction correspondiente). Además, el comando, obtenido por cualquiera de los mecanismos 
         * debe recibir la conexión y la transacción correspondientes (si no hay Transacción, la variable es null, 
         * y ese es el valor que se le pasa al objeto Command)
         */
        protected override System.Data.IDbCommand Comando(string procedimientoAlmacenado)
        {
            System.Data.SqlClient.SqlCommand com;
            if (ColComandos.Contains(procedimientoAlmacenado))
                com = (System.Data.SqlClient.SqlCommand)ColComandos[procedimientoAlmacenado];
            else
            {
                var con2 = new System.Data.SqlClient.SqlConnection(CadenaConexion);
                con2.Open();
                com = new System.Data.SqlClient.SqlCommand(procedimientoAlmacenado, con2) { CommandType = System.Data.CommandType.StoredProcedure };
                System.Data.SqlClient.SqlCommandBuilder.DeriveParameters(com);
                con2.Close();
                con2.Dispose();
                ColComandos.Add(procedimientoAlmacenado, com);
            }//end else
            com.Connection = (System.Data.SqlClient.SqlConnection)Conexion;
            com.Transaction = (System.Data.SqlClient.SqlTransaction)MTransaccion;
            return com;
        }// end Comando

        protected override System.Data.IDbCommand ComandoSql(string comandoSql)
        {
            var com = new System.Data.SqlClient.SqlCommand(comandoSql, (System.Data.SqlClient.SqlConnection)Conexion, (System.Data.SqlClient.SqlTransaction)MTransaccion);
            return com;
        }// end Comando


        /* 
         * Luego implementaremos CrearConexion, donde simplemente se devuelve una nueva instancia del 
         * objeto Conexión de SqlClient, utilizando la cadena de conexión del objeto.
         */
        protected override System.Data.IDbConnection CrearConexion(string cadenaConexion)
        { return new System.Data.SqlClient.SqlConnection(cadenaConexion); }


        //Finalmente, es el turno de definir CrearDataAdapter, el cual aprovecha el método Comando para crear el comando necesario.
        protected override System.Data.IDataAdapter CrearDataAdapter(string procedimientoAlmacenado, params Object[] args)
        {
            var da = new System.Data.SqlClient.SqlDataAdapter((System.Data.SqlClient.SqlCommand)Comando(procedimientoAlmacenado));
            if (args.Length != 0)
                CargarParametros(da.SelectCommand, args);
            return da;
        } // end CrearDataAdapter

        //Finalmente, es el turno de definir CrearDataAdapter, el cual aprovecha el método Comando para crear el comando necesario.
        protected override System.Data.IDataAdapter CrearDataAdapterSql(string comandoSql)
        {
            var da = new System.Data.SqlClient.SqlDataAdapter((System.Data.SqlClient.SqlCommand)ComandoSql(comandoSql));
            return da;
        } // end CrearDataAdapterSql

        /*
         * Definiremos dos constructores especializados, uno que reciba como argumentos los valores de Nombre del Servidor 
         * y de base de datos que son necesarios para acceder a datos, y otro que admita directamente la cadena de conexión completa.
         * Los constructores se definen como procedimientos públicos de nombre New.
         */
        public SqlServer()
        {
            Base = "";
            Servidor = "";
            Usuario = "";
            Password = "";
        }// end DatosSQLServer


        public SqlServer(string cadenaConexion)
        { CadenaConexion = cadenaConexion; }// end DatosSQLServer


        public SqlServer(string servidor, string @base)
        {
            Base = @base;
            Servidor = servidor;
        }// end DatosSQLServer


        public SqlServer(string servidor, string @base, string usuario, string password)
        {
            Base = @base;
            Servidor = servidor;
            Usuario = usuario;
            Password = password;
        }// end DatosSQLServer
    }

    //class SqlServer : Gdata
    //{
    //    static readonly System.Collections.Hashtable ColComandos = new System.Collections.Hashtable();
    //    public override string CadenaConexion { get => MCadenaConexion; set => MCadenaConexion = value; }

    //    // A constructor that supports the complete connection string is defined.

    //    public SqlServer()
    //    {
            
    //    }// end DatosSQLServer


    //    public SqlServer(string cadenaConexion)
    //    {
    //        CadenaConexion = cadenaConexion;
    //    }// end DatosSQLServer

    //    /* 
    //     * Then we will implement CreateConnection, where a new instance of the SqlClient Connection object is simply returned, 
    //     * using the connection string of the object.
    //     */
    //    protected override System.Data.IDbConnection CreateConnection(string cadenaConexion)
    //    { return new System.Data.SqlClient.SqlConnection(cadenaConexion); }

    //    protected override System.Data.IDbCommand CommandSql(string commandSql)
    //    {
    //        var com = new System.Data.SqlClient.SqlCommand(commandSql, (System.Data.SqlClient.SqlConnection)Conexion, (System.Data.SqlClient.SqlTransaction)MTransaccion);
    //        return com;
    //    }// end Comando

    //    //CreateDataAdapterSql is defined, which uses the Command method to create the necessary command.
    //    protected override System.Data.IDataAdapter CreateDataAdapterSql(string comandoSql)
    //    {
    //        var da = new System.Data.SqlClient.SqlDataAdapter((System.Data.SqlClient.SqlCommand)CommandSql(comandoSql));
    //        return da;
    //    } // end CrearDataAdapterSql

    //    protected override IDbCommand Command(string storedProcedure)
    //    {
    //        System.Data.SqlClient.SqlCommand com;
    //        if (ColComandos.Contains(storedProcedure))
    //        {
    //            com = (System.Data.SqlClient.SqlCommand)ColComandos[storedProcedure];
    //        }
    //        else
    //        {
    //            var con2 = new System.Data.SqlClient.SqlConnection(CadenaConexion);
    //            con2.Open();
    //            com = new System.Data.SqlClient.SqlCommand(storedProcedure, con2) { CommandType = System.Data.CommandType.StoredProcedure };
    //            System.Data.SqlClient.SqlCommandBuilder.DeriveParameters(com);
    //            con2.Close();
    //            con2.Dispose();
    //            ColComandos.Add(storedProcedure, com);
    //        }//end else
    //        com.CommandTimeout = 600;
    //        com.Connection = (System.Data.SqlClient.SqlConnection)Conexion;
    //        com.Transaction = (System.Data.SqlClient.SqlTransaction)MTransaccion;
    //        return com;
    //    }

    //    protected override void LoadParameters(IDbCommand com, object[] args)
    //    {
    //        for (int i = 1; i < com.Parameters.Count; i++)
    //        {
    //            var p = (System.Data.SqlClient.SqlParameter)com.Parameters[i];
    //            p.Value = i <= args.Length ? args[i - 1] ?? DBNull.Value : null;
    //        } // end for
    //    }
    //}
}
