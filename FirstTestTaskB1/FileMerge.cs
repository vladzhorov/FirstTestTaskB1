namespace FirstTestTaskB1
{
    public static class FileMerger
    {
        public static void MergeFiles(string inputDirectory, string outputFile, string filterString)
        {
            var files = Directory.GetFiles(inputDirectory, "*.txt");
            var totalDeleted = 0;

            using (var writer = new StreamWriter(outputFile, false))
            {
                foreach (var file in files)
                {
                    int deletedCount = 0;
                    var lines = File.ReadAllLines(file);
                    foreach (var line in lines)
                    {
                        if (line.Contains(filterString))
                        {
                            deletedCount++;
                        }
                        else
                        {
                            writer.WriteLine(line);
                        }
                    }
                    totalDeleted += deletedCount;
                    Console.WriteLine($"File {file} - Deleted lines: {deletedCount}");
                }
            }

            Console.WriteLine($"Total deleted lines: {totalDeleted}");
        }
    }
}