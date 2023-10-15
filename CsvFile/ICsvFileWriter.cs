namespace CsvFile
{
    public interface ICsvFileWriter
    {
        public void Write(string fileName, IEnumerable<Customer> customers);
    }
}
