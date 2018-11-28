using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.data
{
    class Conexion
    {
        public static Gdata GDatos;
        public static bool StartSession(string nombreServidor, string baseDatos, string usuario, string password)
        {
            GDatos = new SqlServer(nombreServidor, baseDatos, usuario, password);
            return GDatos.Autenticar();
        } //fin inicializa sesion

        public static void EndSession()
        {
            GDatos.CerrarConexion();
        } //fin FinalizaSesion
    }

    //class Conexion
    //{
    //    public static Gdata GDatos;
    //    public static bool StartSession()
    //    {
    //        GDatos = new SqlServer(System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
    //        return GDatos.Authenticate();
    //    } //fin inicializa sesion

    //    public static void EndSession()
    //    {
    //        GDatos.CloseConexion();
    //    } //fin FinalizaSesion
    //}
}
