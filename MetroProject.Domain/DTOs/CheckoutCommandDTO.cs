using MetroProject.Application.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroProject.Domain.DTOs
{
    public class CheckoutCommandDTO
    {
        [Required]
        public int Id { get; set; }

        [DataType(DataType.Date, ErrorMessage = "ExpiryDate must be a valid date.")]
        public DateTime Timestamp { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public ArticleQuantiy[] ArticlesQuantity { get; set; }

        [Required]
        public ICollection<int> PaymentIds { get; set; }
    }
}
