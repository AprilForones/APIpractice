using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIpractice
{
    public class Author
    {
        public Author()
        {
            Books = new List<Book>();
        }
        [Key] public int ID { get; set; }

        public string FName { get; set; } = string.Empty;
        public string LName { get; set; } = string.Empty;

        public string Birthdate { get; set; } = string.Empty; 

        //public int? BookId { get; set; }


        public ICollection<Book> Books { get; set; }



    }
}
