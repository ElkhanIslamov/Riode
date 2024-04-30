namespace Riode.Models
{
    public class BasketModel
    {
        public int Id { get; set; }
        public int  ProductId  { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
