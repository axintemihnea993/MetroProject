using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroProject.Domain.DTOs
{
    public class ArticleQuantiy
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Article ID must be greater than 0.")]
        public int ArticleId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }
    }
}
