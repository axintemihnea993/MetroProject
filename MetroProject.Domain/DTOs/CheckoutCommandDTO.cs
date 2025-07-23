using MetroProject.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroProject.Domain.DTOs
{
    public class CheckoutCommandDTO
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int CustomerId { get; set; }
        public ArticleQuantiy[] ArticlesQuantity { get; set; }
        public ICollection<int> PaymentIds { get; set; }
    }
}
