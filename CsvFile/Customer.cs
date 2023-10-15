namespace CsvFile
{
    public class Customer
    {
        public string Name { get; set; }
        public string ContactNumber { get; set; }

        public override string ToString()
        {
            return string.Join(",", Name, ContactNumber);
        }
    }
}