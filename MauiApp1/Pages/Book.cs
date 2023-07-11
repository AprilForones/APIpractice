using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MauiApp1.Pages
{
    public class Book
    {
         public int ID { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int? AuthorId { get; set; }

       
    }
}