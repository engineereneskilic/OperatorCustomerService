using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Final_Projesi_İki_Ders_Bir_Arada_
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
       static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new veritabani_ayari());
        }
    }
}
