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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory
{
    public partial class Item : Form
    {
        OleDbDataReader reader;
        public Item()
        {
            InitializeComponent();
            loadComboBox();
            codeTxt.CharacterCasing = CharacterCasing.Upper;
            nameTxt.CharacterCasing = CharacterCasing.Upper;
            refreshList("");

            dataGridView1.Columns["price"].DefaultCellStyle.Format = "N";//dari propertis juga bisa
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
            nfi = (NumberFormatInfo)nfi.Clone();
            nfi.CurrencySymbol = "";
            Double value;
            if (Double.TryParse(priceTxt.Text, out value))
                priceTxt.Text = String.Format(nfi, "{0:C2}", value);
            else
                priceTxt.Text = String.Empty;
        }

        private void Item_Load(object sender, EventArgs e)
        {
            codeTxt.Enabled = false;
            nameTxt.Enabled = false;
            priceTxt.Enabled = false;
            descriptionTxt.Enabled = false;

            cmbCategory.Enabled = false;
            cmbGroupItem.Enabled = false;
            cmbUnit.Enabled = false;
        }

        public void loadComboBox(){

            Connection.ConnectionClose();          
            Connection.ConnectionOpen();

            //TODO fill unit list
            string query = "SELECT * FROM UNIT WHERE DELETE_FLAG = 'N'";
            OleDbDataAdapter dAdapter = new OleDbDataAdapter(query, Connection.conn); 

            DataSet ds = new DataSet();
            dAdapter.Fill(ds, "Unit");

            cmbUnit.DisplayMember = "name";
            cmbUnit.ValueMember = "id";
            cmbUnit.DataSource = ds.Tables["Unit"];

            //TODO fill group Item List
            query = "SELECT * FROM group_item WHERE DELETE_FLAG = 'N'";
            dAdapter = new OleDbDataAdapter(query, Connection.conn);

            ds = new DataSet();
            dAdapter.Fill(ds, "Group_Item");

            cmbGroupItem.DisplayMember = "name";
            cmbGroupItem.ValueMember = "id";
            cmbGroupItem.DataSource = ds.Tables["Group_Item"];

            //TODO fill category list
            query = "SELECT * FROM category WHERE DELETE_FLAG = 'N'";
            dAdapter = new OleDbDataAdapter(query, Connection.conn);

            ds = new DataSet();
            dAdapter.Fill(ds, "Category");

            cmbCategory.DisplayMember = "name";
            cmbCategory.ValueMember = "id";
            cmbCategory.DataSource = ds.Tables["Category"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "ADD")
            {

                codeTxt.Enabled = true;
                nameTxt.Enabled = true;
                priceTxt.Enabled = true;
                descriptionTxt.Enabled = true;

                cmbCategory.Enabled = true;
                cmbGroupItem.Enabled = true;
                cmbUnit.Enabled = true;

                codeTxt.Text = "";
                nameTxt.Text = "";
                priceTxt.Text = "";
                descriptionTxt.Text = "";

                button1.Text = "SAVE";
                button2.Enabled = false;
                button3.Enabled = false;
                findBtn.Enabled = false;
                button4.Enabled = false;

                checkDataKosongAtauNgak();
                
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
                else if (priceTxt.Text == "")
                {
                    MessageBox.Show("Harga harus diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    priceTxt.Focus();
                }                
                else if (cmbUnit.SelectedValue == null)
                {
                    MessageBox.Show("Satuan harus diisi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbUnit.Focus();
                }
                else if (cmbGroupItem.SelectedValue == null)
                {
                    MessageBox.Show("Kelompok Barang harus diisi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbGroupItem.Focus();
                }
                else if (cmbCategory.SelectedValue == null)
                {                    
                    MessageBox.Show("Kategori harus diisi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbCategory.Focus();
                }
                else
                {
                    Connection.ConnectionClose();
                    Connection.ConnectionOpen();
                    string queryCheck = "SELECT * FROM ITEM WHERE code=@codec and delete_flag='N'";
                    Connection.command = new OleDbCommand(queryCheck, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Console.WriteLine(" codeTxt.Text = " + codeTxt.Text);
                    Connection.command.Parameters.AddWithValue("@codec", codeTxt.Text);
                    reader = Connection.command.ExecuteReader();
                    if (reader.Read())
                    {
                        //String id = Convert.ToString(reader["id"]);
                        //jika data yang mau saya buat itu belum ada...baru bisa insert baru..
                        DialogResult avaiable = MessageBox.Show("Kode Barang " + codeTxt.Text + " sudah ada", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            int id;
                            string query =   "INSERT INTO ITEM(code,name,description,price,unit_id,category_id,group_item_id,delete_flag ) "
                                            +" VALUES( @code,@names,@description,@price,@unit,@category,@groupitem,'N' )";
                            Connection.command = new OleDbCommand(query, Connection.conn);
                            Connection.command.CommandType = CommandType.Text;
                            Connection.command.Parameters.AddWithValue("@code", codeTxt.Text);
                            Connection.command.Parameters.AddWithValue("@names", nameTxt.Text);
                            Connection.command.Parameters.AddWithValue("@description", descriptionTxt.Text);
                            Connection.command.Parameters.AddWithValue("@price", priceTxt.Text);
                            Connection.command.Parameters.AddWithValue("@unit", cmbUnit.SelectedValue.ToString());
                            Connection.command.Parameters.AddWithValue("@category", cmbCategory.SelectedValue.ToString());
                            Connection.command.Parameters.AddWithValue("@groupitem", cmbGroupItem.SelectedValue.ToString());
                            Connection.command.ExecuteNonQuery();

                            Connection.command.CommandText = "Select @@Identity";
                            id = (int)Connection.command.ExecuteScalar();

                            createWarehouseItem(id);
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



                        string query2 = "select top 1 * from ITEM where delete_flag='N' ORDER BY ID DESC ";
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
                        priceTxt.Enabled = false;
                        descriptionTxt.Enabled = false;

                        cmbCategory.Enabled = false;
                        cmbGroupItem.Enabled = false;
                        cmbUnit.Enabled = false;

                        codeTxt.Text = "";
                        nameTxt.Text = "";
                        priceTxt.Text = "";
                        descriptionTxt.Text = "";

                        button1.Text = "ADD";
                        button2.Enabled = true;
                        button3.Enabled = true;
                        findBtn.Enabled = true;
                        button4.Enabled = true;

                        refreshList(nilaiyangdicari);
                    }


                    Connection.ConnectionClose();
                }

                checkDataKosongAtauNgak();
            }
        }

        private void createWarehouseItem(int item_id)
        {
            Connection.ConnectionClose();
            Connection.ConnectionOpen();
            string queryCheck = "SELECT * FROM WAREHOUSE WHERE delete_flag='N'";
            string queryInsert = "INSERT INTO WAREHOUSE_ITEM(item_id,warehouse_id,qty_first,qty_in,qty_out)VALUES('"+item_id+"',@warehouse,0,0,0)";
            Connection.command = new OleDbCommand(queryCheck, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            reader = Connection.command.ExecuteReader();
            while (reader.Read())
            {
                Connection.command = new OleDbCommand(queryInsert, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@warehouse", reader["ID"]);
                Connection.command.ExecuteNonQuery();
            }
        }

        private void refreshList(string nilaiyangdicari)//this one
        {
            List<Tuple<string, string, string, string, string, string>> list = new List<Tuple<string, string, string, string, string, string>>();
            string query = "SELECT  I.ID, I.CODE,I.NAME, I.PRICE,I.DELETE_FLAG, U.CODE AS UNIT, C.CODE AS CATEGORY, G.CODE AS GROUPITEM,U.ID AS UID, C.ID AS CID, G.ID AS GID,I.DESCRIPTION FROM" +
                "((ITEM  I LEFT JOIN UNIT U ON I.UNIT_ID = U.ID ) LEFT JOIN CATEGORY C ON I.CATEGORY_ID = C.ID) LEFT JOIN GROUP_ITEM G ON I.GROUP_ITEM_ID = G.ID ORDER BY I.CODE";


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
                if (dataGridView1.Rows.Count != 0)
                {
                    dataGridView1.Rows[0].Selected = true;
                }               
                
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
                idgudang.Text = row.Cells["id"].Value.ToString();
                codeTxt.Text = row.Cells["code"].Value.ToString();
                nameTxt.Text = row.Cells["name"].Value.ToString();
                priceTxt.Text = row.Cells["price"].Value.ToString();
                descriptionTxt.Text = row.Cells["description"].Value.ToString();
               
                cmbUnit.SelectedValue = row.Cells["uid"].Value.ToString();
                cmbGroupItem.SelectedValue = row.Cells["gid"].Value.ToString();
                cmbCategory.SelectedValue = row.Cells["cid"].Value.ToString();
                // etc.
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "EDIT")
            {

                codeTxt.Enabled = true;
                nameTxt.Enabled = true;
                priceTxt.Enabled = true;
                descriptionTxt.Enabled = true;

                cmbCategory.Enabled = true;
                cmbGroupItem.Enabled = true;
                cmbUnit.Enabled = true;

                button2.Text = "UPDATE";
                button1.Enabled = false;
                button3.Enabled = false;
                findBtn.Enabled = false;
                button4.Enabled = false;

                checkDataKosongAtauNgak();
            }
            else if (button2.Text == "UPDATE")
            {
                if (codeTxt.Text == "")
                {
                    MessageBox.Show("Kode harus diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    codeTxt.Focus();
                    return;
                }
                else if (nameTxt.Text == "")
                {
                    MessageBox.Show("Nama harus diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    nameTxt.Focus();
                    return;
                }
                else if (priceTxt.Text == "")
                {
                    MessageBox.Show("Harga harus diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    priceTxt.Focus();
                    return;
                }
                else if (cmbUnit.SelectedValue == null)
                {
                    MessageBox.Show("Satuan harus diisi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbUnit.Focus();
                    return;
                }
                else if (cmbGroupItem.SelectedValue == null)
                {
                    MessageBox.Show("Kelompok Barang harus diisi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbGroupItem.Focus();
                    return;
                }
                else if (cmbCategory.SelectedValue == null)
                {
                    MessageBox.Show("Kategori harus diisi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbCategory.Focus();
                    return;
                }
                else
                {
                    Connection.ConnectionClose();
                    Connection.ConnectionOpen();
                    string queryCheck = "SELECT * FROM ITEM WHERE code=@codec and delete_flag='N'";
                    Connection.command = new OleDbCommand(queryCheck, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@codec", codeTxt.Text);
                    reader = Connection.command.ExecuteReader();
                    if (reader.Read())
                    {
                        String id = Convert.ToString(reader["id"]);
                        //jika data yang mau saya buat itu belum ada...baru bisa insert baru..
                        if (id == idgudang.Text)
                        {
                            try
                            {
                                string query = "UPDATE ITEM SET code=@code,name=@namex,description=@description,price=@price,unit_id=@unit,category_id=@category,group_item_id=@groupitem WHERE id=@id";
                                Connection.command = new OleDbCommand(query, Connection.conn);
                                Connection.command.CommandType = CommandType.Text;
                                Connection.command.Parameters.AddWithValue("@code", codeTxt.Text);
                                Connection.command.Parameters.AddWithValue("@namex", nameTxt.Text);
                                Connection.command.Parameters.AddWithValue("@price", priceTxt.Text);
                                Connection.command.Parameters.AddWithValue("@unit", cmbUnit.SelectedValue.ToString());
                                Connection.command.Parameters.AddWithValue("@category", cmbCategory.SelectedValue.ToString());
                                Connection.command.Parameters.AddWithValue("@groupitem", cmbGroupItem.SelectedValue.ToString());
                                Connection.command.Parameters.AddWithValue("@description", descriptionTxt.Text);
                                Connection.command.Parameters.AddWithValue("@id", id);

                                Console.WriteLine("code.Text = " + codeTxt.Text);
                                Console.WriteLine("nameTxt.Text = " + nameTxt.Text);
                                Console.WriteLine("price.Text = " + priceTxt.Text);
                                Console.WriteLine("cmbunit.Text = " + cmbUnit.SelectedValue.ToString());
                                Console.WriteLine("cate.Text = " + cmbCategory.SelectedValue.ToString());
                                Console.WriteLine("cmbGroupItem.Text = " + cmbGroupItem.SelectedValue.ToString());
                                Console.WriteLine("descriptionTxt.Text = " + descriptionTxt.Text);

                                Connection.command.ExecuteNonQuery();
                                

                            }
                            catch (SqlException ex)
                            {
                                Console.WriteLine("SQL Exp = "+ex);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Exception Exp = " + ex);
                            }
                           
                            MessageBox.Show("Data Berhasil diperbarui.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            codeTxt.Enabled = false;
                            nameTxt.Enabled = false;
                            priceTxt.Enabled = false;
                            descriptionTxt.Enabled = false;

                            cmbCategory.Enabled = false;
                            cmbGroupItem.Enabled = false;
                            cmbUnit.Enabled = false;

                            refreshList(idgudang.Text);
                        }
                        else
                        {
                            DialogResult avaiable = MessageBox.Show("Kode Barang " + codeTxt.Text + " sudah terpakai", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (avaiable == DialogResult.Yes)
                            {
                                this.Close();
                            }
                            return;
                        }

                    }
                    else
                    {
                        string query = "UPDATE ITEM SET code=@code, name=@name, description=@description, price=@price,unit_id=@unit, category_id=@castegory,group_item_id=@groupitem WHERE id=@id";
                        Connection.command = new OleDbCommand(query, Connection.conn);
                        Connection.command.CommandType = CommandType.Text;
                        Connection.command.Parameters.AddWithValue("@code", codeTxt.Text);
                        Connection.command.Parameters.AddWithValue("@names", nameTxt.Text);
                        Connection.command.Parameters.AddWithValue("@description", descriptionTxt.Text);
                        Connection.command.Parameters.AddWithValue("@price", priceTxt.Text);
                        Connection.command.Parameters.AddWithValue("@unit", cmbUnit.SelectedValue.ToString());
                        Connection.command.Parameters.AddWithValue("@category", cmbCategory.SelectedValue.ToString());
                        Connection.command.Parameters.AddWithValue("@groupitem", cmbGroupItem.SelectedValue.ToString());
                        Connection.command.Parameters.AddWithValue("@id", idgudang.Text);
                        Connection.command.ExecuteNonQuery();


                        MessageBox.Show("Data Berhasil diperbarui.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        codeTxt.Enabled = false;
                        nameTxt.Enabled = false;
                        priceTxt.Enabled = false;
                        descriptionTxt.Enabled = false;

                        cmbCategory.Enabled = false;
                        cmbGroupItem.Enabled = false;
                        cmbUnit.Enabled = false;

                        refreshList(idgudang.Text);
                    }

                    button2.Text = "EDIT";
                    button1.Enabled = true;
                    button3.Enabled = true;
                    findBtn.Enabled = true;
                    button4.Enabled = true;
                }
            }
            checkDataKosongAtauNgak();
        }

        private void findBtn_Click(object sender, EventArgs e)
        {
            Hashtable fieldTable = new Hashtable();
            fieldTable.Add("i.name", "Nama Barang");
            fieldTable.Add("i.code", "Kode Barang");

            int promptValue = SearchItem.ShowDialog("Cari Barang", fieldTable);

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

        private void button4_Click(object sender, EventArgs e)
        {
            if (idgudang.Text == "ID")
            {
                MessageBox.Show("Tidak ada data!!!", "Terjadi Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                StockItem stock = new StockItem(idgudang.Text,nameTxt.Text);
                stock.ShowDialog(this);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.ConnectionClose();
                Connection.ConnectionOpen();
                string queryDeleteGudang = "DELETE FROM WAREHOUSE_ITEM WHERE item_id=@item_id";
                string queryBarang = "DELETE FROM ITEM WHERE id=@id";
                Connection.command = new OleDbCommand(queryDeleteGudang, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@item_id", idgudang.Text);
                Connection.command.ExecuteNonQuery();

                Connection.command = new OleDbCommand(queryBarang, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@id", idgudang.Text);
                Connection.command.ExecuteNonQuery();

                MessageBox.Show("Data Berhasil dihapus.", "Data Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshList("");

                checkDataKosongAtauNgak();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Tidak Bisa dihapus, karena data ini masih dipakai.", "Cannot  Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 
        }

        public void checkDataKosongAtauNgak()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                codeTxt.Text = "";
                nameTxt.Text = "";
                priceTxt.Text = "";
                descriptionTxt.Text = "";

                idgudang.Text = "ID";
            }
        }
    }
}
