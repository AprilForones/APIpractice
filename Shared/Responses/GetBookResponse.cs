using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Shared.Responses
{
    public class GetBookResponse
    {
        public GetBookResponse()
        {
            Author = new GetAuthor();
        }
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } =string.Empty;

        public GetAuthor Author { get; set; }
    }

    public class GetAuthor
    {
       
        public int ID { get; set; }
        public string FName { get; set; } = string.Empty;
        public string LName { get; set; } = string.Empty;
    }
}
