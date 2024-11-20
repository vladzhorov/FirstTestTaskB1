using Dapper;
using FirstTestTaskB1.Constants;
using Npgsql;
using System.Data;

namespace FirstTestTaskB1
{
    public class DataImporter
    {
        private readonly string _connectionString;

        public DataImporter(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void GetSumAndMedian()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var result = connection.QuerySingleOrDefault<(long TotalSum, double? Median)>(SqlQuerieConstants.GetSumAndMedianQuery);

            Console.WriteLine($"Total Sum: {result.TotalSum}");
            Console.WriteLine($"Median: {result.Median ?? 0}");
        }

        public void ImportData(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            int totalLines = lines.Length;  // Общее количество строк в файле
            int importedLines = 0;  // Счетчик импортированных строк

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            foreach (var line in lines)
            {
                // Разделение строки на части с использованием разделителя "||", игнорирование пустых частей
                var parts = line.Split(new[] { "||" }, StringSplitOptions.None)
                                .Where(part => !string.IsNullOrWhiteSpace(part))
                                .ToArray();

                var parameters = new
                {
                    Date = DateTime.ParseExact(parts[0], "dd.MM.yyyy", null),
                    LatinString = parts[1],
                    RussianString = parts[2],
                    EvenInt = int.Parse(parts[3]),
                    DoubleValue = double.Parse(parts[4])
                };

                // Выполнение запроса для импорта данных в базу
                connection.Execute(SqlQuerieConstants.ImportDataQuery, parameters);
                importedLines++;

                Console.WriteLine($"Progress: {importedLines}/{totalLines} lines imported.");
            }
        }
    }
}
