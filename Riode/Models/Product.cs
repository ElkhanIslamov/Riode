﻿namespace Riode.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public int Rating { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastModified { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
