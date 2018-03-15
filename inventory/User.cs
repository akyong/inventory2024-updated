using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory
{
    public partial class User : Form
    {
        OleDbDataReader reader;
        public User()
        {
            InitializeComponent();
            password1Txt.PasswordChar = '*';
            password2Txt.PasswordChar = '*';
        }

        private void User_Load(object sender, EventArgs e)
        {
            usernameTxt.Enabled = false;
            nameTxt.Enabled = false;
            password1Txt.Enabled = false;
            password2Txt.Enabled = false;
            cmbJabatan.Enabled = false;

            toolStripButton10.Visible = false;
            toolStripButton11.Visible = false;

            Hashtable fieldTable = new Hashtable();
            fieldTable.Add("admin", "Admin");
            fieldTable.Add("karyawan", "Karyawan");

            BindingSource bs = new BindingSource();
            bs.DataSource = fieldTable;
            cmbJabatan.DataSource = bs;
            cmbJabatan.DisplayMember = "Value";


            cmbJabatan.SelectedIndex = 0;
            refreshList("");
            dataGridView1.Rows[0].Selected = true;
            //MessageBox.Show((cmbJabatan.SelectedItem as ComboboxItem).Value.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            usernameTxt.Enabled = true;
            nameTxt.Enabled = true;
            password1Txt.Enabled = true;
            password2Txt.Enabled = true;
            cmbJabatan.Enabled = true;

            usernameTxt.Text = "";
            nameTxt.Text = "";
            password1Txt.Text = "";
            password2Txt.Text = "";

            toolStripButton10.Visible = true;
            toolStripButton11.Visible = true;
            toolStripButton1.Enabled = false;
            toolStripButton3.Visible = false;
            toolStripButton8.Enabled = false;
            toolStripButton9.Enabled = false;

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            toolStripButton1.Enabled = false;
            toolStripButton3.Enabled = false;

            toolStripButton10.Visible = true;
            toolStripButton11.Visible = true;

            usernameTxt.Enabled = true;
            nameTxt.Enabled = true;
            password1Txt.Enabled = true;
            password2Txt.Enabled = true;
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            DialogResult batal = MessageBox.Show("Perubahan dibatalkan ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (batal == DialogResult.Yes)
            {
                if (iduser.Text.Equals("ID"))
                {
                    usernameTxt.Enabled = true;
                    nameTxt.Enabled = true;
                    password1Txt.Enabled = true;
                    password2Txt.Enabled = true;
                    cmbJabatan.Enabled = true;

                    toolStripButton1.Visible = true;
                    toolStripButton3.Visible = true;
                    toolStripButton10.Visible = false;
                    toolStripButton11.Visible = false;

                    toolStripButton1.Enabled = true;
                    toolStripButton8.Enabled = true;
                    toolStripButton9.Enabled = true;

                    //int numOfRows = dataGridView1.Rows.Count - 1;
                    //dataGridView1.Rows[numOfRows].Selected = true;

                    //DataGridViewRow selectedRow = dataGridView1.Rows[numOfRows];
                    //string x = Convert.ToString(selectedRow.Cells[0].Value);
                    //string username = Convert.ToString(selectedRow.Cells[1].Value);
                    //string name = Convert.ToString(selectedRow.Cells[2].Value);

                    //int index = dataGridView1.SelectedRows[0].Index;
                    ////string a = Convert.ToString(selectedRow.Cells["ID"].Value); //ID nama dari  header table
                 
                    //idLbl.Text = x;
                    //usernameTxt.Text = username;
                    //nameTxt.Text = name;
                    //password1Txt.Text = "***************************";
                    //password2Txt.Text = "***************************";

                    usernameTxt.Text = "";
                    nameTxt.Text = "";
                    password1Txt.Text = "";
                    password2Txt.Text = "";

                    usernameTxt.Enabled = false;
                    nameTxt.Enabled = false;
                    password1Txt.Enabled = false;
                    password2Txt.Enabled = false;
                    cmbJabatan.Enabled = false;

                }
                else
                {
                    //this.Close();
                    //kodesatuanTxt.Text = kosat;
                    //keteranganTxt.Text = ket;
                    //kodesatuanTxt.Enabled = false;
                    //keteranganTxt.Enabled = false;
                    //deleteFlag.Enabled = false;

                    toolStripButton1.Enabled = true;
                    toolStripButton3.Enabled = true;
                    toolStripButton8.Enabled = true;
                    toolStripButton9.Enabled = true;

                    toolStripButton8.Visible = true;
                    toolStripButton9.Visible = true;

                    toolStripButton10.Visible = false;
                    toolStripButton11.Visible = false;
                }


            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (usernameTxt.Text == "")
            {
                MessageBox.Show("Username harus diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                usernameTxt.Focus();
            }
            else if (nameTxt.Text == "")
            {
                MessageBox.Show("Nama harus diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nameTxt.Focus();
            }
            else if (password1Txt.Text == "")
            {
                MessageBox.Show("Password pertama harus diisi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                password1Txt.Focus();
            }
            else if (password2Txt.Text == "")
            {
                MessageBox.Show("Password kedua harus diisi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                password2Txt.Focus();
            }
            else if (password1Txt.Text != password2Txt.Text)
            {
                MessageBox.Show("Password 1 dan 2 tidak cocok!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                password1Txt.Focus();
            }
            else
            {
                int id = 0;
                Connection.ConnectionClose();
                Connection.ConnectionOpen();
                string queryCheck = "SELECT * FROM USERS WHERE username=@username and delete_flag='N'";
                Connection.command = new OleDbCommand(queryCheck, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@username", usernameTxt.Text);
                reader = Connection.command.ExecuteReader();
                if (reader.Read())
                {
                    DialogResult avaiable = MessageBox.Show("Username " +usernameTxt+ " sudah ada!", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (avaiable == DialogResult.Yes)
                    {
                        this.Close();
                    }
                    return;
                }
                else
                {
                    try
                    {
                        string passwordhash = Encrypt.Hash(password1Txt.Text);
                        string query = "INSERT INTO USERS( [username],[name],[password],jabatan,delete_flag ) VALUES( @username,@names,@password,@jabatan,'N' )";
                        Connection.command = new OleDbCommand(query, Connection.conn);
                        Connection.command.CommandType = CommandType.Text;
                        var itemselected = (DictionaryEntry)cmbJabatan.SelectedItem;
                        var key = itemselected.Key;
                        var value = itemselected.Value;
                        
                        Connection.command.Parameters.AddWithValue("@username", usernameTxt.Text);
                        Connection.command.Parameters.AddWithValue("@names", nameTxt.Text);
                        Connection.command.Parameters.AddWithValue("@password", passwordhash);
                        Connection.command.Parameters.AddWithValue("@jabatan", key.ToString());
                        Connection.command.ExecuteNonQuery();

                        Connection.command.CommandText = "Select @@Identity";
                        id = (int)Connection.command.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("ex 1 = " + ex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("there was another issue!");
                        Console.WriteLine("ex 2 = " + ex);
                    }

                    //string query2 = "select top 1 * from USERS ORDER BY ID DESC ";
                    //string nilaiyangdicari = "";
                    //Connection.command = new OleDbCommand(query2, Connection.conn);
                    //reader = Connection.command.ExecuteReader();
                    //if (reader.Read())
                    //{
                    //    nilaiyangdicari = Convert.ToString(reader["ID"]);
                    //}
                    //else
                    //{
                    //    nilaiyangdicari = "";
                    //}

                    Connection.ConnectionClose();

                    usernameTxt.Enabled = false;
                    nameTxt.Enabled = false;
                    password1Txt.Enabled = false;
                    password2Txt.Enabled = false;

                    usernameTxt.Text = "";
                    nameTxt.Text = "";
                    password1Txt.Text = "";
                    password2Txt.Text = "";



                    refreshList(id.ToString());
                }

            }
        }

        private void refreshList(string nilaiyangdicari)//this one
        {
           string query = "SELECT id,username, name, jabatan FROM USERS ORDER BY USERNAME ASC";


            //dataGridView1.Rows.Clear();
            Connection.ConnectionClose();
            Connection.ConnectionOpen();
            OleDbCommand cmd = new OleDbCommand(query, Connection.conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable scores = new DataTable();
            da.Fill(scores);
            dataGridView1.DataSource = scores;
            if (nilaiyangdicari == "")
            {
                query = "";
            }
            else
            {
                int rowIndex = -1;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["id"].Value.ToString().Equals(nilaiyangdicari))
                    { 
                        rowIndex = row.Index;
                        break;
                    }
                }
                dataGridView1.Rows[rowIndex].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;
            }

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = null;
           
            foreach (DataGridViewCell selectedCell in dataGridView1.SelectedCells)
            {
                cell = selectedCell;
                break;
            }
            if (cell != null)
            {
                DataGridViewRow row = cell.OwningRow;
                iduser.Text = row.Cells["id"].Value.ToString();
                usernameTxt.Text = row.Cells["username"].Value.ToString();
                nameTxt.Text = row.Cells["name"].Value.ToString();
                cmbJabatan.Text = row.Cells["jabatan"].Value.ToString();
                password1Txt.Text = "************";
                password2Txt.Text = "************";
                // etc.
            }
           
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            int numOfRows = dataGridView1.Rows.Count - 1;
            int index = dataGridView1.SelectedRows[0].Index;

            dataGridView1.Rows[0].Selected = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.ConnectionClose();
                Connection.ConnectionOpen();
                string queryDeleteUser = "DELETE FROM USERS WHERE id=@userid";
                Connection.command = new OleDbCommand(queryDeleteUser, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@userid", iduser.Text);
                Connection.command.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil dihapus.", "Data Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshList("");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Tidak Bisa dihapus, karena data ini masih dipakai.", "Cannot  Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        
    }
}
