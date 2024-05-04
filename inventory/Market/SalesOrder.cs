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

namespace inventory.Market
{
    public partial class SalesOrder : Form
    {
        OleDbDataReader reader;
        MDIForm mdi;
        AutoCompleteStringCollection picode = new AutoCompleteStringCollection();
        public SalesOrder(MDIForm mdinya)
        {
            InitializeComponent();
            mdi = mdinya;
            persenTxt.DecimalPlaces = 2;
            persenTxt.Increment = 0.01m;
            loadComboBox();
            //dataGridView1.Columns[1].DefaultCellStyle.Format = "N";//dari propertis juga bisa
            dataGridView1.Columns[2].DefaultCellStyle.Format = "N";//dari propertis juga bisa
            dataGridView1.Columns[3].DefaultCellStyle.Format = "N";//dari propertis juga bisa
            dataGridView1.Columns[4].DefaultCellStyle.Format = "N";//dari propertis juga bisa
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "EDIT")
            {
                nobuktiTxt.Enabled = true;
                tanggalTxt.Enabled = true;
                cmbWarehouse.Enabled = true;
                cmbCustomer.Enabled = true;
                chkTax.Enabled = true;
                descriptionTxt.Enabled = true;
                dataGridView1.Enabled = true;
                btncancel.Visible = true;
                btncancel.Enabled = true;
                persenTxt.Enabled = true;

                button2.Text = "UPDATE";
                button1.Enabled = false;
                button3.Enabled = false;
                findBtn.Enabled = false;
                nopo.Enabled = false;
                persenTxt.Enabled = true;

            }
            else if (button2.Text == "UPDATE")
            {
                Boolean errorDatagridviewnull = false;
                
                if (dataGridView1.Rows.Count == 1)
                {
                    for (int rows = 0; rows < dataGridView1.Rows.Count; rows++)
                    {
                        try
                        {
                            string valueItem = dataGridView1.Rows[rows].Cells[7].Value.ToString();
                            string valueQty = dataGridView1.Rows[rows].Cells[2].Value.ToString();
                            string valueHarga = dataGridView1.Rows[rows].Cells[3].Value.ToString();
                            string subTotal = dataGridView1.Rows[rows].Cells[5].Value.ToString();

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
                        try
                        {
                            string valueItem = dataGridView1.Rows[rows].Cells[7].Value.ToString();
                            string valueQty = dataGridView1.Rows[rows].Cells[2].Value.ToString();
                            string valueHarga = dataGridView1.Rows[rows].Cells[3].Value.ToString();
                            
                            Double qtynya = Double.Parse(valueQty);
                            Double harganya = Double.Parse(valueHarga);
                            Double disc = 0;
                            try
                            {
                                if (!String.IsNullOrEmpty(dataGridView1.Rows[rows].Cells[4].Value.ToString()))//Qty
                                {
                                    double value;
                                    if (Double.TryParse(dataGridView1.Rows[rows].Cells[4].Value.ToString(), out value))
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


                            if (valueItem == null || valueItem == "0")//kalau dropdown item
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
                string queryCheck = "SELECT * FROM INVOICE WHERE invoice_no=@invoice_no and delete_flag='N'";
                Connection.command = new OleDbCommand(queryCheck, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@invoice_no", nobuktiTxt.Text);
                reader = Connection.command.ExecuteReader();
                if (reader.Read())
                {
                    String id = Convert.ToString(reader["id"]);
                    
                    //jika data yang mau saya buat itu belum ada...baru bisa insert baru..
                    if (id == idsalesorder.Text)
                    {

                        if (nobuktiTxt.Text == "")
                        {
                            MessageBox.Show("Nomor Bukti Kosong, Sistem akan mengisinya secara otomatis", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (cmbCustomer.SelectedValue == null)
                        {
                            MessageBox.Show("Customer harus diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbCustomer.Focus();
                            return;
                        }
                        else if (cmbWarehouse.SelectedValue == null)
                        {
                            MessageBox.Show("Gudang harus diisi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbWarehouse.Focus();
                            return;
                        }
                        else if (errorDatagridviewnull) //kalau errorDatagriview nilainya true maka tampilin error message
                        {
                            MessageBox.Show("Qty atau Barang tidak boleh kosong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dataGridView1.Focus();
                            return;
                        }
                        else
                        {
                            try
                            {
                                hapus_sales_order_detail(idsalesorder.Text);
                                Double totalAmount = 0;
                                for (int rows = 0; rows < dataGridView1.Rows.Count - 1; rows++)
                                {
                                    string valueItem = dataGridView1.Rows[rows].Cells[7].Value.ToString();
                                    string valueQty = dataGridView1.Rows[rows].Cells[2].Value.ToString();
                                    string valueHarga = dataGridView1.Rows[rows].Cells[3].Value.ToString();
                                    string valueSubtotal = dataGridView1.Rows[rows].Cells[5].Value.ToString();
                                    //string valueDisc = dataGridView1.Rows[rows].Cells[3].Value.ToString();
                                    string valueDisc = dataGridView1.Rows[rows].Cells[4].Value != null ? dataGridView1.Rows[rows].Cells[4].Value.ToString() : "0";
                                    Double qtynya = Double.Parse(valueQty);
                                    Double harganya = Double.Parse(valueHarga);

                                    Double discountnya = 0;


                                    try
                                    {
                                        if (!String.IsNullOrEmpty(dataGridView1.Rows[rows].Cells[4].Value.ToString()))//Qty
                                        {
                                            double value;
                                            if (Double.TryParse(dataGridView1.Rows[rows].Cells[4].Value.ToString(), out value))
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
                                   
                                    //string valueDisc = dataGridView1.Rows[rows].Cells[3].Value != null ? dataGridView1.Rows[rows].Cells[3].Value.ToString() : "0";
                                   
                                    //Double valuesubtotal = Double.Parse(valueSubtotal);
                                    Double subtotalnya = qtynya * (harganya - discountnya);
                                    totalAmount = totalAmount + subtotalnya;

                                    string valueDesc = "";

                                    if (dataGridView1.Rows[rows].Cells[6].Value != null)
                                    {
                                        valueDesc = dataGridView1.Rows[rows].Cells[6].Value.ToString();
                                    }

                                    try
                                    {
                                        string queryInsertDetail = "INSERT INTO INVOICE_DETAIL(invoice_id,item_id,qty,price,discount,description) "
                                          + " VALUES( @invoice,@item,@qty,@price,@discount,@description)";
                                        Connection.command = new OleDbCommand(queryInsertDetail, Connection.conn);
                                        Connection.command.CommandType = CommandType.Text;
                                        Connection.command.Parameters.AddWithValue("@invoice", id);
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

                                Console.WriteLine("(double)persenTxt.Value   == " + (double)persenTxt.Value);
                                Console.WriteLine("sebelum dikurang diskon total == " + totalAmount);
                                totalAmount = totalAmount - (((double)persenTxt.Value / 100) * totalAmount);
                                string query = "UPDATE INVOICE SET invoice_no=@invoiceno, invoice_date=@invoicedate,warehouse_id=@warehouse,customer_id=@customer,pajak_id=@pajak, description=@description,totalAmount=@totalAmount,po_number=@ponumber, discountTot='"+ persenTxt.Value + "' WHERE id=@id";
                                Connection.command = new OleDbCommand(query, Connection.conn);
                                Connection.command.CommandType = CommandType.Text;
                                Connection.command.Parameters.AddWithValue("@invoiceno", nobuktiTxt.Text);
                                Connection.command.Parameters.Add("@invoicedate", OleDbType.Date).Value = tanggalTxt.Value;
                                Connection.command.Parameters.AddWithValue("@warehouse", cmbWarehouse.SelectedValue);
                                Connection.command.Parameters.AddWithValue("@customer", cmbCustomer.SelectedValue);
                                if (chkTax.Checked == true)
                                {
                                    Connection.command.Parameters.AddWithValue("@pajak", cmbTax.SelectedValue);
                                }
                                else
                                {
                                    Connection.command.Parameters.AddWithValue("@pajak", DBNull.Value);
                                }
                                Console.WriteLine("totallll == " + totalAmount);
                                Connection.command.Parameters.AddWithValue("@description", descriptionTxt.Text);
                                Connection.command.Parameters.AddWithValue("@totalAmount", totalAmount);
                                Connection.command.Parameters.AddWithValue("@ponumber", nopo.Text);
                                Connection.command.Parameters.AddWithValue("@id", idsalesorder.Text);
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

                        update_sub_label(idsalesorder.Text);

                        string queryAmbilSetelahInsert = "SELECT I.CODE AS CODE,I.NAME AS NAME,SO.QTY AS QTY,SO.PRICE AS PRICE, SO.DISCOUNT, (SO.QTY * (SO.PRICE - SO.DISCOUNT)) AS SUBTOTAL,SO.DESCRIPTION AS DESCRIPTION,SO.ITEM_ID AS ID FROM INVOICE_DETAIL SO INNER JOIN ITEM I ON SO.ITEM_ID = I.ID WHERE SO.INVOICE_ID=" + idsalesorder.Text;


                        OleDbCommand cmd = new OleDbCommand(queryAmbilSetelahInsert, Connection.conn);
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        DataTable scores = new DataTable();
                        da.Fill(scores);
                        dataGridView1.DataSource = scores;

                        MessageBox.Show("Data Berhasil diperbarui.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        nobuktiTxt.Enabled = false;
                        tanggalTxt.Enabled = false;
                        cmbWarehouse.Enabled = false;
                        cmbCustomer.Enabled = false;
                        cmbTax.Enabled = false;
                        chkTax.Enabled = false;
                        descriptionTxt.Enabled = false;
                        dataGridView1.Enabled = false;
                        button2.Text = "EDIT";
                        button1.Enabled = true;
                        button3.Enabled = true;
                        findBtn.Enabled = true;
                        nopo.Enabled = false;
                        persenTxt.Enabled = false;
                    }
                }
            }
            Connection.ConnectionClose();
        }

        private void SalesOrder_Load(object sender, EventArgs e)
        {
            nobuktiTxt.Enabled = false;
            tanggalTxt.Enabled = false;
            cmbCustomer.Enabled = false;
            cmbWarehouse.Enabled = false;
            cmbTax.Enabled = false;
            chkTax.Enabled = false;
            descriptionTxt.Enabled = false;
            dataGridView1.Enabled = false;
            btncancel.Visible = false;
            btncancel.Enabled = false;
            nopo.Enabled = false;
            persenTxt.Enabled = false;
            persenTxt.Increment = 0.01m;
           /* nopo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            nopo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            nopo.Enabled = false;*/

            if (idsalesorder.Text == "ID")
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

            //autocomplete No. PO
            Connection.ConnectionClose();
            Connection.ConnectionOpen();

      
            string gkey = "";
            var source = new List<string>();
            Connection.ConnectionClose();
            Connection.ConnectionOpen();
            string queryItem = "SELECT CODE FROM ITEM WHERE DELETE_FLAG ='N'";
         

            Connection.command = new OleDbCommand(queryItem, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            AutoCompleteStringCollection kode = new AutoCompleteStringCollection();
            reader = Connection.command.ExecuteReader();

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    source.Add(reader["code"].ToString());
                    //kode.Add(reader["code"].ToString());
                }
            }
            else
            {
                MessageBox.Show("Data not Found");
            }
            reader.Close();
           /* this.autoCompleteTextbox1.AutoCompleteList = source;*/
            
        }

      
        public void loadComboBox()
        {
            Connection.ConnectionClose();
            Connection.ConnectionOpen();

            //TODO fill Customer list
            string queryCustomer = "SELECT * FROM CUSTOMER ORDER BY NAME ASC";
            OleDbDataAdapter dAdapter = new OleDbDataAdapter(queryCustomer, Connection.conn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds, "Customer");
            cmbCustomer.DisplayMember = "name";
            cmbCustomer.ValueMember = "id";
            cmbCustomer.DataSource = ds.Tables["Customer"];

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
            //string query = "SELECT * FROM ITEM WHERE DELETE_FLAG = 'N' ORDER BY NAME";
            //dAdapter = new OleDbDataAdapter(query, Connection.conn);

            //DataSet ds1 = new DataSet();
            //dAdapter.Fill(ds1, "Item");
            //(dataGridView1.Columns[0] as DataGridViewComboBoxColumn).DisplayMember = "name";
            //(dataGridView1.Columns[0] as DataGridViewComboBoxColumn).ValueMember = "id";
            //(dataGridView1.Columns[0] as DataGridViewComboBoxColumn).DataSource = ds1.Tables["Item"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "ADD")
            {

                nobuktiTxt.Enabled = true;
                tanggalTxt.Enabled = true;
                cmbCustomer.Enabled = true;
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
                btncancel.Visible = true;
                btncancel.Enabled = true;
                nopo.Enabled = true;
                persenTxt.Enabled = true;
                persenTxt.Value = 0;

                loadComboBox();

                //dataGridView1.Refresh();


                button1.Text = "SAVE";
                button2.Enabled = false;
                button3.Enabled = false;
                findBtn.Enabled = false;
                dataGridView1.Enabled = true;
                dataGridView1.ReadOnly = false;
            }
            else if (button1.Text == "SAVE")
            {
                Boolean errorDatagridviewnull = false;
                Double totalAmount = 0;


                int rowCount = dataGridView1.Rows.Count - 1;
                Console.WriteLine(" int rowCount "+ rowCount);
                if (rowCount == 0) {
                    MessageBox.Show("ISI BARANG!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Index < rowCount)
                    {
                        try
                        {                         
                            var valueItem = row.Cells["id"].Value;
                            var valueQty = row.Cells["qty"].Value;
                            var valueHarga = row.Cells["price"].Value;
                            var valueDiscount = row.Cells["discount"].Value;


                            if (valueItem == null || valueQty == null || valueHarga == null) {
                                MessageBox.Show("INFORMASI BARANGHARUS DI ISI!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            Double qtynya = Double.Parse(valueQty.ToString());
                            Double harganya = Double.Parse(valueHarga.ToString());
                            double disc = 0;
                            try
                            {
                                if (valueDiscount != null)
                                {
                                    disc = double.Parse(valueDiscount.ToString());
                                }
                            }
                            catch (Exception)
                            {
                                disc = 0;
                            }




                            Double subtotalnya = qtynya * (harganya - disc);
                            totalAmount = totalAmount + subtotalnya;
                            if (valueItem == null || valueItem == "0")//kalau dropdown item
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
                    }

                }


                if (nobuktiTxt.Text == "")
                {
                    MessageBox.Show("Nomor Bukti Kosong, Sistem akan mengisinya secara otomatis", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                if (cmbCustomer.SelectedValue == null)
                {
                    MessageBox.Show("Customer harus diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbCustomer.Focus();
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
                    return;
                }
                else
                {
                    Connection.ConnectionClose();
                    Connection.ConnectionOpen();

                    string nobukti = "";
                    String hari = String.Format("{0:dd}", DateTime.Now);
                    String bulan = String.Format("{0:MM}", DateTime.Now);
                    String tahun = String.Format("{0:yy}", DateTime.Now);
                    

                    string queryCheck = "SELECT SEQ_NUMBER+1 as SEQ FROM SEQUENCE_STORE WHERE SEQ_CODE='invoice'";
                    Connection.command = new OleDbCommand(queryCheck, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    reader = Connection.command.ExecuteReader();
                    if (reader.Read())
                    {
                        Console.WriteLine("get seq number = "+reader["SEQ"].ToString());
                        nobukti = "SI" + string.Concat(string.Concat(string.Concat(hari, bulan), tahun), getResult(Int32.Parse(reader["SEQ"].ToString())));

                    }
                    string query = "SELECT * FROM INVOICE WHERE invoice_no=@invoiceno and delete_flag='N'";
                    Connection.command = new OleDbCommand(query, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@invoiceno", nobukti);
                    reader = Connection.command.ExecuteReader();
                    if (reader.Read())
                    {
                        //String id = Convert.ToString(reader["id"]);
                        //jika data yang mau saya buat itu belum ada...baru bisa insert baru..
                        DialogResult avaiable = MessageBox.Show("Invoice No. " + nobukti + " sudah ada", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            Connection.ConnectionClose();
                            Connection.ConnectionOpen();
                            int id;
                            totalAmount = totalAmount -  (((double)persenTxt.Value/100)*totalAmount);
                            string assadf = "INSERT INTO INVOICE(invoice_no,invoice_date,customer_id,warehouse_id,pajak_id,description,totalAmount,delete_flag,payment,nopo,do_number,discountTot) VALUES(@invoiceno,@invoicedate,@customer,@warehouse,NULL,'"+ descriptionTxt.Text+"','"+ totalAmount+ "' ,'N','N','"+nopo.Text+"',NULL, '"+persenTxt.Value+"')";
                           /* string queryInsert = "INSERT INTO INVOICE(invoice_no,invoice_date,customer_id,warehouse_id,pajak_id,description,totalAmount,delete_flag,payment,nopo) VALUES('" + nobukti + "','" + tanggalTxt.Value + "','" + cmbCustomer.SelectedValue + "','" + cmbWarehouse.SelectedValue + "'," +
                                            "'','" + descriptionTxt.Text + "'," + totalAmount + ",'N','N','" + nopo.Text + "')";*/

                            Connection.command = new OleDbCommand(assadf, Connection.conn);

                            Connection.command.CommandType = CommandType.Text;

                            Connection.command.Parameters.AddWithValue("@invoiceno", nobukti);
                            Console.WriteLine("tanggal == " + tanggalTxt.Value);
                            Connection.command.Parameters.Add("@invoicedate", OleDbType.Date).Value = tanggalTxt.Text;
                            Connection.command.Parameters.AddWithValue("@customer", cmbCustomer.SelectedValue);
                            Connection.command.Parameters.AddWithValue("@warehouse", cmbWarehouse.SelectedValue);
                            if (chkTax.Checked == true)
                            {
                                Connection.command.Parameters.AddWithValue("@pajak", cmbTax.SelectedValue);
                            }
                            else
                            {
                                Connection.command.Parameters.AddWithValue("@pajak", DBNull.Value);
                            }

                            //Connection.command.Parameters.AddWithValue("@deskripsi", descriptionTxt.Text);
                            //Connection.command.Parameters.AddWithValue("@nopo", nopo.Text);
                            //Connection.command.Parameters.AddWithValue("@totalamount", totalAmount);

                            Console.WriteLine("Query yang akan dieksekusi: " + totalAmount);
                            Connection.command.ExecuteNonQuery();


                            Connection.command.CommandText = "Select @@Identity";
                            id = (int)Connection.command.ExecuteScalar();
                            idsalesorder.Text = id.ToString();
                            string queryGet = "SELECT * FROM INVOICE WHERE id=@id and delete_flag='N'";
                            Connection.command = new OleDbCommand(queryGet, Connection.conn);
                            Connection.command.CommandType = CommandType.Text;
                            Connection.command.Parameters.AddWithValue("@id", id);
                            reader = Connection.command.ExecuteReader();
                            if (reader.Read())
                            {
                                nobuktiTxt.Text = reader["invoice_no"].ToString();
                                tanggalTxt.Value = Convert.ToDateTime(reader["invoice_date"]);
                                cmbWarehouse.SelectedValue = reader["warehouse_id"].ToString();
                                cmbCustomer.SelectedValue = reader["customer_id"].ToString();
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
                            }
                            //Simpan detail
                            try
                            {

                                for (int rows = 0; rows < dataGridView1.Rows.Count - 1; rows++)
                                {
                                    //DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dataGridView1.Rows[rows].Cells[0];
                                    string valueItem = dataGridView1.Rows[rows].Cells[7].Value.ToString();
                                    string valueQty = dataGridView1.Rows[rows].Cells[2].Value.ToString();
                                    string valueHarga = dataGridView1.Rows[rows].Cells[3].Value.ToString();


                                    //string valueDisc = dataGridView1.Rows[rows].Cells[3].Value.ToString();
                                    string valueDisc = dataGridView1.Rows[rows].Cells[4].Value != null ? dataGridView1.Rows[rows].Cells[4].Value.ToString() : "0";
                                    // Jika nilai adalah string kosong atau null, ubah nilai menjadi "0"
                                    if (string.IsNullOrEmpty(valueDisc))
                                    {
                                        valueDisc = "0";
                                    }

                                    Double qtynya = Double.Parse(valueQty);
                                    Double harganya = Double.Parse(valueHarga);
                                    Double discountnya = Double.Parse(valueDisc);


                                    string valueDesc = "";
                                    try
                                    {
                                        if (dataGridView1.Rows[rows].Cells[6].Value != null)
                                        {
                                            valueDesc = dataGridView1.Rows[rows].Cells[6].Value.ToString();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        valueDesc = "";
                                    }


                                    try
                                    {
                                        string queryInsertDetail = "INSERT INTO INVOICE_DETAIL(invoice_id,item_id,qty,price,discount,description) "
                                         + " VALUES( @invoice,@item,@qty,@price,@discount,@description)";
                                        Connection.command = new OleDbCommand(queryInsertDetail, Connection.conn);
                                        Connection.command.CommandType = CommandType.Text;
                                        Connection.command.Parameters.AddWithValue("@invoice", id);
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



                                string queryAddToSequence = "UPDATE SEQUENCE_STORE SET SEQ_NUMBER=SEQ_NUMBER+1 WHERE SEQ_CODE='invoice'";
                                Connection.command = new OleDbCommand(queryAddToSequence, Connection.conn);
                                Connection.command.CommandType = CommandType.Text;
                                Connection.command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Terjadi Kesalahan saat menyimpan data, mohon hubungi pengembang! /n" + ex);
                            }

                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("there was an issue! 2222" + ex);
                        }
                        catch (OleDbException ex)
                        {
                           
                            //Console.WriteLine("Kesalahan saat menjalankan query: " + ex.Message);

                            // Menampilkan jejak tumpukan untuk mengetahui di mana kesalahan terjadi
                            //Console.WriteLine("Jejak tumpukan: " + ex.StackTrace);

                             // Menampilkan pesan kesalahan
                           // Console.WriteLine("Kesalahan saat menjalankan query: " + ex.Message);

                            // Mencetak query string dengan nilai-nilai parameter yang diganti
                            string queryWithParameters = Connection.command.CommandText;
                            foreach (OleDbParameter parameter in Connection.command.Parameters)
                            {
                                queryWithParameters = queryWithParameters.Replace(parameter.ParameterName, parameter.Value.ToString());
                            }
                            //Console.WriteLine("Query yang dieksekusi: " + queryWithParameters);
                        }

                        update_sub_label(idsalesorder.Text);

                        string queryAmbilSetelahInsert = "SELECT I.CODE AS CODE,I.NAME AS NAME,SO.QTY AS QTY,SO.PRICE AS PRICE, SO.DISCOUNT, (SO.QTY * (SO.PRICE - SO.DISCOUNT)) AS SUBTOTAL,SO.DESCRIPTION AS DESCRIPTION,SO.ITEM_ID AS ID FROM INVOICE_DETAIL SO INNER JOIN ITEM I ON SO.ITEM_ID = I.ID WHERE SO.INVOICE_ID=" + idsalesorder.Text;

                        //string queryAmbilSetelahInsert = "SELECT SO.ITEM_ID AS ITEM, SO.QTY AS QTY,SO.PRICE AS PRICE, SO.DISCOUNT ,SO.DESCRIPTION AS DESCRIPTION, (SO.QTY * SO.PRICE - SO.DISCOUNT) AS SUBTOTAL FROM INVOICE_DETAIL SO WHERE SO.INVOICE_ID=" + idsalesorder.Text;

                        OleDbCommand cmd = new OleDbCommand(queryAmbilSetelahInsert, Connection.conn);
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        DataTable scores = new DataTable();
                        da.Fill(scores);
                        dataGridView1.DataSource = scores;




                        nobuktiTxt.Enabled = false;
                        tanggalTxt.Enabled = false;
                        cmbWarehouse.Enabled = false;
                        cmbCustomer.Enabled = false;
                        chkTax.Enabled = false;
                        cmbTax.Enabled = false;
                        descriptionTxt.Enabled = false;
                        dataGridView1.Enabled = false;


                        button1.Text = "ADD";
                        
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = true;
                        button7.Enabled = true;
                        findBtn.Enabled = true;
                        nopo.Enabled = false;
                        persenTxt.Enabled = false;
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

        private void AllowNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public void addItems(AutoCompleteStringCollection col)
        {
            Connection.ConnectionClose();
            Connection.ConnectionOpen();

            //TODO fill Customer list
            string queryItem = "SELECT * FROM ITEM ";

            Connection.command = new OleDbCommand(queryItem, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.Parameters.AddWithValue("@id", idsalesorder);
            reader = Connection.command.ExecuteReader();
            while (reader.Read())
            {
                col.Add(reader["name"].ToString());
            }

        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {    
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                
                Connection.ConnectionClose();
                Connection.ConnectionOpen();

                var source = new List<string>();
                string queryItem = "SELECT * FROM ITEM";              
                
                Connection.command = new OleDbCommand(queryItem, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                AutoCompleteStringCollection kode = new AutoCompleteStringCollection();
                
                reader = Connection.command.ExecuteReader();
               
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        kode.Add(reader["code"].ToString());
                        //source.Add(reader["code"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Data not Found");
                }
                reader.Close();
                //ComboBox txtBusID = e.Control as ComboBox;
                //AutoCompleteTextBoxSample.AutoCompleteTextbox kodeTxt = e.Control as AutoCompleteTextBoxSample.AutoCompleteTextbox;
                TextBox kodeTxt = e.Control as TextBox;
                if (kodeTxt != null)
                {
                    kodeTxt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    kodeTxt.AutoCompleteCustomSource = kode;
                    //kodeTxt.AutoCompleteList = source;
                    kodeTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }

            }


            e.Control.KeyPress -= new KeyPressEventHandler(AllowNumber_KeyPress);
         
            if (((DataGridView)sender).CurrentCell.ColumnIndex == 0) //Assuming 0 is the index of the ComboBox Column you want to show
            {
                TextBox code = e.Control as TextBox;
                code.CharacterCasing = CharacterCasing.Upper;
                if (code != null)
                {
                    code.Leave -= new EventHandler(cb_IndexChanged);
                    // now attach the event handler
                    code.Leave += new EventHandler(cb_IndexChanged);
                }
            }
            
            if (((DataGridView)sender).CurrentCell.ColumnIndex == 2)
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
            else if (((DataGridView)sender).CurrentCell.ColumnIndex == 3)
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
            else if (((DataGridView)sender).CurrentCell.ColumnIndex == 4)
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

        void nm_IndexChanged(object sender, EventArgs e)
        {
            int currentRow = dataGridView1.CurrentRow.Index;
            dataGridView1[0, currentRow].Value = "";
            string idx = "";
            try
            {
                //Console.WriteLine("currentRow = " + currentRow);
                //Console.WriteLine("dataGridView1.Rows[currentRow].Cells[1].Value.ToString() = " + dataGridView1.Rows[currentRow].Cells[1].Value.ToString());
                if (!String.IsNullOrWhiteSpace(dataGridView1.Rows[currentRow].Cells[1].Value.ToString()))//kode
                {
                    Connection.ConnectionClose();
                    Connection.ConnectionOpen();

                    //TODO fill Customer list
                    string queryItem = "SELECT * FROM ITEM WHERE ";

                    if (dataGridView1.CurrentCell.ColumnIndex == 1)
                    {
                        queryItem = queryItem + " NAME = @namex";
                    }
                    else if(dataGridView1.CurrentCell.ColumnIndex == 0){
                        queryItem = queryItem + " CODE=@KODELK";
                    }

                    Connection.command = new OleDbCommand(queryItem, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;

                    if (dataGridView1.CurrentCell.ColumnIndex == 1)
                    {
                        Connection.command.Parameters.AddWithValue("@namex", dataGridView1.Rows[currentRow].Cells[1].Value.ToString());
                    }
                    else if (dataGridView1.CurrentCell.ColumnIndex == 0)
                    {
                        Connection.command.Parameters.AddWithValue("@KODELK", dataGridView1.Rows[currentRow].Cells[0].Value.ToString());
                    }


                    //Console.WriteLine("name d = " + dataGridView1.Rows[currentRow].Cells[1].Value.ToString());
                    reader = Connection.command.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                        {
                            if (dataGridView1.CurrentCell.ColumnIndex == 1)
                            {
                                dataGridView1[0, currentRow].Value = reader["code"].ToString();
                                dataGridView1[7, currentRow].Value = reader["id"].ToString();
                            }
                            else if (dataGridView1.CurrentCell.ColumnIndex == 0)
                            {
                                dataGridView1[1, currentRow].Value = reader["name"].ToString();
                                dataGridView1[7, currentRow].Value = reader["id"].ToString();
                            }

                          
                            idx = reader["id"].ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Data not Found");
                    }
                }
                
            }
            catch (Exception ex)
            {
                dataGridView1[0, currentRow].Value ="a";
                //Console.WriteLine("ex = "+ex);
            }
            reader.Close();
            // Console.WriteLine("idx = " + idx);
            auto_complete_harga(idx, currentRow);
        }
        void cb_IndexChanged(object sender, EventArgs e)
        {
            int currentRow = dataGridView1.CurrentRow.Index;
            int currentCell = dataGridView1.CurrentCell.ColumnIndex;
            /*dataGridView1[1 , currentRow].Value = "";//nama ilangin*/
            string idx = "";

            if (currentCell == 0) {
                try
                {
                    //Console.WriteLine("current row cb i= " + currentRow);

                    if (dataGridView1.Rows[currentRow].Cells[0].Value == null || dataGridView1.Rows[currentRow].Cells[0].Value == DBNull.Value || String.IsNullOrWhiteSpace(dataGridView1.Rows[currentRow].Cells[0].Value.ToString()))//kode
                    {
                        MessageBox.Show("ITEM KOSONG ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        Connection.ConnectionClose();
                        Connection.ConnectionOpen();

                        string queryItem = "SELECT * FROM ITEM WHERE CODE='" + dataGridView1.Rows[currentRow].Cells[0].Value.ToString() + "'";
                        Connection.command = new OleDbCommand(queryItem, Connection.conn);
                        Connection.command.CommandType = CommandType.Text;
                        /*Connection.command.Parameters.AddWithValue("@KODELK", );*/
                        //Console.WriteLine("ddd d = " + dataGridView1.Rows[currentRow].Cells[0].Value.ToString());
                        reader = Connection.command.ExecuteReader();
                        if (reader.Read())
                        {
                            dataGridView1[1, currentRow].Value = reader["name"].ToString();
                            dataGridView1[3, currentRow].Value = reader["price"].ToString();
                            dataGridView1[7, currentRow].Value = reader["id"].ToString();
                            idx = reader["id"].ToString();
                        }
                        else
                        {
                            dataGridView1[1, currentRow].Value = null;
                            dataGridView1[3, currentRow].Value = null;
                            dataGridView1[7, currentRow].Value = null;
                            dataGridView1[0, currentRow].Value = DBNull.Value;
                            idx = "0";
                        }
                        
                    }
                    reader.Close();
                    /*auto_complete_harga(idx, currentRow);*/
                }
                catch (Exception ex)
                {
                    dataGridView1[1, currentRow].Value = "";
                }
            }
          
        }

        void price_leaving(object sender, EventArgs e)//saat edit harga selesai, ubah format ke currency format
        {
           //var tb = dataGridView1.EditingControl as TextBox;
            int currentRow = dataGridView1.CurrentRow.Index;
            int currentCell = dataGridView1.CurrentCell.ColumnIndex;
            //Console.WriteLine(currentRow);
            //Console.WriteLine("currentCell price leaving = " + currentCell);
            /*Console.WriteLine("qty = " + dataGridView1.Rows[currentRow].Cells[2].Value);
           */
            Double qtytmp = 0;//ini bukan qty tp harga!!!
            if (currentCell == 3)
            {
                try
                {
                    string str = dataGridView1.Rows[currentRow].Cells[3].Value.ToString() != null ? dataGridView1.Rows[currentRow].Cells[3].Value.ToString() : null;
                    dataGridView1[3, currentRow].Value = double.Parse(dataGridView1.Rows[currentRow].Cells[3].Value.ToString());

            
                    qtytmp = double.Parse(dataGridView1.Rows[currentRow].Cells[3].Value.ToString());
                    //Console.WriteLine("qtytemmp == "+qtytmp);
                }
                catch (Exception ex)
                {
                    dataGridView1[3, currentRow].Value = 0;
                }

                Double qty = 0;//nilai qty
                Double harga = qtytmp;
                Double discount = 0;

                try
                {
                    Console.WriteLine("aaa qty = " + dataGridView1[2, currentRow].Value.ToString());
                    if (!String.IsNullOrEmpty(dataGridView1[2, currentRow].Value.ToString()))//Qty
                    {
                        double value;
                        if (Double.TryParse(dataGridView1[2, currentRow].Value.ToString(), out value))
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
                    if (!String.IsNullOrEmpty(dataGridView1[4, currentRow].Value.ToString()))//Qty
                    {
                        double value;
                        if (Double.TryParse(dataGridView1[4, currentRow].Value.ToString(), out value))
                            discount = value;
                        else
                            discount = 0; // discount will have null value
                    }
                }
                catch (Exception ex)
                {
                    discount = 0;
                }
                                
                Double subtotal = qty * (harga - discount);
                dataGridView1[5, currentRow].Value = subtotal;
            }

        }

        void qty_leaving(object sender, EventArgs e)//saat edit harga selesai, ubah format ke currency format
        {
            
            //var tb = dataGridView1.EditingControl as TextBox;
            int currentRow = dataGridView1.CurrentRow.Index;
            int currentCell = dataGridView1.CurrentCell.ColumnIndex;
            //Console.WriteLine(currentRow);
            //Console.WriteLine("currentCell = "+ currentCell);
            /*Console.WriteLine("qty = " + dataGridView1.Rows[currentRow].Cells[2].Value);
           */
            Double qtytmp = 0;
            if (currentCell == 2) {
                try
                {
                    string str = dataGridView1.Rows[currentRow].Cells[2].Value.ToString() != null ? dataGridView1.Rows[currentRow].Cells[2].Value.ToString() : null;
                    dataGridView1[2, currentRow].Value = double.Parse(dataGridView1.Rows[currentRow].Cells[2].Value.ToString());

                  
                    qtytmp = double.Parse(dataGridView1.Rows[currentRow].Cells[2].Value.ToString());
                }
                catch (Exception ex)
                {
                    dataGridView1[2, currentRow].Value = 0;
                }

                Double qty = qtytmp;//nilai qty
                Double harga = 0;
                Double discount = 0;

                try
                {
                    //Console.WriteLine("aaa = " + dataGridView1[3, currentRow].Value.ToString());
                    if (!String.IsNullOrEmpty(dataGridView1[3, currentRow].Value.ToString()))//Qty
                    {
                        double value;
                        if (Double.TryParse(dataGridView1[3, currentRow].Value.ToString(), out value))
                            harga = value;
                        else
                            harga = 0; // discount will have null value
                    }
                }
                catch (Exception ex)
                {
                    harga = 0;
                }
                Console.WriteLine("noll");
                try
                {

                    if (dataGridView1[4, currentRow].Value == null || dataGridView1[4, currentRow].Value == DBNull.Value)
                    {

                        discount = 0;
                    }
                    else {
                        discount = Double.Parse(dataGridView1[4, currentRow].Value.ToString());
                    }
                 
                }
                catch (Exception ex)
                {
                    discount = 0;
                    //Console.WriteLine("masuk ke discount ex qty");
                }



                Double subtotal = qty * (harga - discount);
                dataGridView1[5, currentRow].Value = subtotal;
            }
            
          
        }

        void discount_leaving(object sender, EventArgs e)//saat edit harga selesai, ubah format ke currency format
        {
            //Console.WriteLine("masuk discount leaving");
            int currentRow = dataGridView1.CurrentRow.Index;
            int currentColumn = dataGridView1.CurrentCell.ColumnIndex;
            double discountTxt = 0;

            if (currentColumn == 4) {
                try
                {
                    string str = dataGridView1.Rows[currentRow].Cells[4].Value.ToString() != null ? dataGridView1.Rows[currentRow].Cells[4].Value.ToString() : null;
                    dataGridView1[4, currentRow].Value = double.Parse(dataGridView1.Rows[currentRow].Cells[4].Value.ToString());


                    discountTxt = double.Parse(dataGridView1.Rows[currentRow].Cells[4].Value.ToString());
                }
                catch (Exception ex)
                {
                    dataGridView1[4, currentRow].Value = 0;
                }


                Double qty = 0;//nilai qty
                Double harga = 0;
                

                try
                {
                    Console.WriteLine("aaa qty = " + dataGridView1[2, currentRow].Value.ToString());
                    if (!String.IsNullOrEmpty(dataGridView1[2, currentRow].Value.ToString()))//Qty
                    {
                        double value;
                        if (Double.TryParse(dataGridView1[2, currentRow].Value.ToString(), out value))
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
                    //Console.WriteLine("aaa = " + dataGridView1[3, currentRow].Value.ToString());
                    if (!String.IsNullOrEmpty(dataGridView1[3, currentRow].Value.ToString()))//Qty
                    {
                        double value;
                        if (Double.TryParse(dataGridView1[3, currentRow].Value.ToString(), out value))
                            harga = value;
                        else
                            harga = 0; // discount will have null value
                    }
                }
                catch (Exception ex)
                {
                    harga = 0;
                }



                //Console.WriteLine("-------------------- = ");
                //Console.WriteLine("QTY = " + qty);
                //Console.WriteLine("harga = " + harga);
                //Console.WriteLine("discountTxt = " + discountTxt);


                //Double qty = Double.Parse(dataGridView1[1, currentRow].Value != null ? dataGridView1[1, currentRow].Value.ToString() : "0");//nilai qty
                //Double harga = Double.Parse(dataGridView1[2, currentRow].Value != null ? dataGridView1[2, currentRow].Value.ToString() : "0");//nilai price
                //Double discount = Double.Parse(dataGridView1[3, currentRow].Value != null ? dataGridView1[3, currentRow].Value.ToString() : "0");//nilai discount
                Double subtotal = qty * (harga - discountTxt);
                dataGridView1[5, currentRow].Value = subtotal;
            }
           
        }

        public void auto_complete_harga(string item_id, int currentRow)//auto set harga pas barang di pilih
        {
            Connection.ConnectionClose();
            Connection.ConnectionOpen();
             Double harga = 0;
            Console.WriteLine("currentRow = " + currentRow);
            string query = "SELECT PRICE FROM ITEM WHERE ID=@ID";
            Connection.command = new OleDbCommand(query, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.Parameters.AddWithValue("@ID", item_id);
            reader = Connection.command.ExecuteReader();
            if (reader.Read())
            {
                Console.WriteLine("harga rader = " + reader["price"].ToString());
                harga = Double.Parse(reader["price"].ToString());
                Console.WriteLine("haga == " + harga);
                dataGridView1[3, currentRow].Value = harga;

            }
            else {
                Console.WriteLine("Item Code yang anda cari tidak ada");
            }
          
            Double qty = 0;
            Double discount = 0;

            /* try
             {
                 Console.WriteLine("aaa = " + dataGridView1[2, currentRow].Value.ToString());
                 if (!String.IsNullOrWhiteSpace(dataGridView1[2, currentRow].Value.ToString()))//Qty
                 {
                     double value;
                     if (Double.TryParse(dataGridView1[2, currentRow].Value.ToString(), out value))
                         qty = value;
                     else
                         qty = 0; // discount will have null value
                 }
             }
             catch (Exception ex)
             {
                 qty = 0;
             }*/


            /* try
             {
                 if (!String.IsNullOrEmpty(dataGridView1[4, currentRow].Value.ToString()))//discount
                 {
                     double value;
                     if (Double.TryParse(dataGridView1[4, currentRow].Value.ToString(), out value))
                         discount = value;
                     else
                         discount = 0; // discount will have null value
                 }
             }
             catch (Exception ex)
             {
                 discount = 0; // discount will have null value
             }*/

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

        private void findBtn_Click(object sender, EventArgs e)
        {
            Hashtable fieldTable = new Hashtable();
            fieldTable.Add("SO.INVOICE_NO", "No. Delivery");
            fieldTable.Add("SO.DO_NUMBER", "No. Order");

            int promptValue = SearchSalesInvoice.ShowDialog("Cari Invoice", fieldTable);

            if (promptValue != 0)
            {
                idsalesorder.Text = promptValue.ToString();


                update_sub_label(idsalesorder.Text);

                string queryAmbilSetelahInsert = "SELECT I.CODE AS CODE,I.NAME AS NAME,SO.QTY AS QTY ,SO.PRICE AS PRICE, SO.DISCOUNT AS DISCOUNT, (SO.QTY * (SO.PRICE - SO.DISCOUNT)) AS SUBTOTAL, SO.DESCRIPTION AS DESCRIPTION,SO.ITEM_ID AS ID FROM INVOICE_DETAIL SO INNER JOIN ITEM I ON SO.ITEM_ID = I.ID WHERE SO.INVOICE_ID=" + idsalesorder.Text;

                OleDbCommand cmd = new OleDbCommand(queryAmbilSetelahInsert, Connection.conn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataTable scores = new DataTable();
                da.Fill(scores);
                dataGridView1.DataSource = scores;

                button2.Enabled = true;
                button3.Enabled = true;
                if (idsalesorder.Text != "ID")
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
            
        }

        private void hapus_sales_order_detail(string idsalesorder)
        {
            Connection.ConnectionClose();
            Connection.ConnectionOpen(); 
            string queryHapusDetail = "DELETE FROM INVOICE_DETAIL WHERE INVOICE_ID=@id ";
            Connection.command = new OleDbCommand(queryHapusDetail, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.Parameters.AddWithValue("@id", idsalesorder);
            Connection.command.ExecuteNonQuery();           
        }

        private void label9_Click(object sender, EventArgs e)
        {

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
            string queryGet = "SELECT * FROM INVOICE WHERE id=@id and delete_flag='N'";
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

                nobuktiTxt.Text = reader["invoice_no"].ToString();
                tanggalTxt.Value = Convert.ToDateTime(reader["invoice_date"]);
                cmbCustomer.SelectedValue = reader["customer_id"].ToString();
                cmbWarehouse.SelectedValue = reader["warehouse_id"].ToString();
                nopo.Text = reader["nopo"].ToString();
                Decimal nilaiDesimal = reader.GetDecimal(reader.GetOrdinal("discountTot"));
                Console.WriteLine("persen ==== " + nilaiDesimal);
                persenTxt.Value = nilaiDesimal;
               

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

        private void button3_Click(object sender, EventArgs e)
        {
            Connection.ConnectionClose();
            Connection.ConnectionOpen();

            Boolean bolehhapus = false;
            DialogResult dialogResult = MessageBox.Show("Apakah anda yakin akan menghapus Sales Order ini?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                /*string queryCekDulu = "SELECT * FROM INVOICE WHERE ID=@id ";
                Connection.command = new OleDbCommand(queryCekDulu, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@id", idsalesorder.Text);
                reader = Connection.command.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["deliveryorder_id"] != DBNull.Value)
                    {
                        bolehhapus = true;                        
                    }
                }

                if (bolehhapus)
                {
                    MessageBox.Show("Sales Order tidak bisa dihapus, karena sudah diterbitkan!", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {*/
                    string queryHapusDetail = "DELETE FROM INVOICE_DETAIL WHERE INVOICE_ID=@id ";
                    string queryHapusHead = "DELETE FROM INVOICE WHERE ID=@id ";
                    Connection.command = new OleDbCommand(queryHapusDetail, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@id", idsalesorder.Text);
                    Connection.command.ExecuteNonQuery();

                    Connection.command = new OleDbCommand(queryHapusHead, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@id", idsalesorder.Text);
                    Connection.command.ExecuteNonQuery();

                    MessageBox.Show("Bukti Sales Invoice Berhasil dihapus ", "Delete Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                /*}*/               
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Aksi Penghapusan Bukti Pindah Gudang Telah dibatalkan!", "Delete Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {/* //FOR DOING EXPORT TO DELIVERY ORDER
            Connection.ConnectionClose();
            Connection.ConnectionOpen();
            string queryCariSudahExportAtauBelum= "SELECT * FROM INVOICE WHERE id=@id AND deliveryorder_id is null";
            Connection.command = new OleDbCommand(queryCariSudahExportAtauBelum, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.Parameters.AddWithValue("@id", idsalesorder.Text);

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
                string queryCheck = "SELECT SEQ_NUMBER FROM SEQUENCE_STORE WHERE SEQ_CODE='deliveryorder'";
                Connection.command = new OleDbCommand(queryCheck, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                reader = Connection.command.ExecuteReader();
                if (reader.Read())
                {
                    bukti_ke = Int32.Parse(reader["seq_number"].ToString()) + 1;
                }
                nobukti = "DO" + string.Concat(string.Concat(string.Concat(hari, bulan), tahun), getResult(bukti_ke));

                //CEK NOMOR BUKTI UNTUK DO, APAKAH SUDAH DIPAKAI ATAU BELUM
                string query = "SELECT * FROM DELIVERYORDER WHERE delivery_no=@orderno and delete_flag='N'";
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
                    string queryGet = "SELECT * FROM SALESORDER WHERE id=@id and delete_flag='N'";
                    Connection.command = new OleDbCommand(queryGet, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@id", idsalesorder.Text);
                    reader = Connection.command.ExecuteReader();
                    if (reader.Read())
                    {
                        //COPAS DAN MASUKAN KE DELIVERY ORDER BARU
                        string querySet = "INSERT INTO DELIVERYORDER(delivery_no,delivery_date,customer_id,warehouse_id,pajak_id,description,terbitke,totalAmount,delete_flag,so_number,po_number) "
                                                    + " VALUES( @orderno,@orderdate,@customer,@warehouse,@pajak,@description,NULL,@totalamount,'N',@sonumber,@ponumber)";

                        Connection.command = new OleDbCommand(querySet, Connection.conn);
                        Connection.command.CommandType = CommandType.Text;
                        gudangasal = reader["warehouse_id"].ToString();
                        
                        Connection.command.Parameters.AddWithValue("@orderno", nobukti);
                        Connection.command.Parameters.Add("@orderdate", OleDbType.Date).Value = DateTime.Now;
                        Connection.command.Parameters.AddWithValue("@customer", reader["customer_id"].ToString());
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
                        Connection.command.Parameters.AddWithValue("@sonumber", reader["order_no"].ToString());
                        Connection.command.Parameters.AddWithValue("@ponumber", reader["po_number"].ToString());
                        Connection.command.ExecuteNonQuery();


                        Connection.command.CommandText = "Select @@Identity";
                        iddeliveryorder = (int)Connection.command.ExecuteScalar();


                    }
                    //INSERT ALL SALESORDER DETAIL TO DELIVERY ORDER DETAIL
                    string queryGetDetail = "SELECT * FROM SALESORDER_DETAIL WHERE SALESORDER_ID=@salesid";
                    Connection.command = new OleDbCommand(queryGetDetail, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@salesid", idsalesorder.Text);
                    reader = Connection.command.ExecuteReader();
                    while (reader.Read())
                    {
                        string querySetDetail = "INSERT INTO DELIVERYORDER_DETAIL(deliveryorder_id,item_id,qty,price,discount,description) "
                                             + " VALUES( @deliveryorder,@item,@qty,@price,@discount,@description)";
                        Connection.command = new OleDbCommand(querySetDetail, Connection.conn);
                        Connection.command.CommandType = CommandType.Text;
                        Connection.command.Parameters.AddWithValue("@deliveryorder", iddeliveryorder);
                        Connection.command.Parameters.AddWithValue("@item", reader["item_id"].ToString());
                        Connection.command.Parameters.AddWithValue("@qty", reader["qty"].ToString());
                        Connection.command.Parameters.AddWithValue("@price", reader["price"].ToString());
                        Connection.command.Parameters.AddWithValue("@discount", reader["discount"].ToString());
                        Connection.command.Parameters.AddWithValue("@description", reader["description"].ToString());
                        Connection.command.ExecuteNonQuery();

                        UpdateStock.itemKeluar(gudangasal, Int32.Parse(reader["qty"].ToString()), reader["item_id"].ToString() );
                    }

                    string querySetSudahExport = "UPDATE SALESORDER SET deliveryorder_id=@deliveryorderid WHERE id=@id";
                    Connection.command = new OleDbCommand(querySetSudahExport, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@deliveryorderid", iddeliveryorder);
                    Connection.command.Parameters.AddWithValue("@id", idsalesorder.Text);
                    Connection.command.ExecuteNonQuery();

                    string queryAddToSequence = "UPDATE SEQUENCE_STORE SET SEQ_NUMBER=@INCREMENTNYA WHERE SEQ_CODE='deliveryorder'";
                    Connection.command = new OleDbCommand(queryAddToSequence, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@INCREMENTNYA", bukti_ke);
                    Connection.command.ExecuteNonQuery();

                   
                    MessageBox.Show("Sales Order berhasil diterbitkan ke DO!", "Terbit Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               
            }
            else
            {
                MessageBox.Show("Sales Order ini sudah diterbitkan! \n Atau \n Sales Order Tidak ditemukan.", "Terbit Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }*/
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(reporting.ReportViewerSalesInvoice))
                {
                    form.Activate();
                    return;
                }
            }
            reporting.ReportViewerSalesInvoice rv = new reporting.ReportViewerSalesInvoice(idsalesorder.Text);
            rv.MdiParent = mdi;
            rv.Show();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //ReportDocument reportDoc = new ReportDocument();
            string filePath = Application.StartupPath + "/reporting/BuktiSalesInvoice.rpt";
            //reportDoc.Load(filePath);
            //reportDoc.SetParameterValue("salesid", idsalesorder.Text);

            
            //reportDoc.PrintToPrinter(1, true, 0, 0);


            System.Threading.Thread.Sleep(2000);

            ReportDocument cryRpt = new ReportDocument();
            ReportDocument crpt2 = cryRpt;
            cryRpt.Load(filePath);
            cryRpt.SetParameterValue("idinvoice", idsalesorder.Text);

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

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Apakah anda yakin akan menghapus Barang yang telah dipilih?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.RemoveAt(item.Index);
                }
            }  
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            btncancel.Visible = false;
            btncancel.Enabled = false;
            button1.Enabled = true;
            button1.Text = "ADD";
            button2.Text = "EDIT";
            nobuktiTxt.Enabled = false;
            tanggalTxt.Enabled = false;
            cmbCustomer.Enabled = false;
            cmbWarehouse.Enabled = false;
            cmbTax.Enabled = false;
            chkTax.Enabled = false;
            descriptionTxt.Enabled = false;
            dataGridView1.Enabled = false;
            btncancel.Visible = false;
            btncancel.Enabled = false;
            findBtn.Enabled = true;
            if (idsalesorder.Text == "ID")
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

        private void nopo_TextChanged(object sender, EventArgs e)
        {
            nopo.AutoCompleteCustomSource = picode;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           /* int currentCell = dataGridView1.CurrentCell.ColumnIndex;
            int currentRow = dataGridView1.CurrentRow.Index;
            if (currentCell == 0) {
                auto_complete_harga(dataGridView1.Rows[currentRow].Cells[7].Value.ToString(), currentRow);
            }*/
           /* if (dataGridView1.Columns[e.ColumnIndex].Name == "qty")
            {
                
                
            }*/
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            //ReportDocument reportDoc = new ReportDocument();
            string filePath = Application.StartupPath + "/reporting/BuktiSalesInvoiceTanpaHarga.rpt";
            //reportDoc.Load(filePath);
            //reportDoc.SetParameterValue("salesid", idsalesorder.Text);


            //reportDoc.PrintToPrinter(1, true, 0, 0);


            System.Threading.Thread.Sleep(2000);

            ReportDocument cryRpt = new ReportDocument();
            ReportDocument crpt2 = cryRpt;
            cryRpt.Load(filePath);
            cryRpt.SetParameterValue("idinvoice", idsalesorder.Text);

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

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void persenTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }

        private void persenTxt_ValueChanged(object sender, EventArgs e)
        {// Konversi nilai ke format desimal dengan titik sebagai pemisah desimal
            string value = persenTxt.Value.ToString().Replace('.', ',');

            // Konversi nilai ke tipe data double
            if (double.TryParse(value, out double parsedValue))
            {
                // Pastikan nilai tidak melebihi 100
                if (parsedValue > 100)
                {
                    // Jika melebihi 100, atur nilai kembali menjadi 100
                    persenTxt.Value = 100;
                }
            }
        }
    }
}
