using MetroProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroProject.Application.DTOs
{
    public class ArticleDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public string Description { get; set; }
        public int Quantity { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "ExpiryDate must be a valid date.")]
        public DateTime? ExpiryDate { get; set; }


        [DataType(DataType.Date, ErrorMessage = "UpdatedAt must be a valid date.")]
        public DateTime UpdatedAt { get; set; }

        [DataType(DataType.Date, ErrorMessage = "CreatedAt must be a valid date.")]
        public DateTime CreatedAt { get; set; }
    }
}
