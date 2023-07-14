using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Responses
{
    public class GetBookResponse
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public GetAuthor Author { get; set; }
    }

    public class GetAuthor
    {
        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
    }
}
