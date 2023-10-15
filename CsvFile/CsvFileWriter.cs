namespace CsvFile
{
    public class CsvFileWriter : ICsvFileWriter
    {
        private readonly IFileSystem fileSystem;

        public CsvFileWriter(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public void Write(string fileName, IEnumerable<Customer> customers)
        {
            if (customers is null) throw new ArgumentNullException(nameof(customers));

            foreach (Customer customer in customers)
            {
                WriteCustomer(fileName, customer);
            }
        }

        private void WriteCustomer(string fileName, Customer customer)
        {
            if (!IsCustomerDataValid(customer)) return;
            fileSystem.WriteLine(fileName, customer.ToString());
        }

        private bool IsCustomerDataValid(Customer customer)
        {
            return !(string.IsNullOrEmpty(customer.Name) || string.IsNullOrEmpty(customer.ContactNumber));
        }
    }
}
