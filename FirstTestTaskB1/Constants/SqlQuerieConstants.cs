namespace FirstTestTaskB1.Constants
{
    public static class SqlQuerieConstants
    {
        public const string CreateDatabaseQuery = "CREATE DATABASE first_test_task;";
        public const string CreateTableQuery = @"
            CREATE TABLE IF NOT EXISTS Data (
                Id SERIAL PRIMARY KEY,
                Date DATE,
                LatinString VARCHAR(10),
                RussianString VARCHAR(10),
                EvenInt INT,
                DoubleValue DOUBLE PRECISION
            );";
        public const string ImportDataQuery = "INSERT INTO Data (Date, LatinString, RussianString, EvenInt, DoubleValue) VALUES (@Date, @LatinString, @RussianString, @EvenInt, @DoubleValue);";
        public const string GetSumAndMedianQuery = @"
            WITH EvenInts AS (
                SELECT EvenInt
                FROM Data
                WHERE EvenInt IS NOT NULL
            ),
            DoubleValues AS (
                SELECT DoubleValue,
                       ROW_NUMBER() OVER (ORDER BY DoubleValue) AS RowNum,
                       COUNT(*) OVER () AS TotalCount
                FROM Data
                WHERE DoubleValue IS NOT NULL
            )
            SELECT 
                (SELECT SUM(EvenInt) FROM EvenInts) AS TotalSum,
                (SELECT 
                    CASE
                        WHEN TotalCount % 2 = 1 THEN
                            (SELECT DoubleValue FROM DoubleValues WHERE RowNum = (TotalCount / 2 + 1))
                        ELSE
                            (SELECT AVG(DoubleValue) 
                             FROM DoubleValues 
                             WHERE RowNum IN (TotalCount / 2, TotalCount / 2 + 1))
                    END
                ) AS Median
            FROM DoubleValues
            LIMIT 1;";
    }
}
