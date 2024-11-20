using FirstTestTaskB1.Constants;
using System.Text;

namespace FirstTestTaskB1
{
    public static class FileGenerator
    {
        private static Random _random = new Random();

        // Генерирует несколько файлов с случайными строками
        public static void GenerateRandomFiles(string directory, int fileCount, int linesPerFile)
        {
            Directory.CreateDirectory(directory);

            for (int i = 0; i < fileCount; i++)
            {
                var filePath = Path.Combine(directory, $"file_{i + 1}.txt");
                using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    for (int j = 0; j < linesPerFile; j++)
                    {
                        var line = GenerateRandomLine();  // Генерация строки
                        writer.WriteLine(line);  // Запись в файл
                    }
                }
            }
        }

        // Генерация строки с данными
        private static string GenerateRandomLine()
        {
            var randomDate = GenerateRandomDate();
            var latinString = GenerateRandomString(GeneralConstants.LatinLetters, 10);
            var russianString = GenerateRandomString(GeneralConstants.RussianLetters, 10);
            var randomEvenInt = GenerateRandomEvenInt();
            var randomDouble = GenerateRandomDouble();

            return $"{randomDate}||{latinString}||{russianString}||{randomEvenInt}||{randomDouble:0.00000000}||";
        }

        private static string GenerateRandomDate()
        {
            var startDate = new DateTime(2018, 1, 1);
            var endDate = DateTime.Now;
            var range = endDate - startDate;
            return startDate.AddDays(_random.Next((int)range.TotalDays)).ToString("dd.MM.yyyy");
        }

        private static string GenerateRandomString(string chars, int length)
        {
            return new string(Enumerable.Range(0, length).Select(_ => chars[_random.Next(chars.Length)]).ToArray());
        }

        private static int GenerateRandomEvenInt()
        {
            int randomInt;
            do
            {
                randomInt = _random.Next(1, 100000000);
            } while (randomInt % 2 != 0);  // Повторить, пока не будет четным
            return randomInt;
        }

        private static double GenerateRandomDouble() => _random.NextDouble() * (20 - 1) + 1;
    }
}
