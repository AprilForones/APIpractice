using System.ComponentModel.DataAnnotations;

namespace APIpractice
{
    public class Book
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
