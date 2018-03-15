namespace inventory
{
    partial class StockItem
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.warehouse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty_first = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty_in = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty_out = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty_final = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblBarang = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.qtyfirstTxt = new System.Windows.Forms.NumericUpDown();
            this.qtyinTxt = new System.Windows.Forms.NumericUpDown();
            this.qtyoutTxt = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.idwarehouseitem = new System.Windows.Forms.Label();
            this.lblJmlh = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtyfirstTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtyinTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtyoutTxt)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.warehouse,
            this.qty_first,
            this.qty_in,
            this.qty_out,
            this.qty_final});
            this.dataGridView1.Location = new System.Drawing.Point(11, 150);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(354, 144);
            this.dataGridView1.TabIndex = 72;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "id";
            this.ID.HeaderText = "id";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // warehouse
            // 
            this.warehouse.DataPropertyName = "warehouse";
            this.warehouse.HeaderText = "Gudang";
            this.warehouse.Name = "warehouse";
            this.warehouse.ReadOnly = true;
            this.warehouse.Width = 70;
            // 
            // qty_first
            // 
            this.qty_first.DataPropertyName = "qty_first";
            this.qty_first.HeaderText = "Qty Awal";
            this.qty_first.Name = "qty_first";
            this.qty_first.ReadOnly = true;
            this.qty_first.Width = 60;
            // 
            // qty_in
            // 
            this.qty_in.DataPropertyName = "qty_in";
            this.qty_in.HeaderText = "Qty Masuk";
            this.qty_in.Name = "qty_in";
            this.qty_in.ReadOnly = true;
            this.qty_in.Width = 60;
            // 
            // qty_out
            // 
            this.qty_out.DataPropertyName = "qty_out";
            this.qty_out.HeaderText = "Qty Keluar";
            this.qty_out.Name = "qty_out";
            this.qty_out.ReadOnly = true;
            this.qty_out.Width = 60;
            // 
            // qty_final
            // 
            this.qty_final.DataPropertyName = "qty_final";
            this.qty_final.HeaderText = "Qty Akhir";
            this.qty_final.Name = "qty_final";
            this.qty_final.ReadOnly = true;
            this.qty_final.Width = 60;
            // 
            // lblBarang
            // 
            this.lblBarang.AutoSize = true;
            this.lblBarang.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBarang.Location = new System.Drawing.Point(12, 9);
            this.lblBarang.Name = "lblBarang";
            this.lblBarang.Size = new System.Drawing.Size(85, 20);
            this.lblBarang.TabIndex = 73;
            this.lblBarang.Text = "lblBarang";
            this.lblBarang.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 85;
            this.label3.Text = "Qty Keluar";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 81;
            this.label2.Text = "Qty Masuk";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 80;
            this.label4.Text = "Qty Awal";
            // 
            // qtyfirstTxt
            // 
            this.qtyfirstTxt.Location = new System.Drawing.Point(115, 46);
            this.qtyfirstTxt.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.qtyfirstTxt.Name = "qtyfirstTxt";
            this.qtyfirstTxt.Size = new System.Drawing.Size(156, 20);
            this.qtyfirstTxt.TabIndex = 86;
            // 
            // qtyinTxt
            // 
            this.qtyinTxt.Location = new System.Drawing.Point(115, 75);
            this.qtyinTxt.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.qtyinTxt.Name = "qtyinTxt";
            this.qtyinTxt.Size = new System.Drawing.Size(156, 20);
            this.qtyinTxt.TabIndex = 87;
            // 
            // qtyoutTxt
            // 
            this.qtyoutTxt.Location = new System.Drawing.Point(115, 104);
            this.qtyoutTxt.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.qtyoutTxt.Name = "qtyoutTxt";
            this.qtyoutTxt.Size = new System.Drawing.Size(156, 20);
            this.qtyoutTxt.TabIndex = 88;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(277, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 89;
            this.button1.Text = "SAVE";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // idwarehouseitem
            // 
            this.idwarehouseitem.AutoSize = true;
            this.idwarehouseitem.Location = new System.Drawing.Point(293, 33);
            this.idwarehouseitem.Name = "idwarehouseitem";
            this.idwarehouseitem.Size = new System.Drawing.Size(35, 13);
            this.idwarehouseitem.TabIndex = 90;
            this.idwarehouseitem.Text = "label5";
            this.idwarehouseitem.Visible = false;
            // 
            // lblJmlh
            // 
            this.lblJmlh.AutoSize = true;
            this.lblJmlh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJmlh.Location = new System.Drawing.Point(319, 298);
            this.lblJmlh.Name = "lblJmlh";
            this.lblJmlh.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblJmlh.Size = new System.Drawing.Size(46, 17);
            this.lblJmlh.TabIndex = 91;
            this.lblJmlh.Text = "label1";
            this.lblJmlh.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(274, 298);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 92;
            this.label1.Text = "Total :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StockItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 324);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblJmlh);
            this.Controls.Add(this.idwarehouseitem);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.qtyoutTxt);
            this.Controls.Add(this.qtyinTxt);
            this.Controls.Add(this.qtyfirstTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblBarang);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StockItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Barang";
            this.Load += new System.EventHandler(this.StockItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtyfirstTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtyinTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtyoutTxt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn warehouse;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty_first;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty_in;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty_out;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty_final;
        private System.Windows.Forms.Label lblBarang;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown qtyfirstTxt;
        private System.Windows.Forms.NumericUpDown qtyinTxt;
        private System.Windows.Forms.NumericUpDown qtyoutTxt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label idwarehouseitem;
        private System.Windows.Forms.Label lblJmlh;
        private System.Windows.Forms.Label label1;
    }
}