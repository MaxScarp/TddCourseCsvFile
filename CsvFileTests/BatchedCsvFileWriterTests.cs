using CsvFile;
using NUnit.Framework;

namespace CsvFileTests
{
    [TestFixture]
    public class BatchedCsvFileWriterTests
    {
        [TestFixture]
        public class Write
        {
            [TestFixture]
            public class SingleFile
            {
                [Test]
                public void TwoCustomers()
                {
                    // Arrange
                    var customers = CreateCustomers(2);

                    var csvFileWriter = CreateFakeCustomerCsvFileWriter();
                    var fileName = "cust.csv";
                    var sut = CreateSut(csvFileWriter);

                    // Act
                    sut.Write(fileName, customers);

                    // Assert
                    Assert.AreEqual(1, csvFileWriter.Calls.Count());
                    csvFileWriter.AssertCustomersWereWrittenToFile("cust1.csv", customers);
                }

                [Test]
                public void FiveCustomers()
                {
                    // Arrange
                    var customers = CreateCustomers(5);

                    var csvFileWriter = CreateFakeCustomerCsvFileWriter();
                    var fileName = "mycustomers.csv";
                    var sut = CreateSut(csvFileWriter);

                    // Act
                    sut.Write(fileName, customers);

                    // Assert
                    Assert.AreEqual(1, csvFileWriter.Calls.Count());
                    csvFileWriter.AssertCustomersWereWrittenToFile("mycustomers1.csv", customers);
                }

                [Test]
                public void TenCustomers()
                {
                    // Arrange
                    var customers = CreateCustomers(10);

                    var csvFileWriter = CreateFakeCustomerCsvFileWriter();
                    var fileName = "mycustomers.csv";
                    var sut = CreateSut(csvFileWriter);

                    // Act
                    sut.Write(fileName, customers);

                    // Assert
                    Assert.AreEqual(1, csvFileWriter.Calls.Count());
                    csvFileWriter.AssertCustomersWereWrittenToFile("mycustomers1.csv", customers);
                }
            }

            [TestFixture]
            public class TwoFiles
            {
                [Test]
                public void TwelveCustomers()
                {
                    // Arrange
                    var customers = CreateCustomers(12);

                    var csvFileWriter = CreateFakeCustomerCsvFileWriter();
                    var fileName = "cst.csv";
                    var sut = CreateSut(csvFileWriter);

                    // Act
                    sut.Write(fileName, customers);

                    // Assert
                    Assert.AreEqual(2, csvFileWriter.Calls.Count());
                    csvFileWriter.AssertCustomersWereWrittenToFile("cst1.csv", customers.Take(10));
                    csvFileWriter.AssertCustomersWereWrittenToFile("cst2.csv", customers.Skip(10).Take(2));
                }

                [Test]
                public void TwentyCustomers()
                {
                    // Arrange
                    var customers = CreateCustomers(20);

                    var csvFileWriter = CreateFakeCustomerCsvFileWriter();
                    var fileName = "testCustomers.csv";
                    var sut = CreateSut(csvFileWriter);

                    // Act
                    sut.Write(fileName, customers);

                    // Assert
                    Assert.AreEqual(2, csvFileWriter.Calls.Count());
                    csvFileWriter.AssertCustomersWereWrittenToFile("testCustomers1.csv", customers.Take(10));
                    csvFileWriter.AssertCustomersWereWrittenToFile("testCustomers2.csv", customers.Skip(10).Take(10));
                }
            }

            [TestFixture]
            public class ThreeFiles
            {
                [Test]
                public void TwentyTwoCustomers()
                {
                    // Arrange
                    var customers = CreateCustomers(22);

                    var csvFileWriter = CreateFakeCustomerCsvFileWriter();
                    var fileName = "testCustomers.csv";
                    var sut = CreateSut(csvFileWriter);

                    // Act
                    sut.Write(fileName, customers);

                    // Assert
                    Assert.AreEqual(3, csvFileWriter.Calls.Count());
                    csvFileWriter.AssertCustomersWereWrittenToFile("testCustomers1.csv", customers.Take(10));
                    csvFileWriter.AssertCustomersWereWrittenToFile("testCustomers2.csv", customers.Skip(10).Take(10));
                    csvFileWriter.AssertCustomersWereWrittenToFile("testCustomers3.csv", customers.Skip(20));
                }
            }

            [TestFixture]
            public class DifferentBatchSize
            {
                [Test]
                public void FourCustomers_WithBatchSizeOfThree_ShouldBeTwoFiles()
                {
                    // Arrange
                    var customers = CreateCustomers(4);
                    var batchSize = 3;

                    var csvFileWriter = CreateFakeCustomerCsvFileWriter();
                    var fileName = "differentbatchsize.csv";
                    var sut = CreateSut(csvFileWriter, batchSize);

                    // Act
                    sut.Write(fileName, customers);

                    // Assert
                    Assert.AreEqual(2, csvFileWriter.Calls.Count());
                    csvFileWriter.AssertCustomersWereWrittenToFile("differentbatchsize1.csv", customers.Take(batchSize));
                    csvFileWriter.AssertCustomersWereWrittenToFile("differentbatchsize2.csv", customers.Skip(batchSize).Take(1));
                }
            }
        }

        private static List<Customer> CreateCustomers(int numCustomers)
        {
            return new CustomerListBuilder().WithCustomers(numCustomers).Build();
        }

        private static BatchedCsvFileWriter CreateSut(FakeCustomerCsvFileWriter csvFileWriter, int batchSize)
        {
            return new BatchedCsvFileWriter(csvFileWriter, batchSize);
        }

        private static BatchedCsvFileWriter CreateSut(FakeCustomerCsvFileWriter csvFileWriter)
        {
            return new BatchedCsvFileWriter(csvFileWriter, 10);
        }

        private static FakeCustomerCsvFileWriter CreateFakeCustomerCsvFileWriter()
        {
            return new FakeCustomerCsvFileWriter();
        }
    }
}
