using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace SecondTry
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                ApplicationConfiguration.Initialize();

                if (ControlloConnessioneCartelleNas())
                {
                    Application.Run(new Progetto());
                }
                Application.Exit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
         public static bool ControlloConnessioneCartelleNas()
        { 
            try
            {
                var pathCartelle = Directory.GetDirectories("\\\\192.168.1.250\\Occupazioni Suolo\\Documenti per Occupazione");
            }
            catch (Exception)
            {
                MessageBox.Show("Controlla la connessione al nas o la vpn e Riprova");
                return false;
            }
            return true;
        }

    }
}
//Scaffold-DbContext 'Data Source=.\\SQLEXPRESS;Initial Catalog=Occupazione;Integrated Security=True' Microsoft.EntityFrameworkCore.SqlServer