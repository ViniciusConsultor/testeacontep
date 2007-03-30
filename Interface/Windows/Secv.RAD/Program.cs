using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorCRUD
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Acontep.RAD.frmPrincipal());
        }
    }
}