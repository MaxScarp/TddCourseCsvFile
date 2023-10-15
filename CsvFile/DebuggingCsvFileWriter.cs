namespace CsvFile
{
    public class DebuggingCsvFileWriter : ICsvFileWriter
    {
        private ICsvFileWriter csvFileWriter;
        private ICsvFileWriter debugCsvFileWriter;

        public DebuggingCsvFileWriter(ICsvFileWriter csvFileWriter, ICsvFileWriter debugCsvFileWriter)
        {
            this.csvFileWriter = csvFileWriter;
            this.debugCsvFileWriter = debugCsvFileWriter;
        }

        public void Write(string fileName, IEnumerable<Customer> customers)
        {
            csvFileWriter.Write(fileName, customers);

            var baseFileName = Path.GetFileNameWithoutExtension(fileName);
            debugCsvFileWriter.Write($"{baseFileName}-debug.csv", customers);
        }
    }
}
