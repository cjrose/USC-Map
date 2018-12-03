/* Copyright 2018 */
/* Cody Rose */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USC_Map
{
    static class Map
    {
        [STAThread]
        static void Main()
        {
            Database.PopulateDictionary();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI());
        }
    }
}