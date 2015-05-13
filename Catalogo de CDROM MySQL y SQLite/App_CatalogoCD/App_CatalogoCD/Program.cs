using System;
using System.Windows.Forms;
//---------------------------------------------------------------------------------

namespace App_CatalogoCD
{
    class Program
    {
        /*static void Main(string[] args)
        {
			new UI ();
            Console.ReadLine();
        }*/
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI());
        }
    }
}
