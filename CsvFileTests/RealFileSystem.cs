using CsvFile;

namespace CsvFileTests
{
    public class RealFileSystem : IFileSystem
    {
        public void WriteLine(string fileName, string line)
        {
            File.AppendAllLines(fileName, new List<string> { line });
        }
    }
}
