using System;
using System.ComponentModel.DataAnnotations;

namespace MetroProject.Application.DTOs
{
    public class PaymentDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [DataType(DataType.Date, ErrorMessage = "PaymentDate must be a valid date.")]
        public DateTime PaymentDate { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        [Required]
        public string TransactionId { get; set; }

        [DataType(DataType.Date, ErrorMessage = "ProcessedTime must be a valid date.")]
        public DateTime ProcessedTime { get; set; }
        public bool ProcessedStatus { get; set; }

        [DataType(DataType.Date, ErrorMessage = "CreatedOn must be a valid date.")]
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.Date, ErrorMessage = "UpdatedAt must be a valid date.")]
        public DateTime UpdatedAt { get; set; }
    }
}