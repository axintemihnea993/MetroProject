using MetroProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MetroProject.Application.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Amount { get; set; }

        // Optionally include related DTOs for navigation properties
        public CustomerDTO Customer { get; set; }
        public ICollection<Articles> Articles { get; set; }
        public ICollection<PaymentDTO> Payments { get; set; }
    }
}