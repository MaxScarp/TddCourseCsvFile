namespace CsvFile
{
    public class DeduplicatingCsvFileWriter : ICsvFileWriter
    {
        private readonly ICsvFileWriter csvFileWriter;

        public DeduplicatingCsvFileWriter(ICsvFileWriter csvFileWriter)
        {
            this.csvFileWriter = csvFileWriter;
        }

        public void Write(string fileName, IEnumerable<Customer> customers)
        {
            csvFileWriter.Write(fileName, customers.DistinctBy(c => c.Name));
        }
    }
}
