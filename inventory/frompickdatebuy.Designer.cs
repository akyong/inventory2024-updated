namespace inventory
{
    partial class frompickdatebuy
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
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toTxt = new System.Windows.Forms.DateTimePicker();
            this.fromTxt = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 82);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(204, 23);
            this.button1.TabIndex = 129;
            this.button1.Text = "Buka";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 128;
            this.label3.Text = "Jenis";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Semua",
            "Sudah Bayar",
            "Belum Bayar"});
            this.comboBox1.Location = new System.Drawing.Point(73, 47);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(164, 21);
            this.comboBox1.TabIndex = 127;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 126;
            this.label2.Text = "Sampai";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 125;
            this.label1.Text = "Dari";
            // 
            // toTxt
            // 
            this.toTxt.CustomFormat = "dd/MM/yyyy";
            this.toTxt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toTxt.Location = new System.Drawing.Point(243, 21);
            this.toTxt.Name = "toTxt";
            this.toTxt.Size = new System.Drawing.Size(103, 20);
            this.toTxt.TabIndex = 124;
            // 
            // fromTxt
            // 
            this.fromTxt.CustomFormat = "dd/MM/yyyy";
            this.fromTxt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromTxt.Location = new System.Drawing.Point(73, 21);
            this.fromTxt.Name = "fromTxt";
            this.fromTxt.Size = new System.Drawing.Size(101, 20);
            this.fromTxt.TabIndex = 123;
            // 
            // frompickdatebuy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 127);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toTxt);
            this.Controls.Add(this.fromTxt);
            this.Name = "frompickdatebuy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Laporan Pembelian";
            this.Load += new System.EventHandler(this.LaporanPembelian_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker toTxt;
        private System.Windows.Forms.DateTimePicker fromTxt;
    }
}