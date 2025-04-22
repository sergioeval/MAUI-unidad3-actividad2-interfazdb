using System;
using MySql.Data.MySqlClient;

namespace InterfazDb.Helpers
{
    public static class DatabaseModify
    {
        private static readonly string connectionString = "server=localhost;user=root;password=your_password;database=your_db_name";

        public static void ExecuteSql(string sql)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine($"Executed successfully. Rows affected: {rowsAffected}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error executing SQL: " + ex.Message);
                }
            }
        }
    }
}
