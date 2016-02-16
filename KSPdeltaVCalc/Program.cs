using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace KSPdeltaVCalc
{
    static class Program
    {
        static public int language;
        static private bool first = true;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            while (true)
            {
                if (!first)
                {
                    switch (language)
                    {
                        case 0:
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");
                            break;
                        case 1:
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
                            Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-DE");
                            break;
                        case 2:
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
                            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
                            break;
                        case 3:
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
                            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");
                            break;
                        case 4:
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
                            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
                            break;
                        case 5:
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL");
                            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl-PL");
                            break;
                        case 6:
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("cs-CZ");
                            Thread.CurrentThread.CurrentUICulture = new CultureInfo("cs-CZ");
                            break;
                        case 7:
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("aa");
                            Thread.CurrentThread.CurrentUICulture = new CultureInfo("aa");
                            break;
                    }
                }
                first = false;
                Application.Run(new Engines());
            }
        }
    }
}
