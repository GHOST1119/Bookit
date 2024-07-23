namespace Bookit.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
