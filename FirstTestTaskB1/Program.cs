using FirstTestTaskB1;
using FirstTestTaskB1.Constants;

class Program
{
    static void Main(string[] args)
    {
        var dbCreator = new DatabaseCreator();
        dbCreator.CreateDatabase();
        dbCreator.CreateTables();

        Console.WriteLine("Generating files...");
        FileGenerator.GenerateRandomFiles(GeneralConstants.DataDirectory, 100, 10);

        Console.WriteLine("Merging files...");
        FileMerger.MergeFiles(GeneralConstants.DataDirectory, GeneralConstants.MergedFile, GeneralConstants.FilterString);

        var importer = new DataImporter(GeneralConstants.DatabaseConnectionString);
        Console.WriteLine("Importing data...");
        importer.ImportData(GeneralConstants.MergedFile);

        Console.WriteLine("Executing query...");
        importer.GetSumAndMedian();

        Console.WriteLine("All tasks completed.");
    }
}
