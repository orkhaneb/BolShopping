using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BolShpping.Models.VM
{
    public class ViewModelComments
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Text { get; set; }

        public string Website { get; set; }
        public DateTime DateTime { get; set; }

    }
}
