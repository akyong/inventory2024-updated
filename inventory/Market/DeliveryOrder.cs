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
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory.Market
{
    public partial class DeliveryOrder : Form
    {
        OleDbDataReader reader;
        MDIForm mdi;
        public DeliveryOrder(MDIForm mdinya)
        {
            InitializeComponent();
            mdi = mdinya;
            loadComboBox();
            dataGridView1.Columns[1].DefaultCellStyle.Format = "N";//dari propertis juga bisa
            dataGridView1.Columns[2].DefaultCellStyle.Format = "N";//dari propertis juga bisa
            dataGridView1.Columns[3].DefaultCellStyle.Format = "N";//dari propertis juga bisa
            dataGridView1.Columns[4].DefaultCellStyle.Format = "N";//dari propertis juga bisa
        }

        private void DeliveryOrder_Load(object sender, EventArgs e)
        {
            nobuktiTxt.Enabled = false;
            tanggalTxt.Enabled = false;
            cmbCustomer.Enabled = false;
            cmbWarehouse.Enabled = false;
            cmbTax.Enabled = false;
            chkTax.Enabled = false;
            descriptionTxt.Enabled = false;
            dataGridView1.Enabled = false;

            if (iddeliveryorder.Text == "ID")
            {
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }
            else
            {
                button4.Enabled = true;
            }
        }

        private string getResult(int x)
        {
            return x.ToString().PadLeft(4, '0');
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

            //TODO fill ITEM list
            string query = "SELECT * FROM ITEM WHERE DELETE_FLAG = 'N'";
            dAdapter = new OleDbDataAdapter(query, Connection.conn);

            DataSet ds1 = new DataSet();
            dAdapter.Fill(ds1, "Item");
            (dataGridView1.Columns[0] as DataGridViewComboBoxColumn).DisplayMember = "name";
            (dataGridView1.Columns[0] as DataGridViewComboBoxColumn).ValueMember = "id";
            (dataGridView1.Columns[0] as DataGridViewComboBoxColumn).DataSource = ds1.Tables["Item"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Delivery Order harus diterbitkan dari Sales Order!!!", "Delivery Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void findBtn_Click(object sender, EventArgs e)
        {
            Hashtable fieldTable = new Hashtable();
            fieldTable.Add("SO.DELIVERY_NO", "No. Delivery");
            fieldTable.Add("SO.SO_NUMBER", "No. Order");
            int promptValue = 0;

            try
            {
                promptValue = SearchDeliveryOrder.ShowDialog("Cari Bukti Delivery", fieldTable);
            }
            catch (Exception ex)
            {

            }
           
           
            iddeliveryorder.Text = promptValue.ToString();


            update_sub_label(iddeliveryorder.Text);

            string queryAmbilSetelahInsert = "SELECT SO.ITEM_ID AS ITEM, SO.QTY AS QTY ,SO.PRICE AS PRICE, SO.DISCOUNT AS DISCOUNT, (SO.QTY * SO.PRICE - SO.DISCOUNT) AS SUBTOTAL, SO.DESCRIPTION AS DESCRIPTION FROM DELIVERYORDER_DETAIL SO WHERE SO.DELIVERYORDER_ID=" + iddeliveryorder.Text;

            OleDbCommand cmd = new OleDbCommand(queryAmbilSetelahInsert, Connection.conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable scores = new DataTable();
            da.Fill(scores);
            dataGridView1.DataSource = scores;

            button2.Enabled = true;
            button3.Enabled = true;
            if (iddeliveryorder.Text != "ID")
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
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
            string queryGet = "SELECT * FROM DELIVERYORDER WHERE id=@id and delete_flag='N'";
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

                nobuktiTxt.Text = reader["delivery_no"].ToString();
                tanggalTxt.Value = Convert.ToDateTime(reader["delivery_date"]);
                cmbCustomer.SelectedValue = reader["customer_id"].ToString();
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

        private void button6_Click(object sender, EventArgs e)
        {
            //FOR DOING EXPORT TO 
            string gudangasal = "";
            Connection.ConnectionClose();
            Connection.ConnectionOpen();
            string queryCariSudahExportAtauBelum = "SELECT * FROM DELIVERYORDER WHERE id=@id AND invoice_id is null";
            Connection.command = new OleDbCommand(queryCariSudahExportAtauBelum, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.Parameters.AddWithValue("@id", iddeliveryorder.Text);           
            reader = Connection.command.ExecuteReader();
            if (reader.Read())
            {
                //CREATE NOMOR BUKTI UNTUK DO
                string nobukti = "";
                String hari = String.Format("{0:dd}", DateTime.Now);
                String bulan = String.Format("{0:MM}", DateTime.Now);
                String tahun = String.Format("{0:yy}", DateTime.Now);
                int bukti_ke = 0;
                int idinvoice = 0;
                string queryCheck = "SELECT SEQ_NUMBER FROM SEQUENCE_STORE WHERE SEQ_CODE='invoice'";
                Connection.command = new OleDbCommand(queryCheck, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                reader = Connection.command.ExecuteReader();
                if (reader.Read())
                {
                    bukti_ke = Int32.Parse(reader["seq_number"].ToString()) + 1;
                }
                nobukti = "SI" + string.Concat(string.Concat(string.Concat(hari, bulan), tahun), getResult(bukti_ke));

                //CEK NOMOR BUKTI UNTUK DO, APAKAH SUDAH DIPAKAI ATAU BELUM
                string query = "SELECT * FROM INVOICE WHERE invoice_no=@invoiceno and delete_flag='N'";
                Connection.command = new OleDbCommand(query, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@invoiceno", nobukti);
                reader = Connection.command.ExecuteReader();
                if (reader.Read())
                {
                    DialogResult avaiable = MessageBox.Show("Invoice No. " + nobukti + " sudah ada", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (avaiable == DialogResult.Yes)
                    {
                        this.Close();
                    }
                    return;
                }
                else
                {
                    //AMBIL SALESORDER HEAD
                    string queryGet = "SELECT * FROM DELIVERYORDER WHERE id=@id and delete_flag='N'";
                    Connection.command = new OleDbCommand(queryGet, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@id", iddeliveryorder.Text);
                    reader = Connection.command.ExecuteReader();
                    if (reader.Read())
                    {
                        //COPAS DAN MASUKAN KE INVOICE BARU
                        string querySet = "INSERT INTO INVOICE(invoice_no,invoice_date,customer_id,warehouse_id,pajak_id,description,totalAmount,delete_flag,do_number,payment,po_number) "
                                                    + " VALUES( @invoiceno,@invoicedate,@customer,@warehouse,@pajak,@description,@totalamount,'N',@donumber,'N',@ponumber)";

                        Connection.command = new OleDbCommand(querySet, Connection.conn);
                        Connection.command.CommandType = CommandType.Text;
                        gudangasal = reader["warehouse_id"].ToString();

                        Connection.command.Parameters.AddWithValue("@invoiceno", nobukti);
                        Connection.command.Parameters.Add("@invoicedate", OleDbType.Date).Value = DateTime.Now;
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
                        Connection.command.Parameters.AddWithValue("@donumber", reader["delivery_no"].ToString());
                        Connection.command.Parameters.AddWithValue("@ponumber", reader["po_number"].ToString());
                        Connection.command.ExecuteNonQuery();

                        Connection.command.CommandText = "Select @@Identity";
                        idinvoice = (int)Connection.command.ExecuteScalar();


                    }
                    //INSERT ALL DELIVERY ORDER DETAIL TO INVOICE DETAIL
                    string queryGetDetail = "SELECT * FROM DELIVERYORDER_DETAIL WHERE DELIVERYORDER_ID=@deliveryid";
                    Connection.command = new OleDbCommand(queryGetDetail, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@deliveryid", iddeliveryorder.Text);
                    reader = Connection.command.ExecuteReader();
                    while (reader.Read())
                    {
                        string querySetDetail = "INSERT INTO INVOICE_DETAIL(invoice_id,item_id,qty,price,discount,description) "
                                             + " VALUES( @invoice,@item,@qty,@price,@discount,@description)";
                        Connection.command = new OleDbCommand(querySetDetail, Connection.conn);
                        Connection.command.CommandType = CommandType.Text;
                        Connection.command.Parameters.AddWithValue("@invoice", idinvoice);
                        Connection.command.Parameters.AddWithValue("@item", reader["item_id"].ToString());
                        Connection.command.Parameters.AddWithValue("@qty", reader["qty"].ToString());
                        Connection.command.Parameters.AddWithValue("@price", reader["price"].ToString());
                        Connection.command.Parameters.AddWithValue("@discount", reader["discount"].ToString());
                        Connection.command.Parameters.AddWithValue("@description", reader["description"].ToString());
                        Connection.command.ExecuteNonQuery();

                        //UpdateStock.itemKeluar(gudangasal, Int32.Parse(reader["qty"].ToString()), reader["item_id"].ToString());
                    }

                    string querySetSudahExport = "UPDATE DELIVERYORDER SET invoice_id=@invoiceid WHERE id=@id";
                    Connection.command = new OleDbCommand(querySetSudahExport, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@invoiceid", idinvoice);
                    Connection.command.Parameters.AddWithValue("@id", iddeliveryorder.Text);
                    Connection.command.ExecuteNonQuery();

                    string queryAddToSequence = "UPDATE SEQUENCE_STORE SET SEQ_NUMBER=@INCREMENTNYA WHERE SEQ_CODE='invoice'";
                    Connection.command = new OleDbCommand(queryAddToSequence, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@INCREMENTNYA", bukti_ke);
                    Connection.command.ExecuteNonQuery();


                    MessageBox.Show("Delivery Order berhasil diterbitkan ke Invoice!", "Terbit Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                MessageBox.Show("Delivery Order ini sudah diterbitkan! \n Atau \n Delivery Order Tidak ditemukan.", "Terbit Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Boolean bolehhapus = false;
            DialogResult dialogResult = MessageBox.Show("Apakah anda yakin akan menghapus Delivery Order ini?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string queryCekDulu = "SELECT * FROM DELIVERYORDER WHERE ID=@id ";
                Connection.command = new OleDbCommand(queryCekDulu, Connection.conn);
                Connection.command.CommandType = CommandType.Text;
                Connection.command.Parameters.AddWithValue("@id", iddeliveryorder.Text);
                reader = Connection.command.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["invoice_id"] != DBNull.Value)
                    {
                        bolehhapus = true;
                    }
                }

                if (bolehhapus)
                {
                    MessageBox.Show("Delivery Order tidak bisa dihapus, karena sudah diterbitkan!", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string queryUpdateDelivery = "UPDATE DELIVERYORDER SET SO_NUMBER=@sonumber WHERE ID=@id ";
                    string queryUpdateSalesOrder = "UPDATE SALESORDER SET DELIVERYORDER_ID=@delivery_id WHERE DELIVERYORDER_ID=@id ";
                    Connection.command = new OleDbCommand(queryUpdateDelivery, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@sonumber", DBNull.Value);
                    Connection.command.Parameters.AddWithValue("@id", iddeliveryorder.Text);
                    Connection.command.ExecuteNonQuery();

                    Connection.command = new OleDbCommand(queryUpdateSalesOrder, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@delivery_id", DBNull.Value);
                    Connection.command.Parameters.AddWithValue("@id", iddeliveryorder.Text);
                    Connection.command.ExecuteNonQuery();

                    string queryGudangnyaApa = "SELECT * FROM DELIVERYORDER WHERE ID=@id ";
                    string queryDeliveryDetail = "SELECT * FROM DELIVERYORDER_DETAIL WHERE DELIVERYORDER_ID=@id ";
                    string gudang = "";
                    Connection.command = new OleDbCommand(queryGudangnyaApa, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@id", iddeliveryorder.Text);
                    reader = Connection.command.ExecuteReader();
                    if (reader.Read())
                    {
                        gudang = reader["warehouse_id"].ToString();
                    }

                    Connection.command = new OleDbCommand(queryDeliveryDetail, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@id", iddeliveryorder.Text);
                    reader = Connection.command.ExecuteReader();
                    while (reader.Read())
                    {
                        UpdateStock.BatalItemKeluar(gudang, Int32.Parse(reader["qty"].ToString()), reader["item_id"].ToString());
                    }

                    string queryHapusDetail = "DELETE FROM DELIVERYORDER_DETAIL WHERE DELIVERYORDER_ID=@id ";
                    string queryHapusHead = "DELETE FROM DELIVERYORDER WHERE ID=@id ";
                    Connection.command = new OleDbCommand(queryHapusDetail, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@id", iddeliveryorder.Text);
                    Connection.command.ExecuteNonQuery();

                    Connection.command = new OleDbCommand(queryHapusHead, Connection.conn);
                    Connection.command.CommandType = CommandType.Text;
                    Connection.command.Parameters.AddWithValue("@id", iddeliveryorder.Text);
                    Connection.command.ExecuteNonQuery();


                    MessageBox.Show("Bukti Delivery Order Telah Berhasil dihapus ", "Delete Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }               
                
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Aksi Penghapusan Delivery Order Telah dibatalkan!", "Delete Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(reporting.ReportViewerDeliveryOrder))
                {
                    form.Activate();
                    return;
                }
            }
            reporting.ReportViewerDeliveryOrder rv = new reporting.ReportViewerDeliveryOrder(iddeliveryorder.Text);
            rv.MdiParent = mdi;
            rv.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Delivery Order harus diterbitkan dari Sales Order!!!", "Delivery Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            string filePath = Application.StartupPath + "/reporting/BuktiDeliveryOrder.rpt";
            System.Threading.Thread.Sleep(2000);

            ReportDocument cryRpt = new ReportDocument();
            ReportDocument crpt2 = cryRpt;
            cryRpt.Load(filePath);
            cryRpt.SetParameterValue("deliveryorderid", iddeliveryorder.Text);

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

        private void button7_Click(object sender, EventArgs e)
        {
            string filePath = Application.StartupPath + "/reporting/BuktiDeliveryOrderKosong.rpt";
            System.Threading.Thread.Sleep(2000);

            ReportDocument cryRpt = new ReportDocument();
            ReportDocument crpt2 = cryRpt;
            cryRpt.Load(filePath);
            cryRpt.SetParameterValue("deliveryorderid", iddeliveryorder.Text);

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
