namespace Riode.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public double Price { get; set; }

        public int Rating { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
