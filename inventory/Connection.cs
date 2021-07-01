using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;
using Microsoft.Win32;
namespace inventory
{
    class Connection
    {
        public static OleDbConnection conn = new OleDbConnection();
        public static OleDbCommand command = new OleDbCommand();

        public static void ConnectionOpen()
        {
            RegistryKey forlogin = Registry.CurrentUser.OpenSubKey("Software\\Logger", true);
            Object server = forlogin.GetValue("server");
            Object db = forlogin.GetValue("database");
            Object user = forlogin.GetValue("user");
            Object pass = forlogin.GetValue("pass");

            try
            {   
                conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source= " + db.ToString() + ";Jet OLEDB:Database Password=" + pass.ToString() + ";";
                //conn.ConnectionString = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source= " + db); //oleDB 2007 keatas atau .accdb
//                conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\vpc2\Documents\inventory1.accdb;
//                                Jet OLEDB:Database Password=inventory;";
                conn.Open();
            }
            catch (OleDbException e)
            {
                conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source= " + db.ToString() + ";";
                conn.Open();
                Console.WriteLine("gagal koneksi = "+e.InnerException.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("error = "+e);
            }
           
        }

        public static void ConnectionClose() {
            conn.Close();
        }

        public static void ConnectionOpenLogin(string server, string db, string userdb, string passdb)
        {
            try
            {

                conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source= " + db + ";Jet OLEDB:Database Password=" + passdb + ";";
                //conn.ConnectionString = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source= " + db); //oleDB 2007 keatas atau .accdb
                //                conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\vpc2\Documents\inventory1.accdb;
                //                                Jet OLEDB:Database Password=inventory;";
                conn.Open();
            }
            catch (OleDbException e)
            {
                conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source= " + db + ";";
                conn.Open();
                MessageBox.Show(e.ToString(), "gagal koneksi . . .", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("gagal koneksi = " + e.InnerException.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "error . . .", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("error = " + e);
            }

        }
     }

  

    
}
