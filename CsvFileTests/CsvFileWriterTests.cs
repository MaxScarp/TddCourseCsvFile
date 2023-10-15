using CsvFile;
using NSubstitute;
using NUnit.Framework;

namespace CsvFileTests
{
    // SOLID
    // Single responsability
    // Open/Closed
    // Liskov Substitution
    // Interface Segregation
    // Dependency Inversion
    [TestFixture]
    public class CsvFileWriterTests
    {
        [TestFixture]
        public class Write
        {
            [Test]
            public void GivenNoCustomers_ShouldWriteNoCustomerDataToProvidedFile()
            {
                // Arrange
                IFileSystem fileSystem = CreateMockFileSystem();

                CsvFileWriter sut = CreateSut(fileSystem);

                // Act
                sut.Write("c.csv", new List<Customer> { });

                // Assert
                fileSystem.Received(0).WriteLine("c.csv", Arg.Any<string>());
            }

            [Test]
            public void GivenNull_ShouldWriteNoCustomerDataToProvidedFile()
            {
                // Arrange
                IFileSystem fileSystem = CreateMockFileSystem();

                CsvFileWriter sut = CreateSut(fileSystem);

                // Act
                var ex = Assert.Throws<ArgumentNullException>(() => sut.Write("c.csv", null));

                // Assert
                Assert.AreEqual("customers", ex?.ParamName);
            }

            [Test]
            public void GivenCustomers_ShouldWriteOnlyTheCustomersToTheProvidedFile()
            {
                // Arrange
                Customer customer1 = CreateCustomer("abc", "434");
                Customer customer2 = CreateCustomer("def", "767");

                IFileSystem fileSystem = CreateMockFileSystem();

                CsvFileWriter sut = CreateSut(fileSystem);

                // Act
                sut.Write("cust1.csv", new List<Customer> { customer1, customer2 });

                // Assert
                fileSystem.Received(2).WriteLine("cust1.csv", Arg.Any<string>());
            }

            [Test]
            public void GivenOneCustomer_ShouldWriteCustomerDataAsCsvLineToProvidedFile()
            {
                // Arrange
                Customer customer = CreateCustomer("Brendon Page", "1234555678");

                IFileSystem fileSystem = CreateMockFileSystem();

                CsvFileWriter sut = CreateSut(fileSystem);

                // Act
                sut.Write("customer.csv", new List<Customer> { customer });

                // Assert
                AssertCustomerWasWrittenToFile(fileSystem, "customer.csv", customer);
            }

            [Test]
            public void GivenTwoCustomers_ShouldWriteBothCustomersDataAsCsvLinesToProvidedFile()
            {
                // Arrange
                Customer customer1 = CreateCustomer("Jayd Page", "23456789");
                Customer customer2 = CreateCustomer("Peter Wiles", "789456123");

                IFileSystem fileSystem = CreateMockFileSystem();

                CsvFileWriter sut = CreateSut(fileSystem);

                // Act
                sut.Write("cust.csv", new List<Customer> { customer1, customer2 });

                // Assert
                AssertCustomerWasWrittenToFile(fileSystem, "cust.csv", customer1);
                AssertCustomerWasWrittenToFile(fileSystem, "cust.csv", customer2);
            }

            [Test]
            public void GivenThreeCustomers_ShouldWriteAllCustomersDataAsCsvLinesToProvidedFile()
            {
                // Arrange
                Customer customer1 = CreateCustomer("Mark Pearl", "1234567890");
                Customer customer2 = CreateCustomer("Bob", "5432167890");
                Customer customer3 = CreateCustomer("Sylvain", "0987654321");

                IFileSystem fileSystem = CreateMockFileSystem();

                CsvFileWriter sut = CreateSut(fileSystem);

                // Act
                sut.Write("cust1.csv", new List<Customer> { customer1, customer2, customer3 });

                // Assert
                AssertCustomerWasWrittenToFile(fileSystem, "cust1.csv", customer1);
                AssertCustomerWasWrittenToFile(fileSystem, "cust1.csv", customer2);
                AssertCustomerWasWrittenToFile(fileSystem, "cust1.csv", customer3);
            }
        }

        private static void AssertCustomerWasWrittenToFile(IFileSystem fileSystem, string fileName, Customer customer)
        {
            fileSystem.Received(1).WriteLine(fileName, $"{customer.Name},{customer.ContactNumber}");
        }

        private static CsvFileWriter CreateSut(IFileSystem fileSystem)
        {
            return new CsvFileWriter(fileSystem);
        }

        private static Customer CreateCustomer(string name, string contactNumber)
        {
            return new Customer { Name = name, ContactNumber = contactNumber };
        }

        private static IFileSystem CreateMockFileSystem()
        {
            return Substitute.For<IFileSystem>();
        }
    }
}