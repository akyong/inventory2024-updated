namespace inventory
{
    partial class Pajak
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
            this.descriptionTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nameTxt = new System.Windows.Forms.TextBox();
            this.codeTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.idgudang = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.findBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.persentaseTxt = new System.Windows.Forms.NumericUpDown();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.persentase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delete_flag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.persentaseTxt)).BeginInit();
            this.SuspendLayout();
            // 
            // descriptionTxt
            // 
            this.descriptionTxt.Location = new System.Drawing.Point(348, 35);
            this.descriptionTxt.Name = "descriptionTxt";
            this.descriptionTxt.Size = new System.Drawing.Size(156, 20);
            this.descriptionTxt.TabIndex = 65;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(283, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 58;
            this.label4.Text = "Deskripsi";
            // 
            // nameTxt
            // 
            this.nameTxt.Location = new System.Drawing.Point(108, 35);
            this.nameTxt.Name = "nameTxt";
            this.nameTxt.Size = new System.Drawing.Size(156, 20);
            this.nameTxt.TabIndex = 57;
            // 
            // codeTxt
            // 
            this.codeTxt.Location = new System.Drawing.Point(108, 16);
            this.codeTxt.Name = "codeTxt";
            this.codeTxt.Size = new System.Drawing.Size(156, 20);
            this.codeTxt.TabIndex = 56;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 55;
            this.label2.Text = "Nama Pajak";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 54;
            this.label1.Text = "Kode Pajak";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(171, 62);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 62;
            this.button3.Text = "DELETE";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(90, 62);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 61;
            this.button2.Text = "EDIT";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 62);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 60;
            this.button1.Text = "ADD";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // idgudang
            // 
            this.idgudang.AutoSize = true;
            this.idgudang.Location = new System.Drawing.Point(335, 67);
            this.idgudang.Name = "idgudang";
            this.idgudang.Size = new System.Drawing.Size(18, 13);
            this.idgudang.TabIndex = 63;
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
            this.persentase,
            this.description,
            this.delete_flag});
            this.dataGridView1.Location = new System.Drawing.Point(12, 97);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(494, 200);
            this.dataGridView1.TabIndex = 59;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // findBtn
            // 
            this.findBtn.Location = new System.Drawing.Point(252, 62);
            this.findBtn.Name = "findBtn";
            this.findBtn.Size = new System.Drawing.Size(75, 23);
            this.findBtn.TabIndex = 64;
            this.findBtn.Text = "FIND";
            this.findBtn.UseVisualStyleBackColor = true;
            this.findBtn.Click += new System.EventHandler(this.findBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(283, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 66;
            this.label3.Text = "Persentase";
            // 
            // persentaseTxt
            // 
            this.persentaseTxt.Location = new System.Drawing.Point(348, 16);
            this.persentaseTxt.Name = "persentaseTxt";
            this.persentaseTxt.Size = new System.Drawing.Size(156, 20);
            this.persentaseTxt.TabIndex = 67;
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
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "Nama";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // persentase
            // 
            this.persentase.DataPropertyName = "persentase";
            this.persentase.HeaderText = "%";
            this.persentase.Name = "persentase";
            this.persentase.ReadOnly = true;
            this.persentase.Width = 30;
            // 
            // description
            // 
            this.description.DataPropertyName = "description";
            this.description.HeaderText = "Deskripsi";
            this.description.Name = "description";
            this.description.ReadOnly = true;
            // 
            // delete_flag
            // 
            this.delete_flag.DataPropertyName = "delete_flag";
            this.delete_flag.HeaderText = "delete_flag";
            this.delete_flag.Name = "delete_flag";
            this.delete_flag.ReadOnly = true;
            this.delete_flag.Visible = false;
            // 
            // Pajak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 309);
            this.Controls.Add(this.persentaseTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.descriptionTxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nameTxt);
            this.Controls.Add(this.codeTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.idgudang);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.findBtn);
            this.Name = "Pajak";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pajak";
            this.Load += new System.EventHandler(this.Pajak_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.persentaseTxt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox descriptionTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nameTxt;
        private System.Windows.Forms.TextBox codeTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label idgudang;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button findBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown persentaseTxt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn persentase;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn delete_flag;
    }
}