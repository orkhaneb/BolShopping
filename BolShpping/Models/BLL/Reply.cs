using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolShpping.Models.BLL
{
    public class Reply
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Message { get; set; }

        //foreign key connection
        public int CommentId { get; set; }
        
        public virtual Comment Comment { get; set; }
    }
}
