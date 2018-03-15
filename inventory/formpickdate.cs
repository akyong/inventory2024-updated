using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory
{
    public partial class formpickdate : Form
    {
        OleDbDataReader reader;
        MDIForm mdi;
        public formpickdate(MDIForm mdinya)
        {          
            InitializeComponent();
            mdi = mdinya;
            comboBox1.SelectedIndex = comboBox1.FindStringExact("Semua");
        }

        private void formpickdate_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(reporting.ReportViewerLaporanPenjualan))
                {
                    form.Activate();
                    return;
                }
            }
            reporting.ReportViewerLaporanPenjualan rv = new reporting.ReportViewerLaporanPenjualan(fromTxt.Text, toTxt.Text, comboBox1.Text);
            rv.MdiParent = mdi;
            rv.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = Application.StartupPath + "/reporting/LaporanPenjualan.rpt";

            string from =  fromTxt.Text + " 00:00:00";
            string to = toTxt.Text + " 23:59:59";
            string setatus = comboBox1.Text;
            DateTime datefrom = DateTime.ParseExact(from, "dd/M/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dateto = DateTime.ParseExact(to, "dd/M/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            
            System.Threading.Thread.Sleep(2000);

            ReportDocument cryRpt = new ReportDocument();
            ReportDocument crpt2 = cryRpt;
            cryRpt.Load(filePath);
            cryRpt.SetParameterValue("fromDate", datefrom);
            cryRpt.SetParameterValue("toDate", dateto);
            cryRpt.SetParameterValue("status", setatus);

            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;

            RegistryKey forlogin = Registry.CurrentUser.OpenSubKey("Software\\Logger", true);
            Object server = forlogin.GetValue("server");
            Object db = forlogin.GetValue("database");
            Object user = forlogin.GetValue("user");
            Object pass = forlogin.GetValue("pass");


            crConnectionInfo.ServerName = db.ToString();
            crConnectionInfo.DatabaseName = "";
            crConnectionInfo.UserID = "";
            crConnectionInfo.Password = "";

            CrTables = cryRpt.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
            System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();
            string printerName = cryRpt.PrintOptions.PrinterName.ToString();
            cryRpt.PrintOptions.PrinterName = printerName;
            int iCount = 0;

           
            cryRpt.PrintToPrinter(1, true, 0, 0);
            cryRpt.Refresh();            
        }

        private void toTxt_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void fromTxt_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
