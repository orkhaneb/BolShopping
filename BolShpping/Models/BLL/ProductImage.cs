using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BolShpping.Models.BLL
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageCode { get; set; }

        //foreign key connection
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }
    }
}
