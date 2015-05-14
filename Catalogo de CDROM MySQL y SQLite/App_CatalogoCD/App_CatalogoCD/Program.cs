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
            try
            {
                Application.Run(new GUI());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                //MessageBox.Show("No se ha podido acceder a la base de datos...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }
    }
}
