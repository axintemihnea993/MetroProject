using MetroProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MetroProject.Application.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }

        [DataType(DataType.Date, ErrorMessage = "ProcessedTime must be a valid date.")]
        public DateTime CreationDate { get; set; }

        // Optionally include related DTOs for navigation properties
        public CustomerDTO Customer { get; set; }
        public ICollection<ArticleDTO> Articles { get; set; }
        public ICollection<PaymentDTO> Payments { get; set; }
    }
}