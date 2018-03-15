using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory.reporting
{
    public partial class ReportViewerPurchaseInvoice : Form
    {
        string idinvoice;
        public ReportViewerPurchaseInvoice(string invoiceid)
        {
            idinvoice = invoiceid;
            InitializeComponent();
        }

        private void ReportViewerPurchaseInvoice_Load(object sender, EventArgs e)
        {
            ReportDocument reportDoc = new ReportDocument();

            //string filePath1 = "C:/Users/User/documents/visual studio 2013/Projects/belajarVariabelGlobal/belajarVariabelGlobal/Report/CRKGolRekening.rpt";
            //string filePath1 = "C:/Users/User/documents/visual studio 2013/Projects/belajarVariabelGlobal/belajarVariabelGlobal/Report/CrystalReport1.rpt";

            string filePath = Application.StartupPath + "/reporting/BuktiPurchaseInvoice.rpt";
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();

            //Console.WriteLine("filePath = " + filePath);
            reportDoc.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)System.Drawing.Printing.PaperKind.A5Rotated;
            reportDoc.Load(filePath);
            reportDoc.SetParameterValue("idinvoice", idinvoice);



            RegistryKey forlogin = Registry.CurrentUser.OpenSubKey("Software\\Logger", true);
            Object server = forlogin.GetValue("server");
            Object db = forlogin.GetValue("database");
            Object user = forlogin.GetValue("user");
            Object pass = forlogin.GetValue("pass");


            crConnectionInfo.ServerName = db.ToString();
            crConnectionInfo.DatabaseName = "";
            crConnectionInfo.UserID = "";
            crConnectionInfo.Password = "";
            //crConnectionInfo.IntegratedSecurity = true;


            Tables CrTables = reportDoc.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
            crystalReportViewer1.ShowLogo = false;
            crystalReportViewer1.ShowParameterPanelButton = false;
            crystalReportViewer1.ShowGroupTreeButton = false;
            crystalReportViewer1.ShowRefreshButton = false;
            crystalReportViewer1.ShowPrintButton = false;
            crystalReportViewer1.ReportSource = reportDoc;
            crystalReportViewer1.Refresh();
        }
    }
}
