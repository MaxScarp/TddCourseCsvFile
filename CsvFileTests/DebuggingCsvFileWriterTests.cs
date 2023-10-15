using CsvFile;
using NUnit.Framework;

namespace CsvFileTests
{
    [TestFixture]
    public class DebuggingCsvFileWriterTests
    {
        [Test]
        public void TwoCsvsFileWriters()
        {
            // Arrange
            var customers = new CustomerListBuilder().WithCustomers(20).Build();

            var csvFileWriter1 = CreateFakeCustomerCsvFileWriter();
            var csvFileWriter2 = CreateFakeCustomerCsvFileWriter();
            var fileName = "cust.csv";
            var sut = CreateSut(csvFileWriter1, csvFileWriter2);

            // Acts
            sut.Write(fileName, customers);

            // Assert
            csvFileWriter1.AssertCustomersWereWrittenToFile("cust.csv", customers);
            csvFileWriter2.AssertCustomersWereWrittenToFile("cust-debug.csv", customers);
        }

        [Test]
        [Ignore("Only run to test agains the file system")]
        public void RunTheTest()
        {
            var customers = new CustomerListBuilder().WithCustomers(25).Build();
            var productionFileWriter = new DeduplicatingCsvFileWriter(new BatchedCsvFileWriter(new CsvFileWriter(new RealFileSystem()), 15000));
            var debugFileWriter = new BatchedCsvFileWriter(new CsvFileWriter(new RealFileSystem()), 20);

            var sut = CreateSut(productionFileWriter, debugFileWriter);

            sut.Write("test", customers);
        }

        private static DebuggingCsvFileWriter CreateSut(ICsvFileWriter csvFileWriter, ICsvFileWriter debugCsvFileWriter)
        {
            return new DebuggingCsvFileWriter(csvFileWriter, debugCsvFileWriter);
        }

        private static FakeCustomerCsvFileWriter CreateFakeCustomerCsvFileWriter()
        {
            return new FakeCustomerCsvFileWriter();
        }
    }
}
