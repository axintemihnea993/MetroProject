using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroProject.Application.DTOs
{
    public class CustomerDTO
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }
        
        public string Phone { get; set; }

        [DataType(DataType.Date, ErrorMessage = "CreatedOn must be a valid date.")]
        public DateTime CreatedOn { get; set; }
        
        [DataType(DataType.Date, ErrorMessage = "UpdatedAt must be a valid date.")]
        public DateTime UpdatedAt { get; set; }
        
        [DataType(DataType.Date, ErrorMessage = "LastPurchaseDate must be a valid date.")]
        public DateTime? LastPurchaseDate { get; set; }
        
        public string Address { get; set; }
        
        public string City { get; set; }
    }
}
