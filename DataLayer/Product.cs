namespace DataLayer
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double UnitPrice { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
