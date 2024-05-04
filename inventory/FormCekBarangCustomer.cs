using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory.reporting
{
    public partial class history : Form
    {

        MDIForm mdi;
        public history(MDIForm mdinya)
        {
            mdi = mdinya;
            InitializeComponent();
            loadComboBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void history_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'iNVENTORY_BARUDataSet1.invoice_Query' table. You can move, or remove it, as needed.
            this.invoice_QueryTableAdapter.Fill(this.iNVENTORY_BARUDataSet1.invoice_Query);

        }

        private void LoadData(int customerId)
        {
            try
            {
                Connection.ConnectionClose();
                Connection.ConnectionOpen();

                // Buat kueri SQL dengan parameter untuk ID customer
                string query = @"SELECT invoice.invoice_no as [Nomor Nota], 
                                       invoice.invoice_date as [Tgl Nota],
                                       invoice.totalAmount as Jumlah,
                                       item.name AS [Nama Barang], 
                                       invoice_detail.qty, 
                                       invoice.description AS Keterangan, 
                                       invoice_detail.price AS [invoice_detail_price],
                                       invoice_detail.description AS [Keterangan Barang], 
                                       invoice_detail.discount,  
                                       invoice.nopo as [Nota Manual], 
                                       customer.ID
                                FROM   item 
                                       INNER JOIN ((customer 
                                                    INNER JOIN invoice 
                                                            ON customer.[ID] = invoice.[customer_id]) 
                                                   INNER JOIN invoice_detail 
                                                           ON invoice.[ID] = invoice_detail.[invoice_id]) 
                                               ON item.[ID] = invoice_detail.[item_id] 
                                WHERE  customer.ID = @customerId"; // Tambahkan WHERE clause untuk menyaring data berdasarkan ID customer

                // Buat perintah SQL
                OleDbCommand command = new OleDbCommand(query, Connection.conn);
                command.Parameters.AddWithValue("@customerId", customerId); // Tambahkan parameter untuk ID customer

                // Buat adapter data dan dataset
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Tampilkan data dalam DataGridView
                dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Connection.ConnectionClose(); 
            }
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
                LoadData(customerId); // Memanggil metode LoadData dengan ID customer yang dipilih
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
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


            DataTable DT = (DataTable)dataGridView.DataSource;
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
    }
}
