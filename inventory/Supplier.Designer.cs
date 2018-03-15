namespace inventory
{
    partial class Supplier
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
            this.plafonFlag = new System.Windows.Forms.CheckBox();
            this.plafonTxt = new System.Windows.Forms.TextBox();
            this.cmbGroupSupplier = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.findBtn = new System.Windows.Forms.Button();
            this.addressTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nameTxt = new System.Windows.Forms.TextBox();
            this.codeTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.idgudang = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupSupplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plafon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plafontabFlag = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plafon_flag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GSI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // plafonFlag
            // 
            this.plafonFlag.AutoSize = true;
            this.plafonFlag.Location = new System.Drawing.Point(388, 80);
            this.plafonFlag.Name = "plafonFlag";
            this.plafonFlag.Size = new System.Drawing.Size(15, 14);
            this.plafonFlag.TabIndex = 87;
            this.plafonFlag.UseVisualStyleBackColor = true;
            // 
            // plafonTxt
            // 
            this.plafonTxt.Location = new System.Drawing.Point(409, 77);
            this.plafonTxt.Name = "plafonTxt";
            this.plafonTxt.Size = new System.Drawing.Size(135, 20);
            this.plafonTxt.TabIndex = 86;
            // 
            // cmbGroupSupplier
            // 
            this.cmbGroupSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroupSupplier.FormattingEnabled = true;
            this.cmbGroupSupplier.Location = new System.Drawing.Point(122, 77);
            this.cmbGroupSupplier.Name = "cmbGroupSupplier";
            this.cmbGroupSupplier.Size = new System.Drawing.Size(156, 21);
            this.cmbGroupSupplier.TabIndex = 85;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(319, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 84;
            this.label5.Text = "Plafon";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 83;
            this.label3.Text = "Kelompok Supplier";
            // 
            // findBtn
            // 
            this.findBtn.Location = new System.Drawing.Point(266, 109);
            this.findBtn.Name = "findBtn";
            this.findBtn.Size = new System.Drawing.Size(75, 23);
            this.findBtn.TabIndex = 81;
            this.findBtn.Text = "FIND";
            this.findBtn.UseVisualStyleBackColor = true;
            this.findBtn.Click += new System.EventHandler(this.findBtn_Click);
            // 
            // addressTxt
            // 
            this.addressTxt.Location = new System.Drawing.Point(388, 20);
            this.addressTxt.Multiline = true;
            this.addressTxt.Name = "addressTxt";
            this.addressTxt.Size = new System.Drawing.Size(156, 49);
            this.addressTxt.TabIndex = 82;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(317, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 75;
            this.label4.Text = "Alamat";
            // 
            // nameTxt
            // 
            this.nameTxt.Location = new System.Drawing.Point(122, 49);
            this.nameTxt.Name = "nameTxt";
            this.nameTxt.Size = new System.Drawing.Size(156, 20);
            this.nameTxt.TabIndex = 74;
            // 
            // codeTxt
            // 
            this.codeTxt.Location = new System.Drawing.Point(122, 20);
            this.codeTxt.Name = "codeTxt";
            this.codeTxt.Size = new System.Drawing.Size(156, 20);
            this.codeTxt.TabIndex = 73;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 71;
            this.label1.Text = "Kode Supplier";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 72;
            this.label2.Text = "Nama Supplier";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(104, 109);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 78;
            this.button2.Text = "EDIT";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 109);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 77;
            this.button1.Text = "ADD";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // idgudang
            // 
            this.idgudang.AutoSize = true;
            this.idgudang.Location = new System.Drawing.Point(284, 84);
            this.idgudang.Name = "idgudang";
            this.idgudang.Size = new System.Drawing.Size(18, 13);
            this.idgudang.TabIndex = 80;
            this.idgudang.Text = "ID";
            this.idgudang.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.code,
            this.name,
            this.GroupSupplier,
            this.plafon,
            this.plafontabFlag,
            this.address,
            this.plafon_flag,
            this.GSI});
            this.dataGridView1.Location = new System.Drawing.Point(23, 138);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(542, 282);
            this.dataGridView1.TabIndex = 76;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(185, 109);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 79;
            this.button3.Text = "DELETE";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "id";
            this.ID.HeaderText = "id";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // code
            // 
            this.code.DataPropertyName = "code";
            this.code.HeaderText = "Kode";
            this.code.Name = "code";
            this.code.ReadOnly = true;
            this.code.Width = 80;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "Nama";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // GroupSupplier
            // 
            this.GroupSupplier.DataPropertyName = "groupsupplier";
            this.GroupSupplier.HeaderText = "Kelompok";
            this.GroupSupplier.Name = "GroupSupplier";
            this.GroupSupplier.ReadOnly = true;
            // 
            // plafon
            // 
            this.plafon.DataPropertyName = "plafon";
            this.plafon.HeaderText = "Plafon";
            this.plafon.Name = "plafon";
            this.plafon.ReadOnly = true;
            // 
            // plafontabFlag
            // 
            this.plafontabFlag.DataPropertyName = "plafon_flag";
            this.plafontabFlag.FalseValue = "0";
            this.plafontabFlag.HeaderText = "Flag";
            this.plafontabFlag.Name = "plafontabFlag";
            this.plafontabFlag.ReadOnly = true;
            this.plafontabFlag.TrueValue = "1";
            this.plafontabFlag.Width = 50;
            // 
            // address
            // 
            this.address.DataPropertyName = "address";
            this.address.HeaderText = "Alamat";
            this.address.Name = "address";
            this.address.ReadOnly = true;
            // 
            // plafon_flag
            // 
            this.plafon_flag.DataPropertyName = "plafon_flag";
            this.plafon_flag.HeaderText = "plafonflag";
            this.plafon_flag.Name = "plafon_flag";
            this.plafon_flag.ReadOnly = true;
            this.plafon_flag.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.plafon_flag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.plafon_flag.Visible = false;
            // 
            // GSI
            // 
            this.GSI.DataPropertyName = "GROUPSUPPLIERID";
            this.GSI.HeaderText = "GSI";
            this.GSI.Name = "GSI";
            this.GSI.ReadOnly = true;
            this.GSI.Visible = false;
            // 
            // Supplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 440);
            this.Controls.Add(this.plafonFlag);
            this.Controls.Add(this.plafonTxt);
            this.Controls.Add(this.cmbGroupSupplier);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.findBtn);
            this.Controls.Add(this.addressTxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nameTxt);
            this.Controls.Add(this.codeTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.idgudang);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button3);
            this.Name = "Supplier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Supplier";
            this.Load += new System.EventHandler(this.Supplier_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox plafonFlag;
        private System.Windows.Forms.TextBox plafonTxt;
        private System.Windows.Forms.ComboBox cmbGroupSupplier;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button findBtn;
        private System.Windows.Forms.TextBox addressTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nameTxt;
        private System.Windows.Forms.TextBox codeTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label idgudang;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupSupplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn plafon;
        private System.Windows.Forms.DataGridViewCheckBoxColumn plafontabFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn address;
        private System.Windows.Forms.DataGridViewTextBoxColumn plafon_flag;
        private System.Windows.Forms.DataGridViewTextBoxColumn GSI;
    }
}