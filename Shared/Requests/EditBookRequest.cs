using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Requests
{
   public class EditBookRequest:AddBookRequest
    {
        public int Id { get; set; } 
    }
}
