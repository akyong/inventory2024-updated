namespace inventory.Market
{
    partial class SalesOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesOrder));
            this.button4 = new System.Windows.Forms.Button();
            this.nobuktiTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.idsalesorder = new System.Windows.Forms.Label();
            this.findBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.descriptionTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbWarehouse = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tanggalTxt = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbTax = new System.Windows.Forms.ComboBox();
            this.chkTax = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.totalLbl = new System.Windows.Forms.Label();
            this.pajakLbl = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.nilaipjklbl = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.nopo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label11 = new System.Windows.Forms.Label();
            this.persenTxt = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.persenTxt)).BeginInit();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(336, 122);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 101;
            this.button4.Text = "SHOW";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // nobuktiTxt
            // 
            this.nobuktiTxt.Location = new System.Drawing.Point(101, 1);
            this.nobuktiTxt.Name = "nobuktiTxt";
            this.nobuktiTxt.Size = new System.Drawing.Size(164, 20);
            this.nobuktiTxt.TabIndex = 100;
            this.nobuktiTxt.Text = "(AUTO)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 99;
            this.label5.Text = "Order No.";
            // 
            // idsalesorder
            // 
            this.idsalesorder.AutoSize = true;
            this.idsalesorder.Location = new System.Drawing.Point(631, 5);
            this.idsalesorder.Name = "idsalesorder";
            this.idsalesorder.Size = new System.Drawing.Size(18, 13);
            this.idsalesorder.TabIndex = 98;
            this.idsalesorder.Text = "ID";
            // 
            // findBtn
            // 
            this.findBtn.Location = new System.Drawing.Point(255, 122);
            this.findBtn.Name = "findBtn";
            this.findBtn.Size = new System.Drawing.Size(75, 23);
            this.findBtn.TabIndex = 97;
            this.findBtn.Text = "FIND";
            this.findBtn.UseVisualStyleBackColor = true;
            this.findBtn.Click += new System.EventHandler(this.findBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 122);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 94;
            this.button1.Text = "ADD";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(174, 122);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 96;
            this.button3.Text = "DELETE";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 122);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 95;
            this.button2.Text = "EDIT";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // descriptionTxt
            // 
            this.descriptionTxt.Location = new System.Drawing.Point(458, 28);
            this.descriptionTxt.Multiline = true;
            this.descriptionTxt.Name = "descriptionTxt";
            this.descriptionTxt.Size = new System.Drawing.Size(284, 41);
            this.descriptionTxt.TabIndex = 93;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(394, 24);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 92;
            this.label4.Text = "Keterangan";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbWarehouse
            // 
            this.cmbWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWarehouse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWarehouse.FormattingEnabled = true;
            this.cmbWarehouse.Location = new System.Drawing.Point(101, 67);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new System.Drawing.Size(275, 28);
            this.cmbWarehouse.TabIndex = 91;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 90;
            this.label3.Text = "Gudang";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 89;
            this.label2.Text = "Customer";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(101, 39);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(275, 28);
            this.cmbCustomer.TabIndex = 88;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 87;
            this.label1.Text = "Tanggal";
            // 
            // tanggalTxt
            // 
            this.tanggalTxt.CustomFormat = "dd/MM/yyyy";
            this.tanggalTxt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tanggalTxt.Location = new System.Drawing.Point(101, 20);
            this.tanggalTxt.Name = "tanggalTxt";
            this.tanggalTxt.Size = new System.Drawing.Size(164, 20);
            this.tanggalTxt.TabIndex = 86;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(394, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 105;
            this.label6.Text = "Pajak";
            // 
            // cmbTax
            // 
            this.cmbTax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTax.FormattingEnabled = true;
            this.cmbTax.Location = new System.Drawing.Point(476, 1);
            this.cmbTax.Name = "cmbTax";
            this.cmbTax.Size = new System.Drawing.Size(149, 21);
            this.cmbTax.TabIndex = 106;
            this.cmbTax.MouseHover += new System.EventHandler(this.cmbTax_MouseHover);
            // 
            // chkTax
            // 
            this.chkTax.AutoSize = true;
            this.chkTax.Location = new System.Drawing.Point(458, 4);
            this.chkTax.Name = "chkTax";
            this.chkTax.Size = new System.Drawing.Size(15, 14);
            this.chkTax.TabIndex = 107;
            this.chkTax.UseVisualStyleBackColor = true;
            this.chkTax.CheckedChanged += new System.EventHandler(this.chkTax_CheckedChanged);
            this.chkTax.MouseHover += new System.EventHandler(this.chkTax_MouseHover);
            // 
            // totalLbl
            // 
            this.totalLbl.AutoSize = true;
            this.totalLbl.Location = new System.Drawing.Point(724, 474);
            this.totalLbl.Name = "totalLbl";
            this.totalLbl.Size = new System.Drawing.Size(37, 13);
            this.totalLbl.TabIndex = 108;
            this.totalLbl.Text = "Total :";
            // 
            // pajakLbl
            // 
            this.pajakLbl.AutoSize = true;
            this.pajakLbl.Location = new System.Drawing.Point(721, 488);
            this.pajakLbl.Name = "pajakLbl";
            this.pajakLbl.Size = new System.Drawing.Size(40, 13);
            this.pajakLbl.TabIndex = 109;
            this.pajakLbl.Text = "Pajak :";
            this.pajakLbl.Visible = false;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(870, 474);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(10, 13);
            this.label9.TabIndex = 110;
            this.label9.Text = ".";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // nilaipjklbl
            // 
            this.nilaipjklbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nilaipjklbl.AutoSize = true;
            this.nilaipjklbl.Location = new System.Drawing.Point(870, 488);
            this.nilaipjklbl.Name = "nilaipjklbl";
            this.nilaipjklbl.Size = new System.Drawing.Size(10, 13);
            this.nilaipjklbl.TabIndex = 111;
            this.nilaipjklbl.Text = ".";
            this.nilaipjklbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.nilaipjklbl.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(692, 501);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 112;
            this.label7.Text = "Grand Total :";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(870, 501);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 13);
            this.label8.TabIndex = 113;
            this.label8.Text = ".";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(498, 122);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 114;
            this.button7.Text = "PRINT";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // btncancel
            // 
            this.btncancel.Location = new System.Drawing.Point(417, 122);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(75, 23);
            this.btncancel.TabIndex = 116;
            this.btncancel.Text = "CANCEL";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Visible = false;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // nopo
            // 
            this.nopo.Location = new System.Drawing.Point(458, 68);
            this.nopo.MaxLength = 50;
            this.nopo.Name = "nopo";
            this.nopo.Size = new System.Drawing.Size(158, 20);
            this.nopo.TabIndex = 175;
            this.nopo.TextChanged += new System.EventHandler(this.nopo_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(394, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 174;
            this.label10.Text = "No. Manual";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Item,
            this.name,
            this.qty,
            this.Price,
            this.Discount,
            this.subtotal,
            this.description,
            this.id});
            this.dataGridView1.Location = new System.Drawing.Point(12, 159);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1061, 279);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // Item
            // 
            this.Item.DataPropertyName = "code";
            this.Item.HeaderText = "Kode Barang";
            this.Item.Name = "Item";
            this.Item.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Item.Width = 150;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "Nama Barang";
            this.name.Name = "name";
            this.name.Width = 250;
            // 
            // qty
            // 
            this.qty.DataPropertyName = "qty";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Format = "N";
            dataGridViewCellStyle1.NullValue = "0.00";
            this.qty.DefaultCellStyle = dataGridViewCellStyle1;
            this.qty.HeaderText = "Qty";
            this.qty.MaxInputLength = 8;
            this.qty.Name = "qty";
            this.qty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.qty.ToolTipText = "Jumlah Barang yang mau diorder";
            this.qty.Width = 80;
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
            this.Discount.Width = 50;
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
            this.subtotal.Width = 150;
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
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 20;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(20, 464);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 16);
            this.label11.TabIndex = 176;
            this.label11.Text = "Discount ";
            // 
            // persenTxt
            // 
            this.persenTxt.Enabled = false;
            this.persenTxt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.persenTxt.Location = new System.Drawing.Point(100, 463);
            this.persenTxt.Name = "persenTxt";
            this.persenTxt.Size = new System.Drawing.Size(82, 20);
            this.persenTxt.TabIndex = 177;
            this.persenTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.persenTxt.ValueChanged += new System.EventHandler(this.persenTxt_ValueChanged);
            this.persenTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.persenTxt_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(182, 464);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 19);
            this.label12.TabIndex = 178;
            this.label12.Text = "%";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(20, 488);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(317, 13);
            this.label13.TabIndex = 179;
            this.label13.Text = "Info: Satuan pemisah desimal adalah Koma (,) buak titik";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // SalesOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 574);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.persenTxt);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.nopo);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nilaipjklbl);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pajakLbl);
            this.Controls.Add(this.totalLbl);
            this.Controls.Add(this.chkTax);
            this.Controls.Add(this.cmbTax);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.nobuktiTxt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.idsalesorder);
            this.Controls.Add(this.findBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.descriptionTxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbWarehouse);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbCustomer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tanggalTxt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SalesOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INVOICE";
            this.Load += new System.EventHandler(this.SalesOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.persenTxt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox nobuktiTxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label idsalesorder;
        private System.Windows.Forms.Button findBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox descriptionTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbWarehouse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker tanggalTxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbTax;
        private System.Windows.Forms.CheckBox chkTax;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label totalLbl;
        private System.Windows.Forms.Label pajakLbl;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label nilaipjklbl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.TextBox nopo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn subtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown persenTxt;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}