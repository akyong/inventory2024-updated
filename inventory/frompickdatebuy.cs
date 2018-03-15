using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory
{
    public partial class frompickdatebuy : Form
    {
        MDIForm mdi;
        public frompickdatebuy(MDIForm mdinya)
        {
            InitializeComponent();
            mdi = mdinya;
            comboBox1.SelectedIndex = comboBox1.FindStringExact("Semua");
        }

        private void LaporanPembelian_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(reporting.ReportViewerLaporanPembelian))
                {
                    form.Activate();
                    return;
                }
            }
            reporting.ReportViewerLaporanPembelian rv = new reporting.ReportViewerLaporanPembelian(fromTxt.Text, toTxt.Text, comboBox1.Text);
            rv.MdiParent = mdi;
            rv.Show();
        }
    }
}
