using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIpractice
{
    public class Author
    {
        [Key] public int ID { get; set; }

        public string FName { get; set; }
        public string LName { get; set; }

        public string Birthdate { get; set; }

        [ForeignKey("Book")]
        public int? BookId { get; set; }
        public ICollection<Author> Book { get; set; }
    }
}
