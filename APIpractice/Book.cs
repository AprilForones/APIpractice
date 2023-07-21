using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIpractice
{
    public class Book
    {
        public Book()
        {
            Author = new Author();
        }
        [Key]public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [ForeignKey("Author")]
        public int? AuthorId { get; set; }
         
        public Author Author { get; set; }


       }
    }


