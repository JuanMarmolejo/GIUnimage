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
        public static bool StartSession()
        {
            GDatos = new SqlServer(System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            return GDatos.Authenticate();
        } //fin inicializa sesion

        public static void EndSession()
        {
            GDatos.CloseConexion();
        } //fin FinalizaSesion
    }
}
