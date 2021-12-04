using System;
namespace Book_Store
{
    class Book
    {
        public int id { get; set; }
        public string title { get; set; }
        public decimal price { get; set; }
        public string author { get; set; }
        public Category category { get; set;}
    }
}