using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory.searching
{
    public static class ResultTable
    {
        public static OleDbDataReader updateTable(string commandText, DataGridView dataGridView2)
        {
            dataGridView2.Rows.Clear();            
            Connection.ConnectionClose();
            Connection.ConnectionOpen();
            Connection.command = new OleDbCommand(commandText, Connection.conn);          
            OleDbDataReader result = Connection.command.ExecuteReader();
            return result;
        }
    }
}
