using MySql.Data.MySqlClient;
using System.Data;

namespace InterfazDb.Helpers
{
    public class DatabaseGetData
    {
        private readonly string _connectionString;

        public DatabaseGetData()
        {
            _connectionString = "Server=localhost;Port=3306;Database=mydb;User=myuser;Password=alfabeta;";
        }

        public DatabaseGetData(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Executes a SELECT query and returns the results as a DataTable
        /// </summary>
        /// <param name="query">The SELECT query to execute</param>
        /// <returns>DataTable containing the query results</returns>
        public async Task<DataTable> ExecuteSelectQueryAsync(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            await Task.Run(() => adapter.Fill(dataTable));
                        }
                        return dataTable;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error executing query: {ex.Message}", ex);
                }
            }
        }

        /// <summary>
        /// Executes a SELECT query with parameters and returns the results as a DataTable
        /// </summary>
        /// <param name="query">The SELECT query to execute</param>
        /// <param name="parameters">Dictionary of parameter names and values</param>
        /// <returns>DataTable containing the query results</returns>
        public async Task<DataTable> ExecuteSelectQueryWithParametersAsync(string query, Dictionary<string, object> parameters)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }

                        DataTable dataTable = new DataTable();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            await Task.Run(() => adapter.Fill(dataTable));
                        }
                        return dataTable;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error executing parameterized query: {ex.Message}", ex);
                }
            }
        }

        /// <summary>
        /// Executes a SELECT query and returns the first column of the first row
        /// </summary>
        /// <param name="query">The SELECT query to execute</param>
        /// <returns>The first column value of the first row</returns>
        public async Task<object> ExecuteScalarQueryAsync(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        return await command.ExecuteScalarAsync();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error executing scalar query: {ex.Message}", ex);
                }
            }
        }
    }
} 