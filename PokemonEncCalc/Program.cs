using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace PokemonEncCalc
{

    static class Program
    {
        // Declare all forms
        internal static frmMainPage mainForm;

        internal readonly static string version = "5.6";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Properties.Settings.Default.Language == 0)
                detectSystemLanguage();

            // Instanciate all forms
            mainForm = new frmMainPage();
            Application.Run(mainForm);
        }

        static void detectSystemLanguage()
        {
            if (CultureInfo.CurrentUICulture.Name.StartsWith("fr"))
                Properties.Settings.Default.Language = 2;
            else
                Properties.Settings.Default.Language = 1;
        }

    }
}
