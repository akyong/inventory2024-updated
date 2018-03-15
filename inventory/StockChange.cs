using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using inventory.reporting;
using inventory.searching;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory
{
    public partial class StockChange : Form
    {
        OleDbDataReader reader;
        MDIForm mdi;
        public StockChange(MDIForm mdinya)
        {
            InitializeComponent();
            mdi = mdinya;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void StockChange_Load(object sender, EventArgs e)
        {
            nobuktiTxt.Enabled = false;
            tanggalTxt.Enabled = false;
            cmbFrom.Enabled = false;
            cmbTo.Enabled = false;
            descriptionTxt.Enabled = false;
            dataGridView1.Enabled = false;

            if (idpindahgudang.Text == "ID")
            {
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }
            else
            {
                button4.Enabled = true;
            }
            loadComboBox();
           
        }

        public void loadComboBox()
        {

            Connection.ConnectionClose();
            Connection.ConnectionOpen();

            DataTable DT = (DataTable)dataGridView1.DataSource;
            if (DT != null)
                DT.Clear();

            //TODO fill unit list
            string query = "SELECT * FROM ITEM WHERE DELETE_FLAG = 'N'";
            OleDbDataAdapter dAdapter = new OleDbDataAdapter(query, Connection.conn);

            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            dAdapter.Fill(ds, "Item");
            (dataGridView1.Columns[0] as DataGridViewComboBoxColumn).DisplayMember = "name";
            (dataGridView1.Columns[0] as DataGridViewComboBoxColumn).ValueMember = "id";
            (dataGridView1.Columns[0] as DataGridViewComboBoxColumn).DataSource = ds.Tables["Item"];




            //TODO fill group Item List
            query = "SELECT * FROM WAREHOUSE WHERE DELETE_FLAG = 'N'";
            dAdapter = new OleDbDataAdapter(query, Connection.conn);
            ds = new DataSet();
            dAdapter.Fill(ds, "Warehouse");
            dAdapter.Fill(ds2, "Warehouse2");

            cmbFrom.DisplayMember = "name";
            cmbFrom.ValueMember = "id";
            cmbFrom.DataSource = ds.Tables["Warehouse"];

            cmbTo.DisplayMember = "name";
            cmbTo.ValueMember = "id";
            cmbTo.DataSource = ds2.Tables["Warehouse2"];

        //    //TODO fill category list
        //    query = "SELECT * FROM category WHERE DELETE_FLAG = 'N'";
        //    dAdapter = new OleDbDataAdapter(query, Connection.conn);

        //    ds = new DataSet();
        //    dAdapter.Fill(ds, "Category");

        //    cmbCategory.DisplayMember = "name";
        //    cmbCategory.ValueMember = "id";
        //    cmbCategory.DataSource = ds.Tables["Category"];
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column2_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == 1) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column2_KeyPress);
                }
            }
        }

        private void Column2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["Qty"].Value = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "ADD")
            {

                nobuktiTxt.Enabled = true;
                tanggalTxt.Enabled = true;
                cmbFrom.Enabled = true;
                cmbTo.Enabled = true;
                descriptionTxt.Enabled = true;
                dataGridView1.Enabled = true;
                button4.Enabled = false;

                nobuktiTxt.Text = "(AUTO)";
                tanggalTxt.Value = DateTime.Now;
                descriptionTxt.Text = "";

                
                loadComboBox();
                
                //dataGridView1.Refresh();
               

                button1.Text = "SAVE";
                button2.Enabled = false;
                button3.Enabled = false;
                findBtn.Enabled = false;
            }
            else if (button1.Text == "SAVE")
            {
                //TODO Check DataGridview is null or not

                string gudangasal = "";
                string gudangtujuan = "";

                Boolean errorDatagridviewnull = false;
                Console.WriteLine("dataGridView1.Rows.Count = " + dataGridView1.Rows.Count);
                if (dataGridView1.Rows.Count == 1)
                {
                    for (int rows = 0; rows < dataGridView1.Rows.Count; rows++)
                    {
                        DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dataGridView1.Rows[rows].Cells[0];
                        try
                        {
                            string valueItem = dataGridView1.Rows[rows].Cells[0].Value.ToString();
                            string valueQty = dataGridView1.Rows[rows].Cells[1].Value.ToString();
                            int qtynya = Int32.Parse(valueQty);
                            if (valueItem == null)//kalau dropdown item
                            {
                                errorDatagridviewnull = true;
                            }
                            if (qtynya == 0 || qtynya < 0)//kalau qty 0 atau qty kurang dari 0 alias -1 atau lebih kurang dari ini
                            {
                                errorDatagridviewnull = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("ex = "+ex);
                            errorDatagridviewnull = true;
                        }

                        if (errorDatagridviewnull) //kalau errorDatagriview nilainya true maka loopingan diberhentikan.
                        {
                            rows = dataGridView1.Rows.Count;
                        }
                    }
                }
                else
                {
                    for (int rows = 0; rows < dataGridView1.Rows.Count - 1; rows++)
                    {
                        DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dataGridView1.Rows[rows].Cells[0];
                        try
                        {
                            string valueItem = dataGridView1.Rows[rows].Cells[0].Value.ToString();
                            string valueQty = dataGridView1.Rows[rows].Cells[1].Value.ToString();
                            int qtynya = Int32.Parse(valueQty);
                            if (valueItem == null)//kalau dropdown item
                            {
                                errorDatagridviewnull = true;
                            }
                            if (qtynya == 0 || qtynya < 0)//kalau qty 0 atau qty kurang dari 0 alias -1 atau lebih kurang dari ini
                            {
                                errorDatagridviewnull = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            errorDatagridviewnull = true;
                        }

                        if (errorDatagridviewnull) //kalau errorDatagriview nilainya true maka loopingan diberhentikan.
                        {
                            rows = dataGridView1.Rows.Count;
                        }
                    }
                }

                if (nobuktiTxt.Text == "")
                {
                    MessageBox.Show("Nomor Bukti Kosong, Sistem akan mengisinya secara otomatis", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   
                }
                if (cmbFrom.SelectedValue == null)
                {
                    MessageBox.Show("Gudang Asal harus diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbFrom.Focus();
                }
                else if (cmbTo.SelectedValue == null)
                {
                    MessageBox.Show("Gudang Tujuan harus diisi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbTo.Focus();
                }
                else if (cmbTo.SelectedValue.ToString() == cmbFrom.SelectedValue.ToString())
                {
                    MessageBox.Show("Gudang Asal tidak boleh sama dengan Gudang Tujuan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbFrom.Focus();
                }
                else if(errorDatagridviewnull) //kalau errorDatagriview nilainya true maka tampilin error message
                {
                    MessageBox.Show("Barang yang mau dipindahkan tidak boleh kosong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridView1.Focus();
                }
                else
                {
                    Connection.ConnectionClose();
                    Connection.ConnectionOpen();

                    string nobukti = "";
                    String hari = String.Format("{0:dd}", DateTime.Now);
                    String bulan = String.Format("{0:MM}", DateTime.Now);
                    String tahun = String.Format("{0:yy}", DateTime.Now);
                    int bukti_ke = 0;
                    if (nobuktiTxt.Text == "" || nobuktiTxt.Text == "(AUTO)")
                    {
                        string queryCheck = "SELECT SEQ_NUMBER FROM SEQUENCE_STORE WHERE SEQ_CODE='pindahgudang'";
                        Connection.command = new OleDbCommand(queryCheck, Connection.conn);
                        Connection.command.CommandType = CommandType.Text;
                        reader = Connection.command.ExecuteReader();
                        if (reader.Read())
                        {
                            bukti_ke = Int32.Parse(reader["seq_number"].ToString())+1;
                        }
                        nobukti = string.Concat(string.Concat(string.Concat(hari, bulan), tahun),getResult(bukti_ke));
                    }

                    string query = "SELECT * FROM PINDAH_GUDANG WHERE no_bukti=@nobukti and delete_flag='N'";
                    Connection.command = new OleDbCommand(query, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@nobukti", nobukti);
                    reader = Connection.command.ExecuteReader();
                    if (reader.Read())
                    {
                        //String id = Convert.ToString(reader["id"]);
                        //jika data yang mau saya buat itu belum ada...baru bisa insert baru..
                        DialogResult avaiable = MessageBox.Show("No. Bukti" + nobukti + " sudah ada", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            string queryInsert = "INSERT INTO PINDAH_GUDANG(no_bukti,tgl_pindah,from_warehouse,to_warehouse,description,delete_flag) "
                                            + " VALUES( @nobukti,@tanggal,@darigudang,@kegudang,@description,'N' )";
                            Connection.command = new OleDbCommand(queryInsert, Connection.conn);
                            Connection.command.CommandType = CommandType.Text;
                            Connection.command.Parameters.AddWithValue("@nobukti", nobukti);
                            Connection.command.Parameters.Add("@tanggal", OleDbType.Date).Value = tanggalTxt.Value;
                            Connection.command.Parameters.AddWithValue("@darigudang", cmbFrom.SelectedValue);
                            Connection.command.Parameters.AddWithValue("@kegudang", cmbTo.SelectedValue);
                            Connection.command.Parameters.AddWithValue("@description", descriptionTxt.Text);
                            Connection.command.ExecuteNonQuery();

                            gudangasal = cmbFrom.SelectedValue.ToString();
                            gudangtujuan = cmbTo.SelectedValue.ToString();

                            Connection.command.CommandText = "Select @@Identity";
                            id = (int)Connection.command.ExecuteScalar();
                            idpindahgudang.Text = id.ToString();
                            string queryGet = "SELECT * FROM PINDAH_GUDANG WHERE id=@id and delete_flag='N'";
                            Connection.command = new OleDbCommand(queryGet, Connection.conn);
                            Connection.command.CommandType = CommandType.Text;
                            Connection.command.Parameters.AddWithValue("@id", id);
                            reader = Connection.command.ExecuteReader();
                            if (reader.Read())
                            {
                                nobuktiTxt.Text = reader["no_bukti"].ToString();
                                tanggalTxt.Value = Convert.ToDateTime(reader["tgl_pindah"]);
                                cmbFrom.SelectedValue = reader["from_warehouse"].ToString();
                                cmbTo.SelectedValue = reader["to_warehouse"].ToString();
                                descriptionTxt.Text = reader["description"].ToString();
                            }
                            //Simpan detail
                            try
                            {

                                for (int rows = 0; rows < dataGridView1.Rows.Count-1; rows++)
                                {                                    DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dataGridView1.Rows[rows].Cells[0];
                                    Console.WriteLine("cb = " + cb.Value);
                                    string valueItem = dataGridView1.Rows[rows].Cells[0].Value.ToString();
                                    string valueQty = dataGridView1.Rows[rows].Cells[1].Value.ToString();
                                    string valueDesc = "";
                                    if(dataGridView1.Rows[rows].Cells[2].Value != null){
                                        valueDesc = dataGridView1.Rows[rows].Cells[2].Value.ToString();
                                    }
                                    
                                    
                                    string queryInsertDetail = "INSERT INTO PINDAH_GUDANG_DETAIL(pindah_gudang_id,item_id,qty,description) "
                                           + " VALUES( @pindahgudang,@item,@qty,@description)";
                                    Connection.command = new OleDbCommand(queryInsertDetail, Connection.conn);
                                    Connection.command.CommandType = CommandType.Text;
                                    Connection.command.Parameters.AddWithValue("@pindahgudang", id);
                                    Connection.command.Parameters.AddWithValue("@item", valueItem);
                                    Connection.command.Parameters.AddWithValue("@qty", valueQty);
                                    Connection.command.Parameters.AddWithValue("@description", valueDesc);
                                    Connection.command.ExecuteNonQuery();

                                    UpdateStock.UpdateNilaiStock(gudangasal, gudangtujuan, Int32.Parse(valueQty), valueItem);

                                }

                                string queryAddToSequence = "UPDATE SEQUENCE_STORE SET SEQ_NUMBER=@INCREMENTNYA WHERE SEQ_CODE='pindahgudang'";
                                Connection.command = new OleDbCommand(queryAddToSequence, Connection.conn);
                                Connection.command.CommandType = CommandType.Text;
                                Connection.command.Parameters.AddWithValue("@INCREMENTNYA", bukti_ke);
                                Connection.command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Terjadi Kesalahan saat menyimpan data, mohon hubungi pengembang! /n" + ex);
                            }
                            
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



                        string queryAmbilSetelahInsert = "SELECT PD.ITEM_ID AS ITEM, PD.QTY AS QTY, PD.DESCRIPTION AS DESCRIPTION FROM PINDAH_GUDANG_DETAIL PD WHERE PD.PINDAH_GUDANG_ID="+idpindahgudang.Text;
                   

                        OleDbCommand cmd = new OleDbCommand(queryAmbilSetelahInsert, Connection.conn);
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        DataTable scores = new DataTable();
                        da.Fill(scores);
                        dataGridView1.DataSource = scores;

                       


                        nobuktiTxt.Enabled = false;
                        tanggalTxt.Enabled = false;
                        cmbFrom.Enabled = false;
                        cmbTo.Enabled = false;
                        descriptionTxt.Enabled = false;
                        dataGridView1.Enabled = false;


                        button1.Text = "ADD";
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = true;
                        findBtn.Enabled = true;

                        Connection.ConnectionClose();

                      

                        //refreshList(nilaiyangdicari);
                    }


                    Connection.ConnectionClose();
                }

                //checkDataKosongAtauNgak();
            }
        }

        private string getResult(int x)
        {
            return x.ToString().PadLeft(4, '0');
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "EDIT")
            {
                nobuktiTxt.Enabled = true;
                tanggalTxt.Enabled = true;
                cmbFrom.Enabled = true;
                cmbTo.Enabled = true;
                descriptionTxt.Enabled = true;
                dataGridView1.Enabled = true;
               
                button2.Text = "UPDATE";
                button1.Enabled = false;
                button3.Enabled = false;
                findBtn.Enabled = false;

            }
            else if (button2.Text == "UPDATE")
            {
                string gudangasal = "";
                string gudangtujuan = "";

                Boolean errorDatagridviewnull = false;

                if (dataGridView1.Rows.Count == 1)
                {
                    for (int rows = 0; rows < dataGridView1.Rows.Count; rows++)
                    {
                        DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dataGridView1.Rows[rows].Cells[0];
                        try
                        {
                            string valueItem = dataGridView1.Rows[rows].Cells[0].Value.ToString();
                            string valueQty = dataGridView1.Rows[rows].Cells[1].Value.ToString();
                            int qtynya = Int32.Parse(valueQty);
                            if (valueItem == null)//kalau dropdown item
                            {
                                errorDatagridviewnull = true;
                            }
                            if (qtynya == 0 || qtynya < 0)//kalau qty 0 atau qty kurang dari 0 alias -1 atau lebih kurang dari ini
                            {
                                errorDatagridviewnull = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            errorDatagridviewnull = true;
                        }

                        if (errorDatagridviewnull) //kalau errorDatagriview nilainya true maka loopingan diberhentikan.
                        {
                            rows = dataGridView1.Rows.Count;
                        }
                    }
                }
                else
                {
                    for (int rows = 0; rows < dataGridView1.Rows.Count - 1; rows++)
                    {
                        DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dataGridView1.Rows[rows].Cells[0];
                        try
                        {
                            string valueItem = dataGridView1.Rows[rows].Cells[0].Value.ToString();
                            string valueQty = dataGridView1.Rows[rows].Cells[1].Value.ToString();
                            int qtynya = Int32.Parse(valueQty);
                            if (valueItem == null)//kalau dropdown item
                            {
                                errorDatagridviewnull = true;
                            }
                            if (qtynya == 0 || qtynya < 0)//kalau qty 0 atau qty kurang dari 0 alias -1 atau lebih kurang dari ini
                            {
                                errorDatagridviewnull = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            errorDatagridviewnull = true;
                        }

                        if (errorDatagridviewnull) //kalau errorDatagriview nilainya true maka loopingan diberhentikan.
                        {
                            rows = dataGridView1.Rows.Count;
                        }
                    }
                }


                Connection.ConnectionClose();
                Connection.ConnectionOpen();
                string queryCheck = "SELECT * FROM PINDAH_GUDANG WHERE no_bukti=@nobukti and delete_flag='N'";
                Connection.command = new OleDbCommand(queryCheck, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@no_bukti", nobuktiTxt.Text);
                reader = Connection.command.ExecuteReader();
                if (reader.Read())
                {
                    String id = Convert.ToString(reader["id"]);
                    gudangasal = Convert.ToString(reader["from_warehouse"]);
                    gudangtujuan = Convert.ToString(reader["to_warehouse"]);
                    //jika data yang mau saya buat itu belum ada...baru bisa insert baru..
                    if (id == idpindahgudang.Text)
                    {

                        if (nobuktiTxt.Text == "")
                        {
                            MessageBox.Show("Nomor Bukti Kosong, Sistem akan mengisinya secara otomatis", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        if (cmbFrom.SelectedValue == null)
                        {
                            MessageBox.Show("Gudang Asal harus diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbFrom.Focus();
                        }
                        else if (cmbTo.SelectedValue == null)
                        {
                            MessageBox.Show("Gudang Tujuan harus diisi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbTo.Focus();
                        }
                        else if (cmbTo.SelectedValue.ToString() == cmbFrom.SelectedValue.ToString())
                        {
                            MessageBox.Show("Gudang Asal tidak boleh sama dengan Gudang Tujuan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbFrom.Focus();
                        }
                        else if (errorDatagridviewnull) //kalau errorDatagriview nilainya true maka tampilin error message
                        {
                            MessageBox.Show("Barang yang mau dipindahkan tidak boleh kosong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dataGridView1.Focus();
                        }
                        else
                        {
                            try
                            {
                                string query = "UPDATE PINDAH_GUDANG SET no_bukti=@nobukti, tgl_pindah=@tanggal,from_warehouse=@gudangasal,to_warehouse=@gudangtujuan,description=@description WHERE id=@id";
                                Connection.command = new OleDbCommand(query, Connection.conn);
                                Connection.command.CommandType = CommandType.Text;
                                Connection.command.Parameters.AddWithValue("@nobukti", nobuktiTxt.Text);
                                Connection.command.Parameters.Add("@tanggal", OleDbType.Date).Value = tanggalTxt.Value;
                                Connection.command.Parameters.AddWithValue("@darigudang", cmbFrom.SelectedValue);
                                Connection.command.Parameters.AddWithValue("@kegudang", cmbTo.SelectedValue);
                                Connection.command.Parameters.AddWithValue("@description", descriptionTxt.Text);
                                Connection.command.Parameters.AddWithValue("@id", idpindahgudang.Text);
                                Connection.command.ExecuteNonQuery();

                                string queryGetDetail = "SELECT * FROM PINDAH_GUDANG_DETAIL WHERE PINDAH_GUDANG_ID=@id ";
                                Connection.command = new OleDbCommand(queryGetDetail, Connection.conn);
                                Connection.command.CommandType = CommandType.Text;
                                Connection.command.Parameters.AddWithValue("@id", idpindahgudang.Text);
                                reader = Connection.command.ExecuteReader();
                                while (reader.Read())
                                {
                                    Console.WriteLine("item id = " + reader["item_id"].ToString());
                                    UpdateStock.ReverseUpdateNilaiStock(gudangasal, gudangtujuan, Int32.Parse(reader["qty"].ToString()), reader["item_id"].ToString());
                                }

                                string deleteAllDetail = "DELETE FROM PINDAH_GUDANG_DETAIL  WHERE PINDAH_GUDANG_ID=@id ";
                                Connection.command = new OleDbCommand(deleteAllDetail, Connection.conn);
                                Connection.command.CommandType = CommandType.Text;
                                Connection.command.Parameters.AddWithValue("@id", idpindahgudang.Text);
                                Connection.command.ExecuteNonQuery();

                                for (int rows = 0; rows < dataGridView1.Rows.Count - 1; rows++)
                                {
                                    DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dataGridView1.Rows[rows].Cells[0];
                                    Console.WriteLine("cb = " + cb.Value);
                                    string valueItem = dataGridView1.Rows[rows].Cells[0].Value.ToString();
                                    string valueQty = dataGridView1.Rows[rows].Cells[1].Value.ToString();
                                    string valueDesc = "";
                                    if (dataGridView1.Rows[rows].Cells[2].Value != null)
                                    {
                                        valueDesc = dataGridView1.Rows[rows].Cells[2].Value.ToString();
                                    }


                                    string queryInsertDetail = "INSERT INTO PINDAH_GUDANG_DETAIL(pindah_gudang_id,item_id,qty,description) "
                                           + " VALUES( @pindahgudang,@item,@qty,@description)";
                                    Connection.command = new OleDbCommand(queryInsertDetail, Connection.conn);
                                    Connection.command.CommandType = CommandType.Text;
                                    Connection.command.Parameters.AddWithValue("@pindahgudang", id);
                                    Connection.command.Parameters.AddWithValue("@item", valueItem);
                                    Connection.command.Parameters.AddWithValue("@qty", valueQty);
                                    Connection.command.Parameters.AddWithValue("@description", valueDesc);
                                    Connection.command.ExecuteNonQuery();

                                    UpdateStock.UpdateNilaiStock(gudangasal, gudangtujuan, Int32.Parse(valueQty), valueItem);

                                }
                            }
                            catch (SqlException ex)
                            {
                                Console.WriteLine("SQL Exp = " + ex);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Exception Exp = " + ex);
                            }



                        }



                        string queryAmbilSetelahInsert = "SELECT PD.ITEM_ID AS ITEM, PD.QTY AS QTY, PD.DESCRIPTION AS DESCRIPTION FROM PINDAH_GUDANG_DETAIL PD WHERE PD.PINDAH_GUDANG_ID=" + idpindahgudang.Text;


                        OleDbCommand cmd = new OleDbCommand(queryAmbilSetelahInsert, Connection.conn);
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        DataTable scores = new DataTable();
                        da.Fill(scores);
                        dataGridView1.DataSource = scores;

                        MessageBox.Show("Data Berhasil diperbarui.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        nobuktiTxt.Enabled = false;
                        tanggalTxt.Enabled = false;
                        cmbFrom.Enabled = false;
                        cmbTo.Enabled = false;
                        descriptionTxt.Enabled = false;
                        dataGridView1.Enabled = false;
                        button2.Text = "EDIT";
                        button1.Enabled = true;
                        button3.Enabled = true;
                        findBtn.Enabled = true;

                    }
                }
            }
            Connection.ConnectionClose();
        }

        private void findBtn_Click(object sender, EventArgs e)
        {
            Hashtable fieldTable = new Hashtable();
            fieldTable.Add("PD.no_bukti", "No. Bukti");
            //fieldTable.Add("PD.tgl_bukti", "Tanggal Bukti");

            int promptValue = SearchPindahGudang.ShowDialog("Cari Bukti Pindah Gudang", fieldTable);

            idpindahgudang.Text = promptValue.ToString();

            string queryGet = "SELECT * FROM PINDAH_GUDANG WHERE id=@id and delete_flag='N'";
            Connection.command = new OleDbCommand(queryGet, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.Parameters.AddWithValue("@id", idpindahgudang.Text);
            reader = Connection.command.ExecuteReader();
            if (reader.Read())
            {
                nobuktiTxt.Text = reader["no_bukti"].ToString();
                tanggalTxt.Value = Convert.ToDateTime(reader["tgl_pindah"]);
                cmbFrom.SelectedValue = reader["from_warehouse"].ToString();
                cmbTo.SelectedValue = reader["to_warehouse"].ToString();
                descriptionTxt.Text = reader["description"].ToString();
            }

            string queryAmbilSetelahInsert = "SELECT PD.ITEM_ID AS ITEM, PD.QTY AS QTY, PD.DESCRIPTION AS DESCRIPTION FROM PINDAH_GUDANG_DETAIL PD WHERE PD.PINDAH_GUDANG_ID=" + idpindahgudang.Text;
     
            OleDbCommand cmd = new OleDbCommand(queryAmbilSetelahInsert, Connection.conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable scores = new DataTable();
            da.Fill(scores);
            dataGridView1.DataSource = scores;

            button2.Enabled = true;
            button3.Enabled = true;
            if (idpindahgudang.Text != "ID")
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string gudangasal = "";
            string gudangtujuan = "";

            DialogResult dialogResult = MessageBox.Show("Apakah anda yakin akan menghapus bukti pindah gudang ini?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string queryGetBukti = "SELECT * FROM PINDAH_GUDANG WHERE ID=@id";
                Connection.command = new OleDbCommand(queryGetBukti, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@id", idpindahgudang.Text);
                reader = Connection.command.ExecuteReader();
                if (reader.Read())
                {
                    gudangasal = Convert.ToString(reader["from_warehouse"]);
                    gudangtujuan = Convert.ToString(reader["to_warehouse"]);

                }

                string queryGetStock = "SELECT * FROM PINDAH_GUDANG_DETAIL WHERE PINDAH_GUDANG_ID=@id";
                Connection.command = new OleDbCommand(queryGetStock, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@id", idpindahgudang.Text);
                reader = Connection.command.ExecuteReader();
                while (reader.Read())
                {
                    UpdateStock.ReverseUpdateNilaiStock(gudangasal, gudangtujuan, Int32.Parse(reader["qty"].ToString()), reader["item_id"].ToString());
                }

                string queryHapusDetail = "DELETE FROM PINDAH_GUDANG_DETAIL WHERE PINDAH_GUDANG_ID=@id ";
                string queryHapusHead = "DELETE FROM PINDAH_GUDANG WHERE ID=@id ";
                Connection.command = new OleDbCommand(queryHapusDetail, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@id", idpindahgudang.Text);
                Connection.command.ExecuteNonQuery();

                Connection.command = new OleDbCommand(queryHapusHead, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@id", idpindahgudang.Text);
                Connection.command.ExecuteNonQuery();

                MessageBox.Show("Bukti Pindah Gudang Berhasil dihapus, Stock Barang telah dikembalikan kesemula", "Delete Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Aksi Penghapusan Bukti Pindah Gudang Telah dibatalkan!", "Delete Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ReportViewerPindahGudang))
                {
                    form.Activate();
                    return;
                }
            }
            ReportViewerPindahGudang rv = new ReportViewerPindahGudang(idpindahgudang.Text);
            rv.MdiParent = mdi;
            rv.Show();

          

            
        }

        //private void revisiNilaiStock(string idgudangasal,string idgudangtujuan, int jumlahstock, string itemid)
        //{
        //    string queryKurangStock = "UPDATE WAREHOUSE_ITEM SET QTY_OUT= [QTY_OUT]+"+jumlahstock+" WHERE ITEM_ID ="+itemid+" AND WAREHOUSE_ID="+idgudangasal;
        //    string queryTambahStock = "UPDATE WAREHOUSE_ITEM SET QTY_IN= [QTY_IN]+" + jumlahstock + " WHERE ITEM_ID =" + itemid + " AND WAREHOUSE_ID=" + idgudangtujuan;
        //    Connection.command = new OleDbCommand(queryKurangStock, Connection.conn);
        //    Connection.command.CommandType = CommandType.Text;
        //    Connection.command.ExecuteNonQuery();

        //    Connection.command = new OleDbCommand(queryTambahStock, Connection.conn);
        //    Connection.command.CommandType = CommandType.Text;
        //    Connection.command.ExecuteNonQuery();
        
        //}

        
    }
}
