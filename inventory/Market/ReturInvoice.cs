using System;
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

namespace inventory.Market
{
    public partial class ReturInvoice : Form
    {
        OleDbDataReader reader;
        public static string idinvoice;
        public static string noinvoice;
        public static string customer;
        public static int id;
        public ReturInvoice(string id,string nobuk, string cusname)
        {
            InitializeComponent();
            idinvoice = id;
            noinvoice = nobuk;
            customer = cusname;
            label2.Text = noinvoice;
            label4.Text = customer;
        }

        private void ReturInvoice_Load(object sender, EventArgs e)
        {
            Connection.ConnectionClose();
            Connection.ConnectionOpen();

            string tampungid = "0";

            string querygetItem = "SELECT ITEM_ID FROM INVOICE_DETAIL WHERE INVOICE_ID = @invoiceid";
            Connection.command = new OleDbCommand(querygetItem, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.Parameters.AddWithValue("@invoiceid", idinvoice);
            reader = Connection.command.ExecuteReader();
            while (reader.Read())
            {
                tampungid = tampungid + "," + reader["ITEM_ID"].ToString();                
            }
            Console.WriteLine("tampungid = " + tampungid);

            //TODO fill ITEM list
            string query = "SELECT * FROM ITEM WHERE DELETE_FLAG = 'N' AND ID IN(" + tampungid + ") ORDER BY NAME";
            OleDbDataAdapter dAdapter = new OleDbDataAdapter(query, Connection.conn);

            DataSet ds1 = new DataSet();
            dAdapter.Fill(ds1, "Item");
            (dataGridView1.Columns[0] as DataGridViewComboBoxColumn).DisplayMember = "name";
            (dataGridView1.Columns[0] as DataGridViewComboBoxColumn).ValueMember = "id";
            (dataGridView1.Columns[0] as DataGridViewComboBoxColumn).DataSource = ds1.Tables["Item"];
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(AllowNumber_KeyPress);


            if (((DataGridView)sender).CurrentCell.ColumnIndex == 0) //Assuming 0 is the index of the ComboBox Column you want to show
            {
                ComboBox cb = e.Control as ComboBox;
                if (cb != null)
                {
                    cb.SelectionChangeCommitted -= new EventHandler(cb_SelectedIndexChanged);
                    // now attach the event handler
                    cb.SelectionChangeCommitted += new EventHandler(cb_SelectedIndexChanged);
                }
            }
        }

        private void AllowNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tb = dataGridView1.EditingControl as ComboBox;
            if (tb != null)
            {
                string str = tb.SelectedValue != null ? tb.SelectedValue.ToString() : null;
                int currentRow = dataGridView1.CurrentRow.Index;
                Console.WriteLine("currentRow = " + currentRow);
                auto_complete_harga(str, currentRow);
            }
        }

        public void auto_complete_harga(string item_id, int currentRow)//auto set harga pas barang di pilih
        {
            Connection.ConnectionClose();
            Connection.ConnectionOpen();

            string query = "SELECT PRICE FROM ITEM WHERE ID=@ID";
            Connection.command = new OleDbCommand(query, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.Parameters.AddWithValue("@ID", item_id);
            reader = Connection.command.ExecuteReader();
            if (reader.Read())
            {
                Console.WriteLine("harga rader = " + reader["price"].ToString());
                dataGridView1[2, currentRow].Value = Double.Parse(reader["price"].ToString());
            }

            Double qty = 0;
            Double harga = Double.Parse(reader["price"].ToString());
            Double discount = 0;

            try
            {
                if (!String.IsNullOrEmpty(dataGridView1[1, currentRow].Value.ToString()))//Qty
                {
                    double value;
                    if (Double.TryParse(dataGridView1[1, currentRow].Value.ToString(), out value))
                        qty = value;
                    else
                        qty = 0; // discount will have null value
                }
            }
            catch (Exception ex)
            {
                qty = 0;
            }


            try
            {
                if (!String.IsNullOrEmpty(dataGridView1[3, currentRow].Value.ToString()))//discount
                {
                    double value;
                    if (Double.TryParse(dataGridView1[3, currentRow].Value.ToString(), out value))
                        discount = value;
                    else
                        discount = 0; // discount will have null value
                }
            }
            catch (Exception ex)
            {
                discount = 0; // discount will have null value
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Apakah anda yakin akan menghapus Item/Barang ini?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.RemoveAt(item.Index);
                }
            }     
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private string getResult(int x)
        {
            return x.ToString().PadLeft(4, '0');
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Boolean errorDatagridviewnull = false;
            Double totalAmount = 0;
            if (dataGridView1.Rows.Count == 1)
            {
                errorDatagridviewnull = true;
            }
            else if (dataGridView1.Rows.Count == 2)
            {
                for (int rows = 0; rows < dataGridView1.Rows.Count - 1; rows++)
                {
                    DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dataGridView1.Rows[rows].Cells[0];
                    try
                    {
                        string valueItem = dataGridView1.Rows[rows].Cells[0].Value.ToString();
                        string valueQty = dataGridView1.Rows[rows].Cells[1].Value.ToString();
                        string valueHarga = dataGridView1.Rows[rows].Cells[2].Value.ToString();

                        Double qtynya = Double.Parse(valueQty);
                        Double harganya = Double.Parse(valueHarga);
                        Double disc = 0;

                        try
                        {
                            if (!String.IsNullOrEmpty(dataGridView1.Rows[rows].Cells[3].Value.ToString()))//Qty
                            {
                                double value;
                                if (Double.TryParse(dataGridView1.Rows[rows].Cells[3].Value.ToString(), out value))
                                {
                                    disc = value;
                                }
                                else
                                {
                                    disc = 0; // discount will have null value
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            disc = 0;
                        }

                        Double subtotalnya = (qtynya * harganya) - disc;
                        totalAmount = totalAmount + subtotalnya;
                        if (valueItem == null)//kalau dropdown item
                        {
                            errorDatagridviewnull = true;
                        }
                        if (qtynya == 0 || qtynya < 0)//kalau qty 0 atau qty kurang dari 0 alias -1 atau lebih kurang dari ini
                        {
                            errorDatagridviewnull = true;
                        }
                        if (harganya == 0 || harganya < 0)//kalau qty 0 atau qty kurang dari 0 alias -1 atau lebih kurang dari ini
                        {
                            errorDatagridviewnull = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ex = " + ex);
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
                        string valueHarga = dataGridView1.Rows[rows].Cells[2].Value.ToString();


                        Double qtynya = Double.Parse(valueQty);
                        Double harganya = Double.Parse(valueHarga);


                        Double disc = 0;
                        try
                        {
                            if (!String.IsNullOrEmpty(dataGridView1.Rows[rows].Cells[3].Value.ToString()))//Qty
                            {
                                double value;
                                if (Double.TryParse(dataGridView1.Rows[rows].Cells[3].Value.ToString(), out value))
                                {
                                    disc = value;
                                }
                                else
                                {
                                    disc = 0; // discount will have null value
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            disc = 0;
                        }
                        Double subtotalnya = (qtynya * harganya) - disc;
                        totalAmount = totalAmount + subtotalnya;
                        if (valueItem == null)//kalau dropdown item
                        {
                            errorDatagridviewnull = true;
                        }
                        if (qtynya == 0 || qtynya < 0)//kalau qty 0 atau qty kurang dari 0 alias -1 atau lebih kurang dari ini
                        {
                            errorDatagridviewnull = true;
                        }
                        if (harganya == 0 || harganya < 0)//kalau qty 0 atau qty kurang dari 0 alias -1 atau lebih kurang dari ini
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

            if (errorDatagridviewnull) //kalau errorDatagriview nilainya true maka tampilin error message
            {
                MessageBox.Show("Qty atau Barang tidak boleh kosong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                string queryCheck = "SELECT SEQ_NUMBER FROM SEQUENCE_STORE WHERE SEQ_CODE='returnsi'";
                Connection.command = new OleDbCommand(queryCheck, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                reader = Connection.command.ExecuteReader();
                if (reader.Read())
                {
                    bukti_ke = Int32.Parse(reader["seq_number"].ToString()) + 1;
                }
                nobukti = "RP" + string.Concat(string.Concat(string.Concat(hari, bulan), tahun), getResult(bukti_ke));
                Console.WriteLine("nobukti = " + nobukti);
                string query = "SELECT * FROM SIRETURN WHERE RETURNNO=@returnno";
                Connection.command = new OleDbCommand(query, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@returnno", nobukti);
                reader = Connection.command.ExecuteReader();
                if (reader.Read())
                {
                    //String id = Convert.ToString(reader["id"]);
                    //jika data yang mau saya buat itu belum ada...baru bisa insert baru..
                    DialogResult avaiable = MessageBox.Show("No. Retur " + nobukti + " sudah ada", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        
                        string queryInsert = "INSERT INTO SIRETURN(returnno,invoice_id,description) "
                                        + " VALUES( @returnno,@invoiceid,@description)";
                        Connection.command = new OleDbCommand(queryInsert, Connection.conn);
                        Connection.command.CommandType = CommandType.Text;
                        Connection.command.Parameters.AddWithValue("@returnno", nobukti);
                        Connection.command.Parameters.AddWithValue("@invoiceid", idinvoice);
                        Connection.command.Parameters.AddWithValue("@description", descriptionTxt.Text);
                        Connection.command.ExecuteNonQuery();

                        Connection.command.CommandText = "Select @@Identity";
                        id = (int)Connection.command.ExecuteScalar();

                        //string queryGet = "SELECT * FROM INVOICE WHERE id=@id and delete_flag='N'";
                        //Connection.command = new OleDbCommand(queryGet, Connection.conn);
                        //Connection.command.CommandType = CommandType.Text;
                        //Connection.command.Parameters.AddWithValue("@id", id);
                        //reader = Connection.command.ExecuteReader();
                        //if (reader.Read())
                        //{
                        //    nobuktiTxt.Text = reader["invoice_no"].ToString();
                        //    tanggalTxt.Value = Convert.ToDateTime(reader["invoice_date"]);
                        //    cmbWarehouse.SelectedValue = reader["warehouse_id"].ToString();
                        //    cmbCustomer.SelectedValue = reader["customer_id"].ToString();
                        //    Console.WriteLine("pajak = " + reader["pajak_id"].ToString());
                        //    if (reader["pajak_id"].ToString() == "")
                        //    {
                        //        chkTax.Checked = false;
                        //    }
                        //    else
                        //    {
                        //        chkTax.Checked = true;
                        //        cmbTax.SelectedValue = reader["pajak_id"].ToString();
                        //    }

                        //    descriptionTxt.Text = reader["description"].ToString();
                        //}
                        //Simpan detail
                        try
                        {

                            for (int rows = 0; rows < dataGridView1.Rows.Count - 1; rows++)
                            {
                                DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dataGridView1.Rows[rows].Cells[0];
                                string valueItem = dataGridView1.Rows[rows].Cells[0].Value.ToString();
                                string valueQty = dataGridView1.Rows[rows].Cells[1].Value.ToString();
                                string valueHarga = dataGridView1.Rows[rows].Cells[2].Value.ToString();

                                //string valueDisc = dataGridView1.Rows[rows].Cells[3].Value.ToString();
                                string valueDisc = dataGridView1.Rows[rows].Cells[3].Value != null ? dataGridView1.Rows[rows].Cells[3].Value.ToString() : "0";
                                Double qtynya = Double.Parse(valueQty);
                                Double harganya = Double.Parse(valueHarga);

                                Double discountnya = 0;

                                try
                                {
                                    if (!String.IsNullOrEmpty(dataGridView1.Rows[rows].Cells[3].Value.ToString()))//Qty
                                    {
                                        double value;
                                        if (Double.TryParse(dataGridView1.Rows[rows].Cells[3].Value.ToString(), out value))
                                        {
                                            discountnya = value;
                                        }
                                        else
                                        {
                                            discountnya = 0; // discount will have null value
                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    discountnya = 0;
                                }




                                string valueDesc = "";

                                if (dataGridView1.Rows[rows].Cells[5].Value != null)
                                {
                                    valueDesc = dataGridView1.Rows[rows].Cells[5].Value.ToString();
                                }

                                try
                                {
                                    string queryInsertDetail = "INSERT INTO SIRETURN_DETAIL(sireturn_id,item_id,qty,description,price,discount) "
                                      + " VALUES(@idreturn,@item,@qty,@description,@price,@discount)";
                                    Connection.command = new OleDbCommand(queryInsertDetail, Connection.conn);
                                    Connection.command.CommandType = CommandType.Text;
                                    Connection.command.Parameters.AddWithValue("@idreturn", id.ToString());
                                    Connection.command.Parameters.AddWithValue("@item", valueItem);
                                    Connection.command.Parameters.AddWithValue("@qty", valueQty);
                                    Connection.command.Parameters.AddWithValue("@description", valueDesc);
                                    Connection.command.Parameters.AddWithValue("@price", harganya);
                                    Connection.command.Parameters.AddWithValue("@discount", discountnya);
                                    Connection.command.ExecuteNonQuery();
                                }
                                catch (SqlException ex)
                                {
                                    Console.WriteLine("SQL exx == " + ex);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("SQL exx == " + ex);
                                }



                            }



                            string queryAddToSequence = "UPDATE SEQUENCE_STORE SET SEQ_NUMBER=@INCREMENTNYA WHERE SEQ_CODE='returnsi'";
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

                    //update_sub_label(idsalesinvoice.Text);

                    string queryAmbilSetelahInsert = "SELECT SO.ITEM_ID AS ITEM, SO.QTY AS QTY,SO.PRICE AS PRICE, SO.DISCOUNT ,SO.DESCRIPTION AS DESCRIPTION, (SO.QTY * SO.PRICE - SO.DISCOUNT) AS SUBTOTAL FROM SIRETURN_DETAIL SO WHERE SO.SIRETURN_ID=" + id;


                    OleDbCommand cmd = new OleDbCommand(queryAmbilSetelahInsert, Connection.conn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataTable scores = new DataTable();
                    da.Fill(scores);
                    dataGridView1.DataSource = scores;





                    button2.Text = "UPDATE";

                    Connection.ConnectionClose();



                    //refreshList(nilaiyangdicari);
                }


                Connection.ConnectionClose();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string nampung = "";
            string queryGet = "SELECT * FROM item WHERE id not in( select item_id from warehouse_item)";
            Connection.command = new OleDbCommand(queryGet, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.Parameters.AddWithValue("@id", id);
            reader = Connection.command.ExecuteReader();
            while(reader.Read())
            {
               string aaa  = "insert into warehouse_item(item_id,warehouse_id,qty_first,qty_in,qty_out) values(" + reader["id"] + ",3,0,0,0);";
               Connection.command = new OleDbCommand(aaa, Connection.conn);
               Connection.command.CommandType = CommandType.Text;
               Connection.command.ExecuteNonQuery();
            }
        }
    }
}
