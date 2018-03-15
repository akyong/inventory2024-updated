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
    public partial class SalesInvoice : Form
    {
        OleDbDataReader reader;
        MDIForm mdi;
        public SalesInvoice(MDIForm mdinya)
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

            //TODO fill Customer list
            string queryCustomer = "SELECT * FROM CUSTOMER";
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

            ////TODO fill ITEM list
            //string query = "SELECT * FROM ITEM WHERE DELETE_FLAG = 'N' ORDER BY NAME";
            //dAdapter = new OleDbDataAdapter(query, Connection.conn);

            //DataSet ds1 = new DataSet();
            //dAdapter.Fill(ds1, "Item");
            //(dataGridView1.Columns[0] as DataGridViewComboBoxColumn).DisplayMember = "name";
            //(dataGridView1.Columns[0] as DataGridViewComboBoxColumn).ValueMember = "id";
            //(dataGridView1.Columns[0] as DataGridViewComboBoxColumn).DataSource = ds1.Tables["Item"];
        }

        private string getResult(int x)
        {
            return x.ToString().PadLeft(4, '0');
        }

        private void hapus_sales_invoice_detail(string idsalesorder)
        {
            Connection.ConnectionClose();
            Connection.ConnectionOpen();
            string queryHapusDetail = "DELETE FROM INVOICE_DETAIL WHERE INVOICE_ID=@id ";
            Connection.command = new OleDbCommand(queryHapusDetail, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.Parameters.AddWithValue("@id", idsalesorder);
            Connection.command.ExecuteNonQuery();
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
                if(reader["payment"].ToString() == "Y"){
                     label10.Text = "Sudah Bayar";
                }
                else
                {
                    label10.Text = "Belum Bayar";
                }
               
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

        private void SalesInvoice_Load(object sender, EventArgs e)
        {
            nobuktiTxt.Enabled = false;
            tanggalTxt.Enabled = false;
            cmbCustomer.Enabled = false;
            cmbWarehouse.Enabled = false;
            cmbTax.Enabled = false;
            chkTax.Enabled = false;
            descriptionTxt.Enabled = false;
            dataGridView1.Enabled = false;

            if (idsalesinvoice.Text == "ID")
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
                        string queryCheck = "SELECT SEQ_NUMBER FROM SEQUENCE_STORE WHERE SEQ_CODE='invoice'";
                        Connection.command = new OleDbCommand(queryCheck, Connection.conn);
                        Connection.command.CommandType = CommandType.Text;
                        reader = Connection.command.ExecuteReader();
                        if (reader.Read())
                        {
                            bukti_ke = Int32.Parse(reader["seq_number"].ToString()) + 1;
                        }
                        nobukti = "SI" + string.Concat(string.Concat(string.Concat(hari, bulan), tahun), getResult(bukti_ke));
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
                            int id;
                            string queryInsert = "INSERT INTO INVOICE(invoice_no,invoice_date,customer_id,warehouse_id,pajak_id,description,totalAmount,delete_flag,payment) "
                                            + " VALUES( @invoiceno,@invoicedate,@customer,@warehouse,@pajak,@description,@totalamount,'N','N' )";
                            Connection.command = new OleDbCommand(queryInsert, Connection.conn);
                            Connection.command.CommandType = CommandType.Text;
                            Connection.command.Parameters.AddWithValue("@invoiceno", nobukti);
                            Connection.command.Parameters.Add("@invoicedate", OleDbType.Date).Value = tanggalTxt.Value;
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

                            Connection.command.Parameters.AddWithValue("@description", descriptionTxt.Text);
                            Connection.command.Parameters.AddWithValue("@totalAmount", totalAmount);
                            Connection.command.ExecuteNonQuery();

                            Connection.command.CommandText = "Select @@Identity";
                            id = (int)Connection.command.ExecuteScalar();
                            idsalesinvoice.Text = id.ToString();
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



                                string queryAddToSequence = "UPDATE SEQUENCE_STORE SET SEQ_NUMBER=@INCREMENTNYA WHERE SEQ_CODE='invoice'";
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

                        update_sub_label(idsalesinvoice.Text);

                        string queryAmbilSetelahInsert = "SELECT SO.ITEM_ID AS ITEM, SO.QTY AS QTY,SO.PRICE AS PRICE, SO.DISCOUNT ,SO.DESCRIPTION AS DESCRIPTION, (SO.QTY * SO.PRICE - SO.DISCOUNT) AS SUBTOTAL FROM INVOICE_DETAIL SO WHERE SO.INVOICE_ID=" + idsalesinvoice.Text;


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

                        Connection.ConnectionClose();



                        //refreshList(nilaiyangdicari);
                    }


                    Connection.ConnectionClose();
                }
            }
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
                string queryCheck = "SELECT * FROM INVOICE WHERE invoice_no=@invoiceno and delete_flag='N'";
                Connection.command = new OleDbCommand(queryCheck, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@invoiceno", nobuktiTxt.Text);
                reader = Connection.command.ExecuteReader();
                if (reader.Read())
                {
                    String id = Convert.ToString(reader["id"]);

                    //jika data yang mau saya buat itu belum ada...baru bisa insert baru..
                    if (id == idsalesinvoice.Text)
                    {

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
                            try
                            {
                                hapus_sales_invoice_detail(idsalesinvoice.Text);
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
                                    

                                    string valueDisc = dataGridView1.Rows[rows].Cells[3].Value != null ? dataGridView1.Rows[rows].Cells[3].Value.ToString() : "0";
                                    Double qtynya = Double.Parse(valueQty);
                                    Double harganya = Double.Parse(valueHarga);

                                    Double subtotalnya = (qtynya * harganya) - discountnya;
                                    totalAmount = totalAmount + subtotalnya;
                                   
                                    string valueDesc = "";

                                    if (dataGridView1.Rows[rows].Cells[5].Value != null)
                                    {
                                        valueDesc = dataGridView1.Rows[rows].Cells[5].Value.ToString();
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
                                Console.WriteLine("==============================================================s");
                                Console.WriteLine("totalAmount = " + totalAmount);

                                string query = "UPDATE INVOICE SET invoice_no=@invoiceno, invoice_date=@invoicedate,warehouse_id=@warehouse,customer_id=@customer,pajak_id=@pajak, description=@description,totalAmount=@totalAmount WHERE id=@id";
                                Connection.command = new OleDbCommand(query, Connection.conn);
                                Connection.command.CommandType = CommandType.Text;
                                Connection.command.Parameters.AddWithValue("@invoiceno", nobuktiTxt.Text);
                                Connection.command.Parameters.Add("@invoicedate", OleDbType.Date).Value = tanggalTxt.Value;
                                Connection.command.Parameters.AddWithValue("@warehouse", cmbWarehouse.SelectedValue);
                                Connection.command.Parameters.AddWithValue("@cusomter", cmbCustomer.SelectedValue);
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
                                Connection.command.Parameters.AddWithValue("@id", idsalesinvoice.Text);
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

                        update_sub_label(idsalesinvoice.Text);

                        string queryAmbilSetelahInsert = "SELECT SO.ITEM_ID AS ITEM, SO.QTY AS QTY,SO.PRICE AS PRICE, SO.DISCOUNT , (SO.QTY * SO.PRICE - SO.DISCOUNT) AS SUBTOTAL,SO.DESCRIPTION AS DESCRIPTION FROM INVOICE_DETAIL SO WHERE SO.INVOICE_ID=" + idsalesinvoice.Text;


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

                    }
                }
            }
            Connection.ConnectionClose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Boolean bolehhapus = false;
            DialogResult dialogResult = MessageBox.Show("Apakah anda yakin akan menghapus Invoice ini?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string queryCekDulu = "SELECT * FROM INVOICE WHERE ID=@id ";
                Connection.command = new OleDbCommand(queryCekDulu, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@id", idsalesinvoice.Text);
                reader = Connection.command.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["return_si_id"] != DBNull.Value)
                    {
                        bolehhapus = true; //ini nanti mau diganti relation ke retur atau id retur
                    }
                }

                if (bolehhapus)
                {
                    MessageBox.Show("Invoice tidak bisa dihapus, karena sudah terjadi retur!", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string queryUpdateDelivery = "UPDATE INVOICE SET DO_NUMBER=@donumber WHERE ID=@id ";
                    string queryUpdateSalesOrder = "UPDATE DELIVERYORDER SET INVOICE_ID=@invoice_id WHERE invoice_id=@id ";
                    Connection.command = new OleDbCommand(queryUpdateDelivery, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@donumber", DBNull.Value);
                    Connection.command.Parameters.AddWithValue("@id", idsalesinvoice.Text);
                    Connection.command.ExecuteNonQuery();

                    Connection.command = new OleDbCommand(queryUpdateSalesOrder, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@invoice_id", DBNull.Value);
                    Connection.command.Parameters.AddWithValue("@id", idsalesinvoice.Text);
                    Connection.command.ExecuteNonQuery();

                    string queryHapusDetail = "DELETE FROM INVOICE_DETAIL WHERE INVOICE_ID=@id ";
                    string queryHapusHead = "DELETE FROM INVOICE WHERE ID=@id ";
                    Connection.command = new OleDbCommand(queryHapusDetail, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@id", idsalesinvoice.Text);
                    Connection.command.ExecuteNonQuery();

                    Connection.command = new OleDbCommand(queryHapusHead, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@id", idsalesinvoice.Text);
                    Connection.command.ExecuteNonQuery();

                    MessageBox.Show("Bukti Sales Invoice Berhasil dihapus ", "Delete Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Aksi Penghapusan Sales Invoice Telah dibatalkan!", "Delete Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void findBtn_Click(object sender, EventArgs e)
        {
            Hashtable fieldTable = new Hashtable();
            fieldTable.Add("SO.INVOICE_NO", "No. Delivery");
            fieldTable.Add("SO.DO_NUMBER", "No. Order");

            int promptValue = 0;

            try
            {
               promptValue = SearchSalesInvoice.ShowDialog("Cari Invoice", fieldTable);
            }
            catch (Exception ex)
            {

            }          

            idsalesinvoice.Text = promptValue.ToString();
            update_sub_label(idsalesinvoice.Text);

            string queryAmbilSetelahInsert = "SELECT I.CODE AS CODE,I.NAME AS NAME,SO.QTY AS QTY ,SO.PRICE AS PRICE, SO.DISCOUNT AS DISCOUNT, (SO.QTY * SO.PRICE - SO.DISCOUNT) AS SUBTOTAL, SO.DESCRIPTION AS DESCRIPTION,SO.ITEM_ID AS ID FROM INVOICE_DETAIL SO INNER JOIN ITEM I ON SO.ITEM_ID = I.ID WHERE SO.INVOICE_ID=" + idsalesinvoice.Text;

            OleDbCommand cmd = new OleDbCommand(queryAmbilSetelahInsert, Connection.conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable scores = new DataTable();
            da.Fill(scores);
            dataGridView1.DataSource = scores;

            button2.Enabled = true;
            button3.Enabled = true;
            if (idsalesinvoice.Text != "ID")
            {
                button4.Enabled = true;
                button7.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
                button7.Enabled = false;
            }

            //if (promptValue == 0)
            //{
            //    nobuktiTxt.Enabled = false;
            //    tanggalTxt.Enabled = false;
            //    cmbCustomer.Enabled = false;
            //    cmbWarehouse.Enabled = false;
            //    cmbTax.Enabled = false;
            //    chkTax.Enabled = false;
            //    descriptionTxt.Enabled = false;
            //    dataGridView1.Enabled = false;
            //    idsalesinvoice.Text = "ID";
            //    if (idsalesinvoice.Text == "ID")
            //    {
            //        button2.Enabled = false;
            //        button3.Enabled = false;
            //        button4.Enabled = false;
            //    }
            //    else
            //    {
            //        button4.Enabled = true;
            //    }

            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {
            //        if (!row.IsNewRow)
            //            dataGridView1.Rows.Remove(row);
            //    }
            //}
            //else
            //{
                
            //}
            


            
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
                Console.WriteLine("str = " + str);
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

            try
            {
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
            }
            catch (Exception ex)
            {
                qty = 0;
            }      

            try
            {
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
            }
            catch (Exception ex)
            {
                discount = 0;
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

            Double discountTxt = 0;
            try
            {
                string discountText = dataGridView1.Rows[currentRow].Cells[3].Value.ToString();
                if (discountText == "")
                {
                    dataGridView1[3, currentRow].Value = 0;
                }
                else if (Double.Parse(discountText) < 1)
                {
                    dataGridView1[3, currentRow].Value = 0;
                }
                else
                {
                    string str = discountText != null ? discountText : null;
                    dataGridView1[3, currentRow].Value = Double.Parse(discountText);
                }
                discountTxt = Double.Parse(discountText);

            }
            catch (Exception ex)
            {
                dataGridView1[3, currentRow].Value = 0;
            }


            Double qty = 0;//nilai qty
            Double harga = 0;
            Double discount = discountTxt;

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






            Console.WriteLine("qty = " + qty);
            Console.WriteLine("harga = " + harga);
            Console.WriteLine("discount = " + discount);

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
            reporting.ReportViewerSalesInvoice rv = new reporting.ReportViewerSalesInvoice(idsalesinvoice.Text);
            rv.MdiParent = mdi;
            rv.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string queryUpdateBayar = "UPDATE INVOICE SET PAYMENT=@flagpayment WHERE ID=@id ";
                string flag = "";

                string queryCekDulu = "SELECT * FROM INVOICE WHERE ID=@id ";
                Connection.command = new OleDbCommand(queryCekDulu, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@id", idsalesinvoice.Text);
                reader = Connection.command.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["payment"].ToString() != "Y")
                    {
                        flag = "Y";
                        label10.Text = "Sudah Bayar";
                    }
                    else
                    {
                        flag = "N";
                        label10.Text = "Belum Bayar";
                    }
                    
                }

                Connection.command = new OleDbCommand(queryUpdateBayar, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@flagpayment", flag);
                Connection.command.Parameters.AddWithValue("@id", idsalesinvoice.Text);
                Connection.command.ExecuteNonQuery();

                MessageBox.Show("Status Pembayaran telah diubah ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fungsi Pembayaran Error, Hubungi Developer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
          
            //ReportDocument reportDoc = new ReportDocument();
            string filePath = Application.StartupPath + "/reporting/BuktiSalesInvoice.rpt";
            //string filePath = Application.StartupPath + "/reporting/BuktiSalesInvoice-dinga.rpt";
            //reportDoc.Load(filePath);
            //reportDoc.SetParameterValue("salesid", idsalesorder.Text);


            //reportDoc.PrintToPrinter(1, true, 0, 0);


            System.Threading.Thread.Sleep(2000);

            ReportDocument cryRpt = new ReportDocument();
            ReportDocument crpt2 = cryRpt;
            cryRpt.Load(filePath);
            cryRpt.SetParameterValue("idinvoice", idsalesinvoice.Text);

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

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Apakah anda yakin akan menghapus Invoice ini?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.RemoveAt(item.Index);
                }
            }          
            
        }

        private void label10_TextChanged(object sender, EventArgs e)
        {
            if (label10.Text == "Sudah Bayar")
            {
                label10.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                label10.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ReturInvoice ad = new ReturInvoice(idsalesinvoice.Text, nobuktiTxt.Text,cmbCustomer.Text);
            ad.ShowDialog();
        }

    }
}
