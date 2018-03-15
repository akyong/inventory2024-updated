using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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

namespace inventory.Purchase
{
    public partial class PurchaseOrder : Form
    {
        OleDbDataReader reader;
        MDIForm mdi;
        public PurchaseOrder(MDIForm mdinya)
        {
            mdi = mdinya;
            InitializeComponent();
            loadComboBox();
            dataGridView1.Columns[1].DefaultCellStyle.Format = "N";//dari propertis juga bisa
            dataGridView1.Columns[2].DefaultCellStyle.Format = "N";//dari propertis juga bisa
            dataGridView1.Columns[3].DefaultCellStyle.Format = "N";//dari propertis juga bisa
            dataGridView1.Columns[4].DefaultCellStyle.Format = "N";//dari propertis juga bisa
        }

        public void loadComboBox()
        {

            Connection.ConnectionClose();
            Connection.ConnectionOpen();

            //TODO fill SUPPLIER list
            string querySupplier = "SELECT * FROM SUPPLIER";
            OleDbDataAdapter dAdapter = new OleDbDataAdapter(querySupplier, Connection.conn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds, "Supplier");
            cmbSupplier.DisplayMember = "name";
            cmbSupplier.ValueMember = "id";
            cmbSupplier.DataSource = ds.Tables["Supplier"];

            //TODO fill Warehouse List
            string queryWarehouse = "SELECT * FROM WAREHOUSE WHERE DELETE_FLAG = 'N'";
            dAdapter = new OleDbDataAdapter(queryWarehouse, Connection.conn);
            ds = new DataSet();
            dAdapter.Fill(ds, "Warehouse");
            cmbWarehouse.DisplayMember = "name";
            cmbWarehouse.ValueMember = "id";
            cmbWarehouse.DataSource = ds.Tables["Warehouse"];

            //TODO fill Tax list
            string queryTax = "SELECT * FROM PAJAK WHERE DELETE_FLAG = 'N'";
            dAdapter = new OleDbDataAdapter(queryTax, Connection.conn);
            ds = new DataSet();
            dAdapter.Fill(ds, "Tax");
            cmbTax.DisplayMember = "name";
            cmbTax.ValueMember = "id";
            cmbTax.DataSource = ds.Tables["Tax"];

            DataTable DT = (DataTable)dataGridView1.DataSource;
            if (DT != null)
                DT.Clear();

            //TODO fill ITEM list
            string query = "SELECT * FROM ITEM WHERE DELETE_FLAG = 'N' ORDER BY NAME";
            dAdapter = new OleDbDataAdapter(query, Connection.conn);

            DataSet ds1 = new DataSet();
            dAdapter.Fill(ds1, "Item");
            (dataGridView1.Columns[0] as DataGridViewComboBoxColumn).DisplayMember = "name";
            (dataGridView1.Columns[0] as DataGridViewComboBoxColumn).ValueMember = "id";
            (dataGridView1.Columns[0] as DataGridViewComboBoxColumn).DataSource = ds1.Tables["Item"];
        }

        private void PurchaseOrder_Load(object sender, EventArgs e)
        {
            nobuktiTxt.Enabled = false;
            tanggalTxt.Enabled = false;
            cmbSupplier.Enabled = false;
            cmbWarehouse.Enabled = false;
            cmbTax.Enabled = false;
            chkTax.Enabled = false;
            descriptionTxt.Enabled = false;
            dataGridView1.Enabled = false;

            if (idpurchaseorder.Text == "ID")
            {
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button7.Enabled = false;
            }
            else
            {
                button4.Enabled = true;
                button7.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "ADD")
            {

                nobuktiTxt.Enabled = true;
                tanggalTxt.Enabled = true;
                cmbSupplier.Enabled = true;
                cmbWarehouse.Enabled = true;

                descriptionTxt.Enabled = true;
                dataGridView1.Enabled = true;
                button4.Enabled = false;
                button7.Enabled = false;
                chkTax.Enabled = true;

                chkTax.Checked = false;
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

                if (nobuktiTxt.Text == "")
                {
                    MessageBox.Show("Nomor Bukti Kosong, Sistem akan mengisinya secara otomatis", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                if (cmbSupplier.SelectedValue == null)
                {
                    MessageBox.Show("Supplier harus diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSupplier.Focus();
                }
                else if (cmbWarehouse.SelectedValue == null)
                {
                    MessageBox.Show("Gudang harus diisi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbWarehouse.Focus();
                }
                else if (errorDatagridviewnull) //kalau errorDatagriview nilainya true maka tampilin error message
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
                    if (nobuktiTxt.Text == "" || nobuktiTxt.Text == "(AUTO)")
                    {
                        string queryCheck = "SELECT SEQ_NUMBER FROM SEQUENCE_STORE WHERE SEQ_CODE='purchaseorder'";
                        Connection.command = new OleDbCommand(queryCheck, Connection.conn);
                        Connection.command.CommandType = CommandType.Text;
                        reader = Connection.command.ExecuteReader();
                        if (reader.Read())
                        {
                            bukti_ke = Int32.Parse(reader["seq_number"].ToString()) + 1;
                        }
                        nobukti = "PO" + string.Concat(string.Concat(string.Concat(hari, bulan), tahun), getResult(bukti_ke));
                    }

                    string query = "SELECT * FROM PURCHASEORDER WHERE order_no=@orderno and delete_flag='N'";
                    Connection.command = new OleDbCommand(query, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@orderno", nobukti);
                    reader = Connection.command.ExecuteReader();
                    if (reader.Read())
                    {
                        //String id = Convert.ToString(reader["id"]);
                        //jika data yang mau saya buat itu belum ada...baru bisa insert baru..
                        DialogResult avaiable = MessageBox.Show("Order No. " + nobukti + " sudah ada", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            string queryInsert = "INSERT INTO PURCHASEORDER(order_no,order_date,supplier_id,warehouse_id,pajak_id,description,terbitke,totalAmount,delete_flag) "
                                            + " VALUES( @orderno,@orderdate,@supplier,@warehouse,@pajak,@description,NULL,@totalamount,'N' )";
                            Connection.command = new OleDbCommand(queryInsert, Connection.conn);
                            Connection.command.CommandType = CommandType.Text;
                            Connection.command.Parameters.AddWithValue("@orderno", nobukti);
                            Connection.command.Parameters.Add("@orderdate", OleDbType.Date).Value = tanggalTxt.Value;
                            Connection.command.Parameters.AddWithValue("@supplier", cmbSupplier.SelectedValue);
                            Connection.command.Parameters.AddWithValue("@warehouse", cmbWarehouse.SelectedValue);
                            if (chkTax.Checked == true)
                            {
                                Connection.command.Parameters.AddWithValue("@pajak", cmbTax.SelectedValue);
                            }
                            else
                            {
                                Connection.command.Parameters.AddWithValue("@pajak", DBNull.Value);
                            }

                            Connection.command.Parameters.AddWithValue("@description", descriptionTxt.Text);
                            Connection.command.Parameters.AddWithValue("@totalAmount", totalAmount);
                            Connection.command.ExecuteNonQuery();

                            Connection.command.CommandText = "Select @@Identity";
                            id = (int)Connection.command.ExecuteScalar();
                            idpurchaseorder.Text = id.ToString();
                            string queryGet = "SELECT * FROM PURCHASEORDER WHERE id=@id and delete_flag='N'";
                            Connection.command = new OleDbCommand(queryGet, Connection.conn);
                            Connection.command.CommandType = CommandType.Text;
                            Connection.command.Parameters.AddWithValue("@id", id);
                            reader = Connection.command.ExecuteReader();
                            if (reader.Read())
                            {
                                nobuktiTxt.Text = reader["order_no"].ToString();
                                tanggalTxt.Value = Convert.ToDateTime(reader["order_date"]);
                                cmbWarehouse.SelectedValue = reader["warehouse_id"].ToString();
                                cmbSupplier.SelectedValue = reader["supplier_id"].ToString();
                                Console.WriteLine("pajak = " + reader["pajak_id"].ToString());
                                if (reader["pajak_id"].ToString() == "")
                                {
                                    chkTax.Checked = false;
                                }
                                else
                                {
                                    chkTax.Checked = true;
                                    cmbTax.SelectedValue = reader["pajak_id"].ToString();
                                }

                                descriptionTxt.Text = reader["description"].ToString();
                            }
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
                                        string queryInsertDetail = "INSERT INTO PURCHASEORDER_DETAIL(purchaseorder_id,item_id,qty,price,discount,description) "
                                          + " VALUES( @purchaseorder,@item,@qty,@price,@discount,@description)";
                                        Connection.command = new OleDbCommand(queryInsertDetail, Connection.conn);
                                        Connection.command.CommandType = CommandType.Text;
                                        Connection.command.Parameters.AddWithValue("@purchaseorder", id);
                                        Connection.command.Parameters.AddWithValue("@item", valueItem);
                                        Connection.command.Parameters.AddWithValue("@qty", valueQty);
                                        Connection.command.Parameters.AddWithValue("@price", harganya);
                                        Connection.command.Parameters.AddWithValue("@discount", discountnya);
                                        Connection.command.Parameters.AddWithValue("@description", valueDesc);
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



                                string queryAddToSequence = "UPDATE SEQUENCE_STORE SET SEQ_NUMBER=@INCREMENTNYA WHERE SEQ_CODE='purchaseorder'";
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

                        update_sub_label(idpurchaseorder.Text);

                        string queryAmbilSetelahInsert = "SELECT SO.ITEM_ID AS ITEM, SO.QTY AS QTY,SO.PRICE AS PRICE, SO.DISCOUNT , (SO.QTY * SO.PRICE - SO.DISCOUNT) AS SUBTOTAL,SO.DESCRIPTION AS DESCRIPTION FROM PURCHASEORDER_DETAIL SO WHERE SO.PURCHASEORDER_ID=" + idpurchaseorder.Text;


                        OleDbCommand cmd = new OleDbCommand(queryAmbilSetelahInsert, Connection.conn);
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        DataTable scores = new DataTable();
                        da.Fill(scores);
                        dataGridView1.DataSource = scores;




                        nobuktiTxt.Enabled = false;
                        tanggalTxt.Enabled = false;
                        cmbWarehouse.Enabled = false;
                        cmbSupplier.Enabled = false;
                        chkTax.Enabled = false;
                        cmbTax.Enabled = false;
                        descriptionTxt.Enabled = false;
                        dataGridView1.Enabled = false;


                        button1.Text = "ADD";
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button7.Enabled = true;
                        findBtn.Enabled = true;

                        Connection.ConnectionClose();
                        //refreshList(nilaiyangdicari);
                    }
                    Connection.ConnectionClose();
                }
            }
        }
        private string getResult(int x)
        {
            return x.ToString().PadLeft(4, '0');
        }

        private void update_sub_label(string idsalesorder)
        {
            string idpajak = "";
            Double totalamount = 0;
            Double pajaknya = 0;

            NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
            nfi = (NumberFormatInfo)nfi.Clone();
            nfi.CurrencySymbol = "";
            Double value;
            string queryGet = "SELECT * FROM PURCHASEORDER WHERE id=@id and delete_flag='N'";
            Connection.command = new OleDbCommand(queryGet, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.Parameters.AddWithValue("@id", idsalesorder);
            reader = Connection.command.ExecuteReader();
            if (reader.Read())
            {


                totalamount = Double.Parse(reader["totalAmount"].ToString());
                if (Double.TryParse(reader["totalAmount"].ToString(), out value))
                    label9.Text = String.Format(nfi, "{0:C2}", value);
                else
                    label9.Text = String.Empty;

                nobuktiTxt.Text = reader["order_no"].ToString();
                tanggalTxt.Value = Convert.ToDateTime(reader["order_date"]);
                cmbSupplier.SelectedValue = reader["supplier_id"].ToString();
                cmbWarehouse.SelectedValue = reader["warehouse_id"].ToString();

                if (reader["pajak_id"].ToString() == "")
                {
                    chkTax.Checked = false;
                    pajakLbl.Visible = false;
                    nilaipjklbl.Visible = false;
                }
                else
                {
                    chkTax.Checked = true;
                    idpajak = reader["pajak_id"].ToString();
                    cmbTax.SelectedValue = reader["pajak_id"].ToString();
                    cmbTax.Enabled = false;
                    pajakLbl.Visible = true;
                    nilaipjklbl.Visible = true;
                }

                descriptionTxt.Text = reader["description"].ToString();
            }

            string queryGetNilaiPajak = "SELECT * FROM PAJAK WHERE id=@id ";
            Connection.command = new OleDbCommand(queryGetNilaiPajak, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.Parameters.AddWithValue("@id", idpajak);
            reader = Connection.command.ExecuteReader();
            if (reader.Read())
            {
                pajaknya = ((Double.Parse(reader["persentase"].ToString()) / 100) * totalamount);

                if (Double.TryParse(pajaknya.ToString(), out value))
                    nilaipjklbl.Text = String.Format(nfi, "{0:C2}", value);
                else
                    nilaipjklbl.Text = String.Empty;

                //nilaipjklbl.Text = (((Double.Parse(reader["persentase"].ToString()) / 100) * totalamount) + totalamount).ToString();
            }

            if (Double.TryParse((pajaknya + totalamount).ToString(), out value))
                label8.Text = String.Format(nfi, "{0:C2}", value);
            else
                label8.Text = String.Empty;
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
            else if (((DataGridView)sender).CurrentCell.ColumnIndex == 1)
            {
                TextBox qty = e.Control as TextBox;
                if (qty != null)
                {
                    qty.Leave -= new EventHandler(qty_leaving);
                    // now attach the event handler
                    qty.Leave += new EventHandler(qty_leaving);
                    qty.KeyPress += new KeyPressEventHandler(AllowNumber_KeyPress);
                }
            }
            else if (((DataGridView)sender).CurrentCell.ColumnIndex == 2)
            {
                TextBox price = e.Control as TextBox;
                if (price != null)
                {
                    price.Leave -= new EventHandler(price_leaving);
                    // now attach the event handler
                    price.Leave += new EventHandler(price_leaving);
                    price.KeyPress += new KeyPressEventHandler(AllowNumber_KeyPress);
                }
            }
            else if (((DataGridView)sender).CurrentCell.ColumnIndex == 3)
            {
                TextBox dc = e.Control as TextBox;
                if (dc != null)
                {
                    dc.Leave -= new EventHandler(discount_leaving);
                    // now attach the event handler
                    dc.Leave += new EventHandler(discount_leaving);
                    dc.KeyPress += new KeyPressEventHandler(AllowNumber_KeyPress);
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

        void price_leaving(object sender, EventArgs e)//saat edit harga selesai, ubah format ke currency format
        {
            Console.WriteLine("masuk price leaving");
            //var tb = dataGridView1.EditingControl as TextBox;
            int currentRow = dataGridView1.CurrentRow.Index;
            Double price = 0;
            try
            {

                if (!String.IsNullOrEmpty(dataGridView1.Rows[currentRow].Cells[2].Value.ToString()))//Qty
                {
                    double value;
                    if (Double.TryParse(dataGridView1[2, currentRow].Value.ToString(), out value))
                    {
                        price = value;
                        dataGridView1[2, currentRow].Value = value;
                    }
                    else
                    {
                        price = 0; // discount will have null value
                    }

                }


                //string priceTxt = dataGridView1.Rows[currentRow].Cells[2].Value.ToString();
                //if (priceTxt == "")
                //{
                //    dataGridView1[2, currentRow].Value = 0;
                //}
                //else if (Double.Parse(priceTxt) < 1)
                //{
                //    dataGridView1[2, currentRow].Value = 0;
                //}
                //else
                //{
                //    string str = priceTxt != null ? priceTxt : null;
                //    dataGridView1[2, currentRow].Value = Double.Parse(priceTxt);
                //}
            }
            catch (Exception ex)
            {
                dataGridView1[2, currentRow].Value = 0;
            }

            if (dataGridView1[1, currentRow].Value == null)
            {
                Console.WriteLine("null = " + dataGridView1[1, currentRow].Value.ToString());
            }
            else
            {
                Console.WriteLine("null string");
            }
            Double qty = 0;//nilai qty
            Double harga = price;
            Double discount = 0;

            if (!String.IsNullOrEmpty(dataGridView1.Rows[currentRow].Cells[1].Value.ToString()))//Qty
            {
                double value;
                if (Double.TryParse(dataGridView1[1, currentRow].Value.ToString(), out value))
                {
                    qty = value;
                }
                else
                {
                    qty = 0; // discount will have null value
                }

            }

            if (!String.IsNullOrEmpty(dataGridView1.Rows[currentRow].Cells[3].Value.ToString()))//Qty
            {
                double value;
                if (Double.TryParse(dataGridView1[3, currentRow].Value.ToString(), out value))
                {
                    discount = value;
                }
                else
                {
                    discount = 0; // discount will have null value
                }

            }



            //Double qty = Double.Parse(dataGridView1[1, currentRow].Value != null && dataGridView1[1, currentRow].Value != "" ? dataGridView1[1, currentRow].Value.ToString() : "0");//nilai qty
            //Double harga = Double.Parse(dataGridView1[2, currentRow].Value != null && dataGridView1[1, currentRow].Value != "" ? dataGridView1[2, currentRow].Value.ToString() : "0");//nilai price
            //Double discount = Double.Parse(dataGridView1[3, currentRow].Value != null && dataGridView1[1, currentRow].Value != "" ? dataGridView1[3, currentRow].Value.ToString() : "0");//nilai discount
            Double subtotal = (qty * harga) - discount;
            dataGridView1[4, currentRow].Value = subtotal;
        }

        void qty_leaving(object sender, EventArgs e)//saat edit harga selesai, ubah format ke currency format
        {
            Console.WriteLine("masuk qty leaving");
            //var tb = dataGridView1.EditingControl as TextBox;
            int currentRow = dataGridView1.CurrentRow.Index;
            Console.WriteLine("qty = " + dataGridView1.Rows[currentRow].Cells[1].Value);
            Double qtytmp = 0;
            try
            {
                string qtyTxt = dataGridView1.Rows[currentRow].Cells[1].Value.ToString();
                if (qtyTxt == "")
                {
                    dataGridView1[1, currentRow].Value = 0;
                }
                else if (Double.Parse(qtyTxt) < 1)
                {
                    dataGridView1[1, currentRow].Value = 0;
                }
                else
                {
                    string str = qtyTxt != null ? qtyTxt : null;
                    dataGridView1[1, currentRow].Value = Double.Parse(qtyTxt);
                }
                qtytmp = Double.Parse(qtyTxt);
            }
            catch (Exception ex)
            {
                dataGridView1[1, currentRow].Value = 0;
            }

            Double qty = qtytmp;//nilai qty
            Double harga = 0;
            Double discount = 0;

            try
            {
                if (!String.IsNullOrEmpty(dataGridView1[2, currentRow].Value.ToString()))//Qty
                {
                    double value;
                    if (Double.TryParse(dataGridView1[2, currentRow].Value.ToString(), out value))
                        harga = value;
                    else
                        harga = 0; // discount will have null value
                }
            }
            catch (Exception ex)
            {
                harga = 0;
            }

            try
            {
                if (!String.IsNullOrEmpty(dataGridView1[3, currentRow].Value.ToString()))//Qty
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
                discount = 0;
            }



            Double subtotal = (qty * harga) - discount;
            dataGridView1[4, currentRow].Value = subtotal;
        }

        void discount_leaving(object sender, EventArgs e)//saat edit harga selesai, ubah format ke currency format
        {
            Console.WriteLine("masuk discount leaving");
            //var tb = dataGridView1.EditingControl as TextBox;
            int currentRow = dataGridView1.CurrentRow.Index;
            string discountTxt = "";
            try
            {
                discountTxt = dataGridView1.Rows[currentRow].Cells[3].Value.ToString();
                if (discountTxt == "")
                {
                    dataGridView1[3, currentRow].Value = 0;
                }
                else if (Double.Parse(discountTxt) < 1)
                {
                    dataGridView1[3, currentRow].Value = 0;
                }
                else
                {
                    string str = discountTxt != null ? discountTxt : null;
                    dataGridView1[3, currentRow].Value = Double.Parse(discountTxt);
                }
            }
            catch (Exception ex)
            {
                dataGridView1[3, currentRow].Value = 0;
            }


            Double qty = 0;//nilai qty
            Double harga = 0;
            Double discount = Double.Parse(discountTxt);

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


            if (!String.IsNullOrEmpty(dataGridView1[2, currentRow].Value.ToString()))//Qty
            {
                double value;
                if (Double.TryParse(dataGridView1[2, currentRow].Value.ToString(), out value))
                    harga = value;
                else
                    harga = 0; // discount will have null value
            }




            //Double qty = Double.Parse(dataGridView1[1, currentRow].Value != null ? dataGridView1[1, currentRow].Value.ToString() : "0");//nilai qty
            //Double harga = Double.Parse(dataGridView1[2, currentRow].Value != null ? dataGridView1[2, currentRow].Value.ToString() : "0");//nilai price
            //Double discount = Double.Parse(dataGridView1[3, currentRow].Value != null ? dataGridView1[3, currentRow].Value.ToString() : "0");//nilai discount
            Double subtotal = (qty * harga) - discount;
            dataGridView1[4, currentRow].Value = subtotal;
        }

        public void auto_complete_harga(string item_id, int currentRow)//auto set harga pas barang di pilih
        {
            Connection.ConnectionClose();
            Connection.ConnectionOpen();

            Console.WriteLine("currentRow = " + currentRow);
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

            //Double qty      = Double.TryParse(dataGridView1[1, currentRow].Value != null && dataGridView1[1, currentRow].Value != "" ? dataGridView1[1, currentRow].Value.ToString() : "0");//nilai qty
            //Double harga    = Double.Parse(reader["price"].ToString());//nilai harga
            //Double discount = Double.Parse(dataGridView1[3, currentRow].Value != null && dataGridView1[3, currentRow].Value != "" ? dataGridView1[3, currentRow].Value.ToString() : "0");//nilai discount
            //Double subtotal = (qty * harga) - discount;
            //dataGridView1[4, currentRow].Value = subtotal;
        }

        private void chkTax_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTax.Checked == true)
            {
                cmbTax.Enabled = true;
            }
            else
            {
                cmbTax.Enabled = false;
            }
        }

        private void chkTax_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(chkTax, "Centang, Jika Pakai PPN");
        }

        private void cmbTax_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(cmbTax, "Centang Pajak, Jika Pakai PPN");
        }

        private void chkTax_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkTax.Checked == true)
            {
                cmbTax.Enabled = true;
            }
            else
            {
                cmbTax.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Boolean bolehhapus = false;
            DialogResult dialogResult = MessageBox.Show("Apakah anda yakin akan menghapus Purchase Order ini?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string queryCekDulu = "SELECT * FROM PURCHASEORDER WHERE ID=@id ";
                Connection.command = new OleDbCommand(queryCekDulu, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@id", idpurchaseorder.Text);
                reader = Connection.command.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["receiveditem_id"] != DBNull.Value)
                    {
                        bolehhapus = true;
                    }
                }

                if (bolehhapus)
                {
                    MessageBox.Show("Purchase Order tidak bisa dihapus, karena sudah diterbitkan!", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string queryHapusDetail = "DELETE FROM PURCHASEORDER_DETAIL WHERE PURCHASEORDER_ID=@id ";
                    string queryHapusHead = "DELETE FROM PURCHASEORDER WHERE ID=@id ";
                    Connection.command = new OleDbCommand(queryHapusDetail, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@id", idpurchaseorder.Text);
                    Connection.command.ExecuteNonQuery();

                    Connection.command = new OleDbCommand(queryHapusHead, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@id", idpurchaseorder.Text);
                    Connection.command.ExecuteNonQuery();

                    MessageBox.Show("Bukti Purchase Order Berhasil dihapus ", "Delete Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Aksi Penghapusan Bukti Pindah Gudang Telah dibatalkan!", "Delete Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "EDIT")
            {
                nobuktiTxt.Enabled = true;
                tanggalTxt.Enabled = true;
                cmbWarehouse.Enabled = true;
                cmbSupplier.Enabled = true;
                chkTax.Enabled = true;
                descriptionTxt.Enabled = true;
                dataGridView1.Enabled = true;

                button2.Text = "UPDATE";
                button1.Enabled = false;
                button3.Enabled = false;
                findBtn.Enabled = false;

            }
            else if (button2.Text == "UPDATE")
            {
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
                            string valueHarga = dataGridView1.Rows[rows].Cells[2].Value.ToString();
                            string subTotal = dataGridView1.Rows[rows].Cells[4].Value.ToString();
                            Double qtynya = Double.Parse(valueQty);
                            Double harganya = Double.Parse(valueHarga);
                            Double subtotalnya = Double.Parse(subTotal);

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


                Connection.ConnectionClose();
                Connection.ConnectionOpen();
                string queryCheck = "SELECT * FROM PURCHASEORDER WHERE order_no=@orderno and delete_flag='N'";
                Connection.command = new OleDbCommand(queryCheck, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@orderno", nobuktiTxt.Text);
                reader = Connection.command.ExecuteReader();
                if (reader.Read())
                {
                    String id = Convert.ToString(reader["id"]);

                    //jika data yang mau saya buat itu belum ada...baru bisa insert baru..
                    if (id == idpurchaseorder.Text)
                    {

                        if (nobuktiTxt.Text == "")
                        {
                            MessageBox.Show("Nomor Bukti Kosong, Sistem akan mengisinya secara otomatis", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        if (cmbSupplier.SelectedValue == null)
                        {
                            MessageBox.Show("Supplier harus diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbSupplier.Focus();
                        }
                        else if (cmbWarehouse.SelectedValue == null)
                        {
                            MessageBox.Show("Gudang harus diisi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbWarehouse.Focus();
                        }
                        else if (errorDatagridviewnull) //kalau errorDatagriview nilainya true maka tampilin error message
                        {
                            MessageBox.Show("Qty atau Barang tidak boleh kosong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dataGridView1.Focus();
                        }
                        else
                        {
                            try
                            {
                                hapus_purchase_order_detail(idpurchaseorder.Text);
                                Double totalAmount = 0;
                                for (int rows = 0; rows < dataGridView1.Rows.Count - 1; rows++)
                                {
                                    DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dataGridView1.Rows[rows].Cells[0];
                                    string valueItem = dataGridView1.Rows[rows].Cells[0].Value.ToString();
                                    string valueQty = dataGridView1.Rows[rows].Cells[1].Value.ToString();
                                    string valueHarga = dataGridView1.Rows[rows].Cells[2].Value.ToString();
                                    string valueSubtotal = dataGridView1.Rows[rows].Cells[4].Value.ToString();
                                    Double discountnya = 0;
                                    //string valueDisc = dataGridView1.Rows[rows].Cells[3].Value.ToString();
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

                                    string valueDisc = dataGridView1.Rows[rows].Cells[3].Value != null ? dataGridView1.Rows[rows].Cells[3].Value.ToString() : "0";
                                    Double qtynya = Double.Parse(valueQty);
                                    Double harganya = Double.Parse(valueHarga);
                                    Double valuesubtotal = Double.Parse(valueSubtotal);
                                    totalAmount = totalAmount + valuesubtotal;
                                    string valueDesc = "";

                                    if (dataGridView1.Rows[rows].Cells[5].Value != null)
                                    {
                                        valueDesc = dataGridView1.Rows[rows].Cells[5].Value.ToString();
                                    }

                                    try
                                    {
                                        string queryInsertDetail = "INSERT INTO PURCHASEORDER_DETAIL(purchaseorder_id,item_id,qty,price,discount,description) "
                                          + " VALUES( @purchaseorder,@item,@qty,@price,@discount,@description)";
                                        Connection.command = new OleDbCommand(queryInsertDetail, Connection.conn);
                                        Connection.command.CommandType = CommandType.Text;
                                        Connection.command.Parameters.AddWithValue("@purchaseorder", id);
                                        Connection.command.Parameters.AddWithValue("@item", valueItem);
                                        Connection.command.Parameters.AddWithValue("@qty", valueQty);
                                        Connection.command.Parameters.AddWithValue("@price", harganya);
                                        Connection.command.Parameters.AddWithValue("@discount", discountnya);
                                        Connection.command.Parameters.AddWithValue("@description", valueDesc);
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

                                string query = "UPDATE PURCHASEORDER SET order_no=@orderno, order_date=@orderdate,warehouse_id=@warehouse,supplier_id=@supplier,pajak_id=@pajak, description=@description,totalAmount=@totalAmount WHERE id=@id";
                                Connection.command = new OleDbCommand(query, Connection.conn);
                                Connection.command.CommandType = CommandType.Text;
                                Connection.command.Parameters.AddWithValue("@orderno", nobuktiTxt.Text);
                                Connection.command.Parameters.Add("@orderdate", OleDbType.Date).Value = tanggalTxt.Value;
                                Connection.command.Parameters.AddWithValue("@warehouse", cmbWarehouse.SelectedValue);
                                Connection.command.Parameters.AddWithValue("@supplier", cmbSupplier.SelectedValue);
                                if (chkTax.Checked == true)
                                {
                                    Connection.command.Parameters.AddWithValue("@pajak", cmbTax.SelectedValue);
                                }
                                else
                                {
                                    Connection.command.Parameters.AddWithValue("@pajak", DBNull.Value);
                                }

                                Connection.command.Parameters.AddWithValue("@description", descriptionTxt.Text);
                                Connection.command.Parameters.AddWithValue("@totalAmount", totalAmount);
                                Connection.command.Parameters.AddWithValue("@id", idpurchaseorder.Text);
                                Connection.command.ExecuteNonQuery();
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

                        update_sub_label(idpurchaseorder.Text);

                        string queryAmbilSetelahInsert = "SELECT SO.ITEM_ID AS ITEM, SO.QTY AS QTY,SO.PRICE AS PRICE, SO.DISCOUNT , (SO.QTY * SO.PRICE - SO.DISCOUNT) AS SUBTOTAL,SO.DESCRIPTION AS DESCRIPTION FROM PURCHASEORDER_DETAIL SO WHERE SO.PURCHASEORDER_ID=" + idpurchaseorder.Text;


                        OleDbCommand cmd = new OleDbCommand(queryAmbilSetelahInsert, Connection.conn);
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        DataTable scores = new DataTable();
                        da.Fill(scores);
                        dataGridView1.DataSource = scores;

                        MessageBox.Show("Data Berhasil diperbarui.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        nobuktiTxt.Enabled = false;
                        tanggalTxt.Enabled = false;
                        cmbWarehouse.Enabled = false;
                        cmbSupplier.Enabled = false;
                        cmbTax.Enabled = false;
                        chkTax.Enabled = false;
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

        private void hapus_purchase_order_detail(string idsalesorder)
        {
            Connection.ConnectionClose();
            Connection.ConnectionOpen();
            string queryHapusDetail = "DELETE FROM PURCHASEORDER_DETAIL WHERE PURCHASEORDER_ID=@id ";
            Connection.command = new OleDbCommand(queryHapusDetail, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.Parameters.AddWithValue("@id", idsalesorder);
            Connection.command.ExecuteNonQuery();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //FOR DOING EXPORT TO RECEIVED ITEM
            Connection.ConnectionClose();
            Connection.ConnectionOpen();
            string queryCariSudahExportAtauBelum = "SELECT * FROM PURCHASEORDER WHERE id=@id AND receiveditem_id is null";
            Connection.command = new OleDbCommand(queryCariSudahExportAtauBelum, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.Parameters.AddWithValue("@id", idpurchaseorder.Text);

            string gudangasal = "";
            reader = Connection.command.ExecuteReader();
            if (reader.Read())
            {
                //CREATE NOMOR BUKTI UNTUK DO
                string nobukti = "";
                String hari = String.Format("{0:dd}", DateTime.Now);
                String bulan = String.Format("{0:MM}", DateTime.Now);
                String tahun = String.Format("{0:yy}", DateTime.Now);
                int bukti_ke = 0;
                int iddeliveryorder = 0;
                string queryCheck = "SELECT SEQ_NUMBER FROM SEQUENCE_STORE WHERE SEQ_CODE='receiveditem'";
                Connection.command = new OleDbCommand(queryCheck, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
               
                reader = Connection.command.ExecuteReader();
                if (reader.Read())
                {
                    bukti_ke = Int32.Parse(reader["seq_number"].ToString()) + 1;
                }
                
                nobukti = "RI" + string.Concat(string.Concat(string.Concat(hari, bulan), tahun), getResult(bukti_ke));
               
                //CEK NOMOR BUKTI UNTUK DO, APAKAH SUDAH DIPAKAI ATAU BELUM
                string query = "SELECT * FROM RECEIVEDITEM WHERE received_no=@orderno and delete_flag='N'";
                Connection.command = new OleDbCommand(query, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@orderno", nobukti);
                reader = Connection.command.ExecuteReader();
                if (reader.Read())
                {
                    DialogResult avaiable = MessageBox.Show("Order No. " + nobukti + " sudah ada", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (avaiable == DialogResult.Yes)
                    {
                        this.Close();
                    }
                    return;
                }
                else
                {
                    //AMBIL SALESORDER HEAD
                    string queryGet = "SELECT * FROM PURCHASEORDER WHERE id=@id and delete_flag='N'";
                    Connection.command = new OleDbCommand(queryGet, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@id", idpurchaseorder.Text);
                    reader = Connection.command.ExecuteReader();
                    if (reader.Read())
                    {
                        //COPAS DAN MASUKAN KE DELIVERY ORDER BARU
                        string querySet = "INSERT INTO RECEIVEDITEM(received_no,received_date,supplier_id,warehouse_id,pajak_id,description,terbitke,totalAmount,delete_flag,po_number) "
                                                    + " VALUES( @orderno,@orderdate,@supplier,@warehouse,@pajak,@description,NULL,@totalamount,'N',@ponumber)";

                        Connection.command = new OleDbCommand(querySet, Connection.conn);
                        Connection.command.CommandType = CommandType.Text;
                        gudangasal = reader["warehouse_id"].ToString();

                        Connection.command.Parameters.AddWithValue("@orderno", nobukti);
                        Connection.command.Parameters.Add("@orderdate", OleDbType.Date).Value = DateTime.Now;
                        Connection.command.Parameters.AddWithValue("@supplier", reader["supplier_id"].ToString());
                        Connection.command.Parameters.AddWithValue("@warehouse", reader["warehouse_id"].ToString());
                        if (reader["pajak_id"] != DBNull.Value)
                        {
                            Connection.command.Parameters.AddWithValue("@pajak", reader["pajak_id"]);
                        }
                        else
                        {
                            Connection.command.Parameters.AddWithValue("@pajak", DBNull.Value);
                        }

                        Connection.command.Parameters.AddWithValue("@description", reader["description"]);
                        Connection.command.Parameters.AddWithValue("@totalAmount", reader["totalamount"].ToString());
                        Connection.command.Parameters.AddWithValue("@ponumber", reader["order_no"].ToString());
                        Connection.command.ExecuteNonQuery();

                        Connection.command.CommandText = "Select @@Identity";
                        iddeliveryorder = (int)Connection.command.ExecuteScalar();


                    }
                    //INSERT ALL SALESORDER DETAIL TO DELIVERY ORDER DETAIL
                    string queryGetDetail = "SELECT * FROM PURCHASEORDER_DETAIL WHERE PURCHASEORDER_ID=@purchaseid";
                    Connection.command = new OleDbCommand(queryGetDetail, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@purchaseid", idpurchaseorder.Text);
                    reader = Connection.command.ExecuteReader();
                    while (reader.Read())
                    {
                        string querySetDetail = "INSERT INTO RECEIVEDITEM_DETAIL(receiveditem_id,item_id,qty,price,discount,description) "
                                             + " VALUES( @receiveditem,@item,@qty,@price,@discount,@description)";
                        Connection.command = new OleDbCommand(querySetDetail, Connection.conn);
                        Connection.command.CommandType = CommandType.Text;
                        Connection.command.Parameters.AddWithValue("@receiveditem", iddeliveryorder);
                        Connection.command.Parameters.AddWithValue("@item", reader["item_id"].ToString());
                        Connection.command.Parameters.AddWithValue("@qty", reader["qty"].ToString());
                        Connection.command.Parameters.AddWithValue("@price", reader["price"].ToString());
                        Connection.command.Parameters.AddWithValue("@discount", reader["discount"].ToString());
                        Connection.command.Parameters.AddWithValue("@description", reader["description"].ToString());
                        Connection.command.ExecuteNonQuery();

                        UpdateStock.itemKeluar(gudangasal, Int32.Parse(reader["qty"].ToString()), reader["item_id"].ToString());
                    }

                    string querySetSudahExport = "UPDATE PURCHASEORDER SET receiveditem_id=@receiveditemid WHERE id=@id";
                    Connection.command = new OleDbCommand(querySetSudahExport, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@receiveditemid", iddeliveryorder);
                    Connection.command.Parameters.AddWithValue("@id", idpurchaseorder.Text);
                    Connection.command.ExecuteNonQuery();

                    string queryAddToSequence = "UPDATE SEQUENCE_STORE SET SEQ_NUMBER=@INCREMENTNYA WHERE SEQ_CODE='receiveditem'";
                    Connection.command = new OleDbCommand(queryAddToSequence, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@INCREMENTNYA", bukti_ke);
                    Connection.command.ExecuteNonQuery();


                    MessageBox.Show("Purchase Order berhasil diterbitkan ke RI!", "Terbit Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                MessageBox.Show("Purchase Order ini sudah diterbitkan! \n Atau \n Sales Order Tidak ditemukan.", "Terbit Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void findBtn_Click(object sender, EventArgs e)
        {
            Hashtable fieldTable = new Hashtable();
            fieldTable.Add("SO.ORDER_NO", "No. Order");
            //fieldTable.Add("PD.tgl_bukti", "Tanggal Bukti");

            int promptValue = SearchPurchaseOrder.ShowDialog("Cari Bukti Order", fieldTable);

            idpurchaseorder.Text = promptValue.ToString();


            update_sub_label(idpurchaseorder.Text);

            string queryAmbilSetelahInsert = "SELECT SO.ITEM_ID AS ITEM, SO.QTY AS QTY ,SO.PRICE AS PRICE, SO.DISCOUNT AS DISCOUNT, (SO.QTY * SO.PRICE - SO.DISCOUNT) AS SUBTOTAL, SO.DESCRIPTION AS DESCRIPTION FROM PURCHASEORDER_DETAIL SO WHERE SO.PURCHASEORDER_ID=" + idpurchaseorder.Text;

            OleDbCommand cmd = new OleDbCommand(queryAmbilSetelahInsert, Connection.conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable scores = new DataTable();
            da.Fill(scores);
            dataGridView1.DataSource = scores;

            button2.Enabled = true;
            button3.Enabled = true;
            if (idpurchaseorder.Text != "ID")
            {
                button4.Enabled = true;
                button7.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
                button7.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(reporting.ReportViewerPurchaseOrder))
                {
                    form.Activate();
                    return;
                }
            }
            reporting.ReportViewerPurchaseOrder rv = new reporting.ReportViewerPurchaseOrder(idpurchaseorder.Text);
            rv.MdiParent = mdi;
            rv.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //ReportDocument reportDoc = new ReportDocument();
            string filePath = Application.StartupPath + "/reporting/BuktiPurchaseOrder.rpt";
            //reportDoc.Load(filePath);
            //reportDoc.SetParameterValue("salesid", idsalesorder.Text);


            //reportDoc.PrintToPrinter(1, true, 0, 0);


            System.Threading.Thread.Sleep(2000);

            ReportDocument cryRpt = new ReportDocument();
            ReportDocument crpt2 = cryRpt;
            cryRpt.Load(filePath);
            cryRpt.SetParameterValue("idpurchaseorder", idpurchaseorder.Text);

            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;

            RegistryKey forlogin = Registry.CurrentUser.OpenSubKey("Software\\Logger", true);
            Object server = forlogin.GetValue("server");
            Object db = forlogin.GetValue("database");
            Object user = forlogin.GetValue("user");
            Object pass = forlogin.GetValue("pass");


            crConnectionInfo.ServerName = db.ToString();
            crConnectionInfo.DatabaseName = "";
            crConnectionInfo.UserID = "";
            crConnectionInfo.Password = "";

            CrTables = cryRpt.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
            System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();
            string printerName = cryRpt.PrintOptions.PrinterName.ToString();
            cryRpt.PrintOptions.PrinterName = printerName;
            int iCount = 0;

            for (iCount = 0; iCount <= printDoc.PrinterSettings.PaperSizes.Count - 1; iCount++)
            {
                int rawKind3;
                if (printDoc.PrinterSettings.PaperSizes[iCount].PaperName == "vpc")
                {
                    cryRpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)printDoc.PrinterSettings.PaperSizes[iCount].RawKind;
                    break;
                }
                //if 
            }//for 
            cryRpt.PrintToPrinter(1, true, 0, 0);
            cryRpt.Refresh();            
        }
    }
}
