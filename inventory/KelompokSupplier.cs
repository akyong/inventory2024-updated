using inventory.searching;
using Microsoft.Win32;
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
    public partial class KelompokSupplier : Form
    {
        OleDbDataReader reader;
        public KelompokSupplier()
        {
            InitializeComponent();
            codeTxt.CharacterCasing = CharacterCasing.Upper;
            nameTxt.CharacterCasing = CharacterCasing.Upper;
            refreshList("");
        }

        private void KelompokSupplier_Load(object sender, EventArgs e)
        {
            codeTxt.Enabled = false;
            nameTxt.Enabled = false;
            descriptionTxt.Enabled = false;
        }

        private void refreshList(string nilaiyangdicari)//this one
        {
            List<Tuple<string, string, string, string, string, string>> list = new List<Tuple<string, string, string, string, string, string>>();
            string query = "SELECT * FROM GROUP_SUPPLIER WHERE delete_flag='N'";


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

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "ADD")
            {
                codeTxt.Enabled = true;
                nameTxt.Enabled = true;
                descriptionTxt.Enabled = true;

                codeTxt.Text = "";
                nameTxt.Text = "";
                descriptionTxt.Text = "";

                button1.Text = "SAVE";
                button2.Enabled = false;
                button3.Enabled = false;
                findBtn.Enabled = false;
            }
            else if (button1.Text == "SAVE")
            {
                if (codeTxt.Text == "")
                {
                    MessageBox.Show("Kode harus diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    codeTxt.Focus();
                }
                else if (nameTxt.Text == "")
                {
                    MessageBox.Show("Nama harus diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    nameTxt.Focus();
                }
                else
                {
                    Connection.ConnectionClose();
                    Connection.ConnectionOpen();
                    string queryCheck = "SELECT * FROM GROUP_SUPPLIER WHERE code=@codec and delete_flag='N'";
                    Connection.command = new OleDbCommand(queryCheck, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@codec", codeTxt.Text);
                    reader = Connection.command.ExecuteReader();
                    if (reader.Read())
                    {
                        Console.WriteLine("reader ada");
                        //String id = Convert.ToString(reader["id"]);
                        //jika data yang mau saya buat itu belum ada...baru bisa insert baru..
                        DialogResult avaiable = MessageBox.Show("Kode Kelompok " + codeTxt.Text + " sudah ada", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            string query = "INSERT INTO GROUP_SUPPLIER( code,name,description,delete_flag ) VALUES( @code,@names,@description,'N' )";
                            Connection.command = new OleDbCommand(query, Connection.conn);
                            Connection.command.CommandType = CommandType.Text;
                            Connection.command.Parameters.AddWithValue("@code", codeTxt.Text);
                            Connection.command.Parameters.AddWithValue("@names", nameTxt.Text);
                            Connection.command.Parameters.AddWithValue("@description", descriptionTxt.Text);
                            Console.WriteLine("query = " + query);
                            Connection.command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("there was an issue!" + ex);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("there was another issue!");
                            Console.WriteLine("ex = " + ex);
                        }



                        string query2 = "select top 1 * from GROUP_SUPPLIER where delete_flag='N' ORDER BY ID DESC ";
                        string nilaiyangdicari = "";
                        Connection.command = new OleDbCommand(query2, Connection.conn);
                        reader = Connection.command.ExecuteReader();
                        if (reader.Read())
                        {
                            nilaiyangdicari = Convert.ToString(reader["ID"]);
                        }
                        else
                        {
                            nilaiyangdicari = "";
                        }

                        Connection.ConnectionClose();

                        codeTxt.Enabled = false;
                        nameTxt.Enabled = false;
                        descriptionTxt.Enabled = false;

                        codeTxt.Text = "";
                        nameTxt.Text = "";
                        descriptionTxt.Text = "";

                        button1.Text = "ADD";
                        button2.Enabled = true;
                        button3.Enabled = true;
                        findBtn.Enabled = true;

                        refreshList(nilaiyangdicari);
                    }


                    Connection.ConnectionClose();
                }
            }           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "EDIT")
            {
                codeTxt.Enabled = true;
                nameTxt.Enabled = true;
                descriptionTxt.Enabled = true;

                button2.Text = "UPDATE";
                button1.Enabled = false;
                button3.Enabled = false;
                findBtn.Enabled = false;
            }
            else if (button2.Text == "UPDATE")
            {
                if (codeTxt.Text == "")
                {
                    MessageBox.Show("Kode harus diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    codeTxt.Focus();
                }
                else if (nameTxt.Text == "")
                {
                    MessageBox.Show("Nama harus diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    nameTxt.Focus();
                }
                else
                {
                    Connection.ConnectionClose();
                    Connection.ConnectionOpen();
                    string queryCheck = "SELECT * FROM GROUP_SUPPLIER WHERE code=@codec and delete_flag='N'";
                    Connection.command = new OleDbCommand(queryCheck, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@codec", codeTxt.Text);
                    reader = Connection.command.ExecuteReader();
                    if (reader.Read())
                    {
                        String id = Convert.ToString(reader["id"]);
                        Console.WriteLine("id = " + id);
                        //jika data yang mau saya buat itu belum ada...baru bisa insert baru..
                        if (id == idgudang.Text)
                        {
                            Console.WriteLine("id sama");
                            string query = "UPDATE GROUP_SUPPLIER SET code=@code, name=@name,description=@description WHERE id=@id";
                            Connection.command = new OleDbCommand(query, Connection.conn);
                            Connection.command.CommandType = CommandType.Text;
                            Connection.command.Parameters.AddWithValue("@code", codeTxt.Text);
                            Connection.command.Parameters.AddWithValue("@name", nameTxt.Text);
                            Connection.command.Parameters.AddWithValue("@description", descriptionTxt.Text);
                            Connection.command.Parameters.AddWithValue("@id", id);
                            Connection.command.ExecuteNonQuery();

                            MessageBox.Show("Data Berhasil diperbarui.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            codeTxt.Enabled = false;
                            nameTxt.Enabled = false;
                            descriptionTxt.Enabled = false;

                            refreshList(idgudang.Text);
                        }
                        else
                        {
                            DialogResult avaiable = MessageBox.Show("Kode Kelompok " + codeTxt.Text + " sudah terpakai", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (avaiable == DialogResult.Yes)
                            {
                                this.Close();
                            }
                            return;
                        }

                    }
                    else
                    {
                        string query = "UPDATE GROUP_SUPPLIER SET code=@code, name=@name,description=@description WHERE id=@id";
                        Connection.command = new OleDbCommand(query, Connection.conn);
                        Connection.command.CommandType = CommandType.Text;
                        Connection.command.Parameters.AddWithValue("@code", codeTxt.Text);
                        Connection.command.Parameters.AddWithValue("@name", nameTxt.Text);
                        Connection.command.Parameters.AddWithValue("@description", descriptionTxt.Text);
                        Connection.command.Parameters.AddWithValue("@id", idgudang.Text);
                        Connection.command.ExecuteNonQuery();


                        MessageBox.Show("Data Berhasil diperbarui.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        codeTxt.Enabled = false;
                        nameTxt.Enabled = false;
                        descriptionTxt.Enabled = false;

                        refreshList(idgudang.Text);
                    }

                    button2.Text = "EDIT";
                    button1.Enabled = true;
                    button3.Enabled = true;
                    findBtn.Enabled = true;
                }


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.ConnectionClose();
                Connection.ConnectionOpen();
                string queryCheck = "DELETE FROM GROUP_SUPPLIER WHERE id=@id";
                Connection.command = new OleDbCommand(queryCheck, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@id", idgudang.Text);

                Connection.command.ExecuteNonQuery();

                MessageBox.Show("Data Berhasil dihapus.", "Data Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshList("");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Tidak Bisa dihapus, karena data ini masih dipakai.", "Cannot  Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 
            
        }

        private void findBtn_Click(object sender, EventArgs e)
        {
            Hashtable fieldTable = new Hashtable();
            fieldTable.Add("name", "Nama Kelompok");
            fieldTable.Add("code", "Kode Kelompok");

            int promptValue = SearchGroupSupplier.ShowDialog("Cari Kelompok Supplier", fieldTable);

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // 0 is the column index
                if (row.Cells[0].Value.ToString().Equals(promptValue.ToString()))
                {
                    row.Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index; //scroll ke index selected
                    break;
                }
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
                idgudang.Text = row.Cells["id"].Value.ToString();
                codeTxt.Text = row.Cells["code"].Value.ToString();
                nameTxt.Text = row.Cells["name"].Value.ToString();
                descriptionTxt.Text = row.Cells["description"].Value.ToString();
                // etc.
            }        
        }


    }
}
