using Microsoft.Win32;
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

namespace inventory
{
    public partial class StockItem : Form
    {
        OleDbDataReader reader;
        string itemid;
        public StockItem(string item_id,string item_name)
        {
            InitializeComponent();
            itemid = item_id;
            lblBarang.Text = item_name;
            refreshList(item_id);
            jumlahStock(item_id);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void StockItem_Load(object sender, EventArgs e)
        {

        }

        private void refreshList(string item_id)//this one
        {
            string query = "SELECT WI.ID AS ID,W.NAME AS WAREHOUSE,WI.QTY_FIRST AS QTY_FIRST, WI.QTY_IN AS QTY_IN, WI.QTY_OUT AS QTY_OUT,(WI.QTY_FIRST + WI.QTY_IN - WI.QTY_OUT) as QTY_FINAL FROM  WAREHOUSE_ITEM WI  LEFT JOIN WAREHOUSE W ON WI.WAREHOUSE_ID = W.ID WHERE WI.ITEM_ID ="+item_id;
            
            //dataGridView1.Rows.Clear();
            Connection.ConnectionClose();
            Connection.ConnectionOpen();
            OleDbCommand cmd = new OleDbCommand(query, Connection.conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable scores = new DataTable();
            da.Fill(scores);
            dataGridView1.DataSource = scores;            

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
                idwarehouseitem.Text = row.Cells["id"].Value.ToString();
                qtyfirstTxt.Text = row.Cells["qty_first"].Value.ToString();
                qtyinTxt.Text = row.Cells["qty_in"].Value.ToString();
                qtyoutTxt.Text = row.Cells["qty_out"].Value.ToString();
                //int qtyAwal = Int32.Parse(row.Cells["qty_first"].Value.ToString());
                //int qtyMasuk = Int32.Parse(row.Cells["qty_in"].Value.ToString());
                //int qtyKeluar = Int32.Parse(row.Cells["qty_out"].Value.ToString());
                //int qtyakhir = qtyAwal + qtyMasuk - qtyKeluar;                
                // etc.
            }
        }

        private void simpanStock()
        {
            string query = "UPDATE WAREHOUSE_ITEM SET qty_first=@qtyfirst, qty_in=@qtyin, qty_out=@qtout WHERE id=" + idwarehouseitem.Text;

            Console.WriteLine("query = "+query);
            Connection.command = new OleDbCommand(query, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.Parameters.AddWithValue("@qtyfirst", qtyfirstTxt.Text);
            Connection.command.Parameters.AddWithValue("@qtyin", qtyinTxt.Text);
            Connection.command.Parameters.AddWithValue("@qtout", qtyoutTxt.Text);
            Connection.command.ExecuteNonQuery();


            MessageBox.Show("Stock Berhasil diperbarui.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void jumlahStock(string item_id)
        {
            string query = "SELECT SUM(WI.QTY_FIRST+WI.QTY_IN-WI.QTY_OUT) as JUMLAH FROM WAREHOUSE_ITEM WI WHERE WI.ITEM_ID=" + item_id;

            Console.WriteLine("query = " + query);
            Connection.command = new OleDbCommand(query, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            reader = Connection.command.ExecuteReader();
            if (reader.Read())
            {
                //String id = Convert.ToString(reader["id"]);
                lblJmlh.Text = reader["JUMLAH"].ToString();
            }

        }
         

        private void button1_Click(object sender, EventArgs e)
        {
            simpanStock();
            refreshList(itemid);
            jumlahStock(itemid);
        }
    }
}
