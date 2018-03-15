using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory
{
    public static class UpdateStock
    {

        public static void UpdateNilaiStock(string idgudangasal, string idgudangtujuan, int jumlahstock, string itemid)
        {
            string queryKurangStock = "UPDATE WAREHOUSE_ITEM SET QTY_OUT= [QTY_OUT]+" + jumlahstock + " WHERE ITEM_ID =" + itemid + " AND WAREHOUSE_ID=" + idgudangasal;
            string queryTambahStock = "UPDATE WAREHOUSE_ITEM SET QTY_IN= [QTY_IN]+" + jumlahstock + " WHERE ITEM_ID =" + itemid + " AND WAREHOUSE_ID=" + idgudangtujuan;
            Connection.command = new OleDbCommand(queryKurangStock, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.ExecuteNonQuery();

            Connection.command = new OleDbCommand(queryTambahStock, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.ExecuteNonQuery();
        }

        public static void ReverseUpdateNilaiStock(string idgudangasal, string idgudangtujuan, int jumlahstock, string itemid)//dipakai pas update data
        {
            string queryTambahStock = "UPDATE WAREHOUSE_ITEM SET QTY_OUT= [QTY_OUT]-" + jumlahstock + " WHERE ITEM_ID =" + itemid + " AND WAREHOUSE_ID=" + idgudangasal;
            string queryKurangStock = "UPDATE WAREHOUSE_ITEM SET QTY_IN= [QTY_IN]-" + jumlahstock + " WHERE ITEM_ID =" + itemid + " AND WAREHOUSE_ID=" + idgudangtujuan;
            Console.WriteLine("queryTambahStock = " + queryTambahStock);
            Console.WriteLine("queryKurangStock = " + queryKurangStock);

            Connection.command = new OleDbCommand(queryKurangStock, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.ExecuteNonQuery();

            Connection.command = new OleDbCommand(queryTambahStock, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.ExecuteNonQuery();
        }

        public static void itemKeluar(string idgudangasal, int jumlahstock, string itemid)
        {
            string querStockKeluar = "UPDATE WAREHOUSE_ITEM SET QTY_OUT= [QTY_OUT]+" + jumlahstock + " WHERE ITEM_ID =" + itemid + " AND WAREHOUSE_ID=" + idgudangasal;
            Connection.command = new OleDbCommand(querStockKeluar, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.ExecuteNonQuery();                    
        }

        public static void BatalItemKeluar(string idgudangasal, int jumlahstock, string itemid)
        {
            string querStockKeluar = "UPDATE WAREHOUSE_ITEM SET QTY_OUT= [QTY_OUT]-" + jumlahstock + " WHERE ITEM_ID =" + itemid + " AND WAREHOUSE_ID=" + idgudangasal;
            Connection.command = new OleDbCommand(querStockKeluar, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.ExecuteNonQuery();
        }

        public static void itemMasuk(string idgudangasal, int jumlahstock, string itemid)
        {
            string querStockKeluar = "UPDATE WAREHOUSE_ITEM SET QTY_IN= [QTY_IN]+" + jumlahstock + " WHERE ITEM_ID =" + itemid + " AND WAREHOUSE_ID=" + idgudangasal;
            Connection.command = new OleDbCommand(querStockKeluar, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.ExecuteNonQuery();
        }

        public static void BatalItemMasuk(string idgudangasal, int jumlahstock, string itemid)
        {
            string querStockKeluar = "UPDATE WAREHOUSE_ITEM SET QTY_IN= [QTY_IN]-" + jumlahstock + " WHERE ITEM_ID =" + itemid + " AND WAREHOUSE_ID=" + idgudangasal;
            Connection.command = new OleDbCommand(querStockKeluar, Connection.conn);
            Connection.command.CommandType = CommandType.Text;
            Connection.command.ExecuteNonQuery();
        }
    }
}
