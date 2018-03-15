using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory
{
    public partial class CompanyInfo : Form
    {
        OleDbDataReader reader;
        public CompanyInfo()
        {
            InitializeComponent();
        }

        private void CompanyInfo_Load(object sender, EventArgs e)
        {
            Connection.ConnectionClose();           
            Connection.ConnectionOpen();
            string queryCheck = "SELECT * FROM CIF WHERE ID=1";
            Connection.command = new OleDbCommand(queryCheck, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            reader = Connection.command.ExecuteReader();
            if (reader.Read())
            {
                //String id = Convert.ToString(reader["id"]);
                //jika data yang mau saya buat itu belum ada...baru bisa insert baru..
                textBox1.Text = Convert.ToString(reader["name"]);
                textBox2.Text = Convert.ToString(reader["address"]);
                textBox3.Text = Convert.ToString(reader["phone_no"]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "UPDATE CIF SET name=@name, address=@address,phone_no=@phoneno WHERE id=1";
            Connection.command = new OleDbCommand(query, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.Parameters.AddWithValue("@name", textBox1.Text);
            Connection.command.Parameters.AddWithValue("@address", textBox2.Text);
            Connection.command.Parameters.AddWithValue("@phoneno", textBox3.Text);
            Connection.command.ExecuteNonQuery();

            MessageBox.Show("Informasi Berhasil diperbarui.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
    }
}
