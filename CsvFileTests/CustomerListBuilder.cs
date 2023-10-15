using CsvFile;

namespace CsvFileTests
{
    public class CustomerListBuilder
    {
        private const string CUSTOMER_NAMES = "abcdefghijklmnopqrstuvwxyz";

        private int numberOfCustomers;

        public CustomerListBuilder WithCustomers(int numberOfCustomers)
        {
            this.numberOfCustomers = numberOfCustomers;
            return this;
        }

        public List<Customer> Build()
        {
            var customers = new List<Customer>();
            for (int i = 0; i < numberOfCustomers; i++)
            {
                customers.Add(CreateCustomer(CUSTOMER_NAMES.Substring(i, 1), i.ToString()));
            }

            return customers;
        }

        private static Customer CreateCustomer(string name, string contactNumber)
        {
            return new Customer { Name = name, ContactNumber = contactNumber };
        }
    }
}
