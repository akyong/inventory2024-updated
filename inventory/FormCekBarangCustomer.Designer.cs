
namespace inventory.reporting
{
    partial class history
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
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.iNVENTORY_BARUDataSet1 = new inventory.INVENTORY_BARUDataSet1();
            this.invoiceQueryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.invoice_QueryTableAdapter = new inventory.INVENTORY_BARUDataSet1TableAdapters.invoice_QueryTableAdapter();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.iNVENTORY_BARUDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceQueryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 120;
            this.label2.Text = "Customer";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(113, 17);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(253, 21);
            this.cmbCustomer.TabIndex = 119;
            this.cmbCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbCustomer_SelectedIndexChanged);
            // 
            // iNVENTORY_BARUDataSet1
            // 
            this.iNVENTORY_BARUDataSet1.DataSetName = "INVENTORY_BARUDataSet1";
            this.iNVENTORY_BARUDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // invoiceQueryBindingSource
            // 
            this.invoiceQueryBindingSource.DataMember = "invoice Query";
            this.invoiceQueryBindingSource.DataSource = this.iNVENTORY_BARUDataSet1;
            // 
            // invoice_QueryTableAdapter
            // 
            this.invoice_QueryTableAdapter.ClearBeforeFill = true;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(-4, 58);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(1102, 1003);
            this.dataGridView.TabIndex = 122;
            // 
            // history
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 558);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbCustomer);
            this.Name = "history";
            this.Text = "Cek History Item Pembelian";
            this.Load += new System.EventHandler(this.history_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iNVENTORY_BARUDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceQueryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private INVENTORY_BARUDataSet1 iNVENTORY_BARUDataSet1;
        private System.Windows.Forms.BindingSource invoiceQueryBindingSource;
        private INVENTORY_BARUDataSet1TableAdapters.invoice_QueryTableAdapter invoice_QueryTableAdapter;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}