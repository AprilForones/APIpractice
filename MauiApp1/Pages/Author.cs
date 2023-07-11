using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MauiApp1.Pages
{
    public class Author
    {
        [Key] public int ID { get; set; }

        public string FName { get; set; }
        public string LName { get; set; }

        public string Birthdate { get; set; }

        //public int? BookId { get; set; }

        public ICollection<Book> Books { get; set; }

    }
}
