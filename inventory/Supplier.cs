using inventory.searching;
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
    public partial class Supplier : Form
    {
        OleDbDataReader reader;
        public Supplier()
        {
            InitializeComponent();
            codeTxt.CharacterCasing = CharacterCasing.Upper;
            nameTxt.CharacterCasing = CharacterCasing.Upper;
            loadComboBox();
            refreshList("");
            dataGridView1.Columns["plafon"].DefaultCellStyle.Format = "N";//dari propertis juga bisa
        }

        public void loadComboBox()
        {

            Connection.ConnectionClose();
            Connection.ConnectionOpen();

            //TODO fill unit list
            string queryGrupSupplier = "SELECT * FROM GROUP_SUPPLIER WHERE DELETE_FLAG = 'N'";
            OleDbDataAdapter dAdapter = new OleDbDataAdapter(queryGrupSupplier, Connection.conn);

            DataSet ds = new DataSet();
            dAdapter.Fill(ds, "GroupSupplier");

            cmbGroupSupplier.DisplayMember = "name";
            cmbGroupSupplier.ValueMember = "id";
            cmbGroupSupplier.DataSource = ds.Tables["GroupSupplier"];

        }

        private void Supplier_Load(object sender, EventArgs e)
        {
            codeTxt.Enabled = false;
            nameTxt.Enabled = false;
            addressTxt.Enabled = false;
            cmbGroupSupplier.Enabled = false;
            plafonFlag.Enabled = false;
            plafonTxt.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "ADD")
            {
                codeTxt.Enabled = true;
                nameTxt.Enabled = true;
                addressTxt.Enabled = true;
                cmbGroupSupplier.Enabled = true;
                plafonFlag.Enabled = true;
                plafonTxt.Enabled = true;

                codeTxt.Text = "";
                nameTxt.Text = "";
                addressTxt.Text = "";
                plafonTxt.Text = "";
                plafonFlag.Checked = false;

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
                else if (addressTxt.Text == "")
                {
                    MessageBox.Show("Alamat harus diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    addressTxt.Focus();
                }
                else
                {
                    Connection.ConnectionClose();
                    Connection.ConnectionOpen();
                    string queryCheck = "SELECT * FROM SUPPLIER WHERE code=@codec";
                    Connection.command = new OleDbCommand(queryCheck, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@codec", codeTxt.Text);
                    reader = Connection.command.ExecuteReader();
                    if (reader.Read())
                    {
                        DialogResult avaiable = MessageBox.Show("Kode Supplier " + codeTxt.Text + " sudah ada", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            string query = "INSERT INTO SUPPLIER( code,name,address,group_supplier_id,plafon, plafon_flag ) VALUES( @code,@names,@address,@groupsupplier,@plafon,@plafon_flag )";
                            Connection.command = new OleDbCommand(query, Connection.conn);
                            Connection.command.CommandType = CommandType.Text;
                            Connection.command.Parameters.AddWithValue("@code", codeTxt.Text);
                            Connection.command.Parameters.AddWithValue("@names", nameTxt.Text);
                            Connection.command.Parameters.AddWithValue("@address", addressTxt.Text);
                            Connection.command.Parameters.AddWithValue("@groupsupplier", cmbGroupSupplier.SelectedValue);
                            if (plafonTxt.Text == "")
                            {
                                Connection.command.Parameters.AddWithValue("@plafon", "0");
                            }
                            else
                            {
                                Connection.command.Parameters.AddWithValue("@plafon", plafonTxt.Text);
                            }

                            if (plafonFlag.Checked)
                            {
                                Connection.command.Parameters.AddWithValue("@plafon_flag", "1");
                            }
                            else
                            {
                                Connection.command.Parameters.AddWithValue("@plafon_flag", "0");
                            }

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

                        string query2 = "select top 1 * from SUPPLIER ORDER BY ID DESC ";
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
                        addressTxt.Enabled = false;
                        cmbGroupSupplier.Enabled = false;
                        plafonFlag.Enabled = false;
                        plafonTxt.Enabled = false;

                        codeTxt.Text = "";
                        nameTxt.Text = "";
                        addressTxt.Text = "";
                        plafonTxt.Text = "";
                        plafonFlag.Checked = false;

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

        private void refreshList(string nilaiyangdicari)//this one
        {
            string query = "SELECT SP.ID, SP.CODE, SP.NAME, SP.ADDRESS, SP.PLAFON, SP.PLAFON_FLAG, GS.NAME AS GROUPSUPPLIER,GS.ID AS GROUPSUPPLIERID FROM SUPPLIER SP LEFT JOIN GROUP_SUPPLIER GS ON GS.ID = SP.GROUP_SUPPLIER_ID";


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

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "EDIT")
            {
                codeTxt.Enabled = true;
                nameTxt.Enabled = true;
                addressTxt.Enabled = true;
                cmbGroupSupplier.Enabled = true;
                plafonFlag.Enabled = true;
                plafonTxt.Enabled = true;

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
                else if (addressTxt.Text == "")
                {
                    MessageBox.Show("Alamat harus diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    addressTxt.Focus();
                }
                else
                {
                    try
                    {
                        Connection.ConnectionClose();
                        Connection.ConnectionOpen();
                        string queryCheck = "SELECT * FROM CUSTOMER WHERE code=@codec";
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
                                string query = "UPDATE SUPPLIER SET code=@code, name=@name,address=@address, group_supplier_id=@groupsupplier, plafon=@plafon,plafon_flag=@plafonflag WHERE id=@id";
                                Connection.command = new OleDbCommand(query, Connection.conn);
                                Connection.command.CommandType = CommandType.Text;
                                Connection.command.Parameters.AddWithValue("@code", codeTxt.Text);
                                Connection.command.Parameters.AddWithValue("@name", nameTxt.Text);
                                Connection.command.Parameters.AddWithValue("@address", addressTxt.Text);
                                Connection.command.Parameters.AddWithValue("@groupsupplier", cmbGroupSupplier.SelectedValue.ToString());
                                if (plafonTxt.Text == "")
                                {
                                    Connection.command.Parameters.AddWithValue("@plafon", "0");
                                }
                                else
                                {
                                    Connection.command.Parameters.AddWithValue("@plafon", plafonTxt.Text);
                                }
                                if (plafonFlag.Checked)
                                {
                                    Connection.command.Parameters.AddWithValue("@plafonflag", "1");
                                }
                                else
                                {
                                    Connection.command.Parameters.AddWithValue("@plafonflag", "0");
                                }
                                Connection.command.Parameters.AddWithValue("@id", id);
                                Connection.command.ExecuteNonQuery();

                                MessageBox.Show("Data Berhasil diperbarui.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                codeTxt.Enabled = false;
                                nameTxt.Enabled = false;
                                addressTxt.Enabled = false;
                                cmbGroupSupplier.Enabled = false;
                                plafonFlag.Enabled = false;
                                plafonTxt.Enabled = false;

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
                            string query = "UPDATE SUPPLIER SET code=@code, name=@name,address=@address, group_supplier_id=@groupsupplier, plafon=@plafon,plafon_flag=@plafonflag WHERE id=@id";
                            Connection.command = new OleDbCommand(query, Connection.conn);
                            Connection.command.CommandType = CommandType.Text;
                            Connection.command = new OleDbCommand(query, Connection.conn);
                            Connection.command.CommandType = CommandType.Text;
                            Connection.command.Parameters.AddWithValue("@code", codeTxt.Text);
                            Connection.command.Parameters.AddWithValue("@name", nameTxt.Text);
                            Connection.command.Parameters.AddWithValue("@address", addressTxt.Text);
                            Connection.command.Parameters.AddWithValue("@groupsupplier", cmbGroupSupplier.SelectedValue.ToString());
                            Connection.command.Parameters.AddWithValue("@plafon", plafonTxt.Text);
                            if (plafonFlag.Checked)
                            {
                                Connection.command.Parameters.AddWithValue("@plafonflag", "1");
                            }
                            else
                            {
                                Connection.command.Parameters.AddWithValue("@plafonflag", "0");
                            }
                            Connection.command.Parameters.AddWithValue("@id", idgudang.Text);
                            Connection.command.ExecuteNonQuery();


                            MessageBox.Show("Data Berhasil diperbarui.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            codeTxt.Enabled = false;
                            nameTxt.Enabled = false;
                            addressTxt.Enabled = false;
                            cmbGroupSupplier.Enabled = false;
                            plafonFlag.Enabled = false;
                            plafonTxt.Enabled = false;

                            refreshList(idgudang.Text);
                        }

                        button2.Text = "EDIT";
                        button1.Enabled = true;
                        button3.Enabled = true;
                        findBtn.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Mohon tutup aplikasinya dan silakan buka kembali.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.ConnectionClose();
                Connection.ConnectionOpen();
                string queryBarang = "DELETE FROM SUPPLIER WHERE id=@id";

                Connection.command = new OleDbCommand(queryBarang, Connection.conn);
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
            fieldTable.Add("sp.name", "Nama Supplier");
            fieldTable.Add("sp.code", "Kode Supplier");

            int promptValue = SearchSupplier.ShowDialog("Cari Supplier", fieldTable);

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
                addressTxt.Text = row.Cells["address"].Value.ToString();
                plafonTxt.Text = Int32.Parse(row.Cells["plafon"].Value.ToString()).ToString("N");
                if (row.Cells["plafontabFlag"].Value.ToString() == "1")
                {
                    plafonFlag.Checked = true;
                }
                else
                {
                    plafonFlag.Checked = false;
                }

                cmbGroupSupplier.SelectedValue = row.Cells["GSI"].Value.ToString();

                // etc.
            }
        }
    }
}
