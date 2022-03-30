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
                if (!Directory.Exists(Directory.GetCurrentDirectory() + "//Installer//"))
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "//Installer");
                if (File.Exists(Directory.GetCurrentDirectory() + "//Installer//SqlExpressInstaller.exe"))
                {
                    if (!EsistenzaDatabase())
                    {
                        CreateDatabase();
                    }
                    else
                    {
                        if (!EsistenzaTabella())
                        {
                            CreateTable();
                        }
                        if (ControlloConnessioneCartelleNas())
                        {
                            Application.Run(new Progetto());
                        }
                    }
                }
                else
                {
                    InstallSqlAsync().GetAwaiter().GetResult();
                    System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + "//Installer//SqlExpressInstaller.exe");
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static async Task InstallSqlAsync()
        {
            var cli = new WebClient();
            var uri = new Uri("https://download.microsoft.com/download/7/f/8/7f8a9c43-8c8a-4f7c-9f92-83c18d96b681/SQL2019-SSEI-Expr.exe");
            await cli.DownloadFileTaskAsync(uri, Directory.GetCurrentDirectory() + "//Installer//SqlExpressInstaller.exe");
        }
        public static void CreateDatabase()
        {
            String str;
            SqlConnection myConn = new SqlConnection("Server=.\\SQLEXPRESS;Integrated security=SSPI;database=master");

            str = "CREATE DATABASE Occupazione";

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                MessageBox.Show("DataBase is Created Successfully", "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }
        public static bool EsistenzaDatabase()
        {
            bool flag = false;
            SqlConnection myConn = new SqlConnection("Server=.\\SQLEXPRESS;Integrated security=SSPI;database=Occupazione");
            try
            {
                myConn.Open();
                flag = true;
            }
            catch (System.Exception ex)
            {
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
            return flag;
        }
        public static bool EsistenzaTabella()
        {
            var flag = false;
            Int32 newProdID = 0;
            string sql = "SELECT count(*) as IsExists FROM dbo.sysobjects where id = object_id('[dbo].[Occupazione]')";
            using (SqlConnection conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=Occupazione;Trusted_Connection=True;"))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    newProdID = (Int32)cmd.ExecuteScalar();
                    flag = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (newProdID == 1) { flag = true; }
            else { flag = false; }
            return flag;
        }
        public static void CreateTable()
        {
            String str;
            SqlConnection myConn = new SqlConnection("Server=.\\SQLEXPRESS;Integrated security=SSPI;database=Occupazione");

            str = "CREATE TABLE Occupazione ([id][int] IDENTITY(1, 1) NOT NULL,[Indirizzo] [nvarchar] (50) NOT NULL,[Commessa] [nvarchar] (50) NOT NULL,[Data_Inserimento] [datetime] NOT NULL,[Cartella_Destinazione] [nvarchar] (MAX)NOT NULL,CONSTRAINT[PK_Occupazione] PRIMARY KEY CLUSTERED([id] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY]) ON[PRIMARY] ALTER TABLE[dbo].[Occupazione] ADD CONSTRAINT[DF_Occupazione_Data_Inserimento]  DEFAULT(((1) / (1)) / (1900)) FOR[Data_Inserimento]";


            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                MessageBox.Show("Table is Created Successfully", "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
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