using FirstTestTaskB1.Constants;
using Npgsql;

namespace FirstTestTaskB1
{
    public class DatabaseCreator
    {
        public void CreateDatabase()
        {
            using (var conn = new NpgsqlConnection(GeneralConstants.MasterConnectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(SqlQuerieConstants.CreateDatabaseQuery, conn))
                {
                    try
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Database 'first_test_task' created successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Database already exists or error: " + ex.Message);
                    }
                }
            }
        }

        public void CreateTables()
        {
            using (var conn = new NpgsqlConnection(GeneralConstants.DatabaseConnectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(SqlQuerieConstants.CreateTableQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Table 'Data' created successfully.");
                }
            }
        }
    }
}