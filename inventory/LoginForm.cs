using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory
{
    public partial class LoginForm : Form
    {
        OleDbDataReader reader;
        public LoginForm()
        {
            InitializeComponent();
            passwordTxt.PasswordChar = '*';
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {

                RegistryKey forlogin = Registry.CurrentUser.OpenSubKey("Software\\Logger", true);
                Object server = forlogin.GetValue("server");
                Object db = forlogin.GetValue("database");
                Object user = forlogin.GetValue("user");
                Object pass = forlogin.GetValue("pass");

                serverTxt.Text = server.ToString();
                dbTxt.Text = db.ToString();
                userdbTxt.Text = user.ToString();
                passworddbTxt.Text = pass.ToString();

                serverTxt.Enabled = false;
                dbTxt.Enabled = false;
                userdbTxt.Enabled = false;
                passworddbTxt.Enabled = false;

                Console.WriteLine(forlogin.ToString());
                Console.WriteLine("server = " + server.ToString());
                Console.WriteLine("database = " + db.ToString());
                Console.WriteLine("user = " + user.ToString());
                //Console.WriteLine("password = " + pass.ToString());
            }
            catch
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
                key.CreateSubKey("Logger");
            }
        }

     
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (usernameTxt.Text.Equals(""))
                {
                    MessageBox.Show("Username tidak boleh kosong!", "Username", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (passwordTxt.Text.Equals(""))
                {
                    MessageBox.Show("Password tidak boleh kosong", "Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    Connection.ConnectionOpen();
                    string passwordhash = Encrypt.Hash(passwordTxt.Text);
                    string query = "SELECT * from  users WHERE username=@username and delete_Flag='N' ";
                    Connection.command = new OleDbCommand(query, Connection.conn);
                    Connection.command.Parameters.AddWithValue("@username", usernameTxt.Text);
                    //Connection.command.Parameters.AddWithValue("@password", passwordhash);
                    reader = Connection.command.ExecuteReader();
                    if (reader.Read())
                    {
                        
                        String username = Convert.ToString(reader["username"]);
                        String password = Convert.ToString(reader["password"]);
                        String jabatan = Convert.ToString(reader["jabatan"]);

                        if ((username == usernameTxt.Text) && (password == passwordhash))
                        {
                            MessageBox.Show("Login Success", "Login . . .",MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Visible = false;
                            MDIForm formutama = new MDIForm(jabatan);
                            formutama.ShowDialog();
                            
                        }
                        else
                        {
                            MessageBox.Show("Login Gagal, Silakan coba dengan password lain.", "Login . . .", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            usernameTxt.Text = "";
                            passwordTxt.Text = "";
                            usernameTxt.Focus();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Login Gagal, Silakan coba dengan username lain.", "Login . . .", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        usernameTxt.Text = "";
                        passwordTxt.Text = "";
                        usernameTxt.Focus();
                    }                      
                }
                Connection.ConnectionClose();
            }
            catch(Exception Ex)
            {
                MessageBox.Show("Login Failed", "Connection");
                Console.WriteLine("Koneksi Gagal = "+Ex);
                Connection.ConnectionClose();
            }
        }

        private void passwordTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Are Sure Want To Exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes)
                this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (usernameTxt.Text.Equals(""))
            {
                MessageBox.Show("Username tidak boleh kosong!", "Username", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (passwordTxt.Text.Equals(""))
            {
                MessageBox.Show("Password tidak boleh kosong", "Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (saveToSession.Checked == true)
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Logger", true);
                key.SetValue("server", serverTxt.Text);
                key.SetValue("database", dbTxt.Text);
                key.SetValue("user", userdbTxt.Text);
                key.SetValue("pass", passworddbTxt.Text);

                Console.WriteLine("server = " + serverTxt.Text);
                Console.WriteLine("database = " + dbTxt.Text);
                Console.WriteLine("user = " + userdbTxt.Text);
                Console.WriteLine("password = " + passworddbTxt.Text);

                Console.WriteLine("checked!");
            }


            try
            {
                Connection.ConnectionOpenLogin(serverTxt.Text, dbTxt.Text, userdbTxt.Text, passworddbTxt.Text);
                string passwordhash = Encrypt.Hash(passwordTxt.Text);
                string query = "SELECT * from  users WHERE username=@username and delete_Flag='N' ";
                Connection.command = new OleDbCommand(query, Connection.conn);
                Connection.command.Parameters.AddWithValue("@username", usernameTxt.Text);
                //Connection.command.Parameters.AddWithValue("@password", passwordhash);
                reader = Connection.command.ExecuteReader();
                if (reader.Read())
                {

                    String username = Convert.ToString(reader["username"]);
                    String password = Convert.ToString(reader["password"]);
                    String jabatan = Convert.ToString(reader["jabatan"]);

                    Console.WriteLine("passwordhash = " + passwordhash);
                    Console.WriteLine("password = " + password);

                    if ((username == usernameTxt.Text) && (password == passwordhash))
                    {
                        MessageBox.Show("Login Success", "Login . . .", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
                        key.CreateSubKey("Session");
                        key = key.OpenSubKey("Session", true);
                        key.SetValue("server", serverTxt.Text);
                        key.SetValue("database", dbTxt.Text);
                        key.SetValue("user", userdbTxt.Text);
                        key.SetValue("pass", passworddbTxt.Text);

                        this.Visible = false;
                        MDIForm formutama = new MDIForm(jabatan);
                        formutama.ShowDialog();

                    }
                    else
                    {
                        MessageBox.Show("Login Gagal, Silakan coba dengan password lain.", "Login . . .", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        usernameTxt.Text = "";
                        passwordTxt.Text = "";
                        usernameTxt.Focus();
                    }

                }
                else
                {
                    MessageBox.Show("Login Gagal, Silakan coba dengan username lain.", "Login . . .", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    usernameTxt.Text = "";
                    passwordTxt.Text = "";
                    usernameTxt.Focus();
                }                      
            }
            catch (Exception Ex)
            {
                Console.WriteLine("e = " + Ex);
            }

        }

        private void dbTxt_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //System.IO.StreamReader sr = new
                //   System.IO.StreamReader(openFileDialog1.FileName);
                string sourceFile = openFileDialog1.FileName;
                string directoryPath = Path.GetDirectoryName(sourceFile);
                dbTxt.Text = sourceFile;
                MessageBox.Show(sourceFile);
                //sr.Close();
            }
        }

        private void editDataConnection_CheckedChanged(object sender, EventArgs e)
        {
            if (editDataConnection.Checked == false)
            {
                serverTxt.Enabled = false;
                dbTxt.Enabled = false;
                userdbTxt.Enabled = false;
                passworddbTxt.Enabled = false;
            }
            else if (editDataConnection.Checked == true)
            {

                serverTxt.Enabled = true;
                dbTxt.Enabled = true;
                userdbTxt.Enabled = true;
                passworddbTxt.Enabled = true;
            }
        }

        private void dbTxt_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(dbTxt, dbTxt.Text);
        }

        private void passwordTxt_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            
        }

        private void passwordTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click(this, new EventArgs());
            }
        }
    }
}
