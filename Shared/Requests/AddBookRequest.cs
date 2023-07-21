using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Requests
{
    public class AddBookRequest
    {
        [MinLength(5)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int? AuthorId { get; set; }


    }
}
