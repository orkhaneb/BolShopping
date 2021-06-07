using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolShpping.Models.BLL
{
    public class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Message { get; set; }

        public DateTime DateTime { get; set; }

        public AppUser AppUser { get; set; }

        public string AppUserId { get; set; }

        //primary key connection
        public virtual ICollection<Reply> Replies { get; set; }
    }
}
