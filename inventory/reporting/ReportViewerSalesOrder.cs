using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory.reporting
{
    public partial class ReportViewerSalesOrder : Form
    {
        string salesorderid;
        public ReportViewerSalesOrder(string idsalesorder)
        {
            salesorderid = idsalesorder;
            InitializeComponent();
        }

        private void ReportViewerSalesOrder_Load(object sender, EventArgs e)
        {
                     
            //Console.WriteLine("test = " + DefaultPrinterName());
            //Console.WriteLine("test = " + GetPaperSize(DefaultPrinterName(), "vpc"));
            ReportDocument reportDoc = new ReportDocument();

            //string filePath1 = "C:/Users/User/documents/visual studio 2013/Projects/belajarVariabelGlobal/belajarVariabelGlobal/Report/CRKGolRekening.rpt";
            //string filePath1 = "C:/Users/User/documents/visual studio 2013/Projects/belajarVariabelGlobal/belajarVariabelGlobal/Report/CrystalReport1.rpt";

            string filePath = Application.StartupPath + "/reporting/BuktiSalesOrder.rpt";
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();

            //Console.WriteLine("filePath = " + filePath);
            //System.Drawing.Printing.PageSettings pageSettings = new System.Drawing.Printing.PageSettings();
            //pageSettings.PaperSize = new System.Drawing.Printing.PaperSize("name", 400, 600);
            //reportDoc.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)System.Drawing.Printing.PaperKind.A5Rotated;
            //reportDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize;
                        
            reportDoc.Load(filePath);
            reportDoc.SetParameterValue("salesid", salesorderid);



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

        public static string DefaultPrinterName() 
        { 
            string functionReturnValue = null;
            System.Drawing.Printing.PrinterSettings oPS = new System.Drawing.Printing.PrinterSettings(); 

            try { 
                functionReturnValue = oPS.PrinterName; 
            } 
            catch (System.Exception ex) { 
                functionReturnValue = ""; 
            } 
            finally { 
            oPS = null; 
            } 
            return functionReturnValue; 
        }

        public Int32 GetPaperSize(String sPrinterName, String sPaperSizeName)
        {
            PrintDocument docPrintDoc = new PrintDocument();
            docPrintDoc.PrinterSettings.PrinterName = sPrinterName;
            for (int i = 0; i < docPrintDoc.PrinterSettings.PaperSizes.Count; i++)
            {
                int raw = docPrintDoc.PrinterSettings.PaperSizes[i].RawKind;
                if (docPrintDoc.PrinterSettings.PaperSizes[i].PaperName == sPaperSizeName)
                {
                    return raw;
                }
            }
            return 0;
        }
    }

}
