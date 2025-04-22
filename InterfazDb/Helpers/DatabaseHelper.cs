using System;
using MySql.Data.MySqlClient;

// TASK-wip-db helper to insert data , execute sqls 
namespace InterfazDb.Helpers
{
    public static class DatabaseModify
    {
        private static readonly string connectionString = "Server=localhost;Port=3306;Database=mydb;User=myuser;Password=alfabeta;";

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
