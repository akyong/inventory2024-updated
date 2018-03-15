using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory.searching
{
    public static class SearchReceivedItem
    {

        public static int ShowDialog(string Judul, Hashtable fieldTable)
        {
            int liSize = fieldTable.Count;
            string tablename = " ((RECEIVEDITEM SO LEFT JOIN WAREHOUSE WF ON WF.ID = SO.WAREHOUSE_ID) LEFT JOIN SUPPLIER CS ON CS.ID = SO.SUPPLIER_ID)LEFT OUTER JOIN PAJAK PJ ON PJ.ID = SO.PAJAK_ID ";
            string fieldname = " SO.ID, SO.RECEIVED_NO, SO.RECEIVED_DATE, WF.NAME AS WAREHOUSE, CS.NAME AS SUPPLIER, SO.DESCRIPTION, PJ.NAME AS PAJAK, SO.TOTALAMOUNT, SO.PO_NUMBER ";
            BindingSource bs = new BindingSource();
            string sqlQuery;
            int getSelectedRowDialog = 0;

            Form prompt = new Form();
            prompt.Width = 380;
            prompt.Height = 450;
            prompt.Text = Judul;
            prompt.StartPosition = FormStartPosition.CenterParent;
            prompt.MaximizeBox = false;
            prompt.FormBorderStyle = FormBorderStyle.FixedSingle;
            //System.Drawing.Icon ico = new System.Drawing.Icon("C:\\Users\\vpc2\\Pictures\\icon\\find.ico");
            //prompt.Icon = ico;

            TabControl tabPane = new TabControl() { Left = 0, Top = 0, SizeMode = TabSizeMode.FillToRight };
            TabPage tabPage1 = new TabPage() { Name = "tabPage1", Text = "Search" };
            TabPage tabPage2 = new TabPage() { Name = "tabPage2", Text = "Filter" };

            Label searchKey = new Label() { Left = 20, Top = 25, Width = 80, Text = "Search Key" };
            Label searchText = new Label() { Left = 20, Top = 55, Width = 80, Text = "Search Text" };

            ComboBox searchKeyComboBox = new ComboBox() { Left = 100, Top = 22, Width = 230, DropDownStyle = ComboBoxStyle.DropDownList, DataSource = null };
            TextBox searchTextTxt = new TextBox() { Left = 100, Top = 52, Width = 230 };

            Button okay = new Button() { Left = 200, Top = 380, Text = "OK" };
            Button close = new Button() { Left = 280, Top = 380, Text = "Close" };

            close.Click += (s, e) =>
            {
                prompt.Close();
            };



            /*
             * ADD ITEM COMBOBOX
             */

            searchKeyComboBox.Items.Clear();
            bs.DataSource = fieldTable;
            searchKeyComboBox.DataSource = bs;
            searchKeyComboBox.DisplayMember = "Value";

            //searchKeyComboBox.DataSource = fieldTable;

            /*
             * END OF ADDING ITEM COMBOBOX
             */

            /* 
             * TabControl Properties
             */
            tabPane.Width = prompt.Width - 15;
            tabPane.Height = 125;
            //tabPane.Dock = DockStyle.Fill;
            tabPane.TabPages.Add(tabPage1);
            //tabPane.TabPages.Add(tabPage2);

            /*
             * TabPage1 Properties
             */
            tabPage1.Controls.Add(searchKey);
            tabPage1.Controls.Add(searchText);
            tabPage1.Controls.Add(searchKeyComboBox);
            tabPage1.Controls.Add(searchTextTxt);



            /*
             * TabPage2 Properties
             */
            //Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            //NumericUpDown inputBox = new NumericUpDown() { Left = 50, Top = 50, Width = 400 };
            //Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70 };

            //confirmation.Click += (sender, e) => { prompt.Close(); };

            /*
             * Datagridview Settings
             */

            DataGridView dataGridView2 = new DataGridView() { Top = 130, Left = 5, Width = prompt.Width - 25, Height = prompt.Height - 230 };
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.ReadOnly = true;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            DataGridViewTextBoxColumn coloumID = new DataGridViewTextBoxColumn();
            coloumID.DataPropertyName = "id";
            coloumID.HeaderText = "ID";
            coloumID.Visible = false;

            DataGridViewTextBoxColumn coloumnobuk = new DataGridViewTextBoxColumn();
            coloumnobuk.DataPropertyName = "order_no";
            coloumnobuk.HeaderText = "No. Order";
            coloumnobuk.Width = 80;

            DataGridViewTextBoxColumn coloumDate = new DataGridViewTextBoxColumn();
            coloumDate.DataPropertyName = "order_date";
            coloumDate.HeaderText = "Tanggal";
            coloumDate.DefaultCellStyle.Format = "dd/MM/yyyy";
            coloumDate.Width = 80;

            DataGridViewTextBoxColumn columnFrom = new DataGridViewTextBoxColumn();
            //columnHarga.DefaultCellStyle.Format = "N";
            columnFrom.DataPropertyName = "supplier";
            columnFrom.HeaderText = "Supplier";
            columnFrom.Width = 80;

            DataGridViewTextBoxColumn columnTo = new DataGridViewTextBoxColumn();
            columnTo.DataPropertyName = "warehouse";
            columnTo.HeaderText = "Gudang";
            columnTo.Width = 80;

            DataGridViewTextBoxColumn columnTotal = new DataGridViewTextBoxColumn();
            columnTotal.DefaultCellStyle.Format = "N";
            columnTotal.DataPropertyName = "totalAmount";
            columnTotal.HeaderText = "Total Amount";
            columnTotal.Width = 100;

            DataGridViewTextBoxColumn coloumDescription = new DataGridViewTextBoxColumn();
            coloumDescription.DataPropertyName = "description";
            coloumDescription.HeaderText = "Deskripsi";
            coloumDescription.Width = 100;

            DataGridViewTextBoxColumn coloumOrderNo = new DataGridViewTextBoxColumn();
            coloumOrderNo.DataPropertyName = "po_number";
            coloumOrderNo.HeaderText = "Purchase Order";
            coloumOrderNo.Width = 80;


            //coloumDeleteFlag.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView2.CellDoubleClick += (sender, e) =>
            {
                string row = dataGridView2.Rows[e.RowIndex].ToString();
                getSelectedRowDialog = int.Parse(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                prompt.Close();
            };

            dataGridView2.Columns.Add(coloumID);
            dataGridView2.Columns.Add(coloumnobuk);
            dataGridView2.Columns.Add(coloumDate);
            dataGridView2.Columns.Add(columnFrom);
            dataGridView2.Columns.Add(columnTo);
            dataGridView2.Columns.Add(columnTotal);
            dataGridView2.Columns.Add(coloumDescription);
            dataGridView2.Columns.Add(coloumOrderNo);
            

            dataGridView2.Rows.Clear();

            sqlQuery = "SELECT " + fieldname + " FROM " + tablename + " where SO.delete_flag='N' ORDER BY SO.RECEIVED_DATE  DESC";

            Console.WriteLine("sqy = " + sqlQuery);
            OleDbDataReader list = ResultTable.updateTable(sqlQuery, dataGridView2);

            while (list.Read())
            {
                dataGridView2.Rows.Add(list["ID"], list["RECEIVED_NO"], list["RECEIVED_DATE"], list["SUPPLIER"], list["WAREHOUSE"], list["TOTALAMOUNT"], list["DESCRIPTION"], list["PO_NUMBER"]);
            }

            /*
             * END of DatagridView Properties
             */

            /*
             * TextBox event on change
             */

            searchTextTxt.TextChanged += (s, e) =>
            {
                var itemselected = (DictionaryEntry)searchKeyComboBox.SelectedItem;
                var key = itemselected.Key;
                var value = itemselected.Value;

                sqlQuery = "SELECT " + fieldname + " FROM " + tablename + " WHERE "
                         + key.ToString() + " LIKE '" + searchTextTxt.Text + "%' ";



                OleDbDataReader autocomplete = ResultTable.updateTable(sqlQuery, dataGridView2);

                while (autocomplete.Read())
                {
                    dataGridView2.Rows.Add(autocomplete["ID"], autocomplete["RECEIVED_NO"], autocomplete["RECEIVED_DATE"], autocomplete["SUPPLIER"], autocomplete["WAREHOUSE"], autocomplete["TOTALAMOUNT"], autocomplete["DESCRIPTION"], autocomplete["PO_NUMBER"]);
                }
            };


            okay.Click += (s, e) =>
            {
                int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;
                getSelectedRowDialog = int.Parse(dataGridView2.Rows[selectedrowindex].Cells[0].Value.ToString());
                prompt.Close();
            };

            close.Click += (s, e) =>
            {
                prompt.Close();
            };

            /*
             * Button OK & Cancel
             */




            prompt.Controls.Add(tabPane);
            prompt.Controls.Add(dataGridView2);
            prompt.Controls.Add(okay);
            prompt.Controls.Add(close);
            //prompt.Controls.Add(confirmation);
            //prompt.Controls.Add(textLabel);
            //prompt.Controls.Add(inputBox);
            prompt.ShowDialog();
            //return (int)inputBox.Value;
            return getSelectedRowDialog;
        }
    }
}
