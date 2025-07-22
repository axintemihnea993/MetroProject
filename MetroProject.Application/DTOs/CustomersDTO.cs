using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroProject.Application.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? LastPurchaseDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        // Optionally include related transactions as DTOs
        public ICollection<TransactionDTO> Transactions { get; set; }
    }
}
