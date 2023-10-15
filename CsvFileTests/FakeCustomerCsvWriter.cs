using CsvFile;
using NUnit.Framework;

namespace CsvFileTests
{
    public class FakeCustomerCsvFileWriter : ICsvFileWriter
    {
        public List<(string FileName, IEnumerable<Customer> CustomerList)> Calls { get; private set; } = new();

        public void Write(string fileName, IEnumerable<Customer> customers)
        {
            Calls.Add((fileName, customers));
        }

        public void AssertCustomersWereWrittenToFile(string expectedFileName, IEnumerable<Customer> expectedCustomerList)
        {
            var call = Calls.Where(call => call.FileName == expectedFileName);
            Assert.IsTrue(call.Any(), $"No call made for file {expectedFileName}");
            CollectionAssert.AreEquivalent(expectedCustomerList, call.First().CustomerList);
        }
    }
}
