using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataLayer_BuisnessLayer
{
    public class DataBase
    {
        static string connectionString = "server=127.0.0.1;port=3306;username=root;password=root;database=lab_subd";
        MySqlConnection connection = new MySqlConnection(connectionString);

        public MySqlConnection getConection()
        {
            return connection;
        }

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        private MySqlCommand command = new MySqlCommand();
        private string sql = "";
        private int count;
        public int CountBD(MySqlConnection mySqlConnection, string sqlCommand)
        {
            openConnection();
            sql = sqlCommand;
            command.Connection = mySqlConnection;
            command.CommandText = sql;
            string countStr = command.ExecuteScalar().ToString();
            count = int.Parse(countStr);
            closeConnection();

            return count;
        }
    }
}
