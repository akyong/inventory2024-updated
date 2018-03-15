namespace inventory.Purchase
{
    partial class PurchaseInvoice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseInvoice));
            this.button6 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.nobuktiTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.idsalesinvoice = new System.Windows.Forms.Label();
            this.findBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.descriptionTxt = new System.Windows.Forms.TextBox();
            this.cmbWarehouse = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.nilaipjklbl = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pajakLbl = new System.Windows.Forms.Label();
            this.totalLbl = new System.Windows.Forms.Label();
            this.chkTax = new System.Windows.Forms.CheckBox();
            this.cmbTax = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tanggalTxt = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.nopo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(12, 430);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(97, 23);
            this.button6.TabIndex = 159;
            this.button6.Text = "Bayar";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(520, 436);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 167;
            this.label7.Text = "Grand Total :";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(336, 90);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 158;
            this.button4.Text = "SHOW";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // nobuktiTxt
            // 
            this.nobuktiTxt.Location = new System.Drawing.Point(101, 2);
            this.nobuktiTxt.Name = "nobuktiTxt";
            this.nobuktiTxt.Size = new System.Drawing.Size(164, 20);
            this.nobuktiTxt.TabIndex = 157;
            this.nobuktiTxt.Text = "(AUTO)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 156;
            this.label5.Text = "Order No.";
            // 
            // idsalesinvoice
            // 
            this.idsalesinvoice.AutoSize = true;
            this.idsalesinvoice.Location = new System.Drawing.Point(609, 6);
            this.idsalesinvoice.Name = "idsalesinvoice";
            this.idsalesinvoice.Size = new System.Drawing.Size(18, 13);
            this.idsalesinvoice.TabIndex = 155;
            this.idsalesinvoice.Text = "ID";
            // 
            // findBtn
            // 
            this.findBtn.Location = new System.Drawing.Point(255, 90);
            this.findBtn.Name = "findBtn";
            this.findBtn.Size = new System.Drawing.Size(75, 23);
            this.findBtn.TabIndex = 154;
            this.findBtn.Text = "FIND";
            this.findBtn.UseVisualStyleBackColor = true;
            this.findBtn.Click += new System.EventHandler(this.findBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 151;
            this.button1.Text = "ADD";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(174, 90);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 153;
            this.button3.Text = "DELETE";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 90);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 152;
            this.button2.Text = "EDIT";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // descriptionTxt
            // 
            this.descriptionTxt.Location = new System.Drawing.Point(445, 21);
            this.descriptionTxt.Multiline = true;
            this.descriptionTxt.Name = "descriptionTxt";
            this.descriptionTxt.Size = new System.Drawing.Size(261, 40);
            this.descriptionTxt.TabIndex = 150;
            // 
            // cmbWarehouse
            // 
            this.cmbWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWarehouse.FormattingEnabled = true;
            this.cmbWarehouse.Location = new System.Drawing.Point(101, 60);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new System.Drawing.Size(229, 21);
            this.cmbWarehouse.TabIndex = 148;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 147;
            this.label3.Text = "Gudang";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 146;
            this.label2.Text = "Supplier";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(101, 40);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(229, 21);
            this.cmbCustomer.TabIndex = 145;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 144;
            this.label1.Text = "Tanggal";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(363, 25);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 149;
            this.label4.Text = "Keterangan";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.code,
            this.Item,
            this.qty,
            this.Price,
            this.Discount,
            this.subtotal,
            this.description,
            this.id});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(827, 256);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(764, 436);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 13);
            this.label8.TabIndex = 168;
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nilaipjklbl
            // 
            this.nilaipjklbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nilaipjklbl.AutoSize = true;
            this.nilaipjklbl.Location = new System.Drawing.Point(764, 423);
            this.nilaipjklbl.Name = "nilaipjklbl";
            this.nilaipjklbl.Size = new System.Drawing.Size(0, 13);
            this.nilaipjklbl.TabIndex = 166;
            this.nilaipjklbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.nilaipjklbl.Visible = false;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(764, 409);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 13);
            this.label9.TabIndex = 165;
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pajakLbl
            // 
            this.pajakLbl.AutoSize = true;
            this.pajakLbl.Location = new System.Drawing.Point(549, 423);
            this.pajakLbl.Name = "pajakLbl";
            this.pajakLbl.Size = new System.Drawing.Size(40, 13);
            this.pajakLbl.TabIndex = 164;
            this.pajakLbl.Text = "Pajak :";
            this.pajakLbl.Visible = false;
            // 
            // totalLbl
            // 
            this.totalLbl.AutoSize = true;
            this.totalLbl.Location = new System.Drawing.Point(552, 409);
            this.totalLbl.Name = "totalLbl";
            this.totalLbl.Size = new System.Drawing.Size(37, 13);
            this.totalLbl.TabIndex = 163;
            this.totalLbl.Text = "Total :";
            // 
            // chkTax
            // 
            this.chkTax.AutoSize = true;
            this.chkTax.Location = new System.Drawing.Point(427, 5);
            this.chkTax.Name = "chkTax";
            this.chkTax.Size = new System.Drawing.Size(15, 14);
            this.chkTax.TabIndex = 162;
            this.chkTax.UseVisualStyleBackColor = true;
            this.chkTax.CheckedChanged += new System.EventHandler(this.chkTax_CheckedChanged);
            this.chkTax.MouseHover += new System.EventHandler(this.chkTax_MouseHover);
            // 
            // cmbTax
            // 
            this.cmbTax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTax.FormattingEnabled = true;
            this.cmbTax.Location = new System.Drawing.Point(445, 2);
            this.cmbTax.Name = "cmbTax";
            this.cmbTax.Size = new System.Drawing.Size(158, 21);
            this.cmbTax.TabIndex = 161;
            this.cmbTax.MouseHover += new System.EventHandler(this.cmbTax_MouseHover);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(363, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 160;
            this.label6.Text = "Pajak";
            // 
            // tanggalTxt
            // 
            this.tanggalTxt.CustomFormat = "dd/MM/yyyy";
            this.tanggalTxt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tanggalTxt.Location = new System.Drawing.Point(101, 21);
            this.tanggalTxt.Name = "tanggalTxt";
            this.tanggalTxt.Size = new System.Drawing.Size(164, 20);
            this.tanggalTxt.TabIndex = 143;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Location = new System.Drawing.Point(12, 119);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(831, 260);
            this.panel2.TabIndex = 142;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(417, 90);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 169;
            this.button5.Text = "PRINT";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(12, 390);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(97, 23);
            this.button7.TabIndex = 170;
            this.button7.Text = "HAPUS BARIS";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // btncancel
            // 
            this.btncancel.Location = new System.Drawing.Point(499, 89);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(75, 23);
            this.btncancel.TabIndex = 171;
            this.btncancel.Text = "CANCEL";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.button8_Click);
            // 
            // code
            // 
            this.code.DataPropertyName = "code";
            this.code.HeaderText = "Kode Barang";
            this.code.Name = "code";
            this.code.Width = 220;
            // 
            // Item
            // 
            this.Item.DataPropertyName = "name";
            this.Item.HeaderText = "Nama Barang";
            this.Item.Name = "Item";
            this.Item.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Item.Width = 300;
            // 
            // qty
            // 
            this.qty.DataPropertyName = "qty";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Format = "N";
            dataGridViewCellStyle1.NullValue = "0.00";
            this.qty.DefaultCellStyle = dataGridViewCellStyle1;
            this.qty.HeaderText = "Qty";
            this.qty.MaxInputLength = 4;
            this.qty.Name = "qty";
            this.qty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.qty.ToolTipText = "Jumlah Barang yang mau diorder";
            this.qty.Width = 60;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "price";
            dataGridViewCellStyle2.Format = "N";
            this.Price.DefaultCellStyle = dataGridViewCellStyle2;
            this.Price.HeaderText = "Harga";
            this.Price.MaxInputLength = 10;
            this.Price.Name = "Price";
            // 
            // Discount
            // 
            this.Discount.DataPropertyName = "discount";
            dataGridViewCellStyle3.Format = "N";
            dataGridViewCellStyle3.NullValue = "0.00";
            this.Discount.DefaultCellStyle = dataGridViewCellStyle3;
            this.Discount.HeaderText = "Discount";
            this.Discount.MaxInputLength = 10;
            this.Discount.Name = "Discount";
            // 
            // subtotal
            // 
            this.subtotal.DataPropertyName = "subtotal";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.Format = "N";
            this.subtotal.DefaultCellStyle = dataGridViewCellStyle4;
            this.subtotal.HeaderText = "Sub Total";
            this.subtotal.MaxInputLength = 10;
            this.subtotal.Name = "subtotal";
            this.subtotal.ReadOnly = true;
            // 
            // description
            // 
            this.description.DataPropertyName = "description";
            this.description.HeaderText = "Keterangan";
            this.description.MaxInputLength = 255;
            this.description.Name = "description";
            this.description.Width = 200;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(366, 64);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 172;
            this.label10.Text = "No. Faktur";
            // 
            // nopo
            // 
            this.nopo.Location = new System.Drawing.Point(445, 60);
            this.nopo.MaxLength = 50;
            this.nopo.Name = "nopo";
            this.nopo.Size = new System.Drawing.Size(158, 20);
            this.nopo.TabIndex = 173;
            // 
            // PurchaseInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 465);
            this.Controls.Add(this.nopo);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.nobuktiTxt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.idsalesinvoice);
            this.Controls.Add(this.findBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.descriptionTxt);
            this.Controls.Add(this.cmbWarehouse);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbCustomer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.nilaipjklbl);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pajakLbl);
            this.Controls.Add(this.totalLbl);
            this.Controls.Add(this.chkTax);
            this.Controls.Add(this.cmbTax);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tanggalTxt);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PurchaseInvoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Invoice";
            this.Load += new System.EventHandler(this.PurchaseInvoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox nobuktiTxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label idsalesinvoice;
        private System.Windows.Forms.Button findBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox descriptionTxt;
        private System.Windows.Forms.ComboBox cmbWarehouse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label nilaipjklbl;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label pajakLbl;
        private System.Windows.Forms.Label totalLbl;
        private System.Windows.Forms.CheckBox chkTax;
        private System.Windows.Forms.ComboBox cmbTax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker tanggalTxt;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn subtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox nopo;
    }
}