using CsvFile;
using NUnit.Framework;

namespace CsvFileTests
{
    [TestFixture]
    public class DeduplicatingCsvFileWriterTests
    {
        [TestFixture]
        public class Write
        {
            [TestCase(2)]
            [TestCase(3)]
            [TestCase(20)]
            public void UniqueCustomers(int numCustomers)
            {
                // Arrange
                var customers = CreateUniqueCustomers(numCustomers);

                var csvFileWriter = CreateFakeCustomerCsvFileWriter();
                var fileName = "cust.csv";
                var sut = CreateSut(csvFileWriter);

                // Act
                sut.Write(fileName, customers);

                // Assert
                csvFileWriter.AssertCustomersWereWrittenToFile("cust.csv", customers);
            }

            [Test]
            public void TwoDuplicateCustomers()
            {
                // Arrange
                var customerBob1 = new Customer { Name = "Bob", ContactNumber = "123" };
                var customerBob2 = new Customer { Name = "Bob", ContactNumber = "456" };

                var customers = new List<Customer>
                {
                    customerBob1,
                    customerBob2
                };

                var csvFileWriter = CreateFakeCustomerCsvFileWriter();
                var fileName = "cust.csv";
                var sut = CreateSut(csvFileWriter);

                // Act
                sut.Write(fileName, customers);

                // Assert
                csvFileWriter.AssertCustomersWereWrittenToFile("cust.csv", new List<Customer> { customerBob1 });
            }

            [Test]
            public void MultipleDuplicateCustomers()
            {
                // Arrange
                var customerBob1 = new Customer { Name = "Bob", ContactNumber = "123" };
                var customerBob2 = new Customer { Name = "Bob", ContactNumber = "456" };
                var customerPete1 = new Customer { Name = "Pete", ContactNumber = "23456" };
                var customerPete2 = new Customer { Name = "Pete", ContactNumber = "855" };

                List<Customer> uniqueCustomers = CreateUniqueCustomers(3);
                var customers = new List<Customer>
                {
                    customerBob1,
                    customerBob2,
                    customerPete1,
                    customerPete2
                };
                customers.AddRange(uniqueCustomers);

                var expectedCustomers = new List<Customer> { customerBob1, customerPete1 };
                expectedCustomers.AddRange(uniqueCustomers);

                var csvFileWriter = CreateFakeCustomerCsvFileWriter();
                var fileName = "cust.csv";
                var sut = CreateSut(csvFileWriter);

                // Act
                sut.Write(fileName, customers);

                // Assert
                csvFileWriter.AssertCustomersWereWrittenToFile("cust.csv", expectedCustomers);
            }
        }

        private static DeduplicatingCsvFileWriter CreateSut(FakeCustomerCsvFileWriter csvFileWriter)
        {
            return new DeduplicatingCsvFileWriter(csvFileWriter);
        }

        private static FakeCustomerCsvFileWriter CreateFakeCustomerCsvFileWriter()
        {
            return new FakeCustomerCsvFileWriter();
        }

        private static List<Customer> CreateUniqueCustomers(int numCustomers)
        {
            return new CustomerListBuilder().WithCustomers(numCustomers).Build();
        }
    }
}
