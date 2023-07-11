using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIpractice
{
    public class Book
    {
        [Key]public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey("Author")]
        public int? AuthorId { get; set; }
         
        public Author Author { get; set; }


       }
    }


