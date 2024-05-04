using inventory.Market;
using inventory.Purchase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using inventory.reporting;

namespace inventory
{
    public partial class MDIForm : Form
    {
        private int childFormNumber = 0;

        public MDIForm(string jabatan)
        {
            InitializeComponent();

            if (jabatan == "karyawan")
            {
                usersToolStripMenuItem.Visible = false;
            }
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MDIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }

        private void gudangToolStripMenuItem_Click(object sender, EventArgs e)
        {

            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Gudang))
                {
                    form.Activate();
                    return;
                }
            }

            Gudang gudang = new Gudang();
            gudang.MdiParent = this;
            gudang.Show();
        }

        private void kelompokBarangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(GroupItem))
                {
                    form.Activate();
                    return;
                }
            }

            GroupItem groupitem = new GroupItem();
            groupitem.MdiParent = this;
            groupitem.Show();
        }

        private void satuanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Unit))
                {
                    form.Activate();
                    return;
                }
            }

            Unit satuan = new Unit();
            satuan.MdiParent = this;
            satuan.Show();
        }

        private void kategoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Category))
                {
                    form.Activate();
                    return;
                }
            }

            Category category = new Category();
            category.MdiParent = this;
            category.Show();
        }

        private void barangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Item))
                {
                    form.Activate();
                    return;
                }
            }

            Item barang = new Item();
            barang.MdiParent = this;
            barang.Show();
        }

        private void pindahGudangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(StockChange))
                {
                    form.Activate();
                    return;
                }
            }

            StockChange pindahgudang = new StockChange(this);
            pindahgudang.MdiParent = this;
            pindahgudang.Show();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(User))
                {
                    form.Activate();
                    return;
                }
            }

            User users = new User();
            users.MdiParent = this;
            users.Show();
        }

        private void MDIForm_Load(object sender, EventArgs e)
        {

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(CompanyInfo))
                {
                    form.Activate();
                    return;
                }
            }

            CompanyInfo ci = new CompanyInfo();
            ci.MdiParent = this;
            ci.Show();
        }

        private void purchaseInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void purchaseInvoiceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(PurchaseInvoice))
                {
                    form.Activate();
                    return;
                }
            }

            PurchaseInvoice pi = new PurchaseInvoice(this);
            pi.MdiParent = this;
            pi.Show();
        }

        private void purchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(PurchaseOrder))
                {
                    form.Activate();
                    return;
                }
            }

            PurchaseOrder po = new PurchaseOrder(this);
            po.MdiParent = this;
            po.Show();
        }

        private void salesOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(SalesOrder))
                {
                    form.Activate();
                    return;
                }
            }

            SalesOrder so = new SalesOrder(this);
            so.MdiParent = this;
            so.Show();
        }

        private void kelompokCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(KelompokCustomer))
                {
                    form.Activate();
                    return;
                }
            }

            KelompokCustomer gc = new KelompokCustomer();
            gc.MdiParent = this;
            gc.Show();
        }

        private void kelompokSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(KelompokSupplier))
                {
                    form.Activate();
                    return;
                }
            }

            KelompokSupplier gs = new KelompokSupplier();
            gs.MdiParent = this;
            gs.Show();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Customer))
                {
                    form.Activate();
                    return;
                }
            }

            Customer cs = new Customer();
            cs.MdiParent = this;
            cs.Show();
        }

        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Supplier))
                {
                    form.Activate();
                    return;
                }
            }

            Supplier cs = new Supplier();
            cs.MdiParent = this;
            cs.Show();
        }

        private void pajakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Pajak))
                {
                    form.Activate();
                    return;
                }
            }

            Pajak tax = new Pajak();
            tax.MdiParent = this;
            tax.Show();
        }

        private void deliveryOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(DeliveryOrder))
                {
                    form.Activate();
                    return;
                }
            }

            DeliveryOrder delivery = new DeliveryOrder(this);
            delivery.MdiParent = this;
            delivery.Show();
        }

        private void salesInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(SalesInvoice))
                {
                    form.Activate();
                    return;
                }
            }

            SalesInvoice si = new SalesInvoice(this);
            si.MdiParent = this;
            si.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(SalesInvoice))
                {
                    form.Activate();
                    return;
                }
            }

            SalesInvoice si = new SalesInvoice(this);
            si.MdiParent = this;
            si.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(DeliveryOrder))
                {
                    form.Activate();
                    return;
                }
            }

            DeliveryOrder delivery = new DeliveryOrder(this);
            delivery.MdiParent = this;
            delivery.Show();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(SalesOrder))
                {
                    form.Activate();
                    return;
                }
            }

            SalesOrder so = new SalesOrder(this);
            so.MdiParent = this;
            so.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(StockChange))
                {
                    form.Activate();
                    return;
                }
            }

            StockChange pindahgudang = new StockChange(this);
            pindahgudang.MdiParent = this;
            pindahgudang.Show();
        }

        private void toolTip_Popup(object sender, PopupEventArgs e)
        {

        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Item))
                {
                    form.Activate();
                    return;
                }
            }

            Item barang = new Item();
            barang.MdiParent = this;
            barang.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(GroupItem))
                {
                    form.Activate();
                    return;
                }
            }

            GroupItem groupitem = new GroupItem();
            groupitem.MdiParent = this;
            groupitem.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Unit))
                {
                    form.Activate();
                    return;
                }
            }

            Unit satuan = new Unit();
            satuan.MdiParent = this;
            satuan.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Gudang))
                {
                    form.Activate();
                    return;
                }
            }

            Gudang gudang = new Gudang();
            gudang.MdiParent = this;
            gudang.Show();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Category))
                {
                    form.Activate();
                    return;
                }
            }

            Category category = new Category();
            category.MdiParent = this;
            category.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(DesignFor))
                {
                    form.Activate();
                    return;
                }
            }

            DesignFor po = new DesignFor(this);
            po.MdiParent = this;
            po.Show();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(PurchaseOrder))
                {
                    form.Activate();
                    return;
                }
            }

            PurchaseOrder po = new PurchaseOrder(this);
            po.MdiParent = this;
            po.Show();
        }

        private void receivedItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ReceivedItem))
                {
                    form.Activate();
                    return;
                }
            }

            ReceivedItem ri = new ReceivedItem(this);
            ri.MdiParent = this;
            ri.Show();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ReceivedItem))
                {
                    form.Activate();
                    return;
                }
            }

            ReceivedItem ri = new ReceivedItem(this);
            ri.MdiParent = this;
            ri.Show();
        }

        private void penjualanToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(formpickdate))
                {
                    form.Activate();
                    return;
                }
            }

            formpickdate ri = new formpickdate(this);
            ri.MdiParent = this;
            ri.Show();
        }

        private void pembelianToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(frompickdatebuy))
                {
                    form.Activate();
                    return;
                }
            }

            frompickdatebuy ri = new frompickdatebuy(this);
            ri.MdiParent = this;
            ri.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void cekHistoryItemPembelianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(history))
                {
                    form.Activate();
                    return;
                }
            }

            history so = new history(this);
            so.MdiParent = this;
            so.Show();
        }
    }
}
