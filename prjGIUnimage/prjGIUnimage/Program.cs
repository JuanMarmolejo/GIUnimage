using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGIUnimage
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmPrincipal());

            frmLogin frlog = new frmLogin();

            do
            {
                //Run the login form first
                frlog = new frmLogin();
                frlog.ShowDialog();
                if (frlog.DialogResult == DialogResult.No)
                {
                    MessageBox.Show("Mot de passe incorrect");
                }
            } while (frlog.DialogResult == DialogResult.No);

            //If the user is valid, enter the main form.
            if (frlog.DialogResult == DialogResult.OK)
            {
                Application.Run(new frmPrincipal());
            }
            
        }
    }
}
