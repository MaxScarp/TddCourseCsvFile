namespace CsvFile
{
    public class BatchedCsvFileWriter : ICsvFileWriter
    {
        private readonly ICsvFileWriter csvFileWriter;

        private int batchSize;

        public BatchedCsvFileWriter(ICsvFileWriter csvFileWriter, int batchSize)
        {
            this.csvFileWriter = csvFileWriter;
            this.batchSize = batchSize;
        }

        public void Write(string fileName, IEnumerable<Customer> customers)
        {
            var baseFileName = Path.GetFileNameWithoutExtension(fileName);
            var batchCount = 1;
            foreach (var batch in GetBatches(customers))
            {
                csvFileWriter.Write($"{baseFileName}{batchCount}.csv", batch);
                batchCount++;
            }
        }

        private IEnumerable<IEnumerable<Customer>> GetBatches(IEnumerable<Customer> customers)
        {
            IEnumerable<Customer>? batch = customers.Take(batchSize);
            var remainingCustomers = customers.Skip(batchSize);
            while (batch.Any())
            {
                yield return batch;
                batch = remainingCustomers.Take(batchSize);
                remainingCustomers = remainingCustomers.Skip(batchSize);
            }
        }
    }
}
